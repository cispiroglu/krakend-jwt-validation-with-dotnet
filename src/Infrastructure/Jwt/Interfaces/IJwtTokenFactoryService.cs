namespace Jwt.Interfaces;

public interface IJwtTokenFactoryService
{
    Task<string> GenerateTokenAsync(int size = 32);
}