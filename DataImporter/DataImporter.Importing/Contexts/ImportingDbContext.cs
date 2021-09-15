using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataImporter.Importing.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataImporter.Importing.Contexts
{
    public class ImportingDbContext : DbContext, IImportingDbContext 
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public ImportingDbContext(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            if (!dbContextOptionsBuilder.IsConfigured)
            {
                dbContextOptionsBuilder.UseSqlServer(
                    _connectionString,
                    m => m.MigrationsAssembly(_migrationAssemblyName));
            }

            base.OnConfiguring(dbContextOptionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // one to many relationship
            modelBuilder.Entity<Group>()
                .HasMany(g => g.Contacts)
                .WithOne(c => c.Group);
            modelBuilder.Entity<Group>()
                .HasMany(g => g.ExcelFiles)
                .WithOne(E => E.Group);


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ExcelFile> Files { get; set; }
    }
}
