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
            try
            
            {
                var connectionString = sOAPRequestServiceRequest?.ConnectionString;
                if (string.IsNullOrWhiteSpace(connectionString))
                    throw new ArgumentNullException();

                var wrapper = new SqlConnectionWrapper(new SqlConnection(connectionString));
                var auctionEventService = new AuctionEventService(wrapper);
                return new SOAPRequestService(auctionEventService);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught! {ex}");
                throw;
            }
        }
    }
}
