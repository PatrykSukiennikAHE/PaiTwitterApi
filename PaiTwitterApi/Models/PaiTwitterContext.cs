using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PaiTwitterApi.Models
{
    public partial class PaiTwitterContext : DbContext
    {

        public PaiTwitterContext(DbContextOptions<PaiTwitterContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TUser> TUser { get; set; }
        public virtual DbSet<TComment> TComment { get; set; }
        public virtual DbSet<TFollow> TFollow { get; set; }
        public virtual DbSet<TLike> TLike { get; set; }
        public virtual DbSet<TPost> TPost { get; set; }
        public virtual DbSet<TNotification> TNotification { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__tUser__1788CC4C65F92F39");

                entity.ToTable("tUser");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.LastActivity).HasColumnType("datetime");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
