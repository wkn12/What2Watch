
namespace What2Watch.Dal.Repos
{
    public class MovieListRepo :BaseRepo<UserMovieList>,IMovieListRepo
    {
        private readonly IMovieRepo _movieRepo;
        private readonly IUserRepo _userRepo;
        public MovieListRepo(ApplicationDbContext context, IMovieRepo movieRepo,IUserRepo userRepo) : base(context)
        {
            _movieRepo = movieRepo;
            _userRepo = userRepo;
        }

        public int Add(string userId, MovieApiModel entity)
        {
            var movie = _movieRepo.Find(entity.MovieId);
            if (movie == null)
            {
                _movieRepo.Add(new MovieEntity { Id = entity.MovieId, Title = entity.Title });
            }
            var user = _userRepo.Find(userId);
            var movieOnList = user.UserMovies.FirstOrDefault(m => m.MovieId == entity.MovieId);
            if(movieOnList != null)
            {
                return 0;
            }
            user.UserMovies.Add(new UserMovieList { 
                MovieId = entity.MovieId,              
                Note = entity.Note,
                UserScore = entity.UserScore,
                Status = entity.Status });
            return Context.SaveChanges();
        }

        public void ChangeStatus(MovieStatus status)
        {
            throw new NotImplementedException();
        }

        public int Delete(string userId, int movieListId)
        {
            var user = _userRepo.Find(userId);
            var movie = user.UserMovies.First(m => m.Id == movieListId);
            Table.Remove(movie);
            return Context.SaveChanges();
            
        }

        public MovieEntity Find(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserMovieList> GetUserList(string userId)
        {
            var user = _userRepo.Find(userId);
            var list = user.UserMovies;
            return list;
           
        }

        public int Update(string userId, UserMovieList entity)
        {
            var user = _userRepo.Find(userId);
            if(user.Id!= userId)
            {
                return 0;
            }
            var movie = user.UserMovies.FirstOrDefault(m => m.Id == entity.Id);
            if (IsChanged(entity, movie))
            {
            movie.UserScore = entity.UserScore;
            movie.Status = entity.Status;
            movie.Note = entity.Note;

            return Context.SaveChanges();
            }
            else
            {
                return 1;
            }

        }
        private bool IsChanged(UserMovieList newMovie, UserMovieList oldMovie)
        {
            return newMovie.Status!=oldMovie.Status || newMovie.Note!=oldMovie.Note || newMovie.UserScore != oldMovie.UserScore;
        }
    }
}
