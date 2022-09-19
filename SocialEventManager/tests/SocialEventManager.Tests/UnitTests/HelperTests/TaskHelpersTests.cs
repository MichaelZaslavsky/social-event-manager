using AutoFixture.Xunit2;
using FluentAssertions;
using FluentAssertions.Specialized;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Helpers;
using Xunit;
using Xunit.Categories;

namespace SocialEventManager.Tests.UnitTests.HelperTests;

[UnitTest]
[Category(CategoryConstants.Helpers)]
public sealed class TaskHelpersTests
{
    [Fact]
    public async Task WhenAll_Should_ReturnTrue_When_SomeTasksRunInParallelAndNoExceptionsThrown()
    {
        Task<bool> firstTask = SomeTaskWithoutException();
        Task<bool> secondTask = SomeTaskWithoutException();

        await TaskHelpers.WhenAll(firstTask, secondTask);

        firstTask.Result.Should().BeTrue();
        secondTask.Result.Should().BeTrue();
    }

    [Theory]
    [AutoData]
    public async Task WhenAll_Should_ThrowException_When_SomeTasksRunInParallelAndOneTaskHaveAnException(string message)
    {
        Task<bool> firstTask = SomeTaskWithException(message);
        Task<bool> secondTask = SomeTaskWithoutException();

        Func<Task> func = async () => await TaskHelpers.WhenAll(firstTask, secondTask);
        ExceptionAssertions<Exception> exception = await func.Should().ThrowAsync<Exception>();

        exception.Subject.Should().HaveCount(1);
        exception.Subject.Single().Message.Should().Be(message);
    }

    [Theory]
    [AutoData]
    public async Task WhenAll_Should_ThrowExceptions_When_SomeTasksRunInParallelAndSomeOfThemHaveExceptions(
        string firstMessage, string secondMessage)
    {
        Task<bool> firstTask = SomeTaskWithException(firstMessage);
        Task<bool> secondTask = SomeTaskWithException(secondMessage);

        Func<Task> func = async () => await TaskHelpers.WhenAll(firstTask, secondTask);
        ExceptionAssertions<Exception> exception = await func.Should().ThrowAsync<Exception>();

        exception.Subject.Should().HaveCount(2);
        exception.Subject.First().Message.Should().Be(firstMessage);
        exception.Subject.Last().Message.Should().Be(secondMessage);
    }

    private static async Task<bool> SomeTaskWithoutException()
    {
        await Task.Delay(1);
        return true;
    }

    private static async Task<bool> SomeTaskWithException(string message)
    {
        await Task.Delay(1);
        throw new Exception(message);
    }
}
