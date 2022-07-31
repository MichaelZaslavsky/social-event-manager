using AutoMapper;
using Hangfire;
using SocialEventManager.BLL.Models.ContactUs;
using SocialEventManager.Infrastructure.Email;
using SocialEventManager.Shared.Models;

namespace SocialEventManager.BLL.Services.ContactUs;

public class ContactUsService : IContactUsService
{
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;

    public ContactUsService(IEmailService emailService, IMapper mapper)
    {
        _emailService = emailService;
        _mapper = mapper;
    }

    public Task ContactUsAsync(ContactUsDto contactUs)
    {
        EmailDto email = _mapper.Map<EmailDto>(contactUs);
        BackgroundJob.Enqueue(() => _emailService.SendEmailAsync(email));

        return Task.CompletedTask;
    }
}
