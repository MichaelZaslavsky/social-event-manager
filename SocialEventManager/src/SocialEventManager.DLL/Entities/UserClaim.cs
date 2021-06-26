using System;
using Dapper.Contrib.Extensions;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.DAL.Entities
{
    [Table(TableNameConstants.UserClaims)]
    public class UserClaim : ClaimBase
    {
        public Guid UserId { get; set; }
    }
}
