namespace FilmesAPI.FSharp.Profiles

open AutoMapper
open FilmesAPI.FSharp.Models
open FilmesAPI.FSharp.Data.Dtos

type FilmeProfile() as this =
    inherit Profile()

    do
        this.CreateMap<CreateFilmeDto, Filme>() |> ignore
        this.CreateMap<UpdateFilmeDto, Filme>() |> ignore
        this.CreateMap<Filme, ReadFilmeDto>() |> ignore
        ()
        
