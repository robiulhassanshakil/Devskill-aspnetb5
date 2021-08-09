using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.Data;
using SocialNetwork.Profiling.Repositories;

namespace SocialNetwork.Profiling.UniteOfWorks
{
    public interface IProfilingUniteOfWork:IUnitOfWork
    {
        IMemberRepository Members { get; }
        IPhotoRepository Photos { get; }

    }
}
