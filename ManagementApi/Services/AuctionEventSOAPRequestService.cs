namespace ManagementApi.Services
{
    using ManagementApi.Helpers;
    using ManagementApi.Resources;
    using ManagementApi.Responses;
    using ManagementApi.Services.Interfaces;

    public class AuctionEventSOAPRequestService : ISOAPRequestService
    {
        private readonly IAuctionEventService _auctionEventService;

        public AuctionEventSOAPRequestService(IAuctionEventService auctionEventService)
        {
            _auctionEventService = auctionEventService;
        }

        public async Task<string> CreateSOAPRequest(string? SaleId)
        {
            const string nameOfInput = nameof(SaleId);
            AuctionEventsResponse? response;

            if (SOAPRequestHelper.IsValidRequestInput(SaleId))
            {
                response = await _auctionEventService.GetAuctionEventDetailsRouteTwo(int.Parse(SaleId!));
            }
            else
            {
                return string.Format(ApiRequests.InvalidInputMsg, nameOfInput, SaleId);
            }

            return response == null
                ? string.Format(ApiRequests.NoResponseMsg, nameOfInput, SaleId)
                : SOAPRequestHelper.GenerateAuctionEventSOAPRequest(response);
        }
    }
}