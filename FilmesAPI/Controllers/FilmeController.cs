using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos.Filme;
using FilmesAPI.Models;
using FilmesAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmeController : ControllerBase
{
    private readonly FilmeService _filmeService;

    public FilmeController(FilmeService filmeService)
    {
        _filmeService = filmeService;
    }

    [HttpPost]
    public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
    {
        var readDto = _filmeService.AdicionaFilme(filmeDto);
        return CreatedAtAction(nameof(RecuperaFilmePorId), new { Id = readDto.Id }, readDto);
    }

    [HttpGet]
    public IActionResult RecuperaFilmes([FromQuery] int? classificacaoEtaria = null)
    {
        var readDto = _filmeService.RecuperaFilmes(classificacaoEtaria);
        if (readDto is not null) return Ok(readDto);
        return NotFound();
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaFilmePorId(int id)
    {
        var readDto = _filmeService.RecuperaFilmePorId(id);
        if (readDto is not null) return Ok(readDto);
        return NotFound();
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
    {
        var resultado = _filmeService.AtualizaFilme(id, filmeDto);
        if (resultado.IsFailed) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeletaFilme(int id)
    {
        var resultado = _filmeService.DeletaFilme(id);
        if (resultado.IsFailed) return NotFound();

        return NoContent();
    }
}

