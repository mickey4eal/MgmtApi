namespace ManagementApi.Services.Interfaces
{
    public interface ISOAPRequestService
    {
        /// <summary>
        /// Create a SOAP Request for a given Sale Id
        /// </summary>
        /// <param name="saleId"></param>
        /// <returns>Returns a <see cref='string'/></returns>
        Task<string> CreateSOAPRequestForSale(string? saleId);
    }
}
