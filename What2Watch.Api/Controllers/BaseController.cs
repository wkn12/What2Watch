namespace What2Watch.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        private string userEmail { get; set; }
        protected string UserEmail
        {
            get
            {
                if (userEmail == null)
                    userEmail = this.User.FindFirstValue(ClaimTypes.Email);
                return userEmail;
            }
        }
    }
}
