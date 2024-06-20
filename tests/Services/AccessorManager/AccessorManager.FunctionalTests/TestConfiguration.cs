using System;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace AccessorManager.FunctionalTests
{
    static class TestConfiguration
    {
        private static IConfigurationRoot _root;

        public static IConfigurationRoot Get => _root ??= new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", true)
            .AddEnvironmentVariables()
            .AddUserSecrets("E37ABF00-CB39-4DAC-A2B3-A6C89DCD4C6D")
            .Build();
    }
}