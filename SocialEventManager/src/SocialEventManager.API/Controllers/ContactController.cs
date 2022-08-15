using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialEventManager.BLL.Services.Contact;
using SocialEventManager.Shared.Constants;
using SocialEventManager.Shared.Models.Contact;

namespace SocialEventManager.API.Controllers;

/// <summary>
/// Allows contact us.
/// </summary>
[ApiController]
[Route(ApiPathConstants.ApiController)]
[ApiVersion("1.0")]
[Consumes(MediaTypeConstants.ApplicationJson)]
[AllowAnonymous]
public class ContactController : ControllerBase
{
    private readonly IContactService _contactService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ContactController"/> class.
    /// </summary>
    /// <param name="contactService">Contact us service.</param>
    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }

    /// <summary>
    /// Sends an email to <see cref="ContactConstants.Email"/>.
    /// </summary>
    /// <param name="contact">Requested information for contacting us.</param>
    /// <returns>An empty ActionResult.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Post(ContactDto contact)
    {
        await _contactService.ContactAsync(contact);
        return Ok();
    }
}
