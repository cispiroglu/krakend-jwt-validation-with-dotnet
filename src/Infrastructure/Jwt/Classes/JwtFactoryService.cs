using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Jwt.Interfaces;
using Jwt.Models;
using Microsoft.IdentityModel.Tokens;

namespace Jwt.Classes;

public class JwtFactoryService : IJwtFactoryService
    {
        private readonly IJwtTokenHandlerService _jwtTokenHandlerService;
        private readonly IJwtIssuerOptions _jwtIssuerOptions;
        private readonly IJwtConfigurationParams _jwtConfigurationParams;

        public JwtFactoryService(IJwtTokenHandlerService jwtTokenHandlerService,
            IJwtIssuerOptions jwtIssuerOptions,
            IJwtConfigurationParams jwtConfigurationParams)
        {
            _jwtTokenHandlerService = jwtTokenHandlerService;
            _jwtIssuerOptions = jwtIssuerOptions;
            _jwtConfigurationParams = jwtConfigurationParams;
        }

        public async Task<AccessToken> GenerateEncodedTokenAsync(GenerateToken generateToken)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, generateToken.SessionId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, await _jwtIssuerOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, toUnixEpochDate(_jwtIssuerOptions.IssuedAt).ToString(),
                    ClaimValueTypes.Integer64)
            };

            var rsa = RSA.Create();
            rsa.ImportRSAPrivateKey(Convert.FromBase64String(_jwtConfigurationParams.RSAPrivateKey), out _);
            var rsaParameters = rsa.ExportParameters(true);
            var signingCredentials = new SigningCredentials(new RsaSecurityKey(rsaParameters), SecurityAlgorithms.RsaSha256);

            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(_jwtIssuerOptions.Issuer, _jwtIssuerOptions.Audience, claims,
                _jwtIssuerOptions.NotBefore, _jwtIssuerOptions.Expiration, signingCredentials);

            jwt.Header.Add("kid", _jwtConfigurationParams.SecretKey);
            return new AccessToken(_jwtTokenHandlerService.WriteTokenAsync(jwt).Result,
                (int)_jwtIssuerOptions.ValidFor.TotalSeconds);
        }

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long toUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                .TotalSeconds);
    }