namespace FilmesAPI.FSharp.Controllers

open Microsoft.AspNetCore.Mvc
open FilmesAPI.FSharp.Models
open Microsoft.Extensions.Logging
open FilmesAPI.FSharp

[<ApiController>]
[<Route("[controller]")>]
type FilmeController() =
    inherit ControllerBase()

    static let mutable filmes: Filme list = [];
    static let mutable id = 1

    [<HttpPost>]
    member this.AdicionaFilme ([<FromBody>]filme) =
        let filmeWithId = { filme with Id = id }
        filmes <- filmes |> List.append [filmeWithId]
        id <- id + 1
        this.CreatedAtAction("RecuperaFilmePorId", {| id = filmeWithId.Id |}, filmeWithId)

    [<HttpGet>]
    member this.RecuperaFilmes () = this.Ok(filmes)

    [<HttpGet("{id}", Name = "RecuperaFilmePorId")>]
    member this.RecuperaFilmePorId (id: int) : IActionResult = 
        match (filmes |> List.tryFind (fun f -> f.Id = id)) with
        | Some filme -> this.Ok(filme)
        | None -> this.NotFound()


