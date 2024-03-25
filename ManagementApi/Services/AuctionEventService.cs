namespace ManagementApi.Services
{
    using Constants;
    using Interfaces;
    using Responses;
    using System.Collections.Generic;

    public class AuctionEventService : IAuctionEventService
    {
        private readonly ISqlConnectionWrapper _sqlConnectionWrapper;

        public AuctionEventService(ISqlConnectionWrapper sqlConnectionWrapper)
        {
            _sqlConnectionWrapper = sqlConnectionWrapper;
        }

        public async Task<AuctionEventsResponse?> GetAuctionEventDetails(int? saleId)
        {
            try
            {
                AuctionEventsResponse? auctionEventsResponse;

                using (_sqlConnectionWrapper)
                {
                    auctionEventsResponse = await _sqlConnectionWrapper.QuerySingleOrDefaultAsync<AuctionEventsResponse?>(CreateGetAuctionEventDetailsCommand(), new
                    {
                        SaleId = saleId
                    });
                }

                return auctionEventsResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught! {ex}\n");
                return null;
            }
        }

        public async Task<AuctionEventsResponse?> GetAuctionEventDetailsRouteTwo(int? saleId)
        {
            try
            {
                IEnumerable<AuctionEventsResponse?> auctionEventsResponse;

                using (_sqlConnectionWrapper)
                {
                    auctionEventsResponse = await _sqlConnectionWrapper.QueryAsync<AuctionEventsResponse?>(CreateGetAuctionEventDetailsCommand(), new
                    {
                        SaleId = saleId
                    });
                }

                return auctionEventsResponse.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught! {ex}\n");
                return null;
            }
        }

        private static string CreateGetAuctionEventDetailsCommand() => SQLQueries.AUCTION_DETAILS_QUERY;
    }
}