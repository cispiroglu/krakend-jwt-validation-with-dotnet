namespace Jwt.Interfaces;

public interface IJwtConfigurationParams
{
    public string RSAPublicKey { get; set; }
    public string RSAPrivateKey { get; set; }
    public string SecretKey { get; set; }
}