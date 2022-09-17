using System.Diagnostics.CodeAnalysis;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.Shared.Models.Roles;

[Obsolete(GlobalConstants.DapperIdentityObsoleteReason)]
[ExcludeFromCodeCoverage]
public record RoleDto : RoleBase;
