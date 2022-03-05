using JwtAuthenticationServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthenticationServices.Example.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    public ValuesController(IJwtServices jwtServices) =>
        _jwtServices = jwtServices;

    private readonly IJwtServices _jwtServices;

    [HttpGet, Route("authenticated"), Authorize()]
    public IActionResult Authorized() =>
        NoContent();

    [HttpGet, Route("without-authentication")]
    public IActionResult WithoutAuthentication() =>
        NoContent();

    [HttpPost, Route("authenticate")]
    public IActionResult WithoutAuthentication([FromBody] string email) =>
        Ok(_jwtServices.Generate(email));
}