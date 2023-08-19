namespace What2Watch.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class GeneratorController : Controller
    {
        private IMovieService _service;
        private IAppLogging<GeneratorController> _logger;

        public GeneratorController(IMovieService service, IAppLogging<GeneratorController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [Route("/[controller]")]
        [Route("/[controller]/[action]")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GenerateAsync(ShowTypeEnum showType, bool showCast = false)
        {

            ViewBag.Type = showType;
            var movieResponse = await _service.GetRandomMovieAsync(showType);
            if (movieResponse.IsSuccessStatusCode)
            {
                var movie = await movieResponse.Content.ReadFromJsonAsync<MovieDetails>();
                var movieId = movie.Id.Split("/")[2];
                var movieWithCredits = new MovieWithCreditsViewModel();
                movieWithCredits.MovieDetails = movie;
                movieWithCredits.MovieDetails.Id = movieId;
                movieWithCredits.ShowCast = showCast;
                if (showCast)
                {
                    var creditsResponse = await _service.GetCreditsAsync(movieId);
                    if (creditsResponse.IsSuccessStatusCode)
                    {
                        var credits = await creditsResponse.Content.ReadFromJsonAsync<Credits>();
                        movieWithCredits.Credits = credits;
                        return View("Result", movieWithCredits);
                    }
                }
                else
                {
                    movieWithCredits.Credits = null;
                    return View("Result", movieWithCredits);
                }
            }
            ViewData["Error"] = "Something went wrong";
            return View("Result");
        }
    }
}
