using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Jwt.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Jwt.Classes;

public class JwtTokenHandlerService : IJwtTokenHandlerService
{
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
    
    public JwtTokenHandlerService()
    {
        _jwtSecurityTokenHandler ??= new JwtSecurityTokenHandler();
    }
    
    public Task<string> WriteTokenAsync(JwtSecurityToken jwt)
    {
        return Task.FromResult(_jwtSecurityTokenHandler.WriteToken(jwt));
    }
    
    public Task<ClaimsPrincipal> GetClaimsPrincipalAsync(string token, TokenValidationParameters tokenValidationParameters)
    {
        try
        {
            var principal = _jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out _);
    
            return Task.FromResult(principal);
        }
        catch (Exception e)
        {
            throw new Exception($"ValidateTokenAsync Error: {e.Message}");
        }
    }
    
    public Task<bool> ValidateTokenAsync(string token, TokenValidationParameters tokenValidationParameters)
    {
        try
        {
            _jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out _);
            return Task.FromResult(true);
        }
        catch (Exception e)
        {
            Console.WriteLine($"ValidateTokenAsync Error: {e.Message}");
            return Task.FromResult(false);
        }
    }
}