using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace What2Watch.Dal.Repos.Interfaces
{
    public interface IUserRepo:IBaseRepo<ApplicationUser>
    {
        public ApplicationUser Find(string id);
    }
}
