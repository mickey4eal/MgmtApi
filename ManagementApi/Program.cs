// See https://aka.ms/new-console-template for more information
using ManagementApi.Factories;
using ManagementApi.Models;

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
        var request = new SOAPRequestServiceRequest() { ConnectionString = "Trusted_Connection=True;" };
        var factory = new SOAPRequestServiceFactory();
        var soapRequestService = factory.Create(request);
        var result = await soapRequestService.CreateSOAPRequestForSale(consoleInput);
        Console.WriteLine(result);
    }
}
while (shouldProgramRun);