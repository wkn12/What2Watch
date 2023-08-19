namespace What2Watch.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

        public ICollection<UserMovieList> UserMovies { get; set; } = new List<UserMovieList>();

    }
}
