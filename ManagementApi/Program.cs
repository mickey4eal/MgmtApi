// See https://aka.ms/new-console-template for more information
using ManagementApi.Services;

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
        var auctionEventService = new AuctionEventService();
        var soapRequestService = new SOAPRequestService(auctionEventService);
        var result = await soapRequestService.CreateSOAPRequestForSale(consoleInput);
        Console.WriteLine(result);
    }
}
while (shouldProgramRun);