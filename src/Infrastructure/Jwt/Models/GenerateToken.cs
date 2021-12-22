namespace Jwt.Models;

public class GenerateToken
{
    public GenerateToken(Guid sessionId)
    {
        SessionId = sessionId;
    }
    
    public Guid SessionId { get; }
}