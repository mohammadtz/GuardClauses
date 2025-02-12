﻿using System;
using Ardalis.GuardClauses;
using Xunit;

namespace GuardClauses.UnitTests;

public class GuardAgainstNullOrWhiteSpace
{
    [Theory]
    [InlineData("a")]
    [InlineData("1")]
    [InlineData("some text")]
    [InlineData(" leading whitespace")]
    [InlineData("trailing whitespace ")]
    public void DoesNothingGivenNonEmptyStringValue(string nonEmptyString)
    {
        Guard.Against.NullOrWhiteSpace(nonEmptyString, "string");
        Guard.Against.WhiteSpace(nonEmptyString.AsSpan(), "stringSpan");
        Guard.Against.NullOrWhiteSpace(nonEmptyString, "aNumericString");
    }

    [Fact]
    public void ThrowsGivenNullValue()
    {
        Assert.Throws<GuardNullException>(() => Guard.Against.NullOrWhiteSpace(null, "null"));
    }

    [Fact]
    public void ThrowsGivenEmptyString()
    {
        Assert.Throws<GuardNullException>(() => Guard.Against.NullOrWhiteSpace("", "emptystring"));
    }

    [Fact]
    public void ThrowsGivenEmptyStringSpan()
    {
        Assert.Throws<GuardNullException>(() => Guard.Against.WhiteSpace("".AsSpan(), "emptyStringSpan"));
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("   ")]
    public void ThrowsGivenWhiteSpaceString(string whiteSpaceString)
    {
        Assert.Throws<GuardNullException>(() => Guard.Against.NullOrWhiteSpace(whiteSpaceString, "whitespacestring"));
        Assert.Throws<GuardNullException>(() => Guard.Against.WhiteSpace(whiteSpaceString.AsSpan(), "whiteSpaceStringSpan"));
    }

    [Theory]
    [InlineData("a", "a")]
    [InlineData("1", "1")]
    [InlineData("some text", "some text")]
    [InlineData(" leading whitespace", " leading whitespace")]
    [InlineData("trailing whitespace ", "trailing whitespace ")]
    public void ReturnsExpectedValueGivenNonEmptyStringValue(string nonEmptyString, string expected)
    {
        Assert.Equal(expected, Guard.Against.NullOrWhiteSpace(nonEmptyString, "string"));
        Assert.Equal(expected, Guard.Against.WhiteSpace(nonEmptyString.AsSpan(), "stringSpan").ToString());
        Assert.Equal(expected, Guard.Against.NullOrWhiteSpace(nonEmptyString, "aNumericString"));
    }

    [Theory]
    [InlineData(null, "Required input parameterName was empty. (Parameter 'parameterName')")]
    [InlineData("Value is empty", "Value is empty (Parameter 'parameterName')")]
    public void ErrorMessageMatchesExpectedWhenInputIsWhiteSpace(string customMessage, string expectedMessage)
    {
        var exception = Assert.Throws<GuardNullException>(() => Guard.Against.NullOrWhiteSpace(" ", "parameterName", customMessage));
        Assert.NotNull(exception);
        Assert.NotNull(exception.Message);
        Assert.Equal(expectedMessage, exception.Message);
    }

    [Theory]
    [InlineData(null, "Value cannot be null. (Parameter 'parameterName')")]
    [InlineData("Value is null", "Value is null (Parameter 'parameterName')")]
    public void ErrorMessageMatchesExpectedWhenInputIsNull(string customMessage, string expectedMessage)
    {
        string? nullString = null;
        var exception = Assert.Throws<GuardNullException>(() => Guard.Against.NullOrWhiteSpace(nullString, "parameterName", customMessage));
        Assert.NotNull(exception);
        Assert.NotNull(exception.Message);
        Assert.Equal(expectedMessage, exception.Message);
    }

    [Theory]
    [InlineData(null, "Value cannot be null. (Parameter 'xyz')")]
    [InlineData("Value is null", "Value is null (Parameter 'xyz')")]
    public void ErrorMessageMatchesExpectedWhenNameNotExplicitlyProvided(string customMessage, string expectedMessage)
    {
        string? xyz = null;
        var exception = Assert.Throws<GuardNullException>(() => Guard.Against.NullOrWhiteSpace(xyz, message: customMessage));
        Assert.NotNull(exception);
        Assert.NotNull(exception.Message);
        Assert.Equal(expectedMessage, exception.Message);
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData(null, "Please provide correct value")]
    [InlineData("SomeParameter", null)]
    [InlineData("SomeOtherParameter", "Value must be correct")]
    public void ExceptionParamNameMatchesExpectedWhenInputIsWhiteSpace(string expectedParamName, string customMessage)
    {
        var exception = Assert.Throws<GuardNullException>(() => Guard.Against.NullOrWhiteSpace(" ", expectedParamName, customMessage));
        Assert.NotNull(exception);
        Assert.Equal(expectedParamName, exception.ParamName);
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData(null, "Please provide correct value")]
    [InlineData("SomeParameter", null)]
    [InlineData("SomeOtherParameter", "Value must be correct")]
    public void ExceptionParamNameMatchesExpectedWhenInputIsNull(string expectedParamName, string customMessage)
    {
        string? nullString = null;
        var exception = Assert.Throws<GuardNullException>(() => Guard.Against.NullOrWhiteSpace(nullString, expectedParamName, customMessage));
        Assert.NotNull(exception);
        Assert.Equal(expectedParamName, exception.ParamName);
    }
}
