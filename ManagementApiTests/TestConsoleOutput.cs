namespace ManagementApiTests
{
    public class TestConsoleOutput : IDisposable
    {
        private readonly StringWriter _stringWriter;
        private readonly TextWriter _originalConsoleOut;

        public TestConsoleOutput()
        {
            _stringWriter = new StringWriter();
            _originalConsoleOut = Console.Out;
            Console.SetOut(_stringWriter);
        }

        public string GetOutput()
        {
            return _stringWriter.ToString();
        }

        public void Dispose()
        {
            Console.SetOut(_originalConsoleOut);
        }
    }
}
