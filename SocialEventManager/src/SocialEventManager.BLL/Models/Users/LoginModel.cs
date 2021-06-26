using System.ComponentModel.DataAnnotations;

namespace SocialEventManager.BLL.Models.Users
{
    public record LoginModel
    {
        [Required]
        public string UserName { get; init; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; init; }
    }
}
