using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TesteElano.Models
{
    public partial class TesteElanoContext : DbContext
    {
        public TesteElanoContext()
        {
        }

        public TesteElanoContext(DbContextOptions<TesteElanoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Desenvolvedor> Desenvolvedor { get; set; }
        public virtual DbSet<LancamentoHoras> LancamentoHoras { get; set; }
        public virtual DbSet<Projeto> Projeto { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Desenvolvedor>(entity =>
            {
                entity.Property(e => e.Cpf)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.DtNascimento).HasColumnType("datetime");

                entity.Property(e => e.Nome)
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.HasOne(d => d.Projeto)
                    .WithMany(p => p.Desenvolvedor)
                    .HasForeignKey(d => d.ProjetoId)
                    .HasConstraintName("FK__Desenvolv__Proje__0519C6AF");
            });

            modelBuilder.Entity<LancamentoHoras>(entity =>
            {
                entity.HasKey(e => e.LancamentoHorasDesenvolvedorId)
                    .HasName("PK__Lancamen__29730D9A07F6335A");

                entity.Property(e => e.DtFim).HasColumnType("datetime");

                entity.Property(e => e.DtInicio).HasColumnType("datetime");

                entity.HasOne(d => d.Desenvolvedor)
                    .WithMany(p => p.LancamentoHoras)
                    .HasForeignKey(d => d.DesenvolvedorId)
                    .HasConstraintName("FK__Lancament__Desen__09DE7BCC");

                entity.HasOne(d => d.Projeto)
                    .WithMany(p => p.LancamentoHoras)
                    .HasForeignKey(d => d.ProjetoId)
                    .HasConstraintName("FK__Lancament__Proje__0AD2A005");
            });

            modelBuilder.Entity<Projeto>(entity =>
            {
                entity.Property(e => e.DtFim).HasColumnType("datetime");

                entity.Property(e => e.DtInicio).HasColumnType("datetime");

                entity.Property(e => e.Nome)
                    .HasMaxLength(55)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
