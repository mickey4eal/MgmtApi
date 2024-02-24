namespace ManagementApi.Services
{
    using ManagementApi.Models;
    using ManagementApi.Services.Interfaces;
    using System.Threading.Tasks;

    public class AuctionEventItemService : IAuctionEventItemService
    {
        private readonly ISqlConnectionWrapper _sqlConnectionWrapper;

        private const string ITEM_DETAILS_QUERY = @"SELECT
	                                                it.[ExplicitContent],
	                                                it.[Fragile],
	                                                it.[HubId],
	                                                it.[ItemType],
	                                                it.[ParentLotNumber] AS LotNumber,
	                                                ae.[SaleNumber],
	                                                ud.[UniqueIdentifier],
	                                                it.[AuctionEventId],
	                                                it.[ArtistMaker],
	                                                it.[ConditionReport],
	                                                it.[Description],
	                                                it.[Engraved],
	                                                it.[Exhibited],
	                                                it.[ExtraInformation],
	                                                it.[Literature],
	                                                it.[LotEssay],
	                                                it.[PostLotText],
	                                                it.[PreLotText],
	                                                it.[Provenance],
	                                                it.[Title]
                                                FROM
	                                                [dbo].[Items] it
	                                                LEFT JOIN [AuctionEvents] ae ON ae.Id = [AuctionEventId] OUTER APPLY (

		                                                SELECT
			                                                CONCAT(ae.[SaleNumber], '.', it.[ParentLotNumber]) AS UniqueIdentifier
	                                                ) AS ud
                                                WHERE
	                                                it.[Id] = @ItemId";

        private const string ITEM_ENG_TRANSLATIONS_QUERY = @"SELECT
	                                                [ArtistMaker],
	                                                [ConditionReport],
	                                                [Description],
	                                                [Engraved],
	                                                [Exhibited],
	                                                [ExtraInformation],
	                                                [Literature],
	                                                [LotEssay],
	                                                [PostLotText],
	                                                [PreLotText],
	                                                [Provenance],
	                                                [Title]
                                                FROM
	                                                [dbo].[Items]
                                                WHERE
	                                                [Id] = @ItemId";

        private const string ITEM_TRANSLATIONS_QUERY = @"SELECT
												[ArtistMaker],
												[ConditionReport],
												[Description],
												[Engraved],
												[Exhibited],
												[ExtraInformation],
												[CultureCode] AS LanguageCode,
												[Literature],
												[LotEssay],
												[PostLotText],
												[PreLotText],
												[Provenance],
												[Title]
											FROM
												[dbo].[ItemTranslations]
											WHERE
												ItemId = @ItemId";

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
                        // Add Translations
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

        private static string CreateGetItemDetailsCommand() => ITEM_DETAILS_QUERY;

        private static string CreateGetItemEnglishTranslationsCommand() => ITEM_ENG_TRANSLATIONS_QUERY;

        private static string CreateGetItemTranslationsCommand() => ITEM_TRANSLATIONS_QUERY;
    }
}