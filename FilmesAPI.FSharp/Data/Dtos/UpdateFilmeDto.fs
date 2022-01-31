namespace FilmesAPI.FSharp.Data.Dtos

[<CLIMutable>]
type UpdateFilmeDto = {
        Titulo: string
        Diretor: string
        Genero: string
        Duracao: int }

