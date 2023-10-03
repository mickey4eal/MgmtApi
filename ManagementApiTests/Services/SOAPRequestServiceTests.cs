namespace ManagementApiTests.Services
{
    using AutoFixture;
    using AutoFixture.Kernel;
    using ManagementApi.Services;
    using ManagementApi.Services.Interfaces;

    public class SOAPRequestServiceTests
    {
        private readonly ISOAPRequestService _iSOAPRequestService;

        public SOAPRequestServiceTests()
        {
            var fixture = new Fixture();
            fixture.Customizations.Add(
                new TypeRelay(
                    typeof(IAuctionEventService),
                    typeof(AuctionEventService)));
            fixture.Customizations.Add(
                new TypeRelay(
                    typeof(ISOAPRequestService),
                    typeof(SOAPRequestService)));
            _iSOAPRequestService = fixture.Create<ISOAPRequestService>();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("-45")]
        [InlineData("123")]
        [InlineData("123A")]
        [InlineData("sdandapd")]
        [InlineData("dmpqiej32n38hf9c4n9fbc")]
        [InlineData("44444444444444444444444444444444444444444444")]
        private async void Should_Return_Error_Message_When_SaleId_Is_Not_Valid(string? saleId)
        {
            // Arrange
            var expectedResult = $"SaleId {saleId} is not valid. Please enter a valid SaleId.";

            // Act
            var actualResult = await _iSOAPRequestService.CreateSOAPRequestForSale(saleId);

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData("1234")]
        [InlineData("2345")]
        private async void Should_Return_Message_When_SaleId_Is_Valid(string saleId)
        {
            // Act
            var result = await _iSOAPRequestService.CreateSOAPRequestForSale(saleId);

            // Assert
            Assert.NotNull(result);
            Assert.False(string.IsNullOrEmpty(result));
        }
    }
}
