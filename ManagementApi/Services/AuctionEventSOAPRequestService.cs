namespace ManagementApi.Services
{
    using ManagementApi.Helpers;
    using ManagementApi.Responses;
    using ManagementApi.Services.Interfaces;

    public class AuctionEventSOAPRequestService : ISOAPRequestService
    {
        private readonly IAuctionEventService _auctionEventService;

        public AuctionEventSOAPRequestService(IAuctionEventService auctionEventService)
        {
            _auctionEventService = auctionEventService;
        }

        public async Task<string> CreateSOAPRequest(string? saleId)
        {
            AuctionEventsResponse? response;

            if (SOAPRequestHelper.IsValidRequestInput(saleId))
            {
                response = await _auctionEventService.GetAuctionEventDetailsRouteTwo(int.Parse(saleId!));
            }
            else
            {
                return $"SaleId {saleId} is not valid. Please enter a valid SaleId.";
            }

            return response == null
                ? $"No Response for Request with SaleId {saleId}.\nPlease try again with a valid SaleId."
                : SOAPRequestHelper.GenerateAuctionEventSOAPRequest(response);
        }
    }
}