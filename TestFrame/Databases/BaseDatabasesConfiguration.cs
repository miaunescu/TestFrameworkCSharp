using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
