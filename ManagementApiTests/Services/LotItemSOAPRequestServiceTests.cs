namespace ManagementApiTests.Services
{
    using ManagementApi.Helpers;
    using ManagementApi.Models;
    using ManagementApi.Services;
    using ManagementApi.Services.Interfaces;
    using Moq;

    public class LotItemSOAPRequestServiceTests : TestBase
    {
        private readonly Mock<IAuctionEventItemService> _auctionEventItemServiceMock;
        private readonly ISOAPRequestService _soapRequestService;

        public LotItemSOAPRequestServiceTests()
        {
            _auctionEventItemServiceMock = new Mock<IAuctionEventItemService>(); ;
            _soapRequestService = new LotItemSOAPRequestService(_auctionEventItemServiceMock.Object);
        }

        [Theory]
        [InlineData("1234")]
        [InlineData("2345")]
        private async void Should_Return_Message_When_SaleId_Is_Valid(string saleId)
        {
            // Arrange
            var expectedAuctionEventDetails = _fixture.Create<AuctionEventItemResponse>();
            _auctionEventItemServiceMock
                .Setup(s => s.GetAuctionEventItemDetails(It.IsAny<int?>()))
                .ReturnsAsync(expectedAuctionEventDetails);

            // Act
            var actualSOAPRequest = await _soapRequestService.CreateSOAPRequest(saleId);

            // Assert
            Assert.False(string.IsNullOrEmpty(actualSOAPRequest));
            Assert.Contains(SOAPRequestHelper.GenerateLotItemSOAPRequest(expectedAuctionEventDetails), actualSOAPRequest);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("-45")]
        [InlineData("-123")]
        [InlineData("123A")]
        [InlineData("sdandapd")]
        [InlineData("dmpqiej32n38hf9c4n9fbc")]
        [InlineData("44444444444444444444444444444444444444444444")]
        private async void Should_Return_Error_Message_When_ItemId_Is_Not_Valid(string? itemId)
        {
            // Arrange
            var expectedErrorMessage = $"ItemId {itemId} is not valid. Please enter a valid ItemId.";

            // Act
            var actualSOAPRequest = await _soapRequestService.CreateSOAPRequest(itemId);

            // Assert
            Assert.Equal(expectedErrorMessage, actualSOAPRequest);
        }

        [Fact]
        public async Task Create_SOAP_Request_For_Lot_Should_Return_ErrorMessage_When_SaleId_Is_Not_Found()
        {
            // Arrange
            _auctionEventItemServiceMock
                .Setup(s => s.GetAuctionEventItemDetails(It.IsAny<int?>()))
                .ReturnsAsync((AuctionEventItemResponse?)null);

            var expectedErrorMessage = "ItemId -123 is not valid. Please enter a valid ItemId.";

            // Act
            var actualSOAPRequest = await _soapRequestService.CreateSOAPRequest("-123");

            // Assert
            Assert.Equal(expectedErrorMessage, actualSOAPRequest);
        }

        [Theory]
        [InlineData("1234")]
        public async Task Create_SOAP_Request_For_Lot_Should_Return_ErrorMessage_When_SaleId_Is_Valid_But_Response_Is_Null(string itemId)
        {
            // Arrange
            _auctionEventItemServiceMock
                .Setup(s => s.GetAuctionEventItemDetails(It.IsAny<int?>()))
                .ReturnsAsync((AuctionEventItemResponse?)null);

            var expectedErrorMessage = $"No Response for Request with ItemId {itemId}.\r\nPlease try again with a valid ItemId.";

            // Act
            var actualSOAPRequest = await _soapRequestService.CreateSOAPRequest(itemId);

            // Assert
            Assert.Equal(expectedErrorMessage, actualSOAPRequest);
        }

        [Fact]
        public async Task Create_SOAP_Request_For_Lot_Should_Throw_Exception_When_Exception_Occurs()
        {
            // Arrange
            _auctionEventItemServiceMock
                .Setup(s => s.GetAuctionEventItemDetails(It.IsAny<int?>()))
                .ThrowsAsync(new Exception());

            // Act
            var action = async () => await _soapRequestService.CreateSOAPRequest("1234");

            // Assert
            await Assert.ThrowsAsync<Exception>(action);
        }
    }
}