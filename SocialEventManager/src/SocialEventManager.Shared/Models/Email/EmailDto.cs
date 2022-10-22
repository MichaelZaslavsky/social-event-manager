using System.ComponentModel.DataAnnotations;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Extensions;

namespace SocialEventManager.Shared.Models.Email;

public sealed record EmailDto
{
    public EmailDto()
    {
    }

    public EmailDto(string? body, IEnumerable<string> to)
        : this(body, to, Enumerable.Empty<string>(), Enumerable.Empty<string>())
    {
    }

    public EmailDto(string? body, IEnumerable<string> to, IEnumerable<string> bcc)
        : this(body, to, Enumerable.Empty<string>(), bcc)
    {
    }

    public EmailDto(string? body, IEnumerable<string> to, IEnumerable<string> cc, IEnumerable<string> bcc)
    {
        if (to.IsNullOrEmpty() && cc.IsNullOrEmpty() && bcc.IsNullOrEmpty())
        {
            throw new InvalidOperationException(ExceptionConstants.NoRecipientsHaveBeenSpecified);
        }

        Body = body;
        To = to;
        Cc = cc;
        Bcc = bcc;
    }

    [Required]
    required public string Subject { get; init; }

    public string? Body { get; init; }

    public IEnumerable<string> To { get; init; } = Enumerable.Empty<string>();

    public IEnumerable<string> Cc { get; init; } = Enumerable.Empty<string>();

    public IEnumerable<string> Bcc { get; init; } = Enumerable.Empty<string>();
}
