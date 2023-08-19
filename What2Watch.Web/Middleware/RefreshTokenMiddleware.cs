
namespace What2Watch.Web.Middleware
{
    public class RefreshTokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHttpClientFactory _httpClientFactory;
        public RefreshTokenMiddleware(RequestDelegate next, IHttpClientFactory httpClientFactory)
        {
            _next = next;
            _httpClientFactory = httpClientFactory;
        }
        public async Task Invoke(HttpContext context)
        {
            var accessToken = context.Request.Cookies["Token"];
            var refreshToken = context.Request.Cookies["RefreshToken"];
            var name = context.Request.Cookies["Name"];

            if ((string.IsNullOrEmpty(accessToken) || IsTokenExpired(accessToken)) && !string.IsNullOrEmpty(refreshToken))
            {
                var newAccessToken = await GetNewAccessTokenUsingRefreshToken(refreshToken,name);

                if (newAccessToken !=null)
                {
                    context.Response.Cookies.Append("Token", newAccessToken.Token, new CookieOptions
                    {
                        HttpOnly = true,
                        Expires = newAccessToken.Expiration
                    });                 
                }
                else
                {
                    CookiesUtils.DeleteAuthCookies(context);
                }
            }
            await _next(context);
        }

        private bool IsTokenExpired(string accessToken)
        {         
            var jwtHandler = new JwtSecurityTokenHandler();
            var token = jwtHandler.ReadJwtToken(accessToken);         
            return token.ValidTo < DateTime.UtcNow;
        }

        private async Task<LoginResponse> GetNewAccessTokenUsingRefreshToken(string refreshToken,string name)
        {         
           var httpClient = _httpClientFactory.CreateClient("w2wApi");
           TokenModel data = new TokenModel
            {         
                RefreshToken = refreshToken,
                Name = name 
            };
            var response = await httpClient.PostAsJsonAsync("api/Auth/refresh-token", data);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<LoginResponse>(responseData);
                return result;
            }         
            return null;
        }
    }
}
