namespace What2Watch.Web.Utils
{
    static class CookiesUtils
    {
        public static void DeleteAuthCookies(HttpContext context)
        {
            context.Response.Cookies.Delete("Token");
            context.Response.Cookies.Delete("RefreshToken");
            context.Response.Cookies.Delete("Name");
        }
    }
}
