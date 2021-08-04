using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using TicketBookingSystem.Areas.Admin.Models;

namespace TicketBookingSystem
{
    public class WebModule : Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TicketListModel>().AsSelf();

            base.Load(builder);

        }
    }
}
        
