using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Jwt.Interfaces;

public interface IJwtTokenHandlerService
{
    Task<string> WriteTokenAsync(JwtSecurityToken jwt);
    Task<ClaimsPrincipal> GetClaimsPrincipalAsync(string token, TokenValidationParameters tokenValidationParameters);
    Task<bool> ValidateTokenAsync(string token, TokenValidationParameters tokenValidationParameters);
}