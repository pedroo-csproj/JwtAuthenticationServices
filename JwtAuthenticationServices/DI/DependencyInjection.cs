using JwtAuthenticationServices.Implementations;
using JwtAuthenticationServices.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace JwtAuthenticationServices.DI;

public static class DependencyInjection
{
    /// <summary>
    /// make the necessary injections
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddJwtAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IJwtServices, JwtServices>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
            options.TokenValidationParameters = BuildTokenValidationParameters(true, true, true, true,
            configuration["JwtAuthenticationSettings:ValidIssuer"], configuration["JwtAuthenticationSettings:ValidAudience"],
            configuration["JwtAuthenticationSettings:IssuerSigningKey"]));
    }

    private static TokenValidationParameters BuildTokenValidationParameters(bool validateIssuer, bool validateAudience,
        bool validateLifetime, bool validateIssuerSigningKey, string validIssuer, string validAudience, string issuerSigningKey) =>
        new()
        {
            ValidateIssuer = validateIssuer,
            ValidateAudience = validateAudience,
            ValidateLifetime = validateLifetime,
            ValidateIssuerSigningKey = validateIssuerSigningKey,
            ValidIssuer = validIssuer,
            ValidAudience = validAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(issuerSigningKey))
        };
}
