using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Commons.DbAccesorContext
{
    public partial class PlayDbContext : DbContext
    {
        public PlayDbContext()
        {
        }

        public PlayDbContext(DbContextOptions<PlayDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Dataimage> Dataimages { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Student2> Student2s { get; set; }
        public virtual DbSet<Studnet3> Studnet3s { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=master;Trusted_Connection=True;Initial Catalog=PlayGround;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Japanese_CI_AS");

            modelBuilder.Entity<Dataimage>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("DATAIMAGE");

                entity.Property(e => e.Filecontenttype)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("filecontenttype");

                entity.Property(e => e.Filetype)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("filetype");

                entity.Property(e => e.Image).HasColumnType("image");

                entity.Property(e => e.Name)
                    .HasMaxLength(64)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Student");

                entity.Property(e => e.Height)
                    .HasMaxLength(10)
                    .IsFixedLength(true);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Student2>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Student2");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Studnet3>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Studnet3");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
