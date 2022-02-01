using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos.Gerente;
using FilmesAPI.Models;
using FluentResults;

namespace FilmesAPI.Services;

public class GerenteService
{
    private AppDbContext _context;
    private IMapper _mapper;

    public GerenteService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public ReadGerenteDto AdicionaGerente(CreateGerenteDto gerenteDto)
    {
        var gerente = _mapper.Map<Gerente>(gerenteDto);
        _context.Gerentes.Add(gerente);
        _context.SaveChanges();
        return _mapper.Map<ReadGerenteDto>(gerente);
    }

    public ReadGerenteDto? RecuperaGerentesPorId(int id)
    {
        var gerente = _context.Gerentes.FirstOrDefault(gerente => gerente.Id == id);
        if (gerente is not null)
        {
            var gerenteDto = _mapper.Map<ReadGerenteDto>(gerente);

            return gerenteDto;
        }

        return null;
    }

    public Result DeletaGerente(int id)
    {
        var gerente = _context.Gerentes.FirstOrDefault(gerente => gerente.Id == id);
        if (gerente is null) return Result.Fail("Gerente não encontrado");

        _context.Remove(gerente);
        _context.SaveChanges();

        return Result.Ok();
    }
}
