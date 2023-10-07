namespace ManagementApiTests
{
    public abstract class TestBase
    {
        protected IFixture _fixture;

        protected TestBase()
        {
            _fixture = new Fixture();
        }
    }
}