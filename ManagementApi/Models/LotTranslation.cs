namespace ManagementApi.Models
{
    using Constants;

    public class LotTranslation
    {
        public string? ArtistMaker { get; set; }
        public string? ConditionReport { get; set; }
        public string? Description { get; set; }
        public string? Engraved {  get; set; }
        public string? Exhibited { get; set; }
        public string? ExtraInformation { get; set; }
        public string? LanguageCode { get; set; } = Strings.ENGLISH_LANGUAGE_CODE;
        public string? Literature { get; set; }
        public string? LotEssay { get; set; }
        public string? PostLotText { get; set; }
        public string? PreLotText { get; set; }
        public string? Provenance { get; set; }
        public string? Title { get; set; }
    }
}
