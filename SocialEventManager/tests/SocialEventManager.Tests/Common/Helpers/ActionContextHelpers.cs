using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Moq;

namespace SocialEventManager.Tests.Common.Helpers;

internal static class ActionContextHelpers
{
    public static ActionExecutedContext GetMockActionExecutedContext()
    {
        ActionContext actionContext = new(new DefaultHttpContext(), new(), Mock.Of<ActionDescriptor>());
        return new(actionContext, new List<IFilterMetadata>(), new());
    }

    public static ActionExecutingContext GetMockActionExecutingContext()
    {
        ActionContext actionContext = new(new DefaultHttpContext(), new(), Mock.Of<ActionDescriptor>());
        return new(actionContext, new List<IFilterMetadata>(), new Dictionary<string, object?>(), new());
    }
}
