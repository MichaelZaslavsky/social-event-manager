using System.Diagnostics;
using System.Linq.Expressions;
using Serilog;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Infrastructure.Utilities;

/// <summary>
/// An infrastructure to handle exceptions.
/// Wraps the transaction in a try-catch block and saves the exception details to the log.
/// Supports both Synchronous and Asynchronous flows.
/// Use it when you wish to continue the process if an exception occurs.
/// </summary>
public static class Safe
{
    public static TResult? Execute<TResult>(Expression<Func<TResult>> func) => ExecuteFunc(func);

    public static TResult ExecuteWithResultFallback<TResult>(Expression<Func<TResult>> func, TResult result) => ExecuteFunc(func, result)!;

    public static async Task<TResult?> ExecuteAsync<TResult>(Expression<Func<Task<TResult>>> func) => await ExecuteFuncAsync(func);

    public static async Task<TResult> ExecuteWithResultFallbackAsync<TResult>(Expression<Func<Task<TResult>>> func, TResult result) =>
        (await ExecuteFuncAsync(func, result: result))!;

    private static TResult? ExecuteFunc<TResult>(Expression<Func<TResult>> func, TResult? result = default)
    {
        try
        {
            return func.Compile()();
        }
        catch (Exception ex)
        {
            LogError(ex.Demystify());
            return result;
        }
    }

    private static async Task<TResult?> ExecuteFuncAsync<TResult>(Expression<Func<Task<TResult>>> func, TResult? result = default)
    {
        try
        {
            return await func.Compile()();
        }
        catch (Exception ex)
        {
            LogError(ex.Demystify());
            return result;
        }
    }

    private static void LogError(Exception ex)
    {
        string errorDetails = ex is ArgumentNullException
            or ArgumentException
            or NullReferenceException
            or IndexOutOfRangeException
            or InvalidOperationException
            or OutOfMemoryException
            or FormatException
            ? ex.ToString()
            : ExceptionConstants.UnexpectedException(ex);

        Log.Error(errorDetails, ex.StackTrace);
    }
}
