using SocialNetwork.Data;
using SocialNetwork.Profiling.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Profiling.Contexts;

namespace SocialNetwork.Profiling.UniteOfWorks
{
    public class ProfilingUniteOfWork : UnitOfWork, IProfilingUniteOfWork
    {
        public IMemberRepository Members { get; private set; }

        public IPhotoRepository Photos { get; private set; }
        public ProfilingUniteOfWork(IProfilingDbContext context,
            IMemberRepository members,
            IPhotoRepository photos) : base((DbContext)context)
        {
            Members = members;
            Photos = photos;
        }
    }
}
