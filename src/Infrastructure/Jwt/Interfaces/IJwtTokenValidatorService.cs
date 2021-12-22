using System.Security.Claims;

namespace Jwt.Interfaces;

public interface IJwtTokenValidatorService
{
    Task<ClaimsPrincipal> GetPrincipalFromTokenAsync(string token);
    Task<bool> ValidateTokenAsync(string token);
}