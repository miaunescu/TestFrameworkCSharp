using Microsoft.Extensions.Configuration;
using TestFrame.Builder;
using Xunit;
using Xunit.Abstractions;

namespace TestFrame.Base
{
    public class AcceptanceTestsBase<TFixture> : IClassFixture<TFixture> where TFixture : class
    {
        protected readonly ITestOutputHelper output;
        protected readonly IConfiguration config;
        protected TFixture TestFixture { get; set; }

        public AcceptanceTestsBase(string pathToConfigDirectory, ITestOutputHelper outputHelper)
        {
            output = outputHelper;
            config = TestConfigurationBuilder.Build(pathToConfigDirectory);
        }

        public AcceptanceTestsBase(TFixture testFixture, ITestOutputHelper outputHelper)
        {
            output = outputHelper;
            TestFixture = testFixture;
            string currentDirectory = Directory.GetCurrentDirectory();
            string pathToConfig = Path.Combine(currentDirectory, "config");
            config = TestConfigurationBuilder.Build(pathToConfig);
        }
    }
}
