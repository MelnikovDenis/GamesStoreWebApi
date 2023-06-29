﻿using GamesStoreWebApi.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using Microsoft.AspNetCore.Identity;
using GamesStoreWebApi.Models.Persistence.Abstractions;
using GamesStoreWebApi.Models.ViewModels.FromView;
using GamesStoreWebApi.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace GamesStoreWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : Controller
{
    private IConfiguration _configuration;
    private UserManager<ApplicationUser> _userManager { get; set; }
    public AuthenticationController(IConfiguration configuration, UserManager<ApplicationUser> userManager)
    {
        _configuration = configuration;
        _userManager = userManager;
    }
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] CreateUserViewModel createUser) 
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
        var result = await _userManager.CreateAsync(user);
        if (result.Succeeded)
            return Ok(createUser);
        else
            throw new AlreadyExistsException("User with this email or password already exists.");
    }
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginViewModel userViewModel)
    {
        ApplicationUser? user = await _userManager.FindByEmailAsync(userViewModel.Email); 
        if (user is null || !VerifyPasswordHash(userViewModel.Password, user.PasswordHash!))
            throw new InvalidLoginDataException();
        return Ok(CreateToken(user));
    }
    private string CreatePasswordHash(string password) =>
        BCrypt.Net.BCrypt.EnhancedHashPassword(password + _configuration.GetSection("LocalHashParameter"), HashType.SHA512);
    private bool VerifyPasswordHash(string password, string passwordHash) => 
        BCrypt.Net.BCrypt.EnhancedVerify(password + _configuration.GetSection("LocalHashParameter"), passwordHash, HashType.SHA512);
    private string CreateToken(ApplicationUser user)
    {
        var claims = new List<Claim> { new Claim(ClaimTypes.Email, user.Email!) };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("JwtSettings:TokenKey").Value!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            issuer: _configuration.GetSection("JwtSettings:Issuer").Value,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromHours(6d)),
            signingCredentials: creds
        );
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }
}