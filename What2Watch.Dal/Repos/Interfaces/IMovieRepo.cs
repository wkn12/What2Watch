using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace What2Watch.Dal.Repos.Interfaces
{
    public interface IMovieRepo:IBaseRepo<MovieEntity>
    {
        public MovieEntity Find(string id);
    }
}
