using Dapper.Contrib.Extensions;

namespace SocialEventManager.DAL.Entities
{
    public abstract class ClaimBase
    {
        [Computed]
        public int Id { get; set; }

        public string Type { get; set; }

        public string Value { get; set; }
    }
}
