using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Minimal.Models;

public record Filme (
    [Required] string Titulo,
    [Required] string Genero,
    [StringLength(30)] string Diretor,
    [Required] int Duracao
);

