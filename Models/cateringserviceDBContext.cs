using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Catering.Models
{
    public partial class cateringserviceDBContext : DbContext
    {
        public cateringserviceDBContext()
        {
        }

        public cateringserviceDBContext(DbContextOptions<cateringserviceDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CateringDetail> CateringDetail { get; set; }
        public virtual DbSet<Cateringler> Cateringler { get; set; }
        public virtual DbSet<Kullanicilar> Kullanicilar { get; set; }
        public virtual DbSet<Menuler> Menuler { get; set; }
        public virtual DbSet<Sayfalar> Sayfalar { get; set; }
        public virtual DbSet<Yorum> Yorum { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Server=DESKTOP-SK8NDD6\\SQLEXPRESS;Database=cateringserviceDB;uid=sa;password=123456");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CateringDetail>(entity =>
            {
                entity.HasKey(e => e.TurId);

                entity.Property(e => e.TurId).HasColumnName("turID");

                entity.Property(e => e.Aktif).HasColumnName("aktif");

                entity.Property(e => e.CateringId).HasColumnName("cateringID");

                entity.Property(e => e.Cateringfirmabilgi)
                    .HasColumnName("cateringfirmabilgi")
                    .HasMaxLength(250);

                entity.Property(e => e.Eklemetarihi)
                    .HasColumnName("eklemetarihi")
                    .HasColumnType("datetime");

                entity.Property(e => e.Icerik)
                    .HasColumnName("icerik")
                    .HasColumnType("ntext");

                entity.Property(e => e.Silindi).HasColumnName("silindi");

                entity.Property(e => e.Sira).HasColumnName("sira");

                entity.HasOne(d => d.Catering)
                    .WithMany(p => p.CateringDetail)
                    .HasForeignKey(d => d.CateringId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CateringDetail_Cateringler");
            });

            modelBuilder.Entity<Cateringler>(entity =>
            {
                entity.HasKey(e => e.CateringId);

                entity.Property(e => e.CateringId).HasColumnName("cateringID");

                entity.Property(e => e.Aktif).HasColumnName("aktif");

                entity.Property(e => e.Cateringadi)
                    .HasColumnName("cateringadi")
                    .HasMaxLength(100);

                entity.Property(e => e.Silindi).HasColumnName("silindi");
            });

            modelBuilder.Entity<Kullanicilar>(entity =>
            {
                entity.HasKey(e => e.KullaniciId);

                entity.Property(e => e.KullaniciId).HasColumnName("kullaniciID");

                entity.Property(e => e.Adi)
                    .HasColumnName("adi")
                    .HasMaxLength(100);

                entity.Property(e => e.Aktif).HasColumnName("aktif");

                entity.Property(e => e.Eposta)
                    .HasColumnName("eposta")
                    .HasMaxLength(100);

                entity.Property(e => e.Parola)
                    .HasColumnName("parola")
                    .HasMaxLength(35);

                entity.Property(e => e.Silindi).HasColumnName("silindi");

                entity.Property(e => e.Soyadi)
                    .HasColumnName("soyadi")
                    .HasMaxLength(100);

                entity.Property(e => e.Telefon)
                    .HasColumnName("telefon")
                    .HasMaxLength(15);

                entity.Property(e => e.Yetki).HasColumnName("yetki");
            });

            modelBuilder.Entity<Menuler>(entity =>
            {
                entity.HasKey(e => e.MenuId);

                entity.ToTable("menuler");

                entity.Property(e => e.MenuId).HasColumnName("menuID");

                entity.Property(e => e.Aktif).HasColumnName("aktif");

                entity.Property(e => e.Baslik)
                    .HasColumnName("baslik")
                    .HasMaxLength(250);

                entity.Property(e => e.Silindi).HasColumnName("silindi");

                entity.Property(e => e.Sira).HasColumnName("sira");

                entity.Property(e => e.Url)
                    .HasColumnName("url")
                    .HasMaxLength(255);

                entity.Property(e => e.UstId).HasColumnName("ustID");
            });

            modelBuilder.Entity<Sayfalar>(entity =>
            {
                entity.HasKey(e => e.SayfaId);

                entity.Property(e => e.SayfaId).HasColumnName("sayfaID");

                entity.Property(e => e.Aktif).HasColumnName("aktif");

                entity.Property(e => e.Baslik)
                    .HasColumnName("baslik")
                    .HasMaxLength(250);

                entity.Property(e => e.Icerik)
                    .HasColumnName("icerik")
                    .HasColumnType("ntext");

                entity.Property(e => e.Silindi).HasColumnName("silindi");
            });

            modelBuilder.Entity<Yorum>(entity =>
            {
                entity.Property(e => e.YorumId).HasColumnName("yorumID");

                entity.Property(e => e.Aktif).HasColumnName("aktif");

                entity.Property(e => e.Eklemetarihi)
                    .HasColumnName("eklemetarihi")
                    .HasColumnType("datetime");

               

                entity.Property(e => e.Silindi).HasColumnName("silindi");

                entity.Property(e => e.TurId).HasColumnName("turID");

                entity.Property(e => e.UyeId).HasColumnName("uyeID");

                entity.Property(e => e.Yorum1)
                    .HasColumnName("yorum")
                    .HasMaxLength(500);

                entity.HasOne(d => d.Tur)
                    .WithMany(p => p.Yorum)
                    .HasForeignKey(d => d.TurId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Yorum_CateringDetail");

                entity.HasOne(d => d.Uye)
                    .WithMany(p => p.Yorum)
                    .HasForeignKey(d => d.UyeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Yorum_Kullanicilar");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
