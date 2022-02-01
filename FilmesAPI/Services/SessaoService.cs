using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos.Sessao;
using FilmesAPI.Models;

namespace FilmesAPI.Services;

public class SessaoService
{
    private AppDbContext _context;
    private IMapper _mapper;

    public SessaoService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public ReadSessaoDto AdicionaSessao(CreateSessaoDto sessaoDto)
    {
        var sessao = _mapper.Map<Sessao>(sessaoDto);
        _context.Sessoes.Add(sessao);
        _context.SaveChanges();
        return _mapper.Map<ReadSessaoDto>(sessao);
    }

    public ReadSessaoDto? RecuperaSessoesPorId(int id)
    {
        var sessao = _context.Sessoes.FirstOrDefault(sessao => sessao.Id == id);
        if (sessao is not null)
        {
            var sessaoDto = _mapper.Map<ReadSessaoDto>(sessao);

            return sessaoDto;
        }

        return null;
    }
}
