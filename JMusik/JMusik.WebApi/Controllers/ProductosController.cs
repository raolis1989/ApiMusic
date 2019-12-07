using AutoMapper;
using JMusik.Data.Contratos;
using JMusik.Dtos;
using JMusik.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JMusik.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
   
        private readonly IProductosRepository _productosRepository;
        private readonly IMapper _mapper;

        public ProductosController(IProductosRepository productosRepository, IMapper mapper)
        {
            
            _productosRepository = productosRepository;
            _mapper = mapper;
        }

        //// GET: api/Productos
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ProductoDto>>> Get()
        {
            try
            {
                var productos = await _productosRepository.ObtenerProductosAsync();
                return _mapper.Map<List<ProductoDto>>(productos);
            
            }
            catch (Exception ex)
            {

                return BadRequest();
            }
        }

        //// GET: api/Productos/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductoDto>> Get(int id)
        {

            var prod= await _productosRepository.ObtenerProductoAsync(id);
             if(prod==null)
            {
                return NotFound();
            }

            return _mapper.Map<ProductoDto>(prod);

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductoDto>> Post(ProductoDto productoDto)
        {
            try
            {
                var producto = _mapper.Map<Producto>(productoDto);

                var nuevoProducto = await _productosRepository.Agregar(producto);


                if(nuevoProducto==null)
                {
                    return BadRequest();
                }
                var nuevoproductoDTO = _mapper.Map<ProductoDto>(nuevoProducto);

                return CreatedAtAction(nameof(Post), new { id = nuevoproductoDTO.Id }, nuevoproductoDTO);

            }
            catch (Exception ex)
            {

                return BadRequest();
            }
        }


        //// PUT: api/Productos/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Producto>> Put(int id, [FromBody]Producto producto)
        {
            if (producto== null)   
                return NotFound();

            var resultado = await _productosRepository.Actualizar(producto);
            if (!resultado)
                return BadRequest();

            return producto;
        }
       

        //// DELETE: api/Productos/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult>Delete(int id)
        {
            try
            {
                var resultado = await _productosRepository.Eliminar(id);
                if(!resultado)
                {
                    return BadRequest(); 
                }
                return NoContent();

            }
            catch(Exception excepcion)
            {
                return BadRequest();
            }
        }
        //public async Task<ActionResult<Producto>> DeleteProducto(int id)
        //{
        //    var producto = await _context.Productos.FindAsync(id);
        //    if (producto == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Productos.Remove(producto);
        //    await _context.SaveChangesAsync();

        //    return producto;
        //}

        //private bool ProductoExists(int id)
        //{
        //    return _context.Productos.Any(e => e.Id == id);
        //}
    }
}
