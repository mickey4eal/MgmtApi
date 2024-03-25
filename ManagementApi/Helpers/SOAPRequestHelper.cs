namespace ManagementApi.Helpers
{
    using Constants;
    using Models;
    using Responses;

    public static class SOAPRequestHelper
    {
        /// <summary>
        /// Creates the SOAP Request from the auction event response.
        /// </summary>
        /// <param name="auctionEventsResponse"></param>
        /// <returns>The SOAP Request as a <see cref='string'/></returns>
        public static string GenerateAuctionEventSOAPRequest(AuctionEventsResponse auctionEventsResponse)
        {
            return FormatSoapUITemplateWithAuctionEventsResponse(auctionEventsResponse);
        }

        /// <summary>
        /// Creates the SOAP Request from the auction event item response.
        /// </summary>
        /// <param name="auctionEventItemResponse"></param>
        /// <returns>The SOAP Request as a <see cref='string'/></returns>
        public static string GenerateLotItemSOAPRequest(AuctionEventItemResponse auctionEventItemResponse)
        {
            return FormatSoapUITemplateWithAuctionEventItemResponse(auctionEventItemResponse);
        }

        private static string FormatSoapUITemplateWithAuctionEventsResponse(AuctionEventsResponse auctionEventsResponse) => auctionEventsResponse != null
                ? string.Format(Resources.ApiRequests.SoapUIRequestTemplate
                    , auctionEventsResponse.SaleName
                    , auctionEventsResponse.SaleNumber
                    , auctionEventsResponse.SaleStatus
                    , Strings.SALE_TYPE
                    , auctionEventsResponse.SalesTaxExemptionEnabled
                    , auctionEventsResponse.SaleEmail
                    , auctionEventsResponse.SaleHasInBondLots
                    , FormatEmailSignature(auctionEventsResponse.SaleCoordinatorEmailSignature)
                    , Strings.LOT_END_TIME_INTERVAL
                    , auctionEventsResponse.KYCMandatory
                    , auctionEventsResponse.BidIncrementSet
                    , auctionEventsResponse.BondedDeliveryDisabled
                    , auctionEventsResponse.BuyersPremiumOverride
                    , auctionEventsResponse.CompanyCode
                    , auctionEventsResponse.Currency
                    , auctionEventsResponse.DefaultCreditLimit
                    , auctionEventsResponse.AnalyticsSaleType
                    , FormatDateTimeString(auctionEventsResponse.SaleStartTime)
                    , FormatDateTimeString(auctionEventsResponse.SaleEndTime)
                    , FormatDateTimeString(auctionEventsResponse.SessionStartTime)
                    , FormatDateTimeString(auctionEventsResponse.SessionEndTime)
                    , auctionEventsResponse.ShippingConfigurationType
                    , auctionEventsResponse.DateTimeOffsetMinutes
                    , auctionEventsResponse.GeoRestrictionAttribute)
                : string.Empty;

        private static string FormatSoapUITemplateWithAuctionEventItemResponse(AuctionEventItemResponse auctionEventItemResponse) => auctionEventItemResponse != null
            ? string.Format(Resources.ApiRequests.LotItemSoapUIRequestTemplate
                , auctionEventItemResponse.ConsignorType
                , ConvertBooleanToInteger(auctionEventItemResponse.ExplicitContent)
                , ConvertBooleanToInteger(auctionEventItemResponse.Fragile)
                , auctionEventItemResponse.HubCode
                , auctionEventItemResponse.ItemType
                , auctionEventItemResponse.LotNumber
                , auctionEventItemResponse.SaleNumber
                , auctionEventItemResponse.LotReserve
                , auctionEventItemResponse.StartingBidAmount
                , auctionEventItemResponse.UniqueIdentifier
                , FormatLotTranslations(auctionEventItemResponse.LotTranslations))
            : string.Empty;

        private static string FormatLotTranslations(List<LotTranslation> lotTranslations)
        {
            var result = string.Empty;

            if (lotTranslations.Any())
            {
                foreach (var translation in lotTranslations)
                {
                    result += string.Format(Resources.ApiRequests.LotTranslationTemplate
                        , translation.ArtistMaker
                        , translation.ConditionReport
                        , translation.Description
                        , translation.Engraved
                        , translation.Exhibited
                        , translation.ExtraInformation
                        , translation.LanguageCode
                        , translation.Literature
                        , translation.LotEssay
                        , translation.PostLotText
                        , translation.PreLotText
                        , translation.Provenance
                        , translation.Title);
                }
            }

            return result;
        }

        private static string FormatDateTimeString(DateTime dateTime) => dateTime.ToString("yyyy-MM-ddTHH:mm:ss");

        private static string FormatEmailSignature(string? emailSignature) => emailSignature != null
                                                                            ? emailSignature.Replace("<br>", "&lt;br&gt;")
                                                                            : string.Empty;

        private static int ConvertBooleanToInteger(bool isTrue) => isTrue ? 1 : 0;

        public static bool IsValidRequestInput(string? input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            var numbers = new List<char>();
            numbers.AddRange(from number in input
                             select number);

            //checking for negative numbers
            if (numbers[0] == '-')
            {
                return false;
            }

            return int.TryParse(input, out _);
        }
    }
}