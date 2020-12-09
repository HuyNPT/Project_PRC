using System;
using AudioStreaming.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AudioStreaming.Data.Context
{
    public partial class AudioStreamingContext : DbContext
    {
        public AudioStreamingContext()
        {
        }

        public AudioStreamingContext(DbContextOptions<AudioStreamingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Brand> Brand { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<CategoryMedia> CategoryMedia { get; set; }
        public virtual DbSet<CategoryPlaylist> CategoryPlaylist { get; set; }
        public virtual DbSet<CurrentMediaInStore> CurrentMediaInStore { get; set; }
        public virtual DbSet<FavoritePlaylist> FavoritePlaylist { get; set; }
        public virtual DbSet<Media> Media { get; set; }
        public virtual DbSet<Playlist> Playlist { get; set; }
        public virtual DbSet<PlaylistDetail> PlaylistDetail { get; set; }
        public virtual DbSet<PlaylistInStore> PlaylistInStore { get; set; }
        public virtual DbSet<Store> Store { get; set; }
        public virtual DbSet<TimeSubmit> TimeSubmit { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FcmToken)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FirebaseProvider)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirebaseUid)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.IsVip).HasColumnName("IsVIP");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(11)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.BrandName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ImageUrl)
                    .HasColumnName("imageURL")
                    .HasMaxLength(900)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<CategoryMedia>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.CategoryMedia)
                    .HasForeignKey(x => x.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CategoryMedia_Category");

                entity.HasOne(d => d.Media)
                    .WithMany(p => p.CategoryMedia)
                    .HasForeignKey(x => x.MediaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CategoryMedia_Media");
            });

            modelBuilder.Entity<CategoryPlaylist>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.CategoryPlaylist)
                    .HasForeignKey(x => x.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CategoryPlaylist_Category");

                entity.HasOne(d => d.Playlist)
                    .WithMany(p => p.CategoryPlaylist)
                    .HasForeignKey(x => x.PlaylistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CategoryPlaylist_Playlist");
            });

            modelBuilder.Entity<CurrentMediaInStore>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.TimeEnd).HasColumnType("datetime");

                entity.Property(e => e.TimeStart).HasColumnType("datetime");

                entity.HasOne(d => d.Media)
                    .WithMany(p => p.CurrentMediaInStore)
                    .HasForeignKey(x => x.MediaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CurrentMediaInStore_Media");

                entity.HasOne(d => d.Playlist)
                    .WithMany(p => p.CurrentMediaInStore)
                    .HasForeignKey(x => x.PlaylistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CurrentMediaInStore_Playlist");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.CurrentMediaInStore)
                    .HasForeignKey(x => x.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CurrentMediaInStore_Store");
            });

            modelBuilder.Entity<FavoritePlaylist>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.FavoritePlaylist)
                    .HasForeignKey(x => x.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FavoritePlaylist_Account");

                entity.HasOne(d => d.Playlist)
                    .WithMany(p => p.FavoritePlaylist)
                    .HasForeignKey(x => x.PlaylistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FavoritePlaylist_Playlist");
            });

            modelBuilder.Entity<Media>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Author).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.FileName).HasMaxLength(200);

                entity.Property(e => e.ImageUrl)
                    .HasColumnName("ImageURL")
                    .HasMaxLength(700)
                    .IsUnicode(false);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.MusicName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Singer).HasMaxLength(50);

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasColumnName("URL")
                    .HasMaxLength(300)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Playlist>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.ImageUrl).IsUnicode(false);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.PlaylistName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Playlist)
                    .HasForeignKey(x => x.BrandId)
                    .HasConstraintName("FK_Playlist_Brand");
            });

            modelBuilder.Entity<PlaylistDetail>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.HasOne(d => d.Media)
                    .WithMany(p => p.PlaylistDetail)
                    .HasForeignKey(x => x.MediaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlaylistDetail_Media");

                entity.HasOne(d => d.Playlist)
                    .WithMany(p => p.PlaylistDetail)
                    .HasForeignKey(x => x.PlaylistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlaylistDetail_Playlist");
            });

            modelBuilder.Entity<PlaylistInStore>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.HasOne(d => d.Playlist)
                    .WithMany(p => p.PlaylistInStore)
                    .HasForeignKey(x => x.PlaylistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlaylistInStore_Playlist");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.PlaylistInStore)
                    .HasForeignKey(x => x.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PlaylistInStore_Store");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.PassWifi)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.StoreName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Wifi)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Store)
                    .HasForeignKey(x => x.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Store_Brand");
            });

            modelBuilder.Entity<TimeSubmit>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.TimeSubmit1)
                    .HasColumnName("TimeSubmit")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.TimeSubmit)
                    .HasForeignKey(x => x.StoreId)
                    .HasConstraintName("FK_TimeSubmit_Store");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TimeSubmit)
                    .HasForeignKey(x => x.UserId)
                    .HasConstraintName("FK_TimeSubmit_Account1");
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
