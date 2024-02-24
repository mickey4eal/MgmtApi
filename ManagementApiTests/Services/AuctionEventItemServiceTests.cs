namespace ManagementApiTests.Services
{
    using ManagementApi.Models;
    using ManagementApi.Services;
    using ManagementApi.Services.Interfaces;
    using Moq;

    public class AuctionEventItemServiceTests : TestBase
    {
        private readonly Mock<ISqlConnectionWrapper> _sqlConnectionWrapperMock;
        private readonly IAuctionEventItemService _auctionEventItemsService;

        public AuctionEventItemServiceTests()
        {
            _sqlConnectionWrapperMock = new Mock<ISqlConnectionWrapper>(); ;
            _auctionEventItemsService = new AuctionEventItemService(_sqlConnectionWrapperMock.Object);
        }

        [Theory]
        [InlineData(1234)]
        public async Task GetAuctionEventItemDetails_Should_Return_Null_When_Unable_To_Connect_To_Database(int itemId)
        {
            // Act
            var result = await _auctionEventItemsService.GetAuctionEventItemDetails(itemId);

            // Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData(1234)]
        public async Task GetAuctionEventItemDetails_Should_Return_AuctionEventDetails_When_ItemId_Is_ValidAsync(int itemId)
        {
            // Arrange
            var expectedAuctionEventItemDetails = _fixture.Create<AuctionEventItemResponse?>();
            _sqlConnectionWrapperMock
                .Setup(c => c.QuerySingleOrDefaultAsync<AuctionEventItemResponse?>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(expectedAuctionEventItemDetails);

            // Act
            var actualAuctionEventItemDetails = await _auctionEventItemsService.GetAuctionEventItemDetails(itemId);

            // Assert
            Assert.NotNull(actualAuctionEventItemDetails);
            Assert.Equal(expectedAuctionEventItemDetails, actualAuctionEventItemDetails);
        }

        [Fact]
        public async Task GetAuctionEventItemDetails_Should_Return_AuctionEventDetails_When_ItemId_Is_Valid()
        {
            // Arrange
            var expectedAuctionEventItemDetails = _fixture.Create<AuctionEventItemResponse?>();
            _sqlConnectionWrapperMock
                .Setup(c => c.QuerySingleOrDefaultAsync<AuctionEventItemResponse?>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync(expectedAuctionEventItemDetails);

            // Act
            var actualAuctionEventItemDetails = await _auctionEventItemsService.GetAuctionEventItemDetails(123);

            // Assert
            Assert.NotNull(actualAuctionEventItemDetails);
            Assert.Equal(expectedAuctionEventItemDetails, actualAuctionEventItemDetails);
        }

        [Fact]
        public async Task GetAuctionEventItemDetails_Should_Return_Null_When_ItemId_Is_Invalid()
        {
            // Arrange
            _sqlConnectionWrapperMock
                .Setup(c => c.QuerySingleOrDefaultAsync<AuctionEventItemResponse?>(It.IsAny<string>(), It.IsAny<object>()))
                .ReturnsAsync((AuctionEventItemResponse?)null);

            // Act
            var actualAuctionEventItemDetails = await _auctionEventItemsService.GetAuctionEventItemDetails(123);

            // Assert
            Assert.Null(actualAuctionEventItemDetails);
        }

        [Fact]
        public async Task GetAuctionEventItemDetails_Should_Log_Exception_When_Exception_Occurs()
        {
            // Arrange
            AuctionEventItemResponse? actualAuctionEventItemDetails;
            var expectedSubString = "Exception caught!";
            _sqlConnectionWrapperMock
                .Setup(c => c.QuerySingleOrDefaultAsync<AuctionEventItemResponse?>(It.IsAny<string>(), It.IsAny<object>()))
                .ThrowsAsync(new Exception());

            var consoleOutput = new TestConsoleOutput();
            using (consoleOutput)
            {
                // Act
                actualAuctionEventItemDetails = await _auctionEventItemsService.GetAuctionEventItemDetails(1234);
            }
            var actualString = consoleOutput.GetOutput();

            // Assert
            Assert.Null(actualAuctionEventItemDetails);
            Assert.Contains(expectedSubString, actualString);
        }
    }
}