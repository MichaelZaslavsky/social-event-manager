using System.ComponentModel.DataAnnotations;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Extensions;

namespace SocialEventManager.Shared.Models;

public record EmailDto
{
    public EmailDto(string subject, string? body, IEnumerable<string> to)
        : this(subject, body, to, Enumerable.Empty<string>(), Enumerable.Empty<string>())
    {
    }

    public EmailDto(string subject, string? body, IEnumerable<string> to, IEnumerable<string> bcc)
        : this(subject, body, to, Enumerable.Empty<string>(), bcc)
    {
    }

    public EmailDto(string subject, string? body, IEnumerable<string> to, IEnumerable<string> cc, IEnumerable<string> bcc)
    {
        if (to.IsNullOrEmpty() && cc.IsNullOrEmpty() && bcc.IsNullOrEmpty())
        {
            throw new InvalidOperationException(ExceptionConstants.NoRecipientsHaveBeenSpecified);
        }

        Subject = subject;
        Body = body;
        To = to ?? Enumerable.Empty<string>();
        Cc = cc ?? Enumerable.Empty<string>();
        Bcc = bcc ?? Enumerable.Empty<string>();
    }

    [Required]
    public string Subject { get; init; }

    public string? Body { get; init; }

    public IEnumerable<string> To { get; init; }

    public IEnumerable<string> Cc { get; init; }

    public IEnumerable<string> Bcc { get; init; }
}
