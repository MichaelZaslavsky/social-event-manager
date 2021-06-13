using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using SocialEventManager.BLL.Models;
using SocialEventManager.DLL.Entities;
using SocialEventManager.DLL.Infrastructure;
using SocialEventManager.DLL.Repositories;
using SocialEventManager.Shared.Constants.Validations;
using SocialEventManager.Shared.Exceptions;

namespace SocialEventManager.BLL.Services
{
    // Temp class - for test purposes
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UsersService> _logger;
        private readonly IMapper _mapper;

        public UsersService(IUsersRepository usersRepository, IUnitOfWork unitOfWork, ILogger<UsersService> logger, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<int> CreateUser(UserDto userDto)
        {
            User user = _mapper.Map<User>(userDto);

            // Usage of UnitOfWork just for testing & usage example
            _unitOfWork.BeginTransaction();
            int userId = await _usersRepository.InsertAsync(user);
            _unitOfWork.Commit();

            return userId;
        }

        public async Task<UserDto> GetUser(Guid userId)
        {
            User user = await _usersRepository.GetSingleOrDefaultAsync(userId, nameof(User.ExternalId));

            if (user == null)
            {
                throw new NotFoundException($"The user '{userId}' {ValidationConstants.WasNotFound}");
            }

            // Usage of logging only for test purposes
            _logger.LogInformation($"User {user.FirstName} {user.LastName} was found.");

            return _mapper.Map<UserDto>(user);
        }
    }
}
