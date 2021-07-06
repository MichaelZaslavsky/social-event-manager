using ServiceStack.DataAnnotations;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.DAL.Entities
{
    using Dapper.Contrib.Extensions;

    public abstract class ClaimBase
    {
        [Computed]
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        [Required]
        [StringLength(LengthConstants.Length255)]
        public string Type { get; set; }

        [Required]
        [StringLength(StringLengthAttribute.MaxText)]
        public string Value { get; set; }
    }
}
