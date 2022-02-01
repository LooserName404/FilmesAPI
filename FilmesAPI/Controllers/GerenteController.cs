using FilmesAPI.Data.Dtos.Gerente;
using FilmesAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class GerenteController : ControllerBase
{
    private readonly GerenteService _gerenteService;
    public GerenteController(GerenteService gerenteService)
    {
        _gerenteService = gerenteService;
    }

    [HttpPost]
    public IActionResult AdicionaGerente(CreateGerenteDto gerenteDto)
    {
        var readDto = _gerenteService.AdicionaGerente(gerenteDto);
        return CreatedAtAction(nameof(RecuperaGerentesPorId), new { Id = readDto.Id }, readDto);
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaGerentesPorId(int id)
    {
        var readDto = _gerenteService.RecuperaGerentesPorId(id);
        if (readDto is not null) return Ok(readDto);
        return NotFound();
    }

    [HttpDelete("{id}")]
    public IActionResult DeletaGerente(int id)
    {
        var resultado = _gerenteService.DeletaGerente(id);
        if (resultado.IsFailed) return NotFound();
        return NoContent();
    }
}
