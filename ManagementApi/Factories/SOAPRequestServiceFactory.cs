namespace ManagementApi.Factories
{
    using ManagementApi.Factories.Interfaces;
    using ManagementApi.Models;
    using ManagementApi.Services;
    using System.Data.SqlClient;

    public class SOAPRequestServiceFactory : ISOAPRequestServiceFactory
    {
        public SOAPRequestService Create(SOAPRequestServiceRequest sOAPRequestServiceRequest)
        {
            var connectionString = sOAPRequestServiceRequest?.ConnectionString;
            var wrapper = new SqlConnectionWrapper(new SqlConnection(connectionString));
            var auctionEventService = new AuctionEventService(wrapper);

            return new SOAPRequestService(auctionEventService);
        }
    }
}
