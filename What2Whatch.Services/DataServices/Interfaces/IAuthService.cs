namespace What2Watch.Services.DataServices.Interfaces
{
    public interface IAuthService
    {
        public Task<HttpResponseMessage> RegisterAsync(AccountRegisterModel model);
        public Task<HttpResponseMessage> LoginAsync(LoginModel model);
        public Task LogoutAsync(string authToken);

    }
}
