namespace SocialEventManager.BLL.Services.Email;

public interface IEmailRenderer
{
    Task<string> RenderAsync<T>(T model);
}
