namespace ManagementApiTests.Factories
{
    using ManagementApi.Factories;
    using ManagementApi.Factories.Interfaces;
    using ManagementApi.Models;
    using ManagementApi.Services;
    using ManagementApi.Services.Interfaces;
    using Moq;
    using NSubstitute;
    using NSubstitute.ExceptionExtensions;
    using System.Data.SqlClient;

    public class SOAPRequestServiceFactoryTests : TestBase
    {
        private readonly Mock<ISqlConnectionWrapper> _wrapperMock;
        private readonly ISOAPRequestServiceFactory _soapRequestServiceFactory;
        private const string CONNECTION_STRING = "Trusted_Connection=True;";

        public SOAPRequestServiceFactoryTests()
        {
            _wrapperMock = new Mock<ISqlConnectionWrapper>();
            _soapRequestServiceFactory = new SOAPRequestServiceFactory(_wrapperMock.Object);
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
            Assert.Throws<ArgumentException>(() => action());
        }

        [Fact]
        public void Create_ReturnsAuctionEventSOAPRequestService_ForSaleExecution()
        {
            // Arrange
            var soapRequestServiceRequest = new SOAPRequestServiceRequest { ConnectionString = CONNECTION_STRING, ShouldExecuteForSale = true };

            // Act
            var soapRequestService = _soapRequestServiceFactory.Create(soapRequestServiceRequest);

            // Assert
            Assert.IsType<AuctionEventSOAPRequestService>(soapRequestService);
        }

        [Fact]
        public void Create_ReturnsLotItemSOAPRequestService_ForNonSaleExecution()
        {
            // Arrange
            var soapRequestServiceRequest = new SOAPRequestServiceRequest { ConnectionString = CONNECTION_STRING, ShouldExecuteForSale = false };

            // Act
            var soapRequestService = _soapRequestServiceFactory.Create(soapRequestServiceRequest);

            // Assert
            Assert.IsType<LotItemSOAPRequestService>(soapRequestService);
        }

        //[Fact]
        //public void Create_Should_Throw_Exception_When__SoapRequestServiceFactory_Create_Method_Is_Called()
        //{
        //    // Arrange
        //    var exception = new Exception();
        //    var soapRequestServiceRequest = new SOAPRequestServiceRequest { ConnectionString = CONNECTION_STRING, ShouldExecuteForSale = false };
        //    _soapRequestServiceFactory.Create(soapRequestServiceRequest).ThrowsForAnyArgs(exception);

        //    // Act
        //    var action = () => _soapRequestServiceFactory.Create(soapRequestServiceRequest);

        //    // Assert
        //    Assert.Throws<Exception>(() => action());
        //}
    }
}