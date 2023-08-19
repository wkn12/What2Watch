namespace What2Watch.Models.Models
{
    public class Movie
    {
        [Required]
        public string Id { get; set; }
        [Required]
        [JsonPropertyName("l")]
        public string Name { get; set; }
        [JsonPropertyName("i")]
        public Image Image { get; set; }

    }

    public class Image
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public string ImageUrl { get; set; }
    }

}
