using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JMusik.Data.Contratos;
using JMusik.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JMusik.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenesController : ControllerBase
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly ILogger<OrdenesController> _logger;
        private readonly IMapper _mapper;

        public OrdenesController(IOrdersRepository ordersRepository, 
            ILogger<OrdenesController> logger, IMapper mapper)
        {
            _ordersRepository = ordersRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<OrdenDto>>> Get()
        {
            try
            {
                var orders = await _ordersRepository.ObtenerTodosAsync();
                return _mapper.Map<List<OrdenDto>>(orders);

            }
            catch (Exception ex)
            {
                return BadRequest();
             
            }
        }

        [HttpGet]
        [Route("detalles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<OrdenDto>>> GetOrdenesCoonDetalle()
        {
            try
            {
                var orders = await _ordersRepository.ObtenerTodosDetallesAsync();
                return _mapper.Map<List<OrdenDto>>(orders);
            }
            catch (Exception ex)
            {

                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrdenDto>> Get(int id)
        {
            var order = await _ordersRepository.ObtenerAsync(id);
            if(order==null)
            {
                return NotFound();
            }
            return _mapper.Map<OrdenDto>(order);
        }

        [HttpGet("{id}/detalles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrdenDto>> GetOrdenConDetalles(int id)
        {
            var order = await _ordersRepository.ObtenerConDetallesAsync(id);
            if(order==null)
            {
                return NotFound();

            }
            return _mapper.Map<OrdenDto>(order);
        }
    }
}