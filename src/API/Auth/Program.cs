using Auth;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Jwt.Extensions.Autofac;
using Jwt.Interfaces;
using Jwt.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Call UseServiceProviderFactory on the Host sub property 
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Call ConfigureContainer on the Host sub property 
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.AddJwt(ConfigurationHelper.JwtConfigurationParams, ConfigurationHelper.JwtIssuerOptions);
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.MapPost("/Token", async (IJwtFactoryService _jwtFactoryService, IJwtTokenFactoryService _jwtTokenFactoryService) =>
{
    var generateToken = new GenerateToken(Guid.NewGuid());
    var accessToken = await _jwtFactoryService.GenerateEncodedTokenAsync(generateToken);
    var refreshToken = await _jwtTokenFactoryService.GenerateTokenAsync();
    
    var result = new TokenResponseModel
    {
        AccessToken = accessToken,
        RefreshToken = refreshToken
    };

    return result;
});
app.MapGet("/Jwk", (IJwkService jwkService) => jwkService.GetJwk());
app.MapPost("/Validate", async (IJwtTokenValidatorService jwtTokenValidatorService, [FromBody] string token) => await jwtTokenValidatorService.ValidateTokenAsync(token));

app.Run();