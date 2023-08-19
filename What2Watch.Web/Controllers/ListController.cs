namespace What2Watch.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class ListController : Controller
    {
        private readonly IMovieListService _listService;
        private readonly IMovieService _movieService;

        public ListController(IMovieListService listService, IMovieService movieService)
        {
            _listService = listService;
            _movieService = movieService;
        }
        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {

            string jwtToken = Request.Cookies["Token"];
            var response = await _listService.GetUserListAsync(jwtToken);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<UserMovieList>>();
                return View(result);
            }
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "auth");
            }
            ViewData["Error"] = "Something went wrong";
            return View();
        }
        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> EditAsync(UserMovieList model)
        {
            var response = await _movieService.GetMovieByTitleAsync(model.MovieId);
            var movie = await response.Content.ReadFromJsonAsync<MovieDetails>();
            ViewBag.Movie = movie;
            return View(model);
        }
        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> UpdateAsync(UserMovieList model)
        {
            string jwtToken = Request.Cookies["Token"];

            var response = await _listService.UpdateMovieListAsync(model, jwtToken);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "List");
            }
            TempData["Error"] = "Something went wrong";
            return RedirectToAction("Index", "List");
        }
        [HttpPost]
        [Route("Remove")]
        public async Task<IActionResult> RemoveAsync(int id)
        {
            string jwtToken = Request.Cookies["Token"];
            var response = await _listService.RemoveFromListAsync(id, jwtToken);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "List");
            }
            TempData["Error"] = "Something went wrong while removing movie from list. Try again later.";
            return RedirectToAction("Index", "List");
        }
        [HttpPost]
        public async Task<IActionResult> AddToListAsync(string id, string title)
        {

            MovieApiModel movieModel = new()
            {
                MovieId = id,
                Title = title,
                Note = "",
                Status = MovieStatus.Planned,
                UserScore = 0
            };
            string jwtToken = Request.Cookies["Token"];
            var response = await _listService.AddToListAsync(movieModel, jwtToken);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "List");
            }
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("Login", "Auth");
            }
            TempData["Error"] = "Something went wrong while adding movie from list. Try again later.";
            return RedirectToAction("Index", "List");

        }
        [HttpGet]
        public async Task<IActionResult> SearchAsync(string title)
        {

            if (string.IsNullOrEmpty(title))
            {
                return View(null);
            }
            var response = await _movieService.FindMoviesByTitleAsync(title);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<MovieSearchRoot>();
                TempData["Title"] = title;
                return View(result.Results);
            }
            return View(null);
        }

        [HttpGet]
        public async Task<IActionResult> DetailsAsync(string id)
        {
            var movieResponse = await _movieService.GetMovieByTitleAsync(id);
            if (movieResponse.IsSuccessStatusCode)
            {
                var movie = await movieResponse.Content.ReadFromJsonAsync<MovieDetails>();
                var movieId = movie.Id.Split("/")[2];
                var creditsResponse = await _movieService.GetCreditsAsync(id);
                if (creditsResponse.IsSuccessStatusCode)
                {
                    var credits = await creditsResponse.Content.ReadFromJsonAsync<Credits>();
                    var movieWithCredits = new MovieWithCreditsViewModel() { MovieDetails = movie, Credits = credits };
                    movieWithCredits.MovieDetails.Id = movieId;
                    movieWithCredits.MovieDetails.Id = id;
                    return View(movieWithCredits);
                }
            }
            ViewData["Error"] = "Something went wrong";
            return View();

        }
    }
}
