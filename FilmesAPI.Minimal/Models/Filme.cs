using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Minimal.Models;

public class Filme {
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public string Titulo { get; set; } = string.Empty;

    [Required]
    public string Genero { get; set; } = string.Empty;

    [StringLength(30)]
    public string Diretor { get; set; } = string.Empty;

    [Required]
    public int Duracao { get; set; }
}

