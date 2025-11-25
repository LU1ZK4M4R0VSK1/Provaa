using Microsoft.EntityFrameworkCore;

namespace API.Models;

// Contexto de dados da aplicação, responsável pela comunicação com o banco de dados.
// Criado por Luiz Kamarovski
public class AppDataContext : DbContext
{
    // DbSet para a entidade Chamado, permitindo operações de CRUD na tabela de chamados.
    public DbSet<Chamado> Chamados { get; set; }

    // Configura o provedor de banco de dados a ser utilizado (SQLite).
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=LuizKamarovski_Chamados.db");
    }
}
