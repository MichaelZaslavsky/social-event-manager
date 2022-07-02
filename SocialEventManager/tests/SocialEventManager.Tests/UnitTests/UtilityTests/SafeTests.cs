using AutoFixture.Xunit2;
using FluentAssertions;
using SocialEventManager.Infrastructure.Utilities;
using SocialEventManager.Shared.Constants;
using Xunit;
using Xunit.Categories;

namespace SocialEventManager.Tests.UnitTests.UtilityTests;

[UnitTest]
[Category(CategoryConstants.Utilities)]
public class SafeTests
{
    [Theory]
    [InlineAutoData]
    public void Execute_Should_ReturnValue_When_FunctionIsValidWithOneArgument(string argument1)
    {
        string? actual = Safe.Execute(Func, argument1);
        actual.Should().Be(argument1);
    }

    [Theory]
    [InlineAutoData]
    public void Execute_Should_Return_Value_When_FunctionIsValidWithTwoArguments(string argument1, string argument2)
    {
        string? actual = Safe.Execute(Func, argument1, argument2);
        actual.Should().Be(argument1 + argument2);
    }

    [Theory]
    [InlineAutoData]
    public void Execute_Should_ReturnValue_When_FunctionIsValidWithThreeArguments(string argument1, string argument2, string argument3)
    {
        string? actual = Safe.Execute(Func, argument1, argument2, argument3);
        actual.Should().Be(argument1 + argument2 + argument3);
    }

    [Theory]
    [InlineAutoData]
    public void Execute_Should_ReturnValue_When_FunctionIsValidWithFourArguments(string argument1, string argument2, string argument3, string argument4)
    {
        string? actual = Safe.Execute(Func, argument1, argument2, argument3, argument4);
        actual.Should().Be(argument1 + argument2 + argument3 + argument4);
    }

    [Theory]
    [InlineAutoData]
    public void Execute_Should_ReturnDefaultValue_When_FunctionThrowsArgumentNullExceptionWithOneArgument(string argument1)
    {
        string? actual = Safe.Execute(FuncThrowsArgumentNullException, argument1);
        actual.Should().Be(default);
    }

    [Theory]
    [InlineAutoData]
    public void Execute_Should_ReturnResultFallback_When_FunctionThrowsArgumentNullExceptionWithTwoArguments(string argument1, string argument2)
    {
        string? actual = Safe.Execute(FuncThrowsArgumentNullException, argument1, argument2);
        actual.Should().Be(default);
    }

    [Theory]
    [InlineAutoData]
    public void Execute_Should_ReturnResultFallback_When_FunctionThrowsArgumentNullExceptionWithThreeArguments(string argument1, string argument2, string argument3)
    {
        string? actual = Safe.Execute(FuncThrowsArgumentNullException, argument1, argument2, argument3);
        actual.Should().Be(default);
    }

    [Theory]
    [InlineAutoData]
    public void Execute_Should_ReturnResultFallback_When_FunctionThrowsArgumentNullExceptionWithFourArguments(
        string argument1, string argument2, string argument3, string argument4)
    {
        string? actual = Safe.Execute(FuncThrowsArgumentNullException, argument1, argument2, argument3, argument4);
        actual.Should().Be(default);
    }

    [Theory]
    [InlineAutoData]
    public void ExecuteWithResultFallback_Should_ReturnValue_When_FunctionIsValidWithOneArgument(string argument1, string result)
    {
        string? actual = Safe.ExecuteWithResultFallback(Func, argument1, result);
        actual.Should().Be(argument1);
    }

    [Theory]
    [InlineAutoData]
    public void ExecuteWithResultFallback_Should_ReturnValue_When_FunctionIsValidWithTwoArguments(string argument1, string argument2, string result)
    {
        string? actual = Safe.ExecuteWithResultFallback(Func, argument1, argument2, result);
        actual.Should().Be(argument1 + argument2);
    }

    [Theory]
    [InlineAutoData]
    public void ExecuteWithResultFallback_Should_ReturnValue_When_FunctionIsValidWithThreeArguments(string argument1, string argument2, string argument3, string result)
    {
        string? actual = Safe.ExecuteWithResultFallback(Func, argument1, argument2, argument3, result);
        actual.Should().Be(argument1 + argument2 + argument3);
    }

    [Theory]
    [InlineAutoData]
    public void ExecuteWithResultFallback_Should_ReturnValue_When_FunctionIsValidWithFourArguments(string argument1, string argument2, string argument3, string argument4, string result)
    {
        string? actual = Safe.ExecuteWithResultFallback(Func, argument1, argument2, argument3, argument4, result);
        actual.Should().Be(argument1 + argument2 + argument3 + argument4);
    }

    [Theory]
    [InlineAutoData]
    public void ExecuteWithResultFallback_Should_ReturnResultFallback_When_FunctionThrowsArgumentNullExceptionWithOneArgument(string argument1, string result)
    {
        string? actual = Safe.ExecuteWithResultFallback(FuncThrowsArgumentNullException, argument1, result);
        actual.Should().Be(result);
    }

    [Theory]
    [InlineAutoData]
    public void ExecuteWithResultFallback_Should_ReturnResultFallback_When_FunctionThrowsArgumentNullExceptionWithTwoArguments(
        string argument1, string argument2, string result)
    {
        string? actual = Safe.ExecuteWithResultFallback(FuncThrowsArgumentNullException, argument1, argument2, result);
        actual.Should().Be(result);
    }

    [Theory]
    [InlineAutoData]
    public void ExecuteWithResultFallback_Should_ReturnResultFallback_When_FunctionThrowsArgumentNullExceptionWithThreeArguments(
        string argument1, string argument2, string argument3, string result)
    {
        string? actual = Safe.ExecuteWithResultFallback(FuncThrowsArgumentNullException, argument1, argument2, argument3, result);
        actual.Should().Be(result);
    }

    [Theory]
    [InlineAutoData]
    public void ExecuteWithResultFallback_Should_ReturnResultFallback_When_FunctionThrowsArgumentNullExceptionWithFourArguments(
        string argument1, string argument2, string argument3, string argument4, string result)
    {
        string? actual = Safe.ExecuteWithResultFallback(FuncThrowsArgumentNullException, argument1, argument2, argument3, argument4, result);
        actual.Should().Be(result);
    }

    private static string Func(string argument1) => Func(argument1, null!);

    private static string Func(string argument1, string argument2) => Func(argument1, argument2, null!);

    private static string Func(string argument1, string argument2, string argument3) => Func(argument1, argument2, argument3, null!);

    private static string Func(string argument1, string argument2, string argument3, string argument4) => argument1 + argument2 + argument3 + argument4;

    private static string FuncThrowsArgumentNullException(string argument1) => FuncThrowsArgumentNullException(argument1, null!);

    private static string FuncThrowsArgumentNullException(string argument1, string argument2) => FuncThrowsArgumentNullException(argument1, argument2, null!);

    private static string FuncThrowsArgumentNullException(string argument1, string argument2, string argument3) =>
        FuncThrowsArgumentNullException(argument1, argument2, argument3, null!);

    private static string FuncThrowsArgumentNullException(string argument1, string argument2, string argument3, string argument4) =>
        throw new ArgumentNullException(nameof(argument1));
}
