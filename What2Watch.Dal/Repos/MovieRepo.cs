namespace What2Watch.Dal.Repos
{
    public class MovieRepo : BaseRepo<MovieEntity>, IMovieRepo
    {
        public MovieRepo(ApplicationDbContext context) : base(context)
        {
        }
        public virtual MovieEntity Find(string id)
        {
            return Table.Find(id);
        }
    }
}
