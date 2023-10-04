namespace ManagementApiTests
{
    public abstract class TestBase
    {
        protected Fixture _fixture;

        protected TestBase()
        {
            _fixture = new Fixture();
        }
    }
}