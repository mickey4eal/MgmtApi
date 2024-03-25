namespace ManagementApi.Services
{
    using Constants;
    using Interfaces;
    using Models;
    using System.Threading.Tasks;

    public class AuctionEventItemService : IAuctionEventItemService
    {
        private readonly ISqlConnectionWrapper _sqlConnectionWrapper;

        public AuctionEventItemService(ISqlConnectionWrapper sqlConnectionWrapper)
        {
            _sqlConnectionWrapper = sqlConnectionWrapper;
        }

        public async Task<AuctionEventItemResponse?> GetAuctionEventItemDetails(int? itemId)
        {
            try
            {
                AuctionEventItemResponse? auctionEventItemResponse;

                using (_sqlConnectionWrapper)
                {
                    auctionEventItemResponse = await _sqlConnectionWrapper.QuerySingleOrDefaultAsync<AuctionEventItemResponse?>(CreateGetItemDetailsCommand(), new
                    {
                        ItemId = itemId
                    });

                    if (auctionEventItemResponse != null)
                    {
                        // Add Lot Translations
                        var engTranslation = await _sqlConnectionWrapper.QuerySingleOrDefaultAsync<LotTranslation>(CreateGetItemEnglishTranslationsCommand(), new
                        {
                            ItemId = itemId
                        });

                        auctionEventItemResponse.LotTranslations = engTranslation != null
                            ? new List<LotTranslation>() { engTranslation }
                            : new List<LotTranslation>();

                        var otherTranslations = await _sqlConnectionWrapper.QueryAsync<LotTranslation>(CreateGetItemTranslationsCommand(), new
                        {
                            ItemId = itemId
                        });

                        if (otherTranslations != null)
                        {
                            auctionEventItemResponse.LotTranslations.AddRange(otherTranslations);
                        }
                    }
                }

                return auctionEventItemResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught! {ex}\n");
                return null;
            }
        }

        private static string CreateGetItemDetailsCommand() => SQLQueries.ITEM_DETAILS_QUERY;

        private static string CreateGetItemEnglishTranslationsCommand() => SQLQueries.ITEM_ENG_TRANSLATIONS_QUERY;

        private static string CreateGetItemTranslationsCommand() => SQLQueries.ITEM_TRANSLATIONS_QUERY;
    }
}