namespace ManagementApi.Factories
{
    using Interfaces;
    using Models;
    using Services;
    using Services.Interfaces;

    public class SOAPRequestServiceFactory : ISOAPRequestServiceFactory
    {
        private readonly ISqlConnectionWrapper _wrapper;

        public SOAPRequestServiceFactory(ISqlConnectionWrapper wrapper)
        {
            _wrapper = wrapper;
        }

        public ISOAPRequestService Create(SOAPRequestServiceRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.ConnectionString))
            {
                throw new ArgumentException("Request or ConnectionString cannot be null or empty.");
            }

            try
            {
                if (request.ShouldExecuteForSale)
                {
                    var service = new AuctionEventService(_wrapper);
                    return new AuctionEventSOAPRequestService(service);
                }
                else
                {
                    var service = new AuctionEventItemService(_wrapper);
                    return new LotItemSOAPRequestService(service);
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