using JwtAuthenticationServices.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAuthenticationServices.Implementations;

internal class JwtServices : IJwtServices
{
    public JwtServices(IConfiguration configuration) =>
      _configuration = configuration;

    private readonly IConfiguration _configuration;

    public string Generate(string email)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtAuthenticationSettings:IssuerSigningKey"]));

        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = BuildTokenDescriptor(email, _configuration["JwtAuthenticationSettings:ValidIssuer"], _configuration["JwtAuthenticationSettings:ValidAudience"], signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }

    private static IList<Claim> BuildClaims(string email) =>
        new List<Claim>() { new Claim(ClaimTypes.Name, email) };

    private static JwtSecurityToken BuildTokenDescriptor(string email, string validIssuer, string validAudience, SigningCredentials signingCredentials) =>
        new(validIssuer, validAudience, BuildClaims(email), expires: DateTime.Now.AddDays(1), signingCredentials: signingCredentials);
}