namespace ManagementApi.Factories
{
    using ManagementApi.Factories.Interfaces;
    using ManagementApi.Models;
    using ManagementApi.Services;
    using ManagementApi.Services.Interfaces;
    using System.Data.SqlClient;

    public class SOAPRequestServiceFactory : ISOAPRequestServiceFactory
    {
        public ISOAPRequestService Create(SOAPRequestServiceRequest sOAPRequestServiceRequest)
        {
            if (sOAPRequestServiceRequest == null || string.IsNullOrWhiteSpace(sOAPRequestServiceRequest.ConnectionString))
            {
                throw new ArgumentNullException();
            }

            try
            {
                var connectionString = sOAPRequestServiceRequest.ConnectionString;
                var connection = new SqlConnection(connectionString);
                var wrapper = new SqlConnectionWrapper(connection);

                if (sOAPRequestServiceRequest.ShouldExecuteForSale)
                {
                    var auctionEventService = new AuctionEventService(wrapper);
                    return new AuctionEventSOAPRequestService(auctionEventService);
                }
                else
                {
                    var itemService = new AuctionEventItemService(wrapper);
                    return new LotItemSOAPRequestService(itemService);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught! {ex}");
                throw;
            }
        }
    }
}