using SocialNetwork.Profiling.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Profiling.Services
{
    public interface IMemberService
    {
        void CreateMember(Member member);
        (IList<Member> records, int total, int totalDisplay) GetMembers(int pageIndex, int pageSize, string searchText, string sortText);
        Member GetMember(int id);
        void UpdateMember(Member member);
        void DeleteMember(int id);
    }
}
