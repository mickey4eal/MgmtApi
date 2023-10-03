namespace ManagementApi.Services
{
    using System.Linq;
    using ManagementApi.Helpers;
    using ManagementApi.Responses;
    using ManagementApi.Services.Interfaces;

    public class SOAPRequestService : ISOAPRequestService
    {
        private readonly IAuctionEventService _auctionEventService;

        public SOAPRequestService(IAuctionEventService auctionEventService)
        {
            _auctionEventService = auctionEventService;
        }

        public async Task<string> CreateSOAPRequestForSale(string? saleId)
        {
            AuctionEventsResponse? response;

            if (IsValidSaleId(saleId))
            {
                response = await _auctionEventService.GetAuctionEventDetails(int.Parse(saleId!));
            }
            else
            {
                return $"SaleId {saleId} is not valid. Please enter a valid SaleId.";
            }

            return response == null
                ? $"Sale with SaleId {saleId} Not Found."
                : SOAPRequestHelper.GenerateSOAPRequest(response);
        }

        private static bool IsValidSaleId(string? saleId)
        {
            if (string.IsNullOrEmpty(saleId))
            {
                return false;
            }

            var numbers = new List<char>();
            numbers.AddRange(from number in saleId
                             select number);

            //checking for negative numbers and saleId has 4 digits minimum
            if (numbers.Count < 4 || numbers[0] == '-')
            {
                return false;
            }

            return int.TryParse(saleId, out _);
        }
    }
}
