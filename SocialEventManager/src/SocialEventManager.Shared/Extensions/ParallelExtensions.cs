using System.Collections.Concurrent;

namespace SocialEventManager.Shared.Extensions;

public static class ParallelExtensions
{
    // A very efficient way of running parallel foreach asynchronous tasks.
    // For more info: https://medium.com/@alex.puiu/parallel-foreach-async-in-c-36756f8ebe62.
    public static Task ParallelForEachAsync<T>(this IEnumerable<T> source, int maxDegreeOfParallelism, Func<T, Task> body)
    {
        async Task AwaitPartition(IEnumerator<T> partition)
        {
            using (partition)
            {
                while (partition.MoveNext())
                {
                    await body(partition.Current);
                }
            }
        }

        return Task.WhenAll(
            Partitioner
                .Create(source)
                .GetPartitions(maxDegreeOfParallelism)
                .AsParallel()
                .Select(p => AwaitPartition(p)));
    }
}
