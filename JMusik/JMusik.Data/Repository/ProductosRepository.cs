using JMusik.Data.Contratos;
using JMusik.Models;
using JMusik.Models.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMusik.Data.Repository
{
    public class ProductosRepository : IProductosRepository
    {

        
            private TiendaDbContext _contexto;

            public ProductosRepository(TiendaDbContext contexto)
            {
                _contexto = contexto;
            }
            public async Task<bool> Actualizar(Producto producto)
            {
                _contexto.Productos.Attach(producto);
                _contexto.Entry(producto).State = EntityState.Modified;
                try
                {
                    return await _contexto.SaveChangesAsync() > 0 ? true : false;
                }
                catch (Exception excepcion)
                {
                    ;
                }
                return false;
            }

            public async Task<Producto> Agregar(Producto producto)
            {
                _contexto.Productos.Add(producto);
                try
                {
                    await _contexto.SaveChangesAsync();
                }
                catch (Exception excepcion)
                {
                    ;
                }

                return producto;
            }

            public async Task<bool> Eliminar(int id)
            {
                //Se realiza una eliminación suave, solamente inactivamos el producto

                var producto = await _contexto.Productos
                                    .SingleOrDefaultAsync(c => c.Id == id);

                producto.Estatus = EstatusProducto.Inactivo;
                _contexto.Productos.Attach(producto);
                _contexto.Entry(producto).State = EntityState.Modified;

                try
                {
                    return (await _contexto.SaveChangesAsync() > 0 ? true : false);
                }
                catch (Exception excepcion)
                {
                    ;
                }
                return false;

            }

            public async Task<Producto> ObtenerProductoAsync(int id)
            {
                return await _contexto.Productos
                                   .SingleOrDefaultAsync(c => c.Id == id);
            }

            public async Task<List<Producto>> ObtenerProductosAsync()
            {
                return await _contexto.Productos.OrderBy(u => u.Nombre)
                                                .ToListAsync();
            }


        

    }
}
