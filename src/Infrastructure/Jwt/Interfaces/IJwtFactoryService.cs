using Jwt.Models;

namespace Jwt.Interfaces;

public interface IJwtFactoryService
{
    Task<AccessToken> GenerateEncodedTokenAsync(GenerateToken generateToken);
}