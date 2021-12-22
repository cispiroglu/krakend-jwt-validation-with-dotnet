using Autofac;
using Jwt.Classes;
using Jwt.Interfaces;

namespace Jwt.Extensions.Autofac;

public static class JwtExtension
{
    public static void AddJwt(this ContainerBuilder builder, IJwtConfigurationParams jwtConfigurationParams, JwtIssuerOptions jwtIssuerOptions)
        => builder.RegisterModule(new JwtExtensionModule(jwtConfigurationParams, jwtIssuerOptions));
}