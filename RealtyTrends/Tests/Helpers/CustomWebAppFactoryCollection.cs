using Xunit;

namespace Tests.Helpers
{
    [CollectionDefinition("CustomWebAppFactory collection")]
    public class CustomWebAppFactoryCollection : ICollectionFixture<CustomWebAppFactoryFixture>
    {
        // This class has no code. It's just here to be annotated with CollectionDefinition,
        // which will then be used to associate test classes with the CustomWebAppFactoryFixture.
    }
}