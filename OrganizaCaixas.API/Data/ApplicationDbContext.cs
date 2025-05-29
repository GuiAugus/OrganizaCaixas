// Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using OrganizaCaixas.Data.Entities;

namespace OrganizaCaixas.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<CaixaEntity> Caixas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CaixaEntity>().HasData(
                new CaixaEntity { Id = 1, NomeCaixa = "Caixa 1", Altura = 30, Largura = 40, Comprimento = 80 },
                new CaixaEntity { Id = 2, NomeCaixa = "Caixa 2", Altura = 80, Largura = 50, Comprimento = 40 },
                new CaixaEntity { Id = 3, NomeCaixa = "Caixa 3", Altura = 50, Largura = 80, Comprimento = 60 }
            );
        }
    }
}