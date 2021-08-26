using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using SocialNetwork.Profiling.BusinessObjects;
using SocialNetwork.Profiling.Services;

namespace SocialNetwork.Web.Areas.Admin.Models
{
    public class EditMemberModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }

        private readonly IMemberService _memberService;

        public EditMemberModel()
        {
            _memberService = Startup.AutofacContainer.Resolve<IMemberService>();
        }
        public EditMemberModel(IMemberService memberService)
        {
            _memberService = memberService;
        }
        internal void LoadModelData(int id)
        {
            var member = _memberService.GetMember(id);

            Id = member.Id;
            Name = member.Name;
            DateOfBirth = member.DateOfBirth;
            Address = member.Address;
        }

        internal void Update()
        {
            var member = new Member()
            {
                Id = Id,
                Name = Name,
                DateOfBirth = DateOfBirth,
                Address = Address
            };

                _memberService.UpdateMember(member);
        }
    }
}
