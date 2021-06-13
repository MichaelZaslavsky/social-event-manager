using Microsoft.AspNetCore.Identity;

namespace SocialEventManager.BLL.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string AuthenticationType { get; set; }

        public bool IsAuthenticated { get; set; }

        public string Name { get; set; }
    }
}
