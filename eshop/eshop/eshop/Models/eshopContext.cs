using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace eshop.Models
{
    public partial class eshopContext : DbContext
    {
        public eshopContext()
        {
        }

        public eshopContext(DbContextOptions<eshopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Articles> Articles { get; set; }
        public virtual DbSet<ArticlesPictures> ArticlesPictures { get; set; }
        public virtual DbSet<Pictures> Pictures { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=eshop;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity<Articles>(entity =>
            {
                entity.HasKey(e => e.AId);

                entity.Property(e => e.AId)
                    .HasColumnName("a_id")
                    .HasMaxLength(36)
                    .ValueGeneratedNever();

                entity.Property(e => e.ADescription)
                    .IsRequired()
                    .HasColumnName("a_description");

                entity.Property(e => e.APriceunit)
                    .HasColumnName("a_priceunit")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.AQtity)
                    .HasColumnName("a_qtity")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ATitle)
                    .IsRequired()
                    .HasColumnName("a_title")
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<ArticlesPictures>(entity =>
            {
                entity.HasKey(e => new { e.AcArtId, e.AcPicId })
                    .HasName("PK_article_Picture");

                entity.Property(e => e.AcArtId)
                    .HasColumnName("ac_art_id")
                    .HasMaxLength(36);

                entity.Property(e => e.AcPicId)
                    .HasColumnName("ac_pic_id")
                    .HasMaxLength(36);

                entity.HasOne(d => d.AcArt)
                    .WithMany(p => p.ArticlesPictures)
                    .HasForeignKey(d => d.AcArtId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ArticlesP__ac_ar__5CD6CB2B");

                entity.HasOne(d => d.AcPic)
                    .WithMany(p => p.ArticlesPictures)
                    .HasForeignKey(d => d.AcPicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ArticlesP__ac_pi__5DCAEF64");
            });

            modelBuilder.Entity<Pictures>(entity =>
            {
                entity.HasKey(e => e.PId);

                entity.Property(e => e.PId)
                    .HasColumnName("p_id")
                    .HasMaxLength(36)
                    .ValueGeneratedNever();

                entity.Property(e => e.PContent)
                    .IsRequired()
                    .HasColumnName("p_content");

                entity.Property(e => e.PDatecreation)
                    .HasColumnName("p_datecreation")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UId)
                    .HasName("PK_User");

                entity.HasIndex(e => e.ULogin)
                    .HasName("IX_User")
                    .IsUnique();

                entity.Property(e => e.UId)
                    .HasColumnName("u_id")
                    .HasMaxLength(36)
                    .ValueGeneratedNever();

                entity.Property(e => e.UAvatar).HasColumnName("u_avatar");

                entity.Property(e => e.UDatecreation)
                    .HasColumnName("u_datecreation")
                    .HasColumnType("datetime");

                entity.Property(e => e.ULastlogin)
                    .HasColumnName("u_lastlogin")
                    .HasColumnType("datetime");

                entity.Property(e => e.ULogin)
                    .IsRequired()
                    .HasColumnName("u_login")
                    .HasMaxLength(50);

                entity.Property(e => e.UPassword)
                    .IsRequired()
                    .HasColumnName("u_password")
                    .HasMaxLength(256);

                entity.Property(e => e.USalt)
                    .IsRequired()
                    .HasColumnName("u_salt")
                    .HasMaxLength(256);
            });
        }
    }
}
