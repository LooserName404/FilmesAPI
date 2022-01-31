namespace FilmesAPI.FSharp.Data.Dtos

open System

[<CLIMutable>]
type ReadFilmeDto = {
        Id: int
        Titulo: string
        Diretor: string
        Genero: string
        Duracao: int
        HoraDaConsulta: DateTime }

