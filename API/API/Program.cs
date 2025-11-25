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

app.MapGet("/", () => "LuizKamarovski");

//ENDPOINTS DE TAREFA
//GET: http://localhost:5273/api/chamado/listar
app.MapGet("/api/chamado/listar", ([FromServices] AppDataContext ctx) =>
{
    if (ctx.Chamados.Any())
    {
        return Results.Ok(ctx.Chamados.ToList());
    }
    return Results.NotFound();
});

//POST: http://localhost:5273/api/chamado/cadastrar
app.MapPost("/api/chamado/cadastrar", ([FromServices] AppDataContext ctx, [FromBody] Chamado chamado) =>
{
    chamado.Status = "Aberto";
    ctx.Chamados.Add(chamado);
    ctx.SaveChanges();
    return Results.Created("", chamado);
});

//PUT: http://localhost:5273/chamado/alterar/{id}
app.MapPut("/api/chamado/alterar/{id}", ([FromServices] AppDataContext ctx, [FromRoute] string id) =>
{
    var chamado = ctx.Chamados.Find(id);
    if (chamado is null) return Results.NotFound();

    if (chamado.Status == "Aberto") chamado.Status = "Em atendimento";
    else if (chamado.Status == "Em atendimento") chamado.Status = "Resolvido";

    ctx.Chamados.Update(chamado);
    ctx.SaveChanges();
    return Results.Ok(chamado);
});

//GET: http://localhost:5273/chamado/naoconcluidas
app.MapGet("/api/chamado/naoresolvidos", ([FromServices] AppDataContext ctx) =>
{
    //Implementar a listagem dos chamados não resolvidos
    return Results.Ok(ctx.Chamados.Where(x => x.Status != "Resolvido").ToList());
});

//GET: http://localhost:5273/chamado/concluidas
app.MapGet("/api/chamado/resolvidos", ([FromServices] AppDataContext ctx) =>
{
    //Implementar a listagem dos chamados resolvidos
    return Results.Ok(ctx.Chamados.Where(x => x.Status == "Resolvido").ToList());
});

app.UseCors("AcessoTotal");

app.Run();
