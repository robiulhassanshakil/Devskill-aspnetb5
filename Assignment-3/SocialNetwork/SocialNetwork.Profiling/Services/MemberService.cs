using SocialNetwork.Profiling.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.Profiling.UniteOfWorks;

namespace SocialNetwork.Profiling.Services
{
    public class MemberService : IMemberService
    {
        private readonly IProfilingUniteOfWork _profilingUniteOfWork;

        public MemberService(IProfilingUniteOfWork profilingUniteOfWork)
        {
            _profilingUniteOfWork = profilingUniteOfWork;
        }

        public void CreateMember(Member member)
        {
            _profilingUniteOfWork.Members.Add(new Entities.Member()
            {
                Name = member.Name,
                DateOfBirth = member.DateOfBirth,
                Address = member.Address
            });
            _profilingUniteOfWork.Save();
        }

        public (IList<Member> records, int total, int totalDisplay) GetMembers(int pageIndex, int pageSize, string searchText, string sortText)
        {
            var memberData = _profilingUniteOfWork.Members.GetDynamic(
                string.IsNullOrWhiteSpace(searchText) ? null : x => x.Name.Contains(searchText),
                sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from member in memberData.data
                select (new Member()
                {
                    Id = member.Id,
                    Name = member.Name,
                    DateOfBirth = member.DateOfBirth,
                    Address = member.Address
                })).ToList();

            return (resultData, memberData.total, memberData.totalDisplay);
        }
    }
}
