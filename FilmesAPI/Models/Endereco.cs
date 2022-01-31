using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FilmesAPI.Models;

public class Endereco
{
    [Key]
    [Required]
    public int Id { get; set; }

    public string Logradouro { get; set; } = string.Empty;

    public string Bairro { get; set; } = string.Empty;

    public int Numero { get; set; }

    [JsonIgnore]
    public virtual Cinema Cinema { get; set; }
}
