using AutoMapper;
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

    public async Task ContactUsAsync(ContactUsDto contactUs)
    {
        EmailDto email = _mapper.Map<EmailDto>(contactUs);
        await _emailService.SendEmailAsync(email);
    }
}
