using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JMusik.Data;
using JMusik.Models;
using JMusik.Data.Contratos;

namespace JMusik.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
   
        private readonly IProductosRepository _productosRepository;

        public ProductosController(IProductosRepository productosRepository)
        {
            
            _productosRepository = productosRepository;
        }

        //// GET: api/Productos
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            try
            {
                return await _productosRepository.ObtenerProductosAsync();
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
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {

            var prod= await _productosRepository.ObtenerProductoAsync(id);
             if(prod==null)
            {
                return NotFound();
            }

            return prod;

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Producto>> PostProducto(Producto producto)
        {
            try
            {
                var nuevoProducto = await _productosRepository.Agregar(producto);


                if(nuevoProducto==null)
                {
                    return BadRequest();
                }
                return CreatedAtAction(nameof(PostProducto), new { id = nuevoProducto.Id }, producto);

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
        public async Task<ActionResult<Producto>> PutProducto(int id, [FromBody]Producto producto)
        {
            if (producto== null)   
                return NotFound();

            var resultado = await _productosRepository.Actualizar(producto);
            if (!resultado)
                return BadRequest();

            return producto;
        }
       

        //// DELETE: api/Productos/5
        //[HttpDelete("{id}")]
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
