using System;
using System.Threading.Tasks;
using AutoMapper;
using SocialEventManager.BLL.Models;
using SocialEventManager.DLL.Entities;
using SocialEventManager.DLL.Repositories;

namespace SocialEventManager.BLL.Services
{
    // Temp class - for test purposes
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public UsersService(IUsersRepository usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        public async Task<int> CreateUser(UserDto userDto)
        {
            User user = _mapper.Map<User>(userDto);
            return await _usersRepository.InsertAsync(user);
        }

        public async Task<UserDto> GetUser(Guid userId)
        {
            User user = await _usersRepository.GetSingleOfDefaultAsync(userId, nameof(User.ExternalId));
            return _mapper.Map<UserDto>(user);
        }
    }
}
