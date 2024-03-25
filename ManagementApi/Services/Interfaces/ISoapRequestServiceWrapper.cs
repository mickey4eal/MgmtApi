namespace ManagementApi.Services.Interfaces
{
    using Models;

    public interface ISoapRequestServiceWrapper
    {
        /// <summary>
        /// Create a SOAP Request for a given SOAPRequestServiceRequest
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Returns a <see cref='string'/></returns>
        Task<string> CreateSOAPRequest(SOAPRequestServiceRequest request);
    }
}
