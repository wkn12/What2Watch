namespace What2Watch.Models.Models
{
    public class MovieApiRespone
    {
        [JsonPropertyName("d")]
        public IEnumerable<Movie> Movies { get; set; }
    }
}
