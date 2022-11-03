using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MovieAPI.Models
{
    public partial class MovieListContext : DbContext
    {
        public MovieListContext()
        {
        }

        public MovieListContext(DbContextOptions<MovieListContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MovieList> MovieLists { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=MovieList;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieList>(entity =>
            {
                entity.ToTable("MovieList");

                entity.Property(e => e.Genre).HasMaxLength(30);

                entity.Property(e => e.Picture).HasColumnType("image");

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.Year)
                    .HasMaxLength(4)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
