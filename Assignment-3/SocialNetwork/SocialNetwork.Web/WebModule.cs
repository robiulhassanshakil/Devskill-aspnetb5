using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using SocialNetwork.Web.Areas.Admin.Models;

namespace SocialNetwork.Web
{
    public class WebModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MemberListModel>().AsSelf();

            base.Load(builder);
        }
    }
}
