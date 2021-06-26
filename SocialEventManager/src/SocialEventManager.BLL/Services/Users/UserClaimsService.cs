using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SocialEventManager.BLL.Models.Users;
using SocialEventManager.DAL.Entities;
using SocialEventManager.DAL.EqualityComparers;
using SocialEventManager.DAL.Infrastructure;
using SocialEventManager.DAL.Repositories.Users;
using SocialEventManager.Shared.Constants.Validations;
using SocialEventManager.Shared.Exceptions;
using SocialEventManager.Shared.Extensions;

namespace SocialEventManager.BLL.Services.Users
{
    public class UserClaimsService : IUserClaimsService
    {
        private readonly IUserClaimsRepository _userClaimsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserClaimsService(IUserClaimsRepository userClaimsRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userClaimsRepository = userClaimsRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task CreateUserClaims(IEnumerable<UserClaimForCreationDto> userClaimsForCreation)
        {
            IEnumerable<UserClaim> userClaims = _mapper.Map<IEnumerable<UserClaim>>(userClaimsForCreation);
            await _userClaimsRepository.InsertAsync(userClaims);

            return;
        }

        public async Task<IEnumerable<UserClaimDto>> GetUserClaims(Guid userId)
        {
            IEnumerable<UserClaim> userClaims = await _userClaimsRepository.GetAsync(userId, nameof(UserClaim.UserId));

            if (userClaims.IsEmpty())
            {
                throw new NotFoundException($"The user claims for the user '{userId}' {ValidationConstants.WereNotFound}");
            }

            return _mapper.Map<IEnumerable<UserClaimDto>>(userClaims);
        }

        public async Task<IEnumerable<UserClaimDto>> GetUserClaims(string type, string value)
        {
            IEnumerable<UserClaim> userClaims = await _userClaimsRepository.GetUserClaims(type, value);

            if (userClaims.IsEmpty())
            {
                throw new NotFoundException($"The user claims of type '{type}' and value '{value}' {ValidationConstants.WasNotFound}");
            }

            return _mapper.Map<IEnumerable<UserClaimDto>>(userClaims);
        }

        public async Task<bool> ReplaceUserClaim(UserClaimBase currentUserClaim, UserClaimForUpdateDto newUserClaimForUpdate)
        {
            _unitOfWork.BeginTransaction();
            UserClaim userClaim = (await _userClaimsRepository.GetAsync(newUserClaimForUpdate.UserId, nameof(UserClaim.UserId)))
                .SingleOrDefault(uc => uc.Type == currentUserClaim.Type && uc.Value == currentUserClaim.Value);

            if (userClaim == null)
            {
                throw new NotFoundException(
                    $"The user claims of type '{currentUserClaim.Type}' and value '{currentUserClaim.Value}' {ValidationConstants.WasNotFound}");
            }

            userClaim.Type = newUserClaimForUpdate.Type;
            userClaim.Value = newUserClaimForUpdate.Value;

            bool isUpdated = await _userClaimsRepository.UpdateAsync(userClaim);
            _unitOfWork.Commit();

            return isUpdated;
        }

        public async Task<bool> DeleteUserClaims(IEnumerable<UserClaimBase> userClaimsBase, Guid userId)
        {
            _unitOfWork.BeginTransaction();
            IEnumerable<UserClaim> userClaims = (await _userClaimsRepository.GetAsync(userId, nameof(UserClaim.UserId)))
                .Intersect(userClaimsBase.Select(src => new UserClaim { UserId = userId, Type = src.Type, Value = src.Value }), new UserClaimEqualityComparer());

            if (userClaims.IsEmpty())
            {
                throw new NotFoundException(
                    $"The user claims '({string.Join(", ", userClaimsBase)})' for the user '{userId}' {ValidationConstants.WereNotFound}");
            }

            bool isDeleted = await _userClaimsRepository.DeleteUserClaims(userClaims);
            _unitOfWork.Commit();

            return isDeleted;
        }
    }
}
