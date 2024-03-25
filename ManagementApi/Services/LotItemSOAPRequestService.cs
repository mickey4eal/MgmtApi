namespace ManagementApi.Services
{
    using Helpers;
    using Interfaces;
    using Resources;

    public class LotItemSOAPRequestService : ISOAPRequestService
    {
        private readonly IAuctionEventItemService _auctionEventItemService;

        public LotItemSOAPRequestService(IAuctionEventItemService auctionEventItemService)
        {
            _auctionEventItemService = auctionEventItemService;
        }

        public async Task<string> CreateSOAPRequest(string? ItemId)
        {
            const string NAME_OF_INPUT = nameof(ItemId);
            if (!SOAPRequestHelper.IsValidRequestInput(ItemId))
            {
                return string.Format(ApiRequests.InvalidInputMsg, NAME_OF_INPUT, ItemId);
            }

            var response = await _auctionEventItemService.GetAuctionEventItemDetails(int.Parse(ItemId!));

            return response == null
                ? string.Format(ApiRequests.NoResponseMsg, NAME_OF_INPUT, ItemId)
                : SOAPRequestHelper.GenerateLotItemSOAPRequest(response);
        }
    }
}