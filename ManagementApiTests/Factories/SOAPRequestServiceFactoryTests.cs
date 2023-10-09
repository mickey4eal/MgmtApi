using AutoFixture.Kernel;
using ManagementApi.Factories;
using ManagementApi.Factories.Interfaces;
using ManagementApi.Models;

namespace ManagementApiTests.Factories
{
    public class SOAPRequestServiceFactoryTests : TestBase
    {
        private readonly ISOAPRequestServiceFactory _soapRequestServiceFactory;
        private const string CONNECTION_STRING = "Trusted_Connection=True;";

        public SOAPRequestServiceFactoryTests()
        {
            _fixture.Customizations.Add(
                new TypeRelay(
                    typeof(ISOAPRequestServiceFactory),
                    typeof(SOAPRequestServiceFactory)));
            _soapRequestServiceFactory = _fixture.Create<ISOAPRequestServiceFactory>();
        }

        [Fact]
        public void Create_Should_Return_SOAPRequestService_When_ConnectionString_Is_Valid()
        {
            // Arrange
            var soapRequestServiceRequest = new SOAPRequestServiceRequest { ConnectionString = CONNECTION_STRING };

            // Act
            var soapRequestService = _soapRequestServiceFactory.Create(soapRequestServiceRequest);

            // Assert
            Assert.NotNull(soapRequestService);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Create_Should_Throw_ArgumentNullException_When_ConnectionString_Is_Null(string? connectionString)
        {
            // Arrange
            var soapRequestServiceRequest = new SOAPRequestServiceRequest { ConnectionString = connectionString };

            // Act
            var action = () => _soapRequestServiceFactory.Create(soapRequestServiceRequest);

            // Assert
            Assert.Throws<ArgumentNullException>(() => action());
        }
    }
}