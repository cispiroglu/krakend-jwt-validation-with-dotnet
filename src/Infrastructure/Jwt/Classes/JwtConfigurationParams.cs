using Jwt.Interfaces;

namespace Jwt.Classes;

public class JwtConfigurationParams : IJwtConfigurationParams
{
    public string RSAPublicKey { get; set; }
    public string RSAPrivateKey { get; set; }
    public string SecretKey { get; set; }
}