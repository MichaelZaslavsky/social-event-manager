using System;
using System.Threading.Tasks;
using SocialEventManager.BLL.Models;
using SocialEventManager.DLL.Entities;
using SocialEventManager.DLL.Repositories;

namespace SocialEventManager.BLL.Services
{
    // Temp class - for test purposes
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<int> CreateUser(UserDto userDto)
        {
            var user = new User(userDto.ExternalId, userDto.FirstName, userDto.LastName, userDto.Email);
            return await _usersRepository.InsertAsync(user);
        }

        public async Task<UserDto> GetUser(Guid userId)
        {
            User user = await _usersRepository.GetAsync(userId);
            return new UserDto(user.ExternalId, user.FirstName, user.LastName, user.Email);
        }
    }
}
