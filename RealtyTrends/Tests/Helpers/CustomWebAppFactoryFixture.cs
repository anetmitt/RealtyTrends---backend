using System;
using Xunit;

namespace Tests.Helpers
{
    public class CustomWebAppFactoryFixture : IDisposable
    {
        public CustomWebAppFactory<Program> Factory { get; }

        public CustomWebAppFactoryFixture()
        {
            Factory = new CustomWebAppFactory<Program>();
            // Perform additional setup here as needed
        }

        public void Dispose()
        {
            // Clean up the factory instance here as needed
            Factory.Dispose();
        }
    }
}