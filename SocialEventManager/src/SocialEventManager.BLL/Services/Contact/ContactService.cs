using AutoMapper;
using Hangfire;
using SocialEventManager.BLL.Models.Contact;
using SocialEventManager.Infrastructure.Email;
using SocialEventManager.Shared.Models;

namespace SocialEventManager.BLL.Services.Contact;

public class ContactService : IContactService
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
