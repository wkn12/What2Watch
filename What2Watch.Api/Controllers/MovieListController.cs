namespace What2Watch.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MovieListController : BaseController
    {
        private readonly IMovieListRepo _repo;
        private readonly UserManager<ApplicationUser> _userManager;
        public MovieListController(IMovieListRepo repo, UserManager<ApplicationUser> userManager)
        {
            _repo = repo;
            _userManager = userManager;
        }
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<UserMovieList>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(200, "The execution was successful")]
        [SwaggerResponse(400, "The request was invalid")]
        [SwaggerResponse(401, "Unauthorized access attempted")]
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetList()
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(this.UserEmail);
            var list = _repo.GetUserList(user.Id);
            try
            {
                if (list.Count() == 0)
                {
                    return NotFound("No user movies found.");
                }

                return Ok(list);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [ProducesResponseType(typeof(IEnumerable<UserMovieList>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(200, "The execution was successful")]
        [SwaggerResponse(400, "The request was invalid")]
        [SwaggerResponse(401, "Unauthorized access attempted")]
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddToListAsync(MovieApiModel movie)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(this.UserEmail);

            if (ModelState.IsValid)
            {
                var result = _repo.Add(user.Id, movie);
                if (result != 0)
                {
                    return Ok("Movie has been added to list!");
                }
            }
            return BadRequest();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(200, "The execution was successful")]
        [SwaggerResponse(400, "The request was invalid")]
        [SwaggerResponse(401, "Unauthorized access attempted")]
        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> UpdateList(UserMovieList movie)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(this.UserEmail);

            if (ModelState.IsValid)
            {
                var result = _repo.Update(user.Id, movie);
                if (result != 0)
                {
                    return Ok("Movie has been updated!");
                }
            }
            return BadRequest();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(200, "The execution was successful")]
        [SwaggerResponse(400, "The request was invalid")]
        [SwaggerResponse(401, "Unauthorized access attempted")]
        [HttpDelete]
        [Route("Remove/{id}")]
        public async Task<IActionResult> RemoveFromList(int id)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(this.UserEmail);
            var result = _repo.Delete(user.Id, id);
            if (result != 0)
            {
                return Ok("Movie has been removed!");
            }
            return BadRequest();
        }
    }
}
