using ManagementApi.Resources;

namespace ManagementApi.Helpers
{
    public static class ProgramHelper
    {
        public static bool? HasConfirmExecutionRoute(string? consoleInput)
        {
            bool? hasConfirmExecutionRoute = null;

            if (IsBooleanVarNull(hasConfirmExecutionRoute) && (consoleInput?.ToLower() is "s" or "sale"))
            {
                hasConfirmExecutionRoute = true;
            }

            if (IsBooleanVarNull(hasConfirmExecutionRoute) && (consoleInput?.ToLower() is "l" or "lot"))
            {
                hasConfirmExecutionRoute = false;
            }

            if (IsBooleanVarNull(hasConfirmExecutionRoute))
            {
                Console.WriteLine(ApiRequests.StandardCommandPromptMsg);
            }

            return hasConfirmExecutionRoute;
        }

        public static bool ShouldProgramRun(string? consoleInput) => !(consoleInput?.ToLower() is "exit" or "ex");

        private static bool IsBooleanVarNull(bool? value) => value == null;
    }
}