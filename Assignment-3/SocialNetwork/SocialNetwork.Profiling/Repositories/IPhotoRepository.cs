using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.Data;
using SocialNetwork.Profiling.Entities;

namespace SocialNetwork.Profiling.Repositories
{
    public interface IPhotoRepository : IRepository<Photo, int>
    {
    }
}
