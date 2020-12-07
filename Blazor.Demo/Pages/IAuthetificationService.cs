namespace Blazor.Demo.Pages
{
    public interface IAuthetificationService
    {
        bool IsValidLogin(string email, string password);
    }
}