using FilmesAPI.Minimal.Data;
using FilmesAPI.Minimal.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<FilmeContext>(opt => opt.UseSqlite(builder.Configuration.GetConnectionString("FilmeConnection")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/filme", (FilmeContext db, Filme filme) =>
{
    db.Filmes.Add(filme);
    db.SaveChanges();
    return Results.CreatedAtRoute("RecuperaFilmePorId", new { Id = filme.Id }, filme);
});
app.MapGet("/filme", (FilmeContext db) =>
{
    return Results.Ok(db.Filmes);
});
app.MapGet("/filme/{id:int}", (FilmeContext db, int id) =>
{
    var filme = db.Filmes.FirstOrDefault(filme => filme.Id == id);
    if (filme is not null) return Results.Ok(filme);
    return Results.NotFound();
}).WithName("RecuperaFilmePorId");
app.MapPut("/filme/{id:int}", (FilmeContext db, int id, Filme filmeNovo) =>
{
    var filme = db.Filmes.FirstOrDefault(filme => filme.Id == id);
    if (filme is null) return Results.NotFound();
    filme.Titulo = filmeNovo.Titulo;
    filme.Genero = filmeNovo.Genero;
    filme.Duracao = filmeNovo.Duracao;
    filme.Diretor = filmeNovo.Diretor;
    db.SaveChanges();

    return Results.NoContent();
});
app.MapDelete("/filme/{id:int}", (FilmeContext db, int id) =>
{
    var filme = db.Filmes.FirstOrDefault(filme => filme.Id == id);
    if (filme is null) return Results.NotFound();

    db.Filmes.Remove(filme);
    db.SaveChanges();
    return Results.NoContent();
});

app.Run();

