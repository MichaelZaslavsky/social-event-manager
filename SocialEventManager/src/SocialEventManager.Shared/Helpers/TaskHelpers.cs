namespace SocialEventManager.Shared.Helpers;

public static class TaskHelpers
{
    /// <summary>
    /// Creates a task that will complete when all of the task objects in an array have been completed.
    /// Opposed to the System.Threading.Tasks.WhenAll method, it returns all the aggregated exceptions,
    /// and not just the first one.
    /// </summary>
    /// <typeparam name="TResult">The type of the completed task.</typeparam>
    /// <param name="tasks">The tasks to wait on for completion.</param>
    /// <returns>A task that represents the completion of all of the supplied tasks.</returns>
    public static async Task<IEnumerable<TResult>> WhenAll<TResult>(params Task<TResult>[] tasks)
    {
        Task<TResult[]> allTasks = Task.WhenAll(tasks);

        try
        {
            return await allTasks;
        }
        catch
        {
            throw allTasks.Exception!;
        }
    }
}
