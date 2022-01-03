namespace FilmesAPI.FSharp.Data

open Microsoft.EntityFrameworkCore
open FilmesAPI.FSharp.Models

type FilmeContext(options: DbContextOptions) =
    inherit DbContext(options)

    [<DefaultValue>]
    val mutable private filmes : DbSet<Filme>
    member this.Filmes
        with get() = this.filmes
        and private set v = this.filmes <- v
