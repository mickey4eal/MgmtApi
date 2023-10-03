namespace ManagementApi.Models
{
    public enum AuctionEventStatus
    {
        InPreparation = 1,
        BrowsableOnly = 2,
        Live = 3,
        Complete = 4
    }

    public enum SaleType
    {
        Esf,
        Fps,
    }

    public enum ShippingConfigurationType
    {
        Standard,
        PrePacked,
        PostPacked,
    }
}
