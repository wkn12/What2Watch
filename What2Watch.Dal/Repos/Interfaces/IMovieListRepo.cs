using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using What2Watch.Dal.Repos.Base;

namespace What2Watch.Dal.Repos.Interfaces
{
    public interface IMovieListRepo:IBaseRepo<UserMovieList>
    {
      MovieEntity Find(string id);
      void ChangeStatus(MovieStatus status);
      int Add(string userId, MovieApiModel entity);
      IEnumerable<UserMovieList> GetUserList(string userId);
      int Update (string userId, UserMovieList entity);
      int Delete(string userId,int movieListId);
    }
}
