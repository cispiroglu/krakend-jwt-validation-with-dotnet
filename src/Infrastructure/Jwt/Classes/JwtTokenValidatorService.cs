using System.Security.Claims;
using System.Security.Cryptography;
using Jwt.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Jwt.Classes;

public class JwtTokenValidatorService : IJwtTokenValidatorService
{
    private readonly IJwtTokenHandlerService _jwtTokenHandlerService;
    private readonly IJwtConfigurationParams _jwtConfigurationParams;
    private readonly IJwtIssuerOptions _jwtIssuerOptions;

    public JwtTokenValidatorService(IJwtTokenHandlerService jwtTokenHandlerService,
        IJwtConfigurationParams jwtConfigurationParams,
        IJwtIssuerOptions jwtIssuerOptions)
    {
        _jwtTokenHandlerService = jwtTokenHandlerService;
        _jwtConfigurationParams = jwtConfigurationParams;
        _jwtIssuerOptions = jwtIssuerOptions;
    }

    public Task<ClaimsPrincipal> GetPrincipalFromTokenAsync(string token)
    {
        using var rsa = RSA.Create();
        rsa.ImportSubjectPublicKeyInfo(Convert.FromBase64String(_jwtConfigurationParams.RSAPublicKey), out _);
        var rsaParameters = rsa.ExportParameters(false);

        return _jwtTokenHandlerService.GetClaimsPrincipalAsync(token, new TokenValidationParameters
        {
            IssuerSigningKey = new RsaSecurityKey(rsaParameters),
            ValidateAudience = false,
            ValidateIssuer = true,
            ValidIssuer = _jwtIssuerOptions.Issuer,
            RequireSignedTokens = true,
            RequireExpirationTime = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        });
    }
    
    public Task<bool> ValidateTokenAsync(string token)
    {
        using var rsa = RSA.Create();
        rsa.ImportSubjectPublicKeyInfo(Convert.FromBase64String(_jwtConfigurationParams.RSAPublicKey), out _);
        var rsaParameters = rsa.ExportParameters(false);

        return _jwtTokenHandlerService.ValidateTokenAsync(token, new TokenValidationParameters
        {
            IssuerSigningKey = new RsaSecurityKey(rsaParameters),
            ValidateAudience = false,
            ValidateIssuer = true,
            ValidIssuer = _jwtIssuerOptions.Issuer,
            RequireSignedTokens = true,
            RequireExpirationTime = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        });
    }
}