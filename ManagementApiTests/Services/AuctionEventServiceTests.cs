using ManagementApi.Responses;
using ManagementApi.Services;
using ManagementApi.Services.Interfaces;
namespace ManagementApiTests.Services
{
    using Moq;

    public class AuctionEventServiceTests : TestBase
    {
        private readonly Mock<ISqlConnectionWrapper> _sqlConnectionWrapperMock;
        private readonly IAuctionEventService _auctionEventService;

        public AuctionEventServiceTests()
        {
            _sqlConnectionWrapperMock = new Mock<ISqlConnectionWrapper>();
            _auctionEventService = new AuctionEventService(_sqlConnectionWrapperMock.Object);
        }

        [Theory]
        [InlineData(1234)]
        public async Task GetAuctionEventDetails_Should_Return_Null_When_Unable_To_Connect_To_Database(int saleId)
        {
            // Act
            var result = await _auctionEventService.GetAuctionEventDetails(saleId);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData(1234)]
        public async Task GetAuctionEventDetails_Should_Return_AuctionEventDetails_When_SaleId_Is_ValidAsync(int saleId)
        {
            // Arrange
            var expectedAuctionEventDetails = _fixture.Create<AuctionEventsResponse?>();
            _sqlConnectionWrapperMock
                .Setup(c => c.QuerySingleOrDefaultAsync<AuctionEventsResponse?>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(expectedAuctionEventDetails);

            // Act
            var actualAuctionEventDetails = await _auctionEventService.GetAuctionEventDetails(saleId);

            // Assert
            Assert.NotNull(actualAuctionEventDetails);
            Assert.Equal(expectedAuctionEventDetails, actualAuctionEventDetails);
        }

        [Fact]
        public async Task GetAuctionEventDetails_Should_Return_AuctionEventDetails_When_SaleId_Is_Valid()
        {
            // Arrange
            var expectedAuctionEventDetails = _fixture.Create<AuctionEventsResponse?>();
            _sqlConnectionWrapperMock
                .Setup(c => c.QuerySingleOrDefaultAsync<AuctionEventsResponse?>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(expectedAuctionEventDetails);

            // Act
            var actualAuctionEventDetails = await _auctionEventService.GetAuctionEventDetails(123);

            // Assert
            Assert.NotNull(actualAuctionEventDetails);
            Assert.Equal(expectedAuctionEventDetails, actualAuctionEventDetails);
        }

        [Fact]
        public async Task GetAuctionEventDetails_Should_Return_Null_When_SaleId_Is_Invalid()
        {
            // Arrange
            _sqlConnectionWrapperMock
                .Setup(c => c.QuerySingleOrDefaultAsync<AuctionEventsResponse?>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync((AuctionEventsResponse?)null);

            // Act
            var actualAuctionEventDetails = await _auctionEventService.GetAuctionEventDetails(123);

            // Assert
            Assert.Null(actualAuctionEventDetails);
        }

        [Fact]
        public async Task GetAuctionEventDetails_Should_Log_Exception_When_Exception_Occurs()
        {
            // Arrange
            AuctionEventsResponse? actualAuctionEventDetails;
            var expectedSubString = "Exception caught!";
            _sqlConnectionWrapperMock
                .Setup(c => c.QuerySingleOrDefaultAsync<AuctionEventsResponse?>(It.IsAny<string>(), It.IsAny<object>()))
                .ThrowsAsync(new Exception());

            var consoleOutput = new TestConsoleOutput();
            using (consoleOutput)
            {
                // Act
                actualAuctionEventDetails = await _auctionEventService.GetAuctionEventDetails(1234);
            }
            var actualString = consoleOutput.GetOutput();

            // Assert
            Assert.Null(actualAuctionEventDetails);
            Assert.Contains(expectedSubString, actualString);
        }
    }
}