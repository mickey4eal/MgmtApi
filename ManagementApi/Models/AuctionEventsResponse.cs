namespace ManagementApi.Responses
{
    using ManagementApi.Models;

    public class AuctionEventsResponse
    {
        public string? SaleName { get; set; }
        public int? SaleNumber { get; set; }
        public AuctionEventStatus? SaleStatus { get; set; }
        public SaleType? SaleType { get; set; }
        public int? SalesTaxExemptionEnabled { get; set; }
        public string? SaleEmail { get; set; }
        public int? SaleHasInBondLots { get; set; }
        public string? SaleCoordinatorEmailSignature { get; set; }
        public int? LotEndTimeInterval { get; set; }
        public int? KYCMandatory { get; set; }
        public string? BidIncrementSet { get; set; }
        public int? BondedDeliveryDisabled { get; set; }
        public int? BuyersPremiumOverride { get; set; }
        public string? CompanyCode { get; set; }
        public string? Currency { get; set; }
        public string? DefaultCreditLimit { get; set; }
        public string? AnalyticsSaleType { get; set; }
        public DateTime SaleStartTime { get; set; }
        public DateTime SaleEndTime { get; set; }
        public DateTime SessionStartTime { get; set; }
        public DateTime SessionEndTime { get; set; }
        public ShippingConfigurationType? ShippingConfigurationType { get; set; }
        public int? DateTimeOffsetMinutes { get; set; } = 60;
        public string? GeoRestrictionAttribute { get; set; }
    }
}