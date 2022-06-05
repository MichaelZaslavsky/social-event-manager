using AutoMapper;
using Serilog;
using SocialEventManager.DAL.Infrastructure;

namespace SocialEventManager.BLL.Services.Infrastructure;

public class ServiceBase<TIRepository, TEntity>
    where TIRepository : class, IGenericRepository<TEntity>
    where TEntity : class
{
    public ServiceBase(TIRepository repository)
    {
        Repository = repository;
    }

    public ServiceBase(TIRepository repository, IUnitOfWork unitOfWork)
        : this(repository)
    {
        UnitOfWork = unitOfWork;
    }

    public ServiceBase(TIRepository repository, IMapper mapper)
        : this(repository)
    {
        Mapper = mapper;
    }

    public ServiceBase(TIRepository repository, ILogger logger)
        : this(repository)
    {
        Logger = logger;
    }

    public ServiceBase(TIRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        : this(repository, unitOfWork)
    {
        Mapper = mapper;
    }

    public ServiceBase(TIRepository repository, IUnitOfWork unitOfWork, ILogger logger)
        : this(repository, unitOfWork)
    {
        Logger = logger;
    }

    public ServiceBase(TIRepository repository, IMapper mapper, ILogger logger)
        : this(repository, mapper)
    {
        Logger = logger;
    }

    public ServiceBase(TIRepository repository, IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
        : this(repository, unitOfWork, mapper)
    {
        Logger = logger;
    }

    protected TIRepository Repository { get; } = null!;

    protected IUnitOfWork UnitOfWork { get; } = null!;

    protected IMapper Mapper { get; } = null!;

    protected ILogger Logger { get; } = null!;
}
