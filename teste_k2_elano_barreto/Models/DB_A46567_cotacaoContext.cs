using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace teste_k2_elano_barreto.Models
{
    public partial class DB_A46567_cotacaoContext : DbContext
    {
        public DB_A46567_cotacaoContext()
        {
        }

        public DB_A46567_cotacaoContext(DbContextOptions<DB_A46567_cotacaoContext> options)
            : base(options)
        {
        }
        
        public virtual DbSet<Filme> Filme { get; set; }
        public virtual DbSet<Genero> Genero { get; set; }        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=SQL5052.site4now.net;Database=DB_A46567_cotacao;User Id=DB_A46567_cotacao_admin;Password=elise2013;MultipleActiveResultSets=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Filme>(entity =>
            {
                entity.Property(e => e.DataAluguel).HasColumnType("datetime");

                entity.Property(e => e.DateEntrega).HasColumnType("datetime");

                entity.Property(e => e.Genero)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Imagem)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Sinopse)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Titulo)
                    .HasMaxLength(80)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Genero>(entity =>
            {
                entity.Property(e => e.Descricao)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
