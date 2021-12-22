using Autofac;
using Jwt.Classes;
using Jwt.Interfaces;

namespace Jwt.Extensions.Autofac;

public class JwtExtensionModule : Module
{
    private readonly IJwtConfigurationParams _jwtConfigurationParams;
    private readonly JwtIssuerOptions _jwtIssuerOptions;

    public JwtExtensionModule(IJwtConfigurationParams jwtConfigurationParams, JwtIssuerOptions jwtIssuerOptions)
    {
        _jwtConfigurationParams = jwtConfigurationParams;
        _jwtIssuerOptions = jwtIssuerOptions;
    }

    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<JwtFactoryService>().As<IJwtFactoryService>().InstancePerLifetimeScope();
        builder.RegisterType<JwtTokenFactoryService>().As<IJwtTokenFactoryService>().InstancePerLifetimeScope();
        builder.RegisterType<JwtTokenHandlerService>().As<IJwtTokenHandlerService>().InstancePerLifetimeScope();
        builder.RegisterType<JwtTokenValidatorService>().As<IJwtTokenValidatorService>().InstancePerLifetimeScope();
        builder.RegisterType<JwkService>().As<IJwkService>().InstancePerLifetimeScope();

        var jwtIssuerOptions = new JwtIssuerOptions(_jwtIssuerOptions.Issuer, _jwtIssuerOptions.Audience, _jwtIssuerOptions.ValidFor);

        builder.RegisterInstance(jwtIssuerOptions).As<IJwtIssuerOptions>().SingleInstance();
        builder.RegisterInstance(_jwtConfigurationParams).As<IJwtConfigurationParams>().SingleInstance();
    }
}