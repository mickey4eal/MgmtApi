using ManagementApi.Helpers;
using ManagementApi.Models;
using ManagementApi.Services.Interfaces;

namespace ManagementApi.Services
{
    public class LotItemSOAPRequestService : ISOAPRequestService
    {
        private readonly IAuctionEventItemService _auctionEventItemService;

        public LotItemSOAPRequestService(IAuctionEventItemService auctionEventItemService)
        {
            _auctionEventItemService = auctionEventItemService;
        }

        public async Task<string> CreateSOAPRequest(string? itemId)
        {
            AuctionEventItemResponse? response;

            if (SOAPRequestHelper.IsValidRequestInput(itemId))
            {
                response = await _auctionEventItemService.GetAuctionEventItemDetails(int.Parse(itemId!));
            }
            else
            {
                return $"ItemId {itemId} is not valid. Please enter a valid ItemId.";
            }

            return response == null
                ? $"No Response for Request with ItemId {itemId}.\nPlease try again with a valid ItemId."
                : SOAPRequestHelper.GenerateLotItemSOAPRequest(response);
        }
    }
}
