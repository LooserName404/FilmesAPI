using AutoMapper;
using FilmesAPI.Minimal.Data;
using FilmesAPI.Minimal.Data.Dtos;
using FilmesAPI.Minimal.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<FilmeContext>(opt => opt.UseSqlite(builder.Configuration.GetConnectionString("FilmeConnection")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/filme", (FilmeContext db, IMapper mapper, CreateFilmeDto filmeDto) =>
{
    var filme = mapper.Map<Filme>(filmeDto);
    db.Filmes.Add(filme);
    db.SaveChanges();
    return Results.CreatedAtRoute("RecuperaFilmePorId", new { Id = filme.Id }, filme);
});
app.MapGet("/filme", (FilmeContext db) =>
{
    return Results.Ok(db.Filmes);
});
app.MapGet("/filme/{id:int}", (FilmeContext db, IMapper mapper, int id) =>
{
    var filme = db.Filmes.FirstOrDefault(filme => filme.Id == id);
    if (filme is not null)
    {
        var filmeDto = mapper.Map<ReadFilmeDto>(filme);
        filmeDto.HoraDaConsulta = DateTime.Now;
        return Results.Ok(filmeDto);
    }
    return Results.NotFound();
}).WithName("RecuperaFilmePorId");
app.MapPut("/filme/{id:int}", (FilmeContext db, IMapper mapper, int id, UpdateFilmeDto filmeDto) =>
{
    var filme = db.Filmes.FirstOrDefault(filme => filme.Id == id);
    if (filme is null) return Results.NotFound();
    mapper.Map(filmeDto, filme);
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

