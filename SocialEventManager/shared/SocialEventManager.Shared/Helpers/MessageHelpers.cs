using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using SocialEventManager.Infrastructure.Middleware;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Shared.Helpers;

public static class MessageHelpers
{
    public static string? BuildRequestMessage(HttpRequest request)
    {
        if (request is null)
        {
            return null;
        }

        StringBuilder builder = new(GlobalConstants.Size);

        builder
            .Append("--- REQUEST ")
            .Append(request.HttpContext.TraceIdentifier)
            .AppendLine(": BEGIN ---")
            .Append(request.Method)
            .Append(' ')
            .Append(request.Path)
            .Append(request.QueryString.ToUriComponent())
            .Append(' ')
            .AppendLine(request.Protocol);

        if (request.Headers.Count > 0)
        {
            foreach (KeyValuePair<string, StringValues> header in request.Headers)
            {
                builder
                    .Append(header.Key)
                    .Append(": ")
                    .AppendLine(header.Value);
            }
        }

        builder
            .Append("--- REQUEST ")
            .Append(request.HttpContext.TraceIdentifier)
            .AppendLine(": END ---");

        return builder.ToString();
    }

    public static string? BuildResponseMessage(ApiError error)
    {
        if (error is null)
        {
            return null;
        }

        StringBuilder builder = new(GlobalConstants.Size);

        builder
            .Append("--- RESPONSE ")
            .Append(error.Id)
            .AppendLine(": BEGIN ---")
            .Append("Status: ")
            .AppendLine(error.Status.ToString())
            .Append("Title: ")
            .AppendLine(error.Title)
            .Append("Detail: ")
            .AppendLine(error.Data?.Detail)
            .Append("--- REQUEST ")
            .Append(error.Id)
            .AppendLine(": END ---");

        return builder.ToString();
    }
}
