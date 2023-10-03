namespace ManagementApi.Services.Interfaces
{
    using ManagementApi.Responses;

    public interface IAuctionEventService
    {
        Task<AuctionEventsResponse?> GetAuctionEventDetails(int? saleId);
    }
}
