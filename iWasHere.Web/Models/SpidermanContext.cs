using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace iWasHere.Web.Models
{
    public partial class SpidermanContext : DbContext
    {
        public SpidermanContext()
        {
        }

        public SpidermanContext(DbContextOptions<SpidermanContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DictionaryCity> DictionaryCity { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=ts-internship-2019.database.windows.net;Initial Catalog=Spiderman;Persist Security Info=False;User ID=sa_admin;Password=A123456a;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<DictionaryCity>(entity =>
            {
                entity.Property(e => e.DictionaryCityName)
                    .IsRequired()
                    .HasMaxLength(22)
                    .IsUnicode(false);
            });
        }
    }
}
