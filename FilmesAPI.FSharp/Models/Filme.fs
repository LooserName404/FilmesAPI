namespace FilmesAPI.FSharp.Models

open System.ComponentModel.DataAnnotations

type Filme = {
    [<Required>] Titulo: string
    [<Required>] Genero: string
    [<StringLength(30)>] Diretor: string
    [<Range(1, 600)>] Duracao: int
}