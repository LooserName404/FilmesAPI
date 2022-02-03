using FluentResults;
using Microsoft.AspNetCore.Mvc;
using UsuariosAPI.Data.Dtos;
using UsuariosAPI.Services;

namespace UsuariosAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class CadastroController : ControllerBase
{
    private readonly CadastroService _cadastroService;

    public CadastroController(CadastroService cadastroService)
    {
        _cadastroService = cadastroService;
    }

    [HttpPost]
    public IActionResult CadastraUsuario(CreateUsuarioDto createDto)
    {
        var resultado = _cadastroService.CadastrarUsuario(createDto);
        if (resultado.IsFailed) return StatusCode(500);
        return Ok();
    }
}
