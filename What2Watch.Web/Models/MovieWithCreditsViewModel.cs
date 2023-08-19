namespace What2Watch.Web.Models
{
    public class MovieWithCreditsViewModel
    {
        public MovieDetails MovieDetails { get; set; }
        public Credits Credits { get; set; }
        public bool ShowCast { get; set; }
    }
}
