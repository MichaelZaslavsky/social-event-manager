using System.Collections.Concurrent;
using FluentAssertions;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Extensions;
using Xunit;
using Xunit.Categories;

namespace SocialEventManager.Tests.UnitTests.ExtensionTests;

[UnitTest]
[Category(CategoryConstants.Extensions)]
public class ParallelExtensionsTests
{
    private int counter = 1;

    [Theory]
    [InlineData(1, 1)]
    [InlineData(50, 1)]
    [InlineData(50, 2)]
    [InlineData(50, 4)]
    public async Task ParallelForEachAsync_Should_ReturnListOfIntegersWithCorrectData_When_FunctionGetsAnInteger(int batch, int maxDegreeOfParallelism)
    {
        ConcurrentBag<int> numbers = new();

        List<Func<Task<int>>> funcs = Enumerable
            .Range(0, batch)
            .Select(_ => new Func<Task<int>>(GetCounter))
            .ToList();

        await funcs.ParallelForEachAsync(maxDegreeOfParallelism, async func => numbers.Add(await func()));

        numbers.Should().HaveCount(batch);
        numbers.Max().Should().BeLessThan(counter);
    }

    private async Task<int> GetCounter()
    {
        await Task.Delay(10);
        return counter++;
    }
}
