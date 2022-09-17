using System.Diagnostics.CodeAnalysis;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Shared.Models.Accounts;

[Obsolete(GlobalConstants.DapperIdentityObsoleteReason)]
[ExcludeFromCodeCoverage]
public record AccountForCreationDto : AccountBase;
