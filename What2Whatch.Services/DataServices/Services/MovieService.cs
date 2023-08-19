using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace What2Watch.Services.DataServices.Api
{
    public class MovieService : IMovieService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private static List<string> _showList;
        private static ShowTypeEnum _listType;
        public MovieService(IHttpClientFactory clientFactory)
        {
            _httpClientFactory = clientFactory;
        }

        public async Task<HttpResponseMessage> GetRandomMovieAsync(ShowTypeEnum showType)
        {
            if (showType != _listType || (_showList == null || !_showList.Any()))
            {
                _listType = showType;
               
                switch (showType)
                {
                    case ShowTypeEnum.Movie:
                        _showList = (List<string>)await GetTopMoviesAsync();
                        break;
                    case ShowTypeEnum.TvShow:
                        _showList = (List<string>)await GetTopTvShowsAsync();
                        break;
                    default:
                        var fullList = new List<string>();
                        fullList.AddRange(await GetTopMoviesAsync());
                        fullList.AddRange(await GetTopTvShowsAsync());
                        _showList = fullList;
                        break;
                }
            }
            var item = SelectRandom().Split("/")[2];

            var response = await GetMovieByTitleAsync(item);
            
            return response;
        }
        public async Task<HttpResponseMessage> FindMoviesByTitleAsync(string name)
        {
            var client = _httpClientFactory.CreateClient("movieApi");
            var response = await client.GetAsync($"title/find/?q={name}");
          
            return response;
        }

        public async Task<HttpResponseMessage> GetMovieByTitleAsync(string name)
        {
            var client = _httpClientFactory.CreateClient("movieApi");
            var response = await client.GetAsync($"title/get-overview-details?tconst={name}");                
            return response;
        }

        public async Task<IEnumerable<string>> GetTopMoviesAsync()
        {
            var client = _httpClientFactory.CreateClient("movieApi");
            var response = await client.GetAsync("title/get-most-popular-movies");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<IEnumerable<string>>();
            return result;
        }

        public async Task<IEnumerable<string>> GetTopTvShowsAsync()
        {
            var client = _httpClientFactory.CreateClient("movieApi");
            var response = await client.GetAsync("title/get-most-popular-tv-shows");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<IEnumerable<string>>();
            return result;
        }
        public async Task<HttpResponseMessage> GetCreditsAsync(string name)
        {
            var client = _httpClientFactory.CreateClient("movieApi");
            var response = await client.GetAsync($"title/get-full-credits?tconst={name}");          
            return response;
        }
        private static string SelectRandom()
        {
            Random random = new();
            return _showList[random.Next(_showList.Count)];
        }

    }
}
