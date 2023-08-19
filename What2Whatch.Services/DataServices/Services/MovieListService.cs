using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace What2Watch.Services.DataServices.Services
{
    public class MovieListService : IMovieListService
    {
        private readonly IHttpClientFactory _clientFactory;
        public MovieListService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<HttpResponseMessage> AddToListAsync(MovieApiModel movie, string authToken)
        {
            var client = _clientFactory.CreateClient("w2wApi");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            var response = await client.PostAsJsonAsync("api/MovieList/Add", movie);
            if (response == null)
            {
                throw new Exception("Unable to communicate with the service");
            }
            return response;
        }

        public async Task<HttpResponseMessage> GetUserListAsync(string authToken)
        {
            var client = _clientFactory.CreateClient("w2wApi");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            var response = await client.GetAsync("api/MovieList/list");
            if (response == null)
            {
                throw new Exception("Unable to communicate with the service");
            }               
            return response;
        }


        public async Task<HttpResponseMessage> RemoveFromListAsync(int id, string authToken)
        {
            var client = _clientFactory.CreateClient("w2wApi");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            var response = await client.DeleteAsync($"api/MovieList/Remove/{id}");
            if (response == null)
            {
                throw new Exception("Unable to communicate with the service");
            }       
            return response;
        }

        public async Task<HttpResponseMessage> UpdateMovieListAsync(UserMovieList movie, string authToken)
        {
            var client = _clientFactory.CreateClient("w2wApi");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

                var response = await client.PostAsJsonAsync("api/MovieList/Update", movie);
            if (response == null)
            {
                throw new Exception("Unable to communicate with the service");
            }            
            return response;
        }
    }
}
