using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JMusik.Data.Contratos;
using JMusik.Dtos;
using JMusik.Models;
using JMusik.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JMusik.WebApi.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;

        public SessionController(IUsersRepository usersRepository, IMapper mapper, TokenService tokenService)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
            _tokenService = tokenService;
        }


        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> PostLogin(LoginModelDto usuarioLogin)
        {
            var datosLoginUsuario = _mapper.Map<Usuario>(usuarioLogin);

            var resultadoValidacion = await _usersRepository.ValidarDatosLogin(datosLoginUsuario);

            if(!resultadoValidacion.resultado)
            {
                return BadRequest("Usuario/Contraseña Invalidos.");
            }
            return _tokenService.GenerarToken(resultadoValidacion.usuario);


        }

    }
}