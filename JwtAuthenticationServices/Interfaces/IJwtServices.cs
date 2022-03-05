namespace JwtAuthenticationServices.Interfaces;

public interface IJwtServices
{
    string Generate(string email);
}
