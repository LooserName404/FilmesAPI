using AutoMapper;
using FilmesAPI.Data.Dtos.Filme;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles;

public class FilmeProfile : GerenteProfile
{
    public FilmeProfile()
    {
        CreateMap<CreateFilmeDto, Filme>();
        CreateMap<UpdateFilmeDto, Filme>();
        CreateMap<Filme, ReadFilmeDto>();
    }
}

