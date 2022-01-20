﻿using Autofac;
using TicketBookingSystem.Booking.Contexts;
using TicketBookingSystem.Booking.Repositories;
using TicketBookingSystem.Booking.Services;
using TicketBookingSystem.Booking.UniteOfWorks;

namespace TicketBookingSystem.Booking
{
    public class BookingModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public BookingModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<BookingDbContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<BookingDbContext>().As<IBookingDbContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<BookingUniteOfWork>().As<IBookingUniteOfWork>()
                .InstancePerLifetimeScope();


            builder.RegisterType<TicketRepository>().As<ITicketRepository>()
                .InstancePerLifetimeScope();


            builder.RegisterType<TicketService>().As<ITicketService>()
                .InstancePerLifetimeScope();

            base.Load(builder);

        }

    }
}
