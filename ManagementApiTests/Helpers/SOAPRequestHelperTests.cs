namespace ManagementApiTests.Helpers
{
    using AutoFixture;
    using ManagementApi.Helpers;
    using ManagementApi.Responses;

    public class SOAPRequestHelperTests
    {
        [Fact]
        private void Should_Return_FormattedRequest_When_AuctionEventsResponse_Is_Not_Null()
        {
            // Arrange
            var auctionEventsResponse = new Fixture().Create<AuctionEventsResponse>();

            // Act
            var result = SOAPRequestHelper.GenerateSOAPRequest(auctionEventsResponse);

            // Assert
            Assert.NotNull(result);
            Assert.False(string.IsNullOrEmpty(result));
        }

        [Fact]
        private void Should_Return_EmptyString_When_AuctionEventsResponse_Is_Null()
        {
            // Act
            var result = SOAPRequestHelper.GenerateSOAPRequest(null!);

            // Assert
            Assert.Equal(result, string.Empty);
        }
    }
}
