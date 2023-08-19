namespace What2Watch.Dal.EF
{
    public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public virtual DbSet<MovieEntity> Movies { get; set; }
        public DbSet<UserMovieList> UserMovies { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            new ApplicationEntityConfiguration().Configure(builder.Entity<ApplicationUser>());
            new UserMovieConfiguration().Configure(builder.Entity<UserMovieList>());

            OnModelCreatingPartial(builder);
        }
        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {

                throw new CustomConcurrencyException("A concurrency error happened.", ex);
            }
            catch (RetryLimitExceededException ex)
            {

                throw new CustomRetryLimitExceededException("There is a problem with SQL Server.", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new CustomDbUpdateException("An error occurred updating the database", ex);
            }
            catch (Exception ex)
            {
                throw new CustomException("An error occurred updating the database", ex);
            }
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
