using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SCG.AuditoriumBooking.api.Application.Abstractions.Persistence;
using SCG.AuditoriumBooking.api.Application.Abstractions.Security;
using SCG.AuditoriumBooking.api.Application.Features.Authentication;
using SCG.AuditoriumBooking.api.Domain.Entities;
using SCG.AuditoriumBooking.api.Infrastructure.Persistence;
using SCG.AuditoriumBooking.api.Infrastructure.Persistence.Repositories;
using SCG.AuditoriumBooking.api.Infrastructure.Security;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DbConnection")));

// Application and Infrastructure services
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordVerifier, IdentityPasswordVerifier>();
builder.Services.AddScoped<ITokenService, JwtTokenService>();

// Used by IdentityPasswordVerifier
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

// JWT authentication
var jwt = builder.Configuration.GetSection("Jwt");
var jwtKey = jwt["Key"]
    ?? throw new InvalidOperationException("JWT key is missing.");

if (Encoding.UTF8.GetByteCount(jwtKey) < 32)
{
    throw new InvalidOperationException(
        "JWT key must contain at least 32 bytes.");
}

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwt["Issuer"],

            ValidateAudience = true,
            ValidAudience = jwt["Audience"],

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey)),

            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromSeconds(30)
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();