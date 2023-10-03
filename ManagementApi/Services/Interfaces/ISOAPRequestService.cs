namespace ManagementApi.Services.Interfaces
{
    public interface ISOAPRequestService
    {
        Task<string> CreateSOAPRequestForSale(string? saleId);
    }
}
