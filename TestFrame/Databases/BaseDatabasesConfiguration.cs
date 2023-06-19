using Microsoft.Extensions.Configuration;
using TestFrame.Builder;

namespace TestFrame.Databases
{
    public abstract class BaseDatabasesConfiguration
    {
        protected readonly IConfiguration config;

        public BaseDatabasesConfiguration()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string pathToConfig = Path.Combine(currentDirectory, "config");
            config = TestConfigurationBuilder.Build(pathToConfig);
        }
    }
}
