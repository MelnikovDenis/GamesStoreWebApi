using Microsoft.AspNetCore.Identity;
using GamesStoreWebApi.Models;
using GamesStoreWebApi.Models.Entities;
using GamesStoreWebApi.Models.Persistence.Abstractions;
using GamesStoreWebApi.Models.Persistence.Implementations;
using GamesStoreWebApi.Infrastructure.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Lockout.AllowedForNewUsers = false;
}).AddEntityFrameworkStores<ApplicationContext>();
builder.Services.AddTransient<IGenericRepository<Game>, EfGameRepository>();
builder.Services.AddTransient<IGenericRepository<Company>, EfCompanyRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    options => 
    {
        options.AddSecurityDefinition("oauth2", 
            new OpenApiSecurityScheme
            {
                Description = "Standard Authorization header using the Bearer scheme.",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            }
        );
        options.OperationFilter<SecurityRequirementsOperationFilter>();
    }
);

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    options => 
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // указывает, будет ли валидироваться издатель при валидации токена
            ValidateIssuer = true,
            // строка, представляющая издателя
            ValidIssuer = builder.Configuration.GetSection("JwtSettings:Issuer").Value,
            // будет ли валидироваться потребитель токена
            ValidateAudience = false,
            // будет ли валидироваться время существования
            ValidateLifetime = true,
            // установка ключа безопасности
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("JwtSettings:TokenKey").Value!)),
            // валидация ключа безопасности
            ValidateIssuerSigningKey = true
        };
    }
);

var app = builder.Build();

app.UseExceptionHandlerMiddleware();

app.UseAuthentication();

app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.MapControllers();

await app.RunAsync();
