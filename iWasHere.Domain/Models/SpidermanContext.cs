using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace iWasHere.Domain.Models
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

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Currency> Currency { get; set; }
        public virtual DbSet<DictionaryCategory> DictionaryCategory { get; set; }
        public virtual DbSet<DictionaryCity> DictionaryCity { get; set; }
        public virtual DbSet<DictionaryCountry> DictionaryCountry { get; set; }
        public virtual DbSet<DictionaryCounty> DictionaryCounty { get; set; }
        public virtual DbSet<DictionaryCurrency> DictionaryCurrency { get; set; }
        public virtual DbSet<DictionaryLandmarkType> DictionaryLandmarkType { get; set; }
        public virtual DbSet<DictionaryPayment> DictionaryPayment { get; set; }
        public virtual DbSet<DictionarySeason> DictionarySeason { get; set; }
        public virtual DbSet<IepurasulAdrian> IepurasulAdrian { get; set; }
        public virtual DbSet<Image> Image { get; set; }
        public virtual DbSet<Review> Review { get; set; }
        public virtual DbSet<Schedule> Schedule { get; set; }
        public virtual DbSet<Ticket> Ticket { get; set; }
        public virtual DbSet<TicketPayment> TicketPayment { get; set; }
        public virtual DbSet<TouristAttraction> TouristAttraction { get; set; }

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

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.Property(e => e.CurrencyCode)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Data).HasColumnType("datetime");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.HasOne(d => d.DictionaryCurrency)
                    .WithMany(p => p.Currency)
                    .HasForeignKey(d => d.DictionaryCurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Currency__Dictio__28B808A7");
            });

            modelBuilder.Entity<DictionaryCategory>(entity =>
            {
                entity.Property(e => e.DictionaryCategoryName)
                    .IsRequired()
                    .HasMaxLength(22)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DictionaryCity>(entity =>
            {
                entity.Property(e => e.DictionaryCityName)
                    .IsRequired()
                    .HasMaxLength(22)
                    .IsUnicode(false);

                entity.HasOne(d => d.DictionaryCountry)
                    .WithMany(p => p.DictionaryCity)
                    .HasForeignKey(d => d.DictionaryCountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Dictionar__Dicti__0EF836A4");
            });

            modelBuilder.Entity<DictionaryCountry>(entity =>
            {
                entity.Property(e => e.DictionaryCountryCode)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.DictionaryCountryName)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DictionaryCounty>(entity =>
            {
                entity.Property(e => e.DictionaryCountyName)
                    .IsRequired()
                    .HasMaxLength(22)
                    .IsUnicode(false);

                entity.HasOne(d => d.DictionaryCountry)
                    .WithMany(p => p.DictionaryCounty)
                    .HasForeignKey(d => d.DictionaryCountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Dictionar__Dicti__0C1BC9F9");
            });

            modelBuilder.Entity<DictionaryCurrency>(entity =>
            {
                entity.Property(e => e.DictionaryCurrencyCode)
                    .IsRequired()
                    .HasMaxLength(22)
                    .IsUnicode(false);

                entity.Property(e => e.DictionaryCurrencyName)
                    .IsRequired()
                    .HasMaxLength(22)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DictionaryLandmarkType>(entity =>
            {
                entity.HasKey(e => e.DictionaryItemId)
                    .HasName("PK__Dictiona__3857E8A4BE266A8F");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DictionaryItemCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DictionaryItemName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DictionaryPayment>(entity =>
            {
                entity.Property(e => e.DictionaryPaymentType)
                    .IsRequired()
                    .HasMaxLength(22)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DictionarySeason>(entity =>
            {
                entity.Property(e => e.DictionarySeasonName)
                    .IsRequired()
                    .HasMaxLength(22)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<IepurasulAdrian>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.NumeleIepurasului).IsUnicode(false);
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.TouristAttraction)
                    .WithMany(p => p.Image)
                    .HasForeignKey(d => d.TouristAttractionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Image__TouristAt__25DB9BFC");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.Property(e => e.Comment)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.Property(e => e.UserName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.TouristAttraction)
                    .WithMany(p => p.Review)
                    .HasForeignKey(d => d.TouristAttractionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Review__TouristA__22FF2F51");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Review)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Review__UserId__220B0B18");
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.Property(e => e.Day)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Season)
                    .WithMany(p => p.Schedule)
                    .HasForeignKey(d => d.SeasonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Schedule__Season__1F2E9E6D");

                entity.HasOne(d => d.TouristAttraction)
                    .WithMany(p => p.Schedule)
                    .HasForeignKey(d => d.TouristAttractionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Schedule__Touris__1E3A7A34");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Ticket)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ticket__Currency__178D7CA5");

                entity.HasOne(d => d.TouristAttraction)
                    .WithMany(p => p.Ticket)
                    .HasForeignKey(d => d.TouristAttractionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ticket__TouristA__1699586C");
            });

            modelBuilder.Entity<TicketPayment>(entity =>
            {
                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.TicketPayment)
                    .HasForeignKey(d => d.PaymentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TicketPay__Payme__1B5E0D89");

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.TicketPayment)
                    .HasForeignKey(d => d.TicketId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TicketPay__Ticke__1A69E950");
            });

            modelBuilder.Entity<TouristAttraction>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(220)
                    .IsUnicode(false);

                entity.Property(e => e.Latitudine)
                    .HasMaxLength(22)
                    .IsUnicode(false);

                entity.Property(e => e.Longtitudine)
                    .HasMaxLength(22)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(22)
                    .IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.TouristAttraction)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__TouristAt__Categ__11D4A34F");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.TouristAttraction)
                    .HasForeignKey(d => d.CityId)
                    .HasConstraintName("FK__TouristAt__CityI__12C8C788");

                entity.HasOne(d => d.Landmark)
                    .WithMany(p => p.TouristAttraction)
                    .HasForeignKey(d => d.LandmarkId)
                    .HasConstraintName("FK__TouristAt__Landm__13BCEBC1");
            });
        }
    }
}
