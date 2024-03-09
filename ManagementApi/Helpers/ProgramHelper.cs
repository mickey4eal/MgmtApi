namespace ManagementApi.Helpers
{
    using ManagementApi.Constants;
    using ManagementApi.Resources;

    public static class ProgramHelper
    {
        public static bool? HasConfirmedExecutionRoute(string? consoleInput)
        {
            bool? hasConfirmExecutionRoute = null;

            if (IsBooleanNull(hasConfirmExecutionRoute) && (consoleInput?.ToLower() is ProgramHelperStrings.S or ProgramHelperStrings.SALE))
            {
                hasConfirmExecutionRoute = true;
            }

            if (IsBooleanNull(hasConfirmExecutionRoute) && (consoleInput?.ToLower() is ProgramHelperStrings.L or ProgramHelperStrings.LOT))
            {
                hasConfirmExecutionRoute = false;
            }

            if (IsBooleanNull(hasConfirmExecutionRoute))
            {
                Console.WriteLine(ApiRequests.StandardCommandPromptMsg);
            }

            return hasConfirmExecutionRoute;
        }

        public static bool ShouldProgramRun(string? consoleInput) => !(consoleInput?.ToLower() is ProgramHelperStrings.EX or ProgramHelperStrings.EXIT);

        public static bool IsBooleanNull(bool? value) => value == null;
    }
}