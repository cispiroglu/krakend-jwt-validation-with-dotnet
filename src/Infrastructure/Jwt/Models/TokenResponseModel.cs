using Newtonsoft.Json;

namespace Jwt.Models;

public class TokenResponseModel
{
    [JsonProperty("access_token")]
    public AccessToken AccessToken { get; set; }

    [JsonProperty("refresh_token")]
    public string RefreshToken { get; set; }
}