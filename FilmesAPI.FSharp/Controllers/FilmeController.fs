namespace FilmesAPI.FSharp.Controllers

open Microsoft.AspNetCore.Mvc
open FilmesAPI.FSharp.Models
open Microsoft.Extensions.Logging
open FilmesAPI.FSharp
open FilmesAPI.FSharp.Data
open AutoMapper
open FilmesAPI.FSharp.Data.Dtos
open System

[<ApiController>]
[<Route("[controller]")>]
type FilmeController(dbContext : FilmeContext, mapper: IMapper) =
    inherit ControllerBase()

    [<HttpPost>]
    member this.AdicionaFilme ([<FromBody>]filmeDto: CreateFilmeDto) =
        let filme = mapper.Map<Filme>(filmeDto)
        dbContext.Filmes.Add filme |> ignore
        dbContext.SaveChanges() |> ignore
        this.CreatedAtAction("RecuperaFilmePorId", {| id = filme.Id |}, filme)

    [<HttpGet>]
    member this.RecuperaFilmes () = this.Ok(dbContext.Filmes)

    [<HttpGet("{id}", Name = "RecuperaFilmePorId")>]
    member this.RecuperaFilmePorId (id: int) : IActionResult = 
        match dbContext.Filmes |> Seq.tryFind (fun f -> f.Id = id) with
        | Some filme -> 
            let filmeDto = filme |> mapper.Map<ReadFilmeDto>
            this.Ok({filmeDto with HoraDaConsulta = DateTime.Now})
        | None -> this.NotFound()

    [<HttpPut("{id}")>]
    member this.AtualizaFilme (id: int) ([<FromBody>]filmeDto: UpdateFilmeDto) : IActionResult =
        try
            let filme = dbContext.Filmes.Find(id)
            let filmeNovo = mapper.Map(filmeDto, filme)
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
