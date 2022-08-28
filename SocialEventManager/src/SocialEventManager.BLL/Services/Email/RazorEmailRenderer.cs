using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using SocialEventManager.Shared.Constants.Validations;

namespace SocialEventManager.BLL.Services.Email;

public class RazorEmailRenderer : IEmailRenderer
{
    private readonly IRazorViewEngine _razorViewEngine;
    private readonly ITempDataProvider _tempDataProvider;
    private readonly IServiceProvider _serviceProvider;

    public RazorEmailRenderer(
        IRazorViewEngine razorViewEngine,
        ITempDataProvider tempDataProvider,
        IServiceProvider serviceProvider)
    {
        _razorViewEngine = razorViewEngine;
        _tempDataProvider = tempDataProvider;
        _serviceProvider = serviceProvider;
    }

    public async Task<string> RenderAsync<T>(T model)
    {
        string viewPath = $"~/EmailTemplates/{typeof(T).Name}.cshtml";
        ViewEngineResult result = _razorViewEngine.GetView(null, viewPath, true);

        if (!result.Success)
        {
            string searchedLocations = string.Join("\n", result.SearchedLocations);
            throw new InvalidOperationException(ValidationConstants.CouldNotFindThisView(viewPath, searchedLocations));
        }

        IView view = result.View;

        DefaultHttpContext httpContext = new() { RequestServices = _serviceProvider };
        ActionContext actionContext = new(httpContext, httpContext.GetRouteData(), new());

        using StringWriter writer = new();
        ViewDataDictionary viewDataDict = new(new EmptyModelMetadataProvider(), new()) { Model = model };
        ViewContext viewContext = new(actionContext, view, viewDataDict, new TempDataDictionary(httpContext, _tempDataProvider), writer, new());

        await view.RenderAsync(viewContext);
        return writer.ToString();
    }
}
