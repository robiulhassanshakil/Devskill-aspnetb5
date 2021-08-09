using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Data;
using SocialNetwork.Profiling.Contexts;
using SocialNetwork.Profiling.Entities;

namespace SocialNetwork.Profiling.Repositories
{
    public class PhotoRepository : Repository<Photo, int>,
        IPhotoRepository
    {
        public PhotoRepository(IProfilingDbContext context)
            : base((DbContext)context)
        {

        }
    }
}
