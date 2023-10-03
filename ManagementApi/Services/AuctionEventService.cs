namespace ManagementApi.Services
{
    using Dapper;
    using ManagementApi.Responses;
    using ManagementApi.Services.Interfaces;
    using System.Data.SqlClient;

    public class AuctionEventService : IAuctionEventService
    {
        private readonly string _connectionString; 
        private const string CONNECTION_STRING = "connectionString";
        private const string AUCTION_DETAILS_QUERY = @"SELECT
      [SaleName]
	  ,[SaleNumber]
	  ,[StatusCode] AS SaleStatus
	  ,[SaleTypeCode] AS SaleType
	  ,[SalesTaxExemptionEnabled]
	  ,[EmailAddress] AS SaleEmail
	  ,[HasInBondLots] AS SaleHasInBondLots
	  ,[SaleCoordinatorEmailSignatureHtml] AS SaleCoordinatorEmailSignature
	  ,[LotEndTimeInterval]
	  ,[KycMandatory]
	  ,bis.[Name] AS BidIncrementSet
	  ,[BondedDeliveryDisabled]
	  ,[BuyersPremiumOverride]
	  ,bus.[Code] AS CompanyCode
	  --CompanyCode
	  ,cur.[ISO_4217_Code] AS Currency
	  ,[DefaultCreditLimit]
	  ,[AnalyticsSaleType]
	  ,[Starts] AS SaleStartTime
	  ,[Ends] AS SaleEndTime
	  ,aes.[SessionStart] AS SessionStartTime
	  --SessionStart
	  ,aes.[SessionEnd] AS SessionEndTime
	  --SessionEnd
	  ,[ShippingConfigurationType]
	  --OffsetMinutes
	  ,jgrsa.[AttributeName] AS GeoRestrictionAttribute
	  --GeoRestrictionAttribute
  FROM [dbo].[AuctionEvents] ae
  Join [dbo].[Businesses] bus ON ae.[BusinessId] = bus.[Id]
  Join [dbo].[AuctionEventSessions] aes ON ae.[Id] = aes.[AuctionEventId]
  Left Join [dbo].[JdeGeoRestrictionSaleAttributes] jgrsa ON ae.[Id] = jgrsa.[AuctionEventId]
  Join [dbo].[Currencies] cur ON ae.[CurrencyId] = cur.[Id]
  Join [dbo].[BidIncrementSets] bis ON ae.[BidIncrementSetId] = bis.[Id]
  Where ae.[Id] = @SaleId";

        public AuctionEventService()
        {
            _connectionString = CONNECTION_STRING;
        }

        public async Task<AuctionEventsResponse?> GetAuctionEventDetails(int? saleId)
        {
            try
            {
                using var conn = new SqlConnection(_connectionString);

                var auctionEventDetails = await conn.QuerySingleOrDefaultAsync<AuctionEventsResponse>(CreateGetAuctionEventDetailsCommand(), new
                {
                    SaleId = saleId
                });

                return auctionEventDetails;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static string CreateGetAuctionEventDetailsCommand() => AUCTION_DETAILS_QUERY;
    }
}