namespace ManagementApi.Services.Interfaces
{
    using Responses;

    public interface IAuctionEventService
    {
        /// <summary>
        /// Gets the Auction Event Details for a given Sale Id
        /// </summary>
        /// <param name="saleId"></param>
        /// <returns>Returns a <see cref='AuctionEventsResponse'/>;
        /// Throws an <see cref='Exception'/> if a failure occurs during the execution.</returns>
        Task<AuctionEventsResponse?> GetAuctionEventDetails(int? saleId);

        Task<AuctionEventsResponse?> GetAuctionEventDetailsRouteTwo(int? saleId);
    }
}
