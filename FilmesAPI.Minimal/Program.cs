using FilmesAPI.Minimal.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

var filmes = new List<Filme>();
var id = 1;

app.MapPost("/filme", (Filme filme) =>
{
    filme = filme with { Id = id++ };
    filmes.Add(filme);
    return Results.CreatedAtRoute("RecuperaFilmePorId", new { Id = filme.Id }, filme);
});
app.MapGet("/filme", () =>
{
    return Results.Ok(filmes);
});
app.MapGet("/filme/{id:int}", (int id) =>
{
    var filme = filmes.FirstOrDefault(filme => filme.Id == id);
    if (filme is not null) return Results.Ok(filme);
    return Results.NotFound();
}).WithName("RecuperaFilmePorId");

app.Run();

