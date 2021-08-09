using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using SocialNetwork.Profiling.BusinessObjects;
using SocialNetwork.Profiling.Services;

namespace SocialNetwork.Web.Areas.Admin.Models
{
    public class CreateMemberModel
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }

        private readonly IMemberService _memberService;

        public CreateMemberModel()
        {
            _memberService = Startup.AutofacContainer.Resolve<IMemberService>();
        }
        public CreateMemberModel(IMemberService memberService)
        {
            _memberService = memberService;
        }

        public void CreateMember()
        {
            var member = new Member()
            {
                Name = Name,
                DateOfBirth = DateOfBirth,
                Address = Address
            };

            _memberService.CreateMember(member);
        }


    }
}
