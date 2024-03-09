using ManagementApi.Helpers;
using ManagementApi.Models;
using ManagementApi.Resources;
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

        public async Task<string> CreateSOAPRequest(string? ItemId)
        {
            const string nameOfInput = nameof(ItemId);
            AuctionEventItemResponse? response;

            if (SOAPRequestHelper.IsValidRequestInput(ItemId))
            {
                response = await _auctionEventItemService.GetAuctionEventItemDetails(int.Parse(ItemId!));
            }
            else
            {
                return string.Format(ApiRequests.InvalidInputMsg, nameOfInput, ItemId);
            }

            return response == null
                ? string.Format(ApiRequests.NoResponseMsg, nameOfInput, ItemId)
                : SOAPRequestHelper.GenerateLotItemSOAPRequest(response);
        }
    }
}
