using ManagementApi.Models;

namespace ManagementApi.Services.Interfaces
{
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
