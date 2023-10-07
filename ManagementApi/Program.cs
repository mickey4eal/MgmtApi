// See https://aka.ms/new-console-template for more information
using ManagementApi.Services;
using System.Data.SqlClient;

Console.WriteLine("SOAP Request Generator Started\nPlease enter a valid SaleId to Generator Management API Request Template for Sale\nTo End Process, Enter Exit or Ex");
var shouldProgramRun = true;
do
{
    var consoleInput = Console.ReadLine();

    if (consoleInput?.ToLower() is "exit" or "ex")
    {
        shouldProgramRun = false;
    }
    else
    {
        var connectionString = "Trusted_Connection=True;";
        var wrapper = new SqlConnectionWrapper(new SqlConnection(connectionString));
        var auctionEventService = new AuctionEventService(wrapper);
        var soapRequestService = new SOAPRequestService(auctionEventService);
        var result = await soapRequestService.CreateSOAPRequestForSale(consoleInput);
        Console.WriteLine(result);
    }
}
while (shouldProgramRun);