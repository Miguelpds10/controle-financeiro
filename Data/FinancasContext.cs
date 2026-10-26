using Microsoft.EntityFrameworkCore;
using ControleFinanceiro.Models;

namespace ControleFinanceiro.Data
{
    public class FinancasContext : DbContext
    {
        public FinancasContext(DbContextOptions<FinancasContext> options)
            : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Conta> Contas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Registra o enum nativo do Postgres
            modelBuilder.HasPostgresEnum<TipoCategoria>("tipo_categoria", nameTranslator: null);

            // ---------- USUARIO ----------
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuarios");
                entity.Property(u => u.Id).HasColumnName("id");
                entity.Property(u => u.Nome).HasColumnName("nome");
                entity.Property(u => u.Email).HasColumnName("email");
                entity.Property(u => u.SenhaHash).HasColumnName("senha_hash");
            });

            // ---------- CONTA ----------
            modelBuilder.Entity<Conta>(entity =>
            {
                entity.ToTable("contas");
                entity.Property(c => c.Id).HasColumnName("id");
                entity.Property(c => c.Nome).HasColumnName("nome");
                entity.Property(c => c.Saldo).HasColumnName("saldo_inicial");
                entity.Property(c => c.UsuarioId).HasColumnName("usuario_id");
            });

            // ---------- CATEGORIA ----------
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.ToTable("categorias");
                entity.Property(c => c.Id).HasColumnName("id");
                entity.Property(c => c.Nome).HasColumnName("nome");
                entity.Property(c => c.Tipo).HasColumnName("tipo");
                entity.Property(c => c.UsuarioId).HasColumnName("usuario_id");
            });

            // ---------- TRANSACAO ----------
            modelBuilder.Entity<Transacao>(entity =>
            {
                entity.ToTable("transacoes");
                entity.Property(t => t.Id).HasColumnName("id");
                entity.Property(t => t.Descricao).HasColumnName("descricao");
                entity.Property(t => t.Valor).HasColumnName("valor");
                entity.Property(t => t.Date).HasColumnName("data"); // atenção: propriedade "Date" -> coluna "data"
                entity.Property(t => t.ContaId).HasColumnName("conta_id");
                entity.Property(t => t.CategoriaId).HasColumnName("categoria_id");
                entity.Property(t => t.CriadoEm).HasColumnName("criado_em");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}