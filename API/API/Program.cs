using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();

builder.Services.AddCors(options =>
{
   options.AddPolicy("AcessoTotal", builder =>
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

app.UseCors("AcessoTotal");

app.Run();

public record ChamadoDto(string descricao);
// Retorna uma lista de todos os chamados cadastrados.
app.MapGet("/api/chamado/listar", ([FromServices] AppDataContext ctx) =>
{
    if (ctx.Chamados.Any())
    {
        return Results.Ok(ctx.Chamados.ToList());
    }
    return Results.NotFound("Nenhum chamado encontrado.");
});

// Criado por Luiz Kamarovski
//POST: http://localhost:5273/api/chamado/cadastrar
// Cadastra um novo chamado no sistema.
app.MapPost("/api/chamado/cadastrar", ([FromServices] AppDataContext ctx, [FromBody] ChamadoDto novoChamado) =>
{
    var chamado = new Chamado { descricao = novoChamado.descricao };
    ctx.Chamados.Add(chamado);
    ctx.SaveChanges();
    return Results.Created("", chamado);
});

// Criado por Luiz Kamarovski
//PATCH: http://localhost:5273/api/chamado/alterar
// Altera o status de um chamado. A transição de status é: Aberto -> Em atendimento -> Resolvido.
app.MapPatch("/api/chamado/alterar", ([FromServices] AppDataContext ctx, [FromBody] Chamado chamadoAlterado) =>
{
    var chamado = ctx.Chamados.Find(chamadoAlterado.id);
    if (chamado is null) return Results.NotFound("Chamado não encontrado.");

    if (chamado.status == "Aberto")
    {
        chamado.status = "Em atendimento";
    }
    else if (chamado.status == "Em atendimento")
    {
        chamado.status = "Resolvido";
    }

    ctx.Chamados.Update(chamado);
    ctx.SaveChanges();
    return Results.Ok(chamado);
});

// Criado por Luiz Kamarovski
//GET: http://localhost:5273/api/chamado/naoresolvido
// Retorna uma lista de chamados que não foram resolvidos (status "Aberto" ou "Em atendimento").
app.MapGet("/api/chamado/naoresolvido", ([FromServices] AppDataContext ctx) =>
{
    var naoResolvidos = ctx.Chamados.Where(x => x.status == "Aberto" || x.status == "Em atendimento").ToList();
    if (naoResolvidos.Any())
    {
        return Results.Ok(naoResolvidos);
    }
    return Results.NotFound("Nenhum chamado não resolvido encontrado.");
});

// Criado por Luiz Kamarovski
//GET: http://localhost:5273/api/chamado/resolvidos
// Retorna uma lista de todos os chamados que já foram resolvidos.
app.MapGet("/api/chamado/resolvidos", ([FromServices] AppDataContext ctx) =>
{
    var resolvidos = ctx.Chamados.Where(x => x.status == "Resolvido").ToList();
    if (resolvidos.Any())
    {
        return Results.Ok(resolvidos);
    }
    return Results.NotFound("Nenhum chamado resolvido encontrado.");
});

app.UseCors("AcessoTotal");

app.Run();
