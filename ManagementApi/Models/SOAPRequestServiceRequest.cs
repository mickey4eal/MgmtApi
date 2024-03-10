namespace ManagementApi.Models
{
    public class SOAPRequestServiceRequest
    {
        public string? ConnectionString { get; set; }
        public bool ShouldExecuteForSale { get; set; }
        public string? Input {  get; set; }
    }
}
