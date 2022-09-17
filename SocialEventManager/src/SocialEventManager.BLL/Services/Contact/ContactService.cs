using AutoMapper;
using Hangfire;
using SocialEventManager.Infrastructure.Email;
using SocialEventManager.Shared.Models.Contact;
using SocialEventManager.Shared.Models.Email;

namespace SocialEventManager.BLL.Services.Contact;

public sealed class ContactService : IContactService
{
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;

    public ContactService(IEmailService emailService, IMapper mapper)
    {
        _emailService = emailService;
        _mapper = mapper;
    }

    public Task ContactAsync(ContactDto contact)
    {
        EmailDto email = _mapper.Map<EmailDto>(contact);
        BackgroundJob.Enqueue(() => _emailService.SendEmailAsync(email));

        return Task.CompletedTask;
    }
}
