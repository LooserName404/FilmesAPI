namespace FilmesAPI.FSharp.Data.Dtos

[<CLIMutable>]
type CreateFilmeDto = {
        Titulo: string
        Diretor: string
        Genero: string
        Duracao: int }

