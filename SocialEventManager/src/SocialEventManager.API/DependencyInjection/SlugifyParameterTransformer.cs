using System.Text.RegularExpressions;

namespace SocialEventManager.API.DependencyInjection;

public sealed partial class SlugifyParameterTransformer : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value)
    {
        return value is null
            ? null
            : DashSeparatedRegex().Replace(value.ToString()!, "$1-$2").ToLower();
    }

    [GeneratedRegex("([a-z])([A-Z])")]
    private static partial Regex DashSeparatedRegex();
}
