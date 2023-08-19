namespace What2Watch.Services.DataServices.Services
{
    public class AuthService:IAuthService
    {
        private readonly IHttpClientFactory _clientFactory;
        public AuthService(IHttpClientFactory clientFactory)
        {
           _clientFactory = clientFactory;
        }

        public async Task<HttpResponseMessage> RegisterAsync(AccountRegisterModel model)
        {
            var client = _clientFactory.CreateClient("w2wApi");
            var response = await client.PostAsJsonAsync("api/auth/register", model);
            if (response == null)
            {
                throw new Exception("Unable to communicate with the service");
            }
            return response;
        }
        public async Task<HttpResponseMessage> LoginAsync(LoginModel model)
        {
            var client = _clientFactory.CreateClient("w2wApi");
            var response = await client.PostAsJsonAsync("api/auth/login", model);
            if (response == null)
            {
                throw new Exception("Unable to communicate with the service");
            }
            return response;
          
        }

       public async Task LogoutAsync(string authToken)
        {
            var client = _clientFactory.CreateClient("w2wApi");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            await client.PostAsJsonAsync("api/auth/logout", new { });
        }
    }
}
