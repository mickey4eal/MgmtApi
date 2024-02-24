namespace ManagementApi.Services.Interfaces
{
    using ManagementApi.Models;

    public interface IAuctionEventItemService
    {
        /// <summary>
        /// Gets the Auction Event Item Details for a given Item Id
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns>Returns a <see cref='AuctionEventItemResponse'/>;
        /// Throws an <see cref='Exception'/> if a failure occurs during the execution.</returns>
        Task<AuctionEventItemResponse?> GetAuctionEventItemDetails(int? itemId);
    }
}
