namespace JwtAuthenticationServices.Interfaces;

/// <summary>
/// provides util methods for jwts
/// </summary>
public interface IJwtServices
{
    /// <summary>
    /// generate a jwt
    /// </summary>
    /// <param name="email">used to make jwt unique</param>
    /// <returns></returns>
    string Generate(string email);
}