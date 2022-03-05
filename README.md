# JwtAuthenticationServices

[![MIT License](https://img.shields.io/github/license/dotnet/aspnetcore?color=%230b0&style=flat-square)](https://github.com/pedro-octavio/JwtAuthenticationServices/blob/main/LICENSE)

**JwtAuthenticationServices** is a library to facility the implementation of jwt authentication in **.Net** projects.

## Tech

**JwtAuthenticationServices** uses a number of open source projects to work properly:

- [C#](https://docs.microsoft.com/en-us/dotnet/csharp/)
- [.Net](https://docs.microsoft.com/en-us/dotnet/)
- [Jwt](https://jwt.io/)
- [Git](https://git-scm.com/)

## Installation

**JwtAuthenticationServices** requires [.Net Framework](https://docs.microsoft.com/en-us/dotnet/framework/install/guide-for-developers#:~:text=1%20Open%20the%20download%20page%20for%20the%20.NET,architecture%2C%20and%20then%20choose%20Next.%20More%20items...%20) 6+ to run.

### Nuget Package manager
```sh
Install-Package Octasharp.JwtAuthenticationServices
```

### .Net CLI
```sh
dotnet add package Octasharp.JwtAuthenticationServices
```

You can see more ways to install **JwtAuthenticationServices** in your project [here](https://www.nuget.org/packages/Octasharp.JwtAuthenticationServices/).

## Implementing

### add JwtAuthenticationSettings in appsettings

```sh
"JwtAuthenticationSettings": {
    "ValidIssuer": "",
    "ValidAudience": "",
    "IssuerSigningKey": ""
}
```

### inject necessary services

```sh
builder.Services.AddJwtAuthenticationServices(builder.Configuration);
```

### add .net authentication middleware

```sh
app.UseAuthentication();
```

### It's all done! Just inject IJwtServices and call ".Generate"

```sh
public ValuesController(IJwtServices jwtServices) =>
    _jwtServices = jwtServices;

private readonly IJwtServices _jwtServices;

[HttpPost, Route("authenticate")]
public IActionResult WithoutAuthentication([FromBody] string email) =>
    Ok(_jwtServices.Generate(email));
```