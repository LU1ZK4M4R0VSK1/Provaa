using Microsoft.EntityFrameworkCore;

namespace API.Models;

public class AppDataContext : DbContext
{
    public DbSet<Chamado> Chamados { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=LuizKamarovski_Chamados.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chamado>().HasData(
            new Chamado { ChamadoId = "f4a2b8d9-7c4e-4f8e-bdc9-9181e456ad0e", Descricao = "TESTE", CriadoEm = DateTime.Now, Status = "Aberto" }
        );
    }
}
