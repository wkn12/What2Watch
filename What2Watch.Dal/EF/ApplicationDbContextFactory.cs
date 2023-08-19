namespace What2Watch.Dal.EF
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connectionString = @"server=(localdb)\MsSqlLocalDb;Database=What2Watch;Integrated Security=true";
            optionsBuilder.UseSqlServer(connectionString, options => options.EnableRetryOnFailure(60));
            Console.WriteLine(connectionString);
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}