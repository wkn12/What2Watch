namespace What2Watch.Models.Configurations
{
    public class UserMovieConfiguration : IEntityTypeConfiguration<UserMovieList>
    {
        public void Configure(EntityTypeBuilder<UserMovieList> builder)
        {
            builder.HasOne(uml => uml.Movie)
                .WithMany()
                .HasForeignKey(uml => uml.MovieId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(uml => uml.ApplicationUser)
                   .WithMany(au => au.UserMovies)
                   .HasForeignKey(uml => uml.ApplicationUserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
