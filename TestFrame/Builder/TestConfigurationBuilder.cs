using Microsoft.Extensions.Configuration;

namespace TestFrame.Builder
{
    public static class TestConfigurationBuilder
    {

        //
        // Summary:
        //     Initializes an instance of Microsoft.Extensions.Configuration.IConfigurationBuilder
        //     with some conventions conventions. Namely * Adds the appsettings.json file - REQUIRED
        //     from the pathToConfig * Adds the appsettings.{environment}.json files - OPTIONAL
        //     from the pathToConfig. Environment is the value of the "DOTNET_ENVIRONMENT" environment
        //     variable * Adds the Environment Variable configuration provider, so that individual
        //     configuration values can be overridden with environment variables.
        //
        // Parameters:
        //   pathToConfig:
        //     The path to the directory containing the appsettings.json configuration files.
        //
        // Returns:
        //     an instance of Microsoft.Extensions.Configuration.IConfigurationBuilder, for
        //     additional configuration before finalizing into an Microsoft.Extensions.Configuration.IConfiguration
        //     object instance.
        public static IConfigurationBuilder InitBuilder(string pathToConfig)
        {
            string environmentVariable = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
            return new ConfigurationBuilder().SetBasePath(pathToConfig).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).AddJsonFile("appsettings." + environmentVariable + ".json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
        }

        //
        // Summary:
        //     Buidls an an instance of Microsoft.Extensions.Configuration.IConfiguration with
        //     some conventions. Namely * Adds the appsettings.json file - REQUIRED from
        //     the pathToConfig * Adds the appsettings.{environment}.json files - OPTIONAL from
        //     the pathToConfig. Environment is the value of the "DOTNET_ENVIRONMENT" environment
        //     variable * Adds the Environment Variable configuration provider, so that individual
        //     configuration values can be overridden with environment variables. * Adds the
        //     Command Line Argument configuration provider, if any args are provided, so that
        //     configuration values can be overridden with command line arguments.
        //
        // Parameters:
        //   pathToConfig:
        //     The path to the directory containing the appsettings.json configuration files.
        //
        //   args:
        //     (Optional) set of command line arguments to apply to the Microsoft.Extensions.Configuration.IConfiguration.
        //
        // Returns:
        //     an instance of Microsoft.Extensions.Configuration.IConfiguration
        public static IConfiguration Build(string pathToConfig, string[] args = null)
        {
            IConfigurationBuilder configurationBuilder = InitBuilder(pathToConfig);
            if (args != null && args.Length != 0)
            {
                configurationBuilder.AddCommandLine(args);
            }

            return configurationBuilder.Build();
        }
    }
}
