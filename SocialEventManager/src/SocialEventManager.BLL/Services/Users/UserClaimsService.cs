using AutoMapper;
using SocialEventManager.BLL.Models.Users;
using SocialEventManager.BLL.Services.Infrastructure;
using SocialEventManager.DAL.Entities;
using SocialEventManager.DAL.EqualityComparers;
using SocialEventManager.DAL.Infrastructure;
using SocialEventManager.DAL.Repositories.Users;
using SocialEventManager.Shared.Constants.Validations;
using SocialEventManager.Shared.Exceptions;
using SocialEventManager.Shared.Extensions;

namespace SocialEventManager.BLL.Services.Users;

public class UserClaimsService : ServiceBase<IUserClaimsRepository, UserClaim>, IUserClaimsService
{
    public UserClaimsService(IUserClaimsRepository userClaimsRepository, IUnitOfWork unitOfWork, IMapper mapper)
        : base(userClaimsRepository, unitOfWork, mapper)
    {
    }

    public async Task CreateUserClaims(IEnumerable<UserClaimForCreationDto> userClaimsForCreation)
    {
        IEnumerable<UserClaim> userClaims = Mapper.Map<IEnumerable<UserClaim>>(userClaimsForCreation);
        await Repository.InsertAsync(userClaims);

        return;
    }

    public async Task<IEnumerable<UserClaimDto>> GetUserClaims(Guid userId)
    {
        IEnumerable<UserClaim> userClaims = await Repository.GetAsync(userId, nameof(UserClaim.UserId));

        if (userClaims.IsEmpty())
        {
            throw new NotFoundException($"The user claims for the user '{userId}' {ValidationConstants.WereNotFound}");
        }

        return Mapper.Map<IEnumerable<UserClaimDto>>(userClaims);
    }

    public async Task<IEnumerable<UserClaimDto>> GetUserClaims(string type, string value)
    {
        IEnumerable<UserClaim> userClaims = await Repository.GetUserClaims(type, value);

        if (userClaims.IsEmpty())
        {
            throw new NotFoundException($"The user claims of type '{type}' and value '{value}' {ValidationConstants.WasNotFound}");
        }

        return Mapper.Map<IEnumerable<UserClaimDto>>(userClaims);
    }

    public async Task<bool> ReplaceUserClaim(UserClaimBase currentUserClaim, UserClaimForUpdateDto newUserClaimForUpdate)
    {
        UnitOfWork.BeginTransaction();
        UserClaim userClaim = (await Repository.GetAsync(newUserClaimForUpdate.UserId, nameof(UserClaim.UserId)))
            .SingleOrDefault(uc => uc.Type == currentUserClaim.Type && uc.Value == currentUserClaim.Value);

        if (userClaim is null)
        {
            throw new NotFoundException(
                $"The user claims of type '{currentUserClaim.Type}' and value '{currentUserClaim.Value}' {ValidationConstants.WasNotFound}");
        }

        userClaim.Type = newUserClaimForUpdate.Type;
        userClaim.Value = newUserClaimForUpdate.Value;

        bool isUpdated = await Repository.UpdateAsync(userClaim);
        UnitOfWork.Commit();

        return isUpdated;
    }

    public async Task<bool> DeleteUserClaims(IEnumerable<UserClaimBase> userClaimsBase, Guid userId)
    {
        UnitOfWork.BeginTransaction();
        IEnumerable<UserClaim> userClaims = (await Repository.GetAsync(userId, nameof(UserClaim.UserId)))
            .Intersect(userClaimsBase.Select(src => new UserClaim { UserId = userId, Type = src.Type, Value = src.Value }), new UserClaimEqualityComparer());

        if (userClaims.IsEmpty())
        {
            throw new NotFoundException(
                $"The user claims '({string.Join(", ", userClaimsBase)})' for the user '{userId}' {ValidationConstants.WereNotFound}");
        }

        bool isDeleted = await Repository.DeleteUserClaims(userClaims);
        UnitOfWork.Commit();

        return isDeleted;
    }
}
