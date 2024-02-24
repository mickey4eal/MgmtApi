namespace ManagementApi.Services.Interfaces
{
    public interface ISOAPRequestService
    {
        /// <summary>
        /// Create a SOAP Request for a given input
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Returns a <see cref='string'/></returns>
        Task<string> CreateSOAPRequest(string? input);
    }
}
