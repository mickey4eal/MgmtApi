namespace ManagementApi.Helpers
{
    using ManagementApi.Constants;
    using ManagementApi.Resources;

    public static class ProgramHelper
    {
        public static bool? HasConfirmedExecutionRoute(string? consoleInput)
        {
            bool? hasConfirmedExecutionRoute = null;

            if (IsBooleanNull(hasConfirmedExecutionRoute) && (consoleInput?.ToLower() is ProgramHelperStrings.S or ProgramHelperStrings.SALE))
            {
                hasConfirmedExecutionRoute = true;
            }

            if (IsBooleanNull(hasConfirmedExecutionRoute) && (consoleInput?.ToLower() is ProgramHelperStrings.L or ProgramHelperStrings.LOT))
            {
                hasConfirmedExecutionRoute = false;
            }

            if (IsBooleanNull(hasConfirmedExecutionRoute))
            {
                Console.WriteLine(ApiRequests.StandardCommandPromptMsg);
            }

            return hasConfirmedExecutionRoute;
        }

        public static bool ShouldProgramRun(string? consoleInput) => !(consoleInput?.ToLower() is ProgramHelperStrings.EX or ProgramHelperStrings.EXIT);

        public static bool IsBooleanNull(bool? value) => value == null;
    }
}