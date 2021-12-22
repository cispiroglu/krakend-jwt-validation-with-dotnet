using Newtonsoft.Json;

namespace Jwt.Models;

public sealed class AccessToken
{
    public AccessToken(string token, int expiresIn)
    {
        Token = token;
        ExpiresIn = expiresIn;
    }
        
    [JsonProperty("token")]
    public string Token { get; }
        
    [JsonProperty("expires_in")]
    public int ExpiresIn { get; }
}