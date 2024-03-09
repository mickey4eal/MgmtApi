// See https://aka.ms/new-console-template for more information
using ManagementApi.Factories;
using ManagementApi.Helpers;
using ManagementApi.Models;
using ManagementApi.Resources;
using ManagementApi.Services;
using System.Data.SqlClient;

Console.WriteLine(ApiRequests.IntroCommandPromptMsg + ApiRequests.StandardCommandPromptMsg);

bool shouldProgramRun;
const string connectionString = "Trusted_Connection=True;";
do
{
    string? consoleInput = Console.ReadLine();
    shouldProgramRun = ProgramHelper.ShouldProgramRun(consoleInput);
    var shouldExecuteForSale = ProgramHelper.HasConfirmedExecutionRoute(consoleInput);

    if (shouldExecuteForSale != null)
    {
        var consoleMsg = shouldExecuteForSale.Value
            ? ApiRequests.RequestInputPromptMsgSale
            : ApiRequests.RequestInputPromptMsgLot;
        Console.WriteLine(consoleMsg);

        consoleInput = Console.ReadLine();
        shouldProgramRun = ProgramHelper.ShouldProgramRun(consoleInput);

        if (shouldProgramRun)
        {
            var request = new SOAPRequestServiceRequest() { ConnectionString = connectionString, ShouldExecuteForSale = shouldExecuteForSale.Value };
            var connection = new SqlConnection(request.ConnectionString);
            var wrapper = new SqlConnectionWrapper(connection);
            var soapFactory = new SOAPRequestServiceFactory(wrapper);
            var soapRequestService = soapFactory.Create(request);
            var result = await soapRequestService.CreateSOAPRequest(consoleInput);
            Console.WriteLine(result);
        }
    }
}
while (shouldProgramRun);