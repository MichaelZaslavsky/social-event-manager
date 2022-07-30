using Microsoft.AspNetCore.Mvc;
using SocialEventManager.BLL.Models.ContactUs;
using SocialEventManager.BLL.Services.ContactUs;
using SocialEventManager.Shared.Constants;

namespace SocialEventManager.API.Controllers;

/// <summary>
/// Allows contact us.
/// </summary>
[ApiController]
[Route(ApiPathConstants.ApiController)]
[ApiVersion("1.0")]
[Consumes(MediaTypeConstants.ApplicationJson)]
public class ContactUsController : ControllerBase
{
    private readonly IContactUsService _contactUsService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ContactUsController"/> class.
    /// </summary>
    /// <param name="contactUsService">Contacts us service.</param>
    public ContactUsController(IContactUsService contactUsService)
    {
        _contactUsService = contactUsService;
    }

    /// <summary>
    /// Sends an email to <see cref="ContactConstants.Email"/>.
    /// </summary>
    /// <param name="contactUs">Requested information for contacting us.</param>
    /// <returns>An empty ActionResult.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Post(ContactUsDto contactUs)
    {
        await _contactUsService.ContactUsAsync(contactUs);
        return Ok();
    }
}
