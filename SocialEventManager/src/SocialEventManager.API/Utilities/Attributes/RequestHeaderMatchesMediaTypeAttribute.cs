using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using SocialEventManager.Shared.Helpers;

namespace SocialEventManager.API.Utilities.Attributes;

[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = true)]
public class RequestHeaderMatchesMediaTypeAttribute : Attribute, IActionConstraint
{
    private readonly MediaTypeCollection _mediaTypes = new();
    private readonly string _requestHeaderToMatch;

    public RequestHeaderMatchesMediaTypeAttribute(string requestHeaderToMatch, string mediaType, params string[] otherMediaTypes)
    {
        ArgumentNullExceptionHelpers.ThrowIfNull((requestHeaderToMatch, nameof(requestHeaderToMatch)), (mediaType, nameof(mediaType)));

        _requestHeaderToMatch = requestHeaderToMatch;

        // Check if the inputted media types are valid media types and add them to the _mediaTypes collection.
        if (MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue? parsedMediaType))
        {
            _mediaTypes.Add(parsedMediaType);
        }
        else
        {
            throw new ArgumentException(nameof(mediaType));
        }

        foreach (string otherMediaType in otherMediaTypes)
        {
            if (MediaTypeHeaderValue.TryParse(otherMediaType, out MediaTypeHeaderValue? parsedOtherMediaType))
            {
                _mediaTypes.Add(parsedOtherMediaType);
            }
            else
            {
                throw new ArgumentException(nameof(otherMediaTypes));
            }
        }
    }

    public int Order
    {
        get
        {
            return 0;
        }
    }

    public bool Accept(ActionConstraintContext context)
    {
        IHeaderDictionary requestHeaders = context.RouteContext.HttpContext.Request.Headers;
        if (!requestHeaders.ContainsKey(_requestHeaderToMatch))
        {
            return false;
        }

        MediaType parsedRequestMediaType = new(requestHeaders[_requestHeaderToMatch]);

        // If one of the media types matches, return true.
        foreach (string mediaType in _mediaTypes)
        {
            MediaType parsedMediaType = new(mediaType);
            if (parsedRequestMediaType.Equals(parsedMediaType))
            {
                return true;
            }
        }

        return false;
    }
}
