using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos.Sessao;
using FilmesAPI.Models;
using FilmesAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class SessaoController : ControllerBase
{
    private readonly SessaoService _sessaoService;

    public SessaoController(SessaoService sessaoService)
    {
        _sessaoService = sessaoService;
    }

    [HttpPost]
    public IActionResult AdicionaSessao(CreateSessaoDto sessaoDto)
    {
        var readDto = _sessaoService.AdicionaSessao(sessaoDto);
        return CreatedAtAction(nameof(RecuperaSessoesPorId), new { Id = readDto.Id }, readDto);
    }

    public IActionResult RecuperaSessoesPorId(int id)
    {
        var readDto = _sessaoService.RecuperaSessoesPorId(id);
        if (readDto is not null) return Ok(readDto);
        return NotFound();
    }
}
