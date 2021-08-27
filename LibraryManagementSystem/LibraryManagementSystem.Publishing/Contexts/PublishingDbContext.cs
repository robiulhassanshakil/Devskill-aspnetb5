using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.Publishing.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Publishing.Contexts
{
    public class PublishingDbContext : DbContext, IPublishingDbContext
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public PublishingDbContext(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            if (!dbContextOptionsBuilder.IsConfigured)
            {
                dbContextOptionsBuilder.UseSqlServer(_connectionString,
                    m => m.MigrationsAssembly(_migrationAssemblyName));
            }
            base.OnConfiguring(dbContextOptionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<BookAuthors>()
                .HasKey(cs => new { cs.BookId, cs.AuthorId });

            modelBuilder.Entity<BookAuthors>()
                .HasOne(cs => cs.Book)
                .WithMany(c => c.Authors)
                .HasForeignKey(cs => cs.BookId);

            modelBuilder.Entity<BookAuthors>()
                .HasOne(cs => cs.Author)
                .WithMany(s => s.WrittenBooks)
                .HasForeignKey(cs => cs.AuthorId);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}
