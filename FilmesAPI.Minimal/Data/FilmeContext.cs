using FilmesAPI.Minimal.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Minimal.Data
{
    public class FilmeContext: DbContext
    {
        public FilmeContext(DbContextOptions<FilmeContext> options) : base(options)
        {

        }

        public DbSet<Filme> Filmes { get; set; }
    }
}
