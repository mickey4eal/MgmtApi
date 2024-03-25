namespace ManagementApi.Models
{
    using Constants;

    public class AuctionEventItemResponse
    {
        public string ConsignorType { get; set; } = Strings.CONSIGNOR_TYPE;
        public bool ExplicitContent { get; set; }
        public bool Fragile {  get; set; }
        public int HubCode { get; set; } = Numbers.HUB_CODE;
        public string ItemType { get; set; } = Strings.S;
        public int LotNumber { get; set; }
        public int SaleNumber { get; set; }
        public int LotReserve { get; set; } = Numbers.LOT_RESERVE;
        public int StartingBidAmount { get; set; } = Numbers.STARTING_BID;
        public List<LotTranslation> LotTranslations { get; set; }
        public string UniqueIdentifier { get; set; }
    }
}
