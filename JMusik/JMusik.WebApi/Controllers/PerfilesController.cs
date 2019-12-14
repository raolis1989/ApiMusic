using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JMusik.Data.Contratos;
using JMusik.Dtos;
using JMusik.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;

namespace JMusik.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilesController : ControllerBase
    {
        private readonly IGenericoRepository<Perfil> _perfilesRepository;
        private readonly ILogger<PerfilesController> _logger;
        private readonly IMapper _mapper;

        public PerfilesController(IGenericoRepository<Perfil> perfilesRepository,
            ILogger<PerfilesController> logger, IMapper mapper)
        {
            _perfilesRepository = perfilesRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<PerfilDto>>> Get()
        {
            try
            {
                var perfiles = await _perfilesRepository.ObtenerTodosAsync();
                return _mapper.Map<List<PerfilDto>>(perfiles);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(Get)}:" + ex.Message);
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PerfilDto>> Get(int id)
        {
            var perfil = await _perfilesRepository.ObtenerAsync(id);
            if (perfil == null)
            {
                return NotFound();
            }
            return _mapper.Map<PerfilDto>(perfil);

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PerfilDto>>Post(PerfilDto perfilDto)
        {
            try
            {
                var perfil = _mapper.Map<Perfil>(perfilDto);
                var nuevoPerfil = await _perfilesRepository.Agregar(perfil);
                if(nuevoPerfil==null)
                {
                    return BadRequest();
                }
                var nuevoPefilDto = _mapper.Map<PerfilDto>(nuevoPerfil);
                return CreatedAtAction(nameof(Post), new { id = nuevoPefilDto.Id }, nuevoPefilDto);

            }
            catch (Exception ex)
            {

                _logger.LogError($"Error en {nameof(Post)}: "+ ex.Message);
                return BadRequest();
            }
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PerfilDto>> Put(int id, [FromBody]PerfilDto perfilDto)
        {
            if (perfilDto == null)
                return NotFound();

            var perfil = _mapper.Map<Perfil>(perfilDto);
            var resultado = await _perfilesRepository.Actualizar(perfil);
            if (!resultado)
                return BadRequest();

            return perfilDto;
        }




        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var resultado = await _perfilesRepository.Eliminar(id);
                if(!resultado)
                {
                    return BadRequest();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(Delete)}: " + ex.Message);
                return BadRequest();
            }
        }
    }
}