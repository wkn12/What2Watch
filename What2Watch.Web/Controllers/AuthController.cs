namespace What2Watch.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthController(IAuthService authService,IHttpContextAccessor contextAccessor )
        {
            _authService = authService;
            _httpContextAccessor = contextAccessor;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsync(AccountRegisterModel model)
        {

           if (!ModelState.IsValid)
            {
                return View(model);
            }
            var response = await _authService.RegisterAsync(model);

            if (response.IsSuccessStatusCode)
            {                
                TempData["Message"] = "Registration successful. You can now log in.";
                return RedirectToAction("Login", "Auth");
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {             
                var errors = await response.Content.ReadFromJsonAsync<Dictionary<string, string[]>>();
                foreach (var error in errors)
                {
                    foreach (var errorMessage in error.Value)
                    {
                        ModelState.AddModelError(error.Key, errorMessage);
                    }
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
            }
            return View(model);
        }    

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model,string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var response =await _authService.LoginAsync(model);

                if (response.IsSuccessStatusCode)
                {
                    
                    var responseData = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<LoginResponse>(responseData);
                    Response.Cookies.Append("Token", result.Token, new CookieOptions
                    {
                        HttpOnly = true,
                        Expires = result.Expiration,
                        IsEssential = true

                    });
                    if (model.RememberMe)
                    {
                        Response.Cookies.Append("RefreshToken", result.RefreshToken, new CookieOptions
                        {
                            HttpOnly = true,
                            Expires = DateTime.UtcNow.AddDays(7),
                            IsEssential = true
                        });
                        Response.Cookies.Append("Name", result.Name, new CookieOptions { 
                             HttpOnly = true, Expires = DateTime.UtcNow.AddDays(7),
                             IsEssential = true
                         });
                    }
                    else
                    {
                        Response.Cookies.Append("RefreshToken", result.RefreshToken, new CookieOptions{HttpOnly = true,IsEssential = true});
                        Response.Cookies.Append("Name", result.Name, new CookieOptions { HttpOnly = true,IsEssential = true });
                    }

                    if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                    {
                        
                        return Redirect(ReturnUrl);
                    }
                    else
                    {                      
                        return RedirectToAction("Index", "Home");
                    }
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    ViewData["ErrorMessage"]="Invalid email or password.";
                }
                else
                {
                    ViewData["ErrorMessage"] = "An error occurred while processing your request.";
                }
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            var context = _httpContextAccessor.HttpContext;
            string jwtToken = Request.Cookies["Token"];
            await _authService.LogoutAsync(jwtToken);           
            CookiesUtils.DeleteAuthCookies(context);
            return RedirectToAction("Index","Home");
        }
    }
}
