using AutoMapper;
using FilmesAPI.Minimal.Data.Dtos;
using FilmesAPI.Minimal.Models;

namespace FilmesAPI.Minimal.Profiles;

public class FilmeProfile : Profile
{
    public FilmeProfile()
    {
        CreateMap<CreateFilmeDto, Filme>();
        CreateMap<UpdateFilmeDto, Filme>();
        CreateMap<Filme, ReadFilmeDto>();
    }
}

