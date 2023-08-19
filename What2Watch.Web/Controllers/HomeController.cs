namespace What2Watch.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly IAppLogging<HomeController> _logger;
        
       
        public HomeController(IAppLogging<HomeController> logger, IHttpContextAccessor context)
        {
            _logger = logger;
        }

        [Route("/")]
        [Route("/[controller]")]
        [Route("/[controller]/[action]")]
        [HttpGet]
        public  IActionResult Index()
        {
            //List<string> _images = new List<string>();

            //_images.AddRange(Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Content\Images")));
            //for (int i = 0; i < _images.Count; i++)
            //{
            //    // get rid of the fully qualified path name
            //    _images[i] = _images[i].Replace(@"C:\wherever\files\are\", "");
            //    // change the slashes for web
            //    _images[i] = _images[i].Replace('\\', '/');
            //}
            //return View(_images);

            return View();

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}