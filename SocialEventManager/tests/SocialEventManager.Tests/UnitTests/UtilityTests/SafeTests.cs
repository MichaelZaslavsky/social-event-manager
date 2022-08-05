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
    #region Synchronous

    [Theory]
    [AutoData]
    public void Execute_ReturnsValue_WhenFunctionIsValidWithOneArgument(string argument1)
    {
        string? actual = Safe.Execute(() => Func(argument1));
        actual.Should().Be(argument1);
    }

    [Theory]
    [AutoData]
    public void Execute_ReturnsValue_WhenFunctionIsValidWithTwoArguments(string argument1, string argument2)
    {
        string? actual = Safe.Execute(() => Func(argument1, argument2));
        actual.Should().Be(argument1 + argument2);
    }

    [Theory]
    [AutoData]
    public void Execute_ReturnsValue_WhenFunctionIsValidWithThreeArguments(string argument1, string argument2, string argument3)
    {
        string? actual = Safe.Execute(() => Func(argument1, argument2, argument3));
        actual.Should().Be(argument1 + argument2 + argument3);
    }

    [Theory]
    [AutoData]
    public void Execute_ReturnsValue_WhenFunctionIsValidWithFourArguments(string argument1, string argument2, string argument3, string argument4)
    {
        string? actual = Safe.Execute(() => Func(argument1, argument2, argument3, argument4));
        actual.Should().Be(argument1 + argument2 + argument3 + argument4);
    }

    [Theory]
    [AutoData]
    public void Execute_ReturnsDefaultValue_WhenFunctionThrowsArgumentNullExceptionWithOneArgument(string argument1)
    {
        string? actual = Safe.Execute(() => FuncThrowsArgumentNullException(argument1));
        actual.Should().Be(default);
    }

    [Theory]
    [AutoData]
    public void Execute_ReturnsResultFallback_WhenFunctionThrowsArgumentNullExceptionWithTwoArguments(string argument1, string argument2)
    {
        string? actual = Safe.Execute(() => FuncThrowsArgumentNullException(argument1, argument2));
        actual.Should().Be(default);
    }

    [Theory]
    [AutoData]
    public void Execute_ReturnsResultFallback_WhenFunctionThrowsArgumentNullExceptionWithThreeArguments(string argument1, string argument2, string argument3)
    {
        string? actual = Safe.Execute(() => FuncThrowsArgumentNullException(argument1, argument2, argument3));
        actual.Should().Be(default);
    }

    [Theory]
    [AutoData]
    public void Execute_ReturnsResultFallback_WhenFunctionThrowsArgumentNullExceptionWithFourArguments(
        string argument1, string argument2, string argument3, string argument4)
    {
        string? actual = Safe.Execute(() => FuncThrowsArgumentNullException(argument1, argument2, argument3, argument4));
        actual.Should().Be(default);
    }

    [Theory]
    [AutoData]
    public void ExecuteWithResultFallback_ReturnsValue_WhenFunctionIsValidWithOneArgument(string argument1, string result)
    {
        string? actual = Safe.ExecuteWithResultFallback(() => Func(argument1), result);
        actual.Should().Be(argument1);
    }

    [Theory]
    [AutoData]
    public void ExecuteWithResultFallback_ReturnsValue_WhenFunctionIsValidWithTwoArguments(string argument1, string argument2, string result)
    {
        string? actual = Safe.ExecuteWithResultFallback(() => Func(argument1, argument2), result);
        actual.Should().Be(argument1 + argument2);
    }

    [Theory]
    [AutoData]
    public void ExecuteWithResultFallback_ReturnsValue_WhenFunctionIsValidWithThreeArguments(string argument1, string argument2, string argument3, string result)
    {
        string? actual = Safe.ExecuteWithResultFallback(() => Func(argument1, argument2, argument3), result);
        actual.Should().Be(argument1 + argument2 + argument3);
    }

    [Theory]
    [AutoData]
    public void ExecuteWithResultFallback_ReturnsValue_WhenFunctionIsValidWithFourArguments(string argument1, string argument2, string argument3, string argument4, string result)
    {
        string? actual = Safe.ExecuteWithResultFallback(() => Func(argument1, argument2, argument3, argument4), result);
        actual.Should().Be(argument1 + argument2 + argument3 + argument4);
    }

    [Theory]
    [AutoData]
    public void ExecuteWithResultFallback_ReturnsResultFallback_WhenFunctionThrowsArgumentNullExceptionWithOneArgument(string argument1, string result)
    {
        string? actual = Safe.ExecuteWithResultFallback(() => FuncThrowsArgumentNullException(argument1), result);
        actual.Should().Be(result);
    }

    [Theory]
    [AutoData]
    public void ExecuteWithResultFallback_ReturnsResultFallback_WhenFunctionThrowsArgumentNullExceptionWithTwoArguments(
        string argument1, string argument2, string result)
    {
        string? actual = Safe.ExecuteWithResultFallback(() => FuncThrowsArgumentNullException(argument1, argument2), result);
        actual.Should().Be(result);
    }

    [Theory]
    [AutoData]
    public void ExecuteWithResultFallback_ReturnsResultFallback_WhenFunctionThrowsArgumentNullExceptionWithThreeArguments(
        string argument1, string argument2, string argument3, string result)
    {
        string? actual = Safe.ExecuteWithResultFallback(() => FuncThrowsArgumentNullException(argument1, argument2, argument3), result);
        actual.Should().Be(result);
    }

    [Theory]
    [AutoData]
    public void ExecuteWithResultFallback_ReturnsResultFallback_WhenFunctionThrowsArgumentNullExceptionWithFourArguments(
        string argument1, string argument2, string argument3, string argument4, string result)
    {
        string? actual = Safe.ExecuteWithResultFallback(() => FuncThrowsArgumentNullException(argument1, argument2, argument3, argument4), result);
        actual.Should().Be(result);
    }

    #endregion Synchronous

    #region Asynchronous

    [Theory]
    [AutoData]
    public async Task ExecuteAsync_ReturnsValue_WhenFunctionIsValidWithOneArgument(string argument1)
    {
        string? actual = await Safe.ExecuteAsync(() => FuncAsync(argument1));
        actual.Should().Be(argument1);
    }

    [Theory]
    [AutoData]
    public async Task ExecuteAsync_ReturnsValue_WhenFunctionIsValidWithTwoArguments(string argument1, string argument2)
    {
        string? actual = await Safe.ExecuteAsync(() => FuncAsync(argument1, argument2));
        actual.Should().Be(argument1 + argument2);
    }

    [Theory]
    [AutoData]
    public async Task ExecuteAsync_ReturnsValue_WhenFunctionIsValidWithThreeArguments(string argument1, string argument2, string argument3)
    {
        string? actual = await Safe.ExecuteAsync(() => FuncAsync(argument1, argument2, argument3));
        actual.Should().Be(argument1 + argument2 + argument3);
    }

    [Theory]
    [AutoData]
    public async Task ExecuteAsync_ReturnsValue_WhenFunctionIsValidWithFourArguments(
        string argument1, string argument2, string argument3, string argument4)
    {
        string? actual = await Safe.ExecuteAsync(() => FuncAsync(argument1, argument2, argument3, argument4));
        actual.Should().Be(argument1 + argument2 + argument3 + argument4);
    }

    [Theory]
    [AutoData]
    public async Task ExecuteAsync_ReturnsDefaultValue_WhenFunctionThrowsArgumentNullExceptionWithOneArgument(string argument1)
    {
        string? actual = await Safe.ExecuteAsync(() => FuncThrowsArgumentNullExceptionAsync(argument1));
        actual.Should().Be(default);
    }

    [Theory]
    [AutoData]
    public async Task ExecuteAsync_ReturnsResultFallback_WhenFunctionThrowsArgumentNullExceptionWithTwoArguments(
        string argument1, string argument2)
    {
        string? actual = await Safe.ExecuteAsync(() => FuncThrowsArgumentNullExceptionAsync(argument1, argument2));
        actual.Should().Be(default);
    }

    [Theory]
    [AutoData]
    public async Task ExecuteAsync_ReturnsResultFallback_WhenFunctionThrowsArgumentNullExceptionWithThreeArguments(
        string argument1, string argument2, string argument3)
    {
        string? actual = await Safe.ExecuteAsync(() => FuncThrowsArgumentNullExceptionAsync(argument1, argument2, argument3));
        actual.Should().Be(default);
    }

    [Theory]
    [AutoData]
    public async Task ExecuteAsync_ReturnsResultFallback_WhenFunctionThrowsArgumentNullExceptionWithFourArguments(
        string argument1, string argument2, string argument3, string argument4)
    {
        string? actual = await Safe.ExecuteAsync(() => FuncThrowsArgumentNullExceptionAsync(argument1, argument2, argument3, argument4));
        actual.Should().Be(default);
    }

    [Theory]
    [AutoData]
    public async Task ExecuteWithResultFallbackAsync_ReturnsValue_WhenFunctionIsValidWithOneArgument(string argument1, string result)
    {
        string? actual = await Safe.ExecuteWithResultFallbackAsync(() => FuncAsync(argument1), result);
        actual.Should().Be(argument1);
    }

    [Theory]
    [AutoData]
    public async Task ExecuteWithResultFallbackAsync_ReturnsValue_WhenFunctionIsValidWithTwoArguments(
        string argument1, string argument2, string result)
    {
        string? actual = await Safe.ExecuteWithResultFallbackAsync(() => FuncAsync(argument1, argument2), result);
        actual.Should().Be(argument1 + argument2);
    }

    [Theory]
    [AutoData]
    public async Task ExecuteWithResultFallbackAsync_ReturnsValue_WhenFunctionIsValidWithThreeArguments(
        string argument1, string argument2, string argument3, string result)
    {
        string? actual = await Safe.ExecuteWithResultFallbackAsync(() => FuncAsync(argument1, argument2, argument3), result);
        actual.Should().Be(argument1 + argument2 + argument3);
    }

    [Theory]
    [AutoData]
    public async Task ExecuteWithResultFallbackAsync_ReturnsValue_WhenFunctionIsValidWithFourArguments(
        string argument1, string argument2, string argument3, string argument4, string result)
    {
        string? actual = await Safe.ExecuteWithResultFallbackAsync(() => FuncAsync(argument1, argument2, argument3, argument4), result);
        actual.Should().Be(argument1 + argument2 + argument3 + argument4);
    }

    [Theory]
    [AutoData]
    public async Task ExecuteWithResultFallbackAsync_ReturnsResultFallback_WhenFunctionThrowsArgumentNullExceptionWithOneArgument(
        string argument1, string result)
    {
        string? actual = await Safe.ExecuteWithResultFallbackAsync(() => FuncThrowsArgumentNullExceptionAsync(argument1), result);
        actual.Should().Be(result);
    }

    [Theory]
    [AutoData]
    public async Task ExecuteWithResultFallbackAsync_ReturnsResultFallback_WhenFunctionThrowsArgumentNullExceptionWithTwoArguments(
    string argument1, string argument2, string result)
    {
        string? actual = await Safe.ExecuteWithResultFallbackAsync(() => FuncThrowsArgumentNullExceptionAsync(argument1, argument2), result);
        actual.Should().Be(result);
    }

    [Theory]
    [AutoData]
    public async Task ExecuteWithResultFallbackAsync_ReturnsResultFallback_WhenFunctionThrowsArgumentNullExceptionWithThreeArguments(
        string argument1, string argument2, string argument3, string result)
    {
        string? actual = await Safe.ExecuteWithResultFallbackAsync(() => FuncThrowsArgumentNullExceptionAsync(argument1, argument2, argument3), result);
        actual.Should().Be(result);
    }

    [Theory]
    [AutoData]
    public async Task ExecuteWithResultFallbackAsync_ReturnsResultFallback_WhenFunctionThrowsArgumentNullExceptionWithFourArguments(
        string argument1, string argument2, string argument3, string argument4, string result)
    {
        string? actual = await Safe.ExecuteWithResultFallbackAsync(() => FuncThrowsArgumentNullExceptionAsync(argument1, argument2, argument3, argument4), result);
        actual.Should().Be(result);
    }

    #endregion Asynchronous

    private static string Func(string argument1) => Func(argument1, null!);

    private static string Func(string argument1, string argument2) => Func(argument1, argument2, null!);

    private static string Func(string argument1, string argument2, string argument3) => Func(argument1, argument2, argument3, null!);

    private static string Func(string argument1, string argument2, string argument3, string argument4) => argument1 + argument2 + argument3 + argument4;

    private static string FuncThrowsArgumentNullException(string argument1) => FuncThrowsArgumentNullException(argument1, null!);

    private static string FuncThrowsArgumentNullException(string argument1, string argument2) => FuncThrowsArgumentNullException(argument1, argument2, null!);

    private static string FuncThrowsArgumentNullException(string argument1, string argument2, string argument3) =>
        FuncThrowsArgumentNullException(argument1, argument2, argument3, null!);

    private static string FuncThrowsArgumentNullException(string argument1, string argument2, string argument3, string argument4)
    {
        if (argument1 is null)
        {
            throw new ArgumentNullException(nameof(argument1));
        }

        if (argument2 is null)
        {
            throw new ArgumentNullException(nameof(argument2));
        }

        if (argument3 is null)
        {
            throw new ArgumentNullException(nameof(argument3));
        }

        if (argument4 is null)
        {
            throw new ArgumentNullException(nameof(argument4));
        }

        throw new ArgumentNullException(nameof(argument1));
    }

    private static async Task<string> FuncAsync(string argument1) =>
        await FuncAsync(argument1, null!);

    private static async Task<string> FuncAsync(string argument1, string argument2) =>
        await FuncAsync(argument1, argument2, null!);

    private static async Task<string> FuncAsync(string argument1, string argument2, string argument3) =>
        await FuncAsync(argument1, argument2, argument3, null!);

    private static async Task<string> FuncAsync(string argument1, string argument2, string argument3, string argument4) =>
        await Task.FromResult(argument1 + argument2 + argument3 + argument4);

    private static async Task<string> FuncThrowsArgumentNullExceptionAsync(string argument1) =>
        await FuncThrowsArgumentNullExceptionAsync(argument1, null!);

    private static async Task<string> FuncThrowsArgumentNullExceptionAsync(string argument1, string argument2) =>
        await FuncThrowsArgumentNullExceptionAsync(argument1, argument2, null!);

    private static async Task<string> FuncThrowsArgumentNullExceptionAsync(string argument1, string argument2, string argument3) =>
        await FuncThrowsArgumentNullExceptionAsync(argument1, argument2, argument3, null!);

    private static async Task<string> FuncThrowsArgumentNullExceptionAsync(string argument1, string argument2, string argument3, string argument4)
    {
        if (argument1 is null)
        {
            return await Task.FromException<string>(new ArgumentNullException(nameof(argument1)));
        }

        if (argument2 is null)
        {
            return await Task.FromException<string>(new ArgumentNullException(nameof(argument2)));
        }

        if (argument3 is null)
        {
            return await Task.FromException<string>(new ArgumentNullException(nameof(argument3)));
        }

        if (argument4 is null)
        {
            return await Task.FromException<string>(new ArgumentNullException(nameof(argument4)));
        }

        return await Task.FromException<string>(new ArgumentNullException(nameof(argument1)));
    }
}
