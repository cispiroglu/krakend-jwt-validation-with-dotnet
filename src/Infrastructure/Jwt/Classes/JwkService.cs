using System.Security.Cryptography;
using Jwt.Interfaces;
using Jwt.Models;
using Microsoft.IdentityModel.Tokens;

namespace Jwt.Classes;

public class JwkService : IJwkService
{
    private readonly IJwtConfigurationParams _jwtConfigurationParams;
        
    public JwkService(IJwtConfigurationParams jwtConfigurationParams)
    {
        _jwtConfigurationParams = jwtConfigurationParams;
    }

    public JwkWrapper GetJwk()
    {
        using var rsa = RSA.Create();
        rsa.ImportSubjectPublicKeyInfo(Convert.FromBase64String(_jwtConfigurationParams.RSAPublicKey), out _);
        var rsaParameters = rsa.ExportParameters(false);

        var jwkWrapper = new JwkWrapper();
        jwkWrapper.keys.Add(new Jwk
        {
            use = "sig",
            kid = _jwtConfigurationParams.SecretKey,
            e = Base64UrlEncoder.Encode(rsaParameters.Exponent),
            n = Base64UrlEncoder.Encode(rsaParameters.Modulus),
            kty = "RSA",
            alg = "RSA256"
        });
            
        return jwkWrapper;
    }
}