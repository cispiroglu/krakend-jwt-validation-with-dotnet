using System.Security.Cryptography;
using Jwt.Interfaces;

namespace Jwt.Classes;

public sealed class JwtTokenFactoryService : IJwtTokenFactoryService
{
    public Task<string> GenerateTokenAsync(int size = 32)
    {
        var randomNumber = new byte[size];
        using var rng = RandomNumberGenerator.Create();
        
        rng.GetBytes(randomNumber);
        return Task.FromResult(Convert.ToBase64String(randomNumber));
    }
}