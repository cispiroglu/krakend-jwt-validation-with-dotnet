using Jwt.Classes;
using Jwt.Interfaces;

namespace Auth;

public static class ConfigurationHelper
{
    private static IConfiguration  _configuration;
    private static IJwtConfigurationParams _jwtConfigurationParams;
    private static JwtIssuerOptions _jwtIssuerOptions; 
    
    public static IConfiguration Configuration
    {
        get
        {
            if (_configuration != null) 
                return _configuration;
            
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.development.json", optional: true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();

            return _configuration;
        }
    }
    
    public static IJwtConfigurationParams JwtConfigurationParams
    {
        get
        {
            if (_jwtConfigurationParams != null) 
                return _jwtConfigurationParams;
            
            _jwtConfigurationParams = Configuration.GetSection(nameof(JwtConfigurationParams)).Get<JwtConfigurationParams>();

            return _jwtConfigurationParams;
        }
    }
    
    public static JwtIssuerOptions JwtIssuerOptions
    {
        get
        {
            if (_jwtIssuerOptions != null) 
                return _jwtIssuerOptions;
            
            _jwtIssuerOptions = Configuration.GetSection(nameof(JwtIssuerOptions)).Get<JwtIssuerOptions>();

            return _jwtIssuerOptions;
        }
    }
}