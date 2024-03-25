namespace ManagementApiTests.Helpers
{
    using ManagementApi.Constants;
    using ManagementApi.Helpers;
    using Xunit;

    public class ProgramHelperTests
    {
        [Fact]
        public void HasConfirmedExecutionRoute_ReturnsTrueForSaleInput()
        {
            // Arrange
            string consoleInput = Strings.S;

            // Act
            bool? result = ProgramHelper.HasConfirmedExecutionRoute(consoleInput);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void HasConfirmedExecutionRoute_ReturnsTrueForSaleInputIgnoreCase()
        {
            // Arrange
            string consoleInput = Strings.SALE;

            // Act
            bool? result = ProgramHelper.HasConfirmedExecutionRoute(consoleInput);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void HasConfirmedExecutionRoute_ReturnsFalseForLotInput()
        {
            // Arrange
            string consoleInput = Strings.L;

            // Act
            bool? result = ProgramHelper.HasConfirmedExecutionRoute(consoleInput);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void HasConfirmedExecutionRoute_ReturnsNullForInvalidInput()
        {
            // Arrange
            string consoleInput = "invalid";

            // Act
            bool? result = ProgramHelper.HasConfirmedExecutionRoute(consoleInput);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void ShouldProgramRun_ReturnsTrueForNonExitInput()
        {
            // Arrange
            string consoleInput = "any input";

            // Act
            bool result = ProgramHelper.ShouldProgramRun(consoleInput);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void ShouldProgramRun_ReturnsFalseForExitInputIgnoreCase()
        {
            // Arrange
            string consoleInput = Strings.EXIT;

            // Act
            bool result = ProgramHelper.ShouldProgramRun(consoleInput);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsBooleanNull_ReturnsTrueForNullValue()
        {
            // Arrange
            bool? value = null;

            // Act
            bool result = ProgramHelper.IsBooleanNull(value);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsBooleanNull_ReturnsFalseForNonNullValue()
        {
            // Arrange
            bool? value = true;

            // Act
            bool result = ProgramHelper.IsBooleanNull(value);

            // Assert
            Assert.False(result);
        }
    }
}