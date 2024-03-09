// See https://aka.ms/new-console-template for more information
using ManagementApi.Helpers;
using ManagementApi.Models;
using ManagementApi.Resources;
using ManagementApi.Services.Wrappers;
using System.Data.SqlClient;

Console.WriteLine(ApiRequests.IntroCommandPromptMsg + "\n" + ApiRequests.StandardCommandPromptMsg);

bool shouldRun;
const string connectionString = "Trusted_Connection=True;";
do
{
    string? input = Console.ReadLine();
    shouldRun = ProgramHelper.ShouldProgramRun(input);
    var shouldExecuteForSale = ProgramHelper.HasConfirmedExecutionRoute(input);

    if (shouldExecuteForSale != null)
    {
        var promptConsoleMsg = shouldExecuteForSale.Value
            ? ApiRequests.RequestInputPromptMsgSale
            : ApiRequests.RequestInputPromptMsgLot;
        Console.WriteLine(promptConsoleMsg);

        input = Console.ReadLine();
        shouldRun = ProgramHelper.ShouldProgramRun(input);

        if (shouldRun)
        {
            var request = new SOAPRequestServiceRequest()
            {
                ConnectionString = connectionString,
                ShouldExecuteForSale = shouldExecuteForSale.Value,
                Input = input
            };

            using (var connection = new SqlConnection(request.ConnectionString))
            {
                var serviceWrapper = new SoapRequestServiceWrapper(connection);
                var result = await serviceWrapper.CreateSOAPRequest(request);
                Console.WriteLine(result);
            }
        }
    }
}
while (shouldRun);