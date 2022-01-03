namespace FilmesAPI.FSharp.Controllers

open Microsoft.AspNetCore.Mvc
open FilmesAPI.FSharp.Models
open Microsoft.Extensions.Logging
open FilmesAPI.FSharp

[<ApiController>]
[<Route("[controller]")>]
type FilmeController(logger: ILogger<FilmeController>) =
    inherit ControllerBase()

    static let mutable filmes: Filme list = [];

    [<HttpPost>]
    member _.AdicionaFilme ([<FromBody>]filme) =
        filmes <- filme::filmes
        ()


