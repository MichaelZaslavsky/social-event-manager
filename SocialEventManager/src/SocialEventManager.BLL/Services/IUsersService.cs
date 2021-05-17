using System;
using System.Threading.Tasks;
using SocialEventManager.BLL.Models;

namespace SocialEventManager.BLL.Services
{
    // Temp interface - for test purposes
    public interface IUsersService
    {
        Task<int> CreateUser(UserDto userDto);

        Task<UserDto> GetUser(Guid userId);
    }
}
