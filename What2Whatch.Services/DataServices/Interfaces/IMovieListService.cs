namespace What2Watch.Services.DataServices.Interfaces
{
    public interface IMovieListService
    {
        Task<HttpResponseMessage> AddToListAsync(MovieApiModel movie,string authToken);
        Task<HttpResponseMessage> GetUserListAsync(string authToken);
        Task<HttpResponseMessage> UpdateMovieListAsync(UserMovieList movie, string authToken);
        Task<HttpResponseMessage> RemoveFromListAsync(int id, string authToken);
    }
}
