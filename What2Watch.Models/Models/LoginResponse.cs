namespace What2Watch.Models.Models
{
    public class LoginResponse
    {
       
            public string Token { get; set; }
            public string RefreshToken { get; set; }
            public DateTime Expiration { get; set; }
            public string Name { get; set; }
    }
}
