using GamesStoreWebApi.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using Microsoft.AspNetCore.Identity;
using GamesStoreWebApi.Models.ViewModels.FromView;
using GamesStoreWebApi.Exceptions;
using System.Data;
using GamesStoreWebApi.Models.Persistence.Implementations;
using GamesStoreWebApi.Models.Persistence.Abstractions;
using System.Net;

namespace GamesStoreWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : Controller
{
    private IConfiguration _configuration { get; }

    private UserManager<ApplicationUser> _userManager { get; set; }
    private IUnitOfWork UnitOfWork { get; set; }
    public AuthenticationController(IConfiguration configuration, UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
    {
        _configuration = configuration;
        _userManager = userManager;
        UnitOfWork = unitOfWork;
    }
    [HttpPost("Register")]
    public async Task<IActionResult> Register(CreateUserViewModel createUser) 
    {
        var id = Guid.NewGuid();
        var user = new ApplicationUser
        {
            Id = id,
            Email = createUser.Email,
            EmailConfirmed = false,
            UserName = createUser.UserName,
            PhoneNumber = null,
            PhoneNumberConfirmed = false,
            LockoutEnabled = false,
            AccessFailedCount = 0,
            PasswordHash = CreatePasswordHash(createUser.Password)
        };
        var createUserResult = await _userManager.CreateAsync(user);
        var giveRoleResult = await _userManager.AddToRoleAsync(user, "User");
        if (createUserResult.Succeeded && giveRoleResult.Succeeded) 
        {  
            return Ok(createUser);
        }
        else 
        {
            throw new AlreadyExistsException("User with this email or password already exists.");
        }         
    }
    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginViewModel userViewModel)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(userViewModel.Email); 
        if (user is null || !VerifyPasswordHash(userViewModel.Password, user.PasswordHash!))
            throw new InvalidLoginDataException();

        var refreshToken = GenerateRefreshToken(user);
        await SetRefreshToken(refreshToken);

        return Ok(await CreateAccessToken(user));
    }
    [HttpPost("RefreshToken")]
    public async Task<IActionResult> RefreshToken()
    {
        //удаляем старый RefreshToken, если он существует
        string? refreshTokenStr = null;
        if (Request.Cookies.TryGetValue("RefreshToken", out refreshTokenStr))
        {
            
        }
        else 
        {
            throw new WebApiException("Where is your refresh token?", HttpStatusCode.Unauthorized);
        }
        var refreshToken = await UnitOfWork.RefreshTokenRepository.GetById(new Guid(refreshTokenStr));

        if(refreshToken.Expires < DateTime.UtcNow)
            throw new WebApiException("Refresh token expired.", HttpStatusCode.Unauthorized);

        return Ok(await CreateAccessToken(refreshToken.User!));
    }
    private string CreatePasswordHash(string password) =>
        BCrypt.Net.BCrypt.EnhancedHashPassword(password + _configuration.GetSection("LocalHashParameter"), HashType.SHA512);

    private bool VerifyPasswordHash(string password, string passwordHash) => 
        BCrypt.Net.BCrypt.EnhancedVerify(password + _configuration.GetSection("LocalHashParameter"), passwordHash, HashType.SHA512);

    private async Task<string> CreateAccessToken(ApplicationUser user)
    {
        
        var claims = new List<Claim> { new Claim(ClaimTypes.Email, user.Email!) };
        claims.AddRange(from role in await _userManager.GetRolesAsync(user) select new Claim(ClaimTypes.Role, role));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("JwtSettings:TokenKey").Value!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            issuer: _configuration.GetSection("JwtSettings:Issuer").Value,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromSeconds(double.Parse(_configuration.GetSection("JwtSettings:AccessTokenExpires").Value!))),
            signingCredentials: creds
        );
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }
    private async Task SetRefreshToken(RefreshToken newRefreshToken) 
    {
        //удаляем старый RefreshToken, если он существует
        string? oldRefreshTokenStr = null; 
        if(Request.Cookies.TryGetValue("RefreshToken", out oldRefreshTokenStr)) 
        {
            var oldRefreshToken = await UnitOfWork.RefreshTokenRepository.GetById(new Guid(oldRefreshTokenStr));
            UnitOfWork.RefreshTokenRepository.Delete(oldRefreshToken);
        }
        //задаём настройки куки (максимальная безопасность, поэтому httpsOnly и Secure)
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            Expires = newRefreshToken.Expires
        };
        //добавляем новый RefreshToken в куки
        Response.Cookies.Append("RefreshToken", newRefreshToken.Token.ToString(), cookieOptions);
        //сохраняем новый RefreshToken в БД
        UnitOfWork.RefreshTokenRepository.Create(newRefreshToken);
        await UnitOfWork.Save();
    }
    private RefreshToken GenerateRefreshToken(ApplicationUser user) 
    {
        return
            new RefreshToken
            {
                Expires = DateTime.UtcNow.Add(
                    TimeSpan.FromSeconds(double.Parse(_configuration.GetSection("JwtSettings:AccessTokenExpires").Value!))
                ),
                User = user
            };
    }
}