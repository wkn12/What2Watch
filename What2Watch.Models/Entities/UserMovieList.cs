namespace What2Watch.Models.Entities
{
    public class UserMovieList:BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int Id { get; set; }

        [ForeignKey(nameof(Movie))]
        [Column(Order = 3)]
        public string MovieId { get; set; }
        public MovieEntity Movie { get; set; }
        public MovieStatus Status { get; set; }
        [DisplayName("My score")]
        [Range(0,10,ErrorMessage ="Score must be between 0 and 10.")]
        public int UserScore { get; set; }

        public string Note { get; set; }
        public string ApplicationUserId { get; set; }
        [JsonIgnore]
        public ApplicationUser ApplicationUser { get; set; }

    }
}
