
using What2Watch.Dal.Repos;
using What2Watch.Dal.Repos.Interfaces;

namespace What2Watch.Services.DataServices
{
    public static class DataServiceConfiguration
    {
        public static IServiceCollection AddDataServices(
            this IServiceCollection services,ConfigurationManager config)
        {
            var settings = config.GetSection("MovieApiSettings");
            var apiKey = settings.GetValue<string>("ApiKey");
            var apiHost = settings.GetValue<string>("ApiHost");


            services.AddHttpClient("movieApi", client =>
             {
                 client.BaseAddress = new Uri("https://online-movie-database.p.rapidapi.com/");
                 client.DefaultRequestHeaders.Add("X-RapidAPI-Key", apiKey);
                 client.DefaultRequestHeaders.Add("X-RapidAPI-Host", apiHost);
             });

            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IMovieListService, MovieListService>();
            
            return services;
        }
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IMovieListRepo, MovieListRepo>();
            services.AddScoped<IMovieRepo, MovieRepo>();
            services.AddScoped<IUserRepo, UserRepo>();

            return services;
        }
    }
}