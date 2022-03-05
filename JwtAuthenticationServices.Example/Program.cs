using JwtAuthenticationServices.DI;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddJwtAuthenticationServices(builder.Configuration);

builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo()
        {
            Title = "Jwt Authentication Services Example",
            Version = "1.0.0",
            Description = "Example of Jwt Authentication Services implementation.",
            Contact = new OpenApiContact
            {
                Name = "Pedro Octávio",
                Email = "pedrooctavio.dev@outlook.com",
                Url = new Uri("https://github.com/pedro-octavio")
            },
            License = new OpenApiLicense
            {
                Name = "MIT",
                Url = new Uri("https://github.com/pedro-octavio/JwtAuthenticationServices/blob/main/LICENSE")
            }
        });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Authorization header using the Bearer scheme."
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
    });

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();