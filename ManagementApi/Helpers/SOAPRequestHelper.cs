namespace ManagementApi.Helpers
{
    using ManagementApi.Responses;

    public static class SOAPRequestHelper
    {
        public static string GenerateSOAPRequest(AuctionEventsResponse auctionEventsResponse)
        {
            return FormatSoapUITemplateWithAuctionEventsResponse(auctionEventsResponse);
        }

        private static string FormatSoapUITemplateWithAuctionEventsResponse(AuctionEventsResponse auctionEventsResponse) => auctionEventsResponse != null
                ? string.Format(Resources.ApiRequests.SoapUIRequestTemplate,
                    auctionEventsResponse.SaleName
                    , auctionEventsResponse.SaleNumber
                    , auctionEventsResponse.SaleStatus
                    , auctionEventsResponse.SaleType
                    , auctionEventsResponse.SalesTaxExemptionEnabled
                    , auctionEventsResponse.SaleEmail
                    , auctionEventsResponse.SaleHasInBondLots
                    , auctionEventsResponse.SaleCoordinatorEmailSignature
                    , auctionEventsResponse.LotEndTimeInterval
                    , auctionEventsResponse.KYCMandatory
                    , auctionEventsResponse.BidIncrementSet
                    , auctionEventsResponse.BondedDeliveryDisabled
                    , auctionEventsResponse.BuyersPremiumOverride
                    , auctionEventsResponse.CompanyCode
                    , auctionEventsResponse.Currency
                    , auctionEventsResponse.DefaultCreditLimit
                    , auctionEventsResponse.AnalyticsSaleType
                    , auctionEventsResponse.SaleStartTime
                    , auctionEventsResponse.SaleEndTime
                    , auctionEventsResponse.SessionStartTime
                    , auctionEventsResponse.SessionEndTime
                    , auctionEventsResponse.ShippingConfigurationType
                    , auctionEventsResponse.DateTimeOffsetMinutes
                    , auctionEventsResponse.GeoRestrictionAttribute)
                : string.Empty;
    }
}
