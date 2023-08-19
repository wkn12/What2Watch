namespace What2Watch.Models.Models
{

    public class MovieDetails
    {
        public string Id { get; set; }
        public TitleDescription Title { get; set; }
        public Ratings Ratings { get; set; }
        public List<string> Genres { get; set; }
        public string ReleaseDate { get; set; }
        public PlotOutline PlotOutline { get; set; }
        public PlotSummary PlotSummary { get; set; }
    }
    public class TitleDescription
    {
        public string Id { get; set; }
        public ImageDetails Image { get; set; }
        public int RunningTimeInMinutes { get; set; }
        public string NextEpisode { get; set; }
        public int NumberOfEpisodes { get; set; }
        public int SeriesEndYear { get; set; }
        public int SeriesStartYear { get; set; }
        public string Title { get; set; }
        public string TitleType { get; set; }
        public int Year { get; set; }
    }
    public class PlotOutline
    {
        public string Text { get; set; }
    }

    public class PlotSummary
    {
        public string Text { get; set; }
    }

    public class Ratings
    {
        public bool CanRate { get; set; }
        public double Rating { get; set; }
        public int RatingCount { get; set; }
    }

    public class ImageDetails
    {
        public int Height { get; set; }
        public string Id { get; set; }
        public string Url { get; set; }
        public int Width { get; set; }
    }

    public class SearchResult
    {
        public string Id { get; set; }
        public ImageDetails Image { get; set; }
        public string Title { get; set; }
        public string TitleType { get; set; }
        public int Year { get; set; }

    }

    public class MovieSearchRoot
    {    
        public List<SearchResult> Results { get; set; }
    }

}

