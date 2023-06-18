using System;
using System.Collections;
using System.Collections.Generic;
using Ardalis.GuardClauses;
using Xunit;

namespace GuardClauses.UnitTests;

public class GuardAgainstNullOrInvalidInput
{
    [Theory]
    [ClassData(typeof(ArgumentNullExceptionClassData))]
    public void ThrowsArgumentNullExceptionWhenInputIsNull(string input, Func<string, bool> func)
    {
        Assert.Throws<GuardNullException>("string",
            () => Guard.Against.NullOrInvalidInput(input, "string", func));
    }

    [Theory]
    [ClassData(typeof(ArgumentExceptionClassData))]
    public void ThrowsArgumentExceptionWhenInputIsInvalid(string input, Func<string, bool> func)
    {
        Assert.Throws<GuardInvalidException>("string",
            () => Guard.Against.NullOrInvalidInput(input, "string", func));
    }

    [Theory]
    [ClassData(typeof(ValidClassData))]
    public void ReturnsExceptedValueWhenGivenValidInput(int input, Func<int, bool> func)
    {
        var result = Guard.Against.NullOrInvalidInput(input, nameof(input), func);
        Assert.Equal(input, result);
    }

    [Theory]
    [InlineData(null, "Input parameterName did not satisfy the options (Parameter 'parameterName')", typeof(GuardInvalidException), 10)]
    [InlineData("Evaluation failed", "Evaluation failed (Parameter 'parameterName')", typeof(GuardInvalidException), 10)]
    [InlineData(null, "Value cannot be null. (Parameter 'parameterName')", typeof(GuardNullException))]
    [InlineData("Please provide correct value", "Please provide correct value (Parameter 'parameterName')", typeof(GuardNullException))]
    public void ErrorMessageMatchesExpected(string customMessage, string expectedMessage, Type exceptionType, int? input = null)
    {
        var exception = Assert.Throws(exceptionType,
            () => Guard.Against.NullOrInvalidInput(input, "parameterName", x => x > 20, customMessage));

        Assert.NotNull(exception);
        Assert.NotNull(exception.Message);
        Assert.Equal(expectedMessage, exception.Message);
    }

    [Theory]
    [InlineData(null, null, typeof(GuardInvalidException), 10)]
    [InlineData(null, "Please provide correct value", typeof(GuardInvalidException), 10)]
    [InlineData("SomeParameter", null, typeof(GuardNullException))]
    [InlineData("SomeOtherParameter", "Value must be correct", typeof(GuardNullException))]
    public void ExceptionParamNameMatchesExpected(string expectedParamName, string customMessage, Type exceptionType, int? input = null)
    {
        var exception = Assert.Throws(exceptionType,
            () => Guard.Against.NullOrInvalidInput(input, expectedParamName, x => x > 20, customMessage));

        Assert.IsAssignableFrom<GuardNullException>(exception);
        Assert.NotNull(exception);
        Assert.Equal(expectedParamName, (exception as GuardNullException)!.ParamName);
    }

    public class ValidClassData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 20, (Func<int, bool>)(x => x > 10) };
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class ArgumentNullExceptionClassData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { null!, (Func<string, bool>)(x => x.Length > 10) };
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class ArgumentExceptionClassData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { "TestData", (Func<string, bool>)(x => x.Length > 10) };
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
