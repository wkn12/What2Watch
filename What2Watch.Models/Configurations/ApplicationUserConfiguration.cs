namespace What2Watch.Models.Configurations
{
    public class ApplicationEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(u => u.UserName).IsRequired(false);
        }
    }
}