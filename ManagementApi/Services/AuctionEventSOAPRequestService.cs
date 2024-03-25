namespace ManagementApi.Services
{
    using Helpers;
    using Interfaces;
    using Resources;

    public class AuctionEventSOAPRequestService : ISOAPRequestService
    {
        private readonly IAuctionEventService _auctionEventService;

        public AuctionEventSOAPRequestService(IAuctionEventService auctionEventService)
        {
            _auctionEventService = auctionEventService;
        }

        public async Task<string> CreateSOAPRequest(string? SaleId)
        {
            const string NAME_OF_INPUT = nameof(SaleId);
            if (!SOAPRequestHelper.IsValidRequestInput(SaleId))
            {
                return string.Format(ApiRequests.InvalidInputMsg, NAME_OF_INPUT, SaleId);
            }

            var response = await _auctionEventService.GetAuctionEventDetailsRouteTwo(int.Parse(SaleId!));

            return response == null
                ? string.Format(ApiRequests.NoResponseMsg, NAME_OF_INPUT, SaleId)
                : SOAPRequestHelper.GenerateAuctionEventSOAPRequest(response);
        }
    }
}