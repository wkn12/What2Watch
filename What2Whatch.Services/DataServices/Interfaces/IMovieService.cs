namespace What2Watch.Services.DataServices.Interfaces
{
    public interface IMovieService
    {
        Task<HttpResponseMessage> FindMoviesByTitleAsync(string name);
        Task<HttpResponseMessage> GetMovieByTitleAsync(string name);
        Task<IEnumerable<string>> GetTopMoviesAsync();
        Task<IEnumerable<string>> GetTopTvShowsAsync();
        Task<HttpResponseMessage> GetRandomMovieAsync(ShowTypeEnum showType);
        Task<HttpResponseMessage> GetCreditsAsync(string name);
    }
}
