using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Moq;

namespace SocialEventManager.Tests.Common.Helpers;

internal static class ActionContextHelpers
{
    public static ActionExecutedContext GetMockActionExecutedContext() =>
        new(GetMockActionContext(), new List<IFilterMetadata>(), new());

    public static ActionExecutingContext GetMockActionExecutingContext() =>
        new(GetMockActionContext(), new List<IFilterMetadata>(), new Dictionary<string, object?>(), new());

    public static ActionExecutionDelegate GetMockActionExecutionDelegate() =>
        new(() => Task.FromResult(GetMockActionExecutedContext()));

    private static ActionContext GetMockActionContext() => new(new DefaultHttpContext(), new(), Mock.Of<ActionDescriptor>());
}
