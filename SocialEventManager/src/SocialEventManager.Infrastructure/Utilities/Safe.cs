using Serilog;
using SocialEventManager.Shared.Common.Constants;

namespace SocialEventManager.Infrastructure.Utilities;

/// <summary>
/// An infrastructure to handle exceptions.
/// Wraps the transaction in a try-catch block and saves the exception details to the log.
/// Use it when you wish to continue the process if an exception occurs.
/// </summary>
public static class Safe
{
    public static TResult? Execute<T1, TResult>(Func<T1, TResult> func, T1 t1) =>
        Execute<T1, EmptyT, EmptyT, EmptyT, TResult>(func1: func, t1: t1);

    public static TResult? Execute<T1, T2, TResult>(Func<T1, T2, TResult> func, T1 t1, T2 t2) =>
        Execute<T1, T2, EmptyT, EmptyT, TResult>(func2: func, t1: t1, t2: t2);

    public static TResult? Execute<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> func, T1 t1, T2 t2, T3 t3) =>
        Execute<T1, T2, T3, EmptyT, TResult>(func3: func, t1: t1, t2: t2, t3: t3);

    public static TResult? Execute<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> func, T1 t1, T2 t2, T3 t3, T4 t4) =>
        Execute<T1, T2, T3, T4, TResult>(func4: func, t1: t1, t2: t2, t3: t3, t4: t4);

    public static TResult ExecuteWithResultFallback<T1, TResult>(Func<T1, TResult> func, T1 t1, TResult result) =>
        Execute<T1, EmptyT, EmptyT, EmptyT, TResult>(func1: func, t1: t1, result: result)!;

    public static TResult ExecuteWithResultFallback<T1, T2, TResult>(Func<T1, T2, TResult> func, T1 t1, T2 t2, TResult result) =>
        Execute<T1, T2, EmptyT, EmptyT, TResult>(func2: func, t1: t1, t2: t2, result: result)!;

    public static TResult ExecuteWithResultFallback<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> func, T1 t1, T2 t2, T3 t3, TResult result) =>
        Execute<T1, T2, T3, EmptyT, TResult>(func3: func, t1: t1, t2: t2, t3: t3, result: result)!;

    public static TResult ExecuteWithResultFallback<T1, T2, T3, T4, TResult>(Func<T1, T2, T3, T4, TResult> func, T1 t1, T2 t2, T3 t3, T4 t4, TResult result) =>
        Execute<T1, T2, T3, T4, TResult>(func4: func, t1: t1, t2: t2, t3: t3, t4: t4, result: result)!;

    private static TResult? Execute<T1, T2, T3, T4, TResult>(
        Func<T1, TResult>? func1 = default,
        Func<T1, T2, TResult>? func2 = default,
        Func<T1, T2, T3, TResult>? func3 = default,
        Func<T1, T2, T3, T4, TResult>? func4 = default,
        T1 t1 = default!,
        T2 t2 = default!,
        T3 t3 = default!,
        T4 t4 = default!,
        TResult? result = default)
    {
        try
        {
            if (func1 != default)
            {
                return func1(t1);
            }

            if (func2 != default)
            {
                return func2(t1, t2);
            }

            if (func3 != default)
            {
                return func3(t1, t2, t3);
            }

            if (func4 != default)
            {
                return func4(t1, t2, t3, t4);
            }

            return default;
        }
        catch (Exception ex)
        {
            string errorDetails = ex switch
            {
                _ when ex is ArgumentNullException
                    or ArgumentException
                    or NullReferenceException
                    or IndexOutOfRangeException
                    or InvalidOperationException
                    or OutOfMemoryException
                    or FormatException => ex.ToString(),
                _ => ExceptionConstants.UnexpectedException(ex),
            };

            Log.Error(errorDetails);
            return result;
        }
    }

    // A record that is used as a generic placeholder and doesn't have a real usage.
    private record EmptyT();
}
