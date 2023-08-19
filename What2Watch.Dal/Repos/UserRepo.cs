namespace What2Watch.Dal.Repos
{
    public class UserRepo : BaseRepo<ApplicationUser>, IUserRepo
    {
        public UserRepo(ApplicationDbContext context) : base(context)
        {
        }

        public ApplicationUser Find(string id)
        {
         return Context.Users.Include(u => u.UserMovies).ThenInclude(m => m.Movie).FirstOrDefault(user => user.Id == id);
        }
    }
}
