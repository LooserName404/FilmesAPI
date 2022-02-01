using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos.Filme;
using FilmesAPI.Models;
using FluentResults;

namespace FilmesAPI.Services;

public class FilmeService
{
    private AppDbContext _context;
    private IMapper _mapper;

    public FilmeService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public ReadFilmeDto AdicionaFilme(CreateFilmeDto filmeDto)
    {
        var filme = _mapper.Map<Filme>(filmeDto);
        _context.Filmes.Add(filme);
        _context.SaveChanges();
        return _mapper.Map<ReadFilmeDto>(filme);
    }

    public List<ReadFilmeDto>? RecuperaFilmes(int? classificacaoEtaria)
    {
        List<Filme> filmes;
        if (classificacaoEtaria is null)
        {
            filmes = _context.Filmes.ToList();
        }
        else
        {
            filmes = _context.Filmes.Where(filme => filme.ClassificacaoEtaria <= classificacaoEtaria).ToList();
        }
        if (filmes is not null)
        {
            var readDto = _mapper.Map<List<ReadFilmeDto>>(filmes);
            return readDto;
        }

        return null;
    }

    public ReadFilmeDto? RecuperaFilmePorId(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme is not null)
        {
            var filmeDto = _mapper.Map<ReadFilmeDto>(filme);

            return filmeDto;
        }

        return null;
    }

    public Result AtualizaFilme(int id, UpdateFilmeDto filmeDto)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme is null) return Result.Fail("Filme não encontrado");

        _mapper.Map(filmeDto, filme);

        _context.SaveChanges();
        return Result.Ok();
    }

    public Result DeletaFilme(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme is null) return Result.Fail("Filme não encontrado");

        _context.Remove(filme);
        _context.SaveChanges();
        return Result.Ok();
    }
}
