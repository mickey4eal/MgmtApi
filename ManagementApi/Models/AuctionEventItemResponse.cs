namespace ManagementApi.Models
{
    public class AuctionEventItemResponse
    {
        public string ConsignorType { get; set; } = "Private";//Change to String Resource
        public bool ExplicitContent { get; set; }
        public bool Fragile {  get; set; }
        public int HubCode { get; set; } = 130;
        public string ItemType { get; set; } = "S";
        public int LotNumber { get; set; }
        public int SaleNumber { get; set; }
        public int LotReserve { get; set; } = 500;
        public int StartingBidAmount { get; set; } = 1500;
        public List<LotTranslation> LotTranslations { get; set; }
        public string UniqueIdentifier { get; set; }
    }
}
