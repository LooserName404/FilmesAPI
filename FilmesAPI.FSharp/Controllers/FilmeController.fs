namespace FilmesAPI.FSharp.Controllers

open Microsoft.AspNetCore.Mvc
open FilmesAPI.FSharp.Models
open Microsoft.Extensions.Logging
open FilmesAPI.FSharp
open FilmesAPI.FSharp.Data

[<ApiController>]
[<Route("[controller]")>]
type FilmeController(dbContext : FilmeContext) =
    inherit ControllerBase()

    [<HttpPost>]
    member this.AdicionaFilme ([<FromBody>]filme) =
        dbContext.Filmes.Add filme |> ignore
        dbContext.SaveChanges() |> ignore
        this.CreatedAtAction("RecuperaFilmePorId", {| id = filme.Id |}, filme)

    [<HttpGet>]
    member this.RecuperaFilmes () = this.Ok(dbContext.Filmes)

    [<HttpGet("{id}", Name = "RecuperaFilmePorId")>]
    member this.RecuperaFilmePorId (id: int) : IActionResult = 
        match dbContext.Filmes |> Seq.tryFind (fun f -> f.Id = id) with
        | Some filme -> this.Ok(filme)
        | None -> this.NotFound()

    [<HttpPut("{id}")>]
    member this.AtualizaFilme (id: int) ([<FromBody>]filmeNovo : Filme) : IActionResult =
        try
            let filme = dbContext.Filmes.Find(id)
            dbContext.Entry(filme).CurrentValues.SetValues({filmeNovo with Id = filme.Id})
            dbContext.SaveChanges() |> ignore
            this.NoContent()
        with
        | :? System.ArgumentNullException -> this.NotFound()

    [<HttpDelete("{id}")>]
    member this.DeletaFilme id : IActionResult =
        match dbContext.Filmes |> Seq.tryFind (fun f -> f.Id = id) with
        | Some filme -> 
            dbContext.Remove(filme) |> ignore
            dbContext.SaveChanges() |> ignore
            this.NoContent()
        | None -> this.NotFound()
