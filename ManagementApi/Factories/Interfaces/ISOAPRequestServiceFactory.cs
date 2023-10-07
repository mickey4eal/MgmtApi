namespace ManagementApi.Factories.Interfaces
{
    using ManagementApi.Models;
    using ManagementApi.Services;

    public interface ISOAPRequestServiceFactory
    {
        SOAPRequestService Create(SOAPRequestServiceRequest sOAPRequestServiceRequest);
    }
}
