namespace ManagementApi.Factories.Interfaces
{
    using ManagementApi.Models;
    using ManagementApi.Services;

    public interface ISOAPRequestServiceFactory
    {
        /// <summary>
        /// Creates an instance of the SOAPRequestService.
        /// </summary>
        /// <param name="sOAPRequestServiceRequest"></param>
        /// <returns>Returns an instance of <see cref='SOAPRequestService'/> if creation process is successful.
        /// Throws <see cref='ArgumentNullException'/> and <see cref='Exception'/> is failure occurs during creation process.</returns>
        SOAPRequestService Create(SOAPRequestServiceRequest sOAPRequestServiceRequest);
    }
}
