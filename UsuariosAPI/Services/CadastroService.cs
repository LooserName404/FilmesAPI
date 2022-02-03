using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using UsuariosAPI.Data.Dtos;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services;

public class CadastroService
{
    private readonly IMapper _mapper;
    private readonly UserManager<IdentityUser<int>> _userManager;

    public CadastroService(IMapper mapper, UserManager<IdentityUser<int>> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    public Result CadastrarUsuario(CreateUsuarioDto createDto)
    {
        var usuario = _mapper.Map<Usuario>(createDto);
        var usuarioIdentity = _mapper.Map<IdentityUser<int>>(usuario);
        var resultadoIdentity = _userManager.CreateAsync(usuarioIdentity, createDto.Password);
        if (resultadoIdentity.Result.Succeeded) return Result.Ok();
        return Result.Fail("Falha ao cadastrar usuário");
    }
}
