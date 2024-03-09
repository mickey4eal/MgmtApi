namespace ManagementApiTests.Services
{
    using ManagementApi.Helpers;
    using ManagementApi.Responses;
    using ManagementApi.Services;
    using ManagementApi.Services.Interfaces;
    using Moq;

    public class AuctionEventSOAPRequestServiceTests : TestBase
    {
        private readonly Mock<IAuctionEventService> _auctionEventServiceMock;
        private readonly ISOAPRequestService _soapRequestService;

        public AuctionEventSOAPRequestServiceTests()
        {
            _auctionEventServiceMock = new Mock<IAuctionEventService>();
            _soapRequestService = new AuctionEventSOAPRequestService(_auctionEventServiceMock.Object);
        }

        [Theory]
        [InlineData("1234")]
        [InlineData("2345")]
        private async void Should_Return_Message_When_SaleId_Is_Valid(string saleId)
        {
            // Arrange
            var expectedAuctionEventDetails = _fixture.Create<AuctionEventsResponse>();
            _auctionEventServiceMock
                .Setup(s => s.GetAuctionEventDetailsRouteTwo(It.IsAny<int?>()))
                .ReturnsAsync(expectedAuctionEventDetails);

            // Act
            var actualSOAPRequest = await _soapRequestService.CreateSOAPRequest(saleId);

            // Assert
            Assert.False(string.IsNullOrEmpty(actualSOAPRequest));
            Assert.Contains(SOAPRequestHelper.GenerateAuctionEventSOAPRequest(expectedAuctionEventDetails), actualSOAPRequest);
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
        private async void Should_Return_Error_Message_When_SaleId_Is_Not_Valid(string? saleId)
        {
            // Arrange
            var expectedErrorMessage = $"SaleId {saleId} is not valid. Please enter a valid SaleId.";

            // Act
            var actualSOAPRequest = await _soapRequestService.CreateSOAPRequest(saleId);

            // Assert
            Assert.Equal(expectedErrorMessage, actualSOAPRequest);
        }

        [Fact]
        public async Task Create_SOAP_Request_For_Sale_Should_Return_ErrorMessage_When_SaleId_Is_Not_Found()
        {
            // Arrange
            _auctionEventServiceMock
                .Setup(s => s.GetAuctionEventDetails(It.IsAny<int?>()))
                .ReturnsAsync((AuctionEventsResponse?)null);

            var expectedErrorMessage = "SaleId -123 is not valid. Please enter a valid SaleId.";

            // Act
            var actualSOAPRequest = await _soapRequestService.CreateSOAPRequest("-123");

            // Assert
            Assert.Equal(expectedErrorMessage, actualSOAPRequest);
        }

        [Theory]
        [InlineData("1234")]
        public async Task Create_SOAP_Request_For_Sale_Should_Return_ErrorMessage_When_SaleId_Is_Valid_But_Response_Is_Null(string saleId)
        {
            // Arrange
            _auctionEventServiceMock
                .Setup(s => s.GetAuctionEventDetails(It.IsAny<int?>()))
                .ReturnsAsync((AuctionEventsResponse?)null);

            var expectedErrorMessage = $"No Response for Request with SaleId {saleId}.\r\nPlease try again with a valid SaleId.";

            // Act
            var actualSOAPRequest = await _soapRequestService.CreateSOAPRequest(saleId);

            // Assert
            Assert.Equal(expectedErrorMessage, actualSOAPRequest);
        }

        [Fact]
        public async Task Create_SOAP_Request_For_Sale_Should_Throw_Exception_When_Exception_Occurs()
        {
            // Arrange
            _auctionEventServiceMock
                .Setup(s => s.GetAuctionEventDetailsRouteTwo(It.IsAny<int?>()))
                .ThrowsAsync(new Exception());

            // Act
            var action = async () => await _soapRequestService.CreateSOAPRequest("1234");

            // Assert
            await Assert.ThrowsAsync<Exception>(action);
        }
    }
}