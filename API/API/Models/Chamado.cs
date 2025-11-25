using System.ComponentModel.DataAnnotations;

namespace API.Models;

// Modelo de dados para representar um Chamado
// Criado por Luiz Kamarovski
public class Chamado
{
    // Identificador único do chamado
    [Key]
    public int id { get; set; }

    // Descrição detalhada do problema ou solicitação
    public string? descricao { get; set; }

    // Status atual do chamado (Ex: Aberto, Em atendimento, Resolvido)
    public string? status { get; set; } = "Aberto";
}
