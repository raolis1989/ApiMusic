using JMusik.Data.Contratos;
using JMusik.Models;
using JMusik.Models.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMusik.Data.Repository
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly TiendaDbContext _contexto;
        private readonly ILogger<OrdersRepository> _logger;
        private DbSet<Orden> _dbSet;

        public OrdersRepository(TiendaDbContext contexto, ILogger<OrdersRepository>logger)
        {
            _contexto = contexto;
            _logger = logger;
            _dbSet = _contexto.Set<Orden>();
        }
        public async Task<bool> Actualizar(Orden entity)
        {
            _dbSet.Attach(entity);
            _contexto.Entry(entity).State = EntityState.Modified;
            try
            {
                return await _contexto.SaveChangesAsync() > 0 ? true : false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"error en {nameof(Actualizar)}: {ex.Message} ");

                
            }
            return false;
        }
        public async Task<Orden> Agregar(Orden entity)
        {
            entity.EstatusOrden = EstatusOrden.Activo;
            entity.FechaRegistro = DateTime.UtcNow;
            _dbSet.Add(entity);

            try
            {
                await _contexto.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(Agregar)}: {ex.Message}");
                return null;
            }
            return entity;
        }
        public async  Task<bool> Eliminar(int id)
        {
            var entity = await _dbSet.SingleOrDefaultAsync(u => u.Id == id);
            entity.EstatusOrden = EstatusOrden.Inactivo;
            try
            {
                return (await _contexto.SaveChangesAsync() > 0 ? true : false);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Error en {nameof(Eliminar)}: {ex.Message}");
            }
            return false;
        }
        public async Task<Orden> ObtenerAsync(int id)
        {
            return await _dbSet.Include(order => order.Usuario)
                                .SingleOrDefaultAsync(c => c.Id == id
                                && c.EstatusOrden == EstatusOrden.Activo);
        }
        public async Task<Orden> ObtenerConDetallesAsync(int id)
        {
            return await _dbSet.Include(orden => orden.Usuario)
                                .Include(orden => orden.DetalleOrden)
                                .ThenInclude(DetalleOrden => DetalleOrden.Producto)
                                .SingleOrDefaultAsync(c => c.Id == id
                                && c.EstatusOrden == EstatusOrden.Activo);
        }
        public async Task<IEnumerable<Orden>> ObtenerTodosAsync()
        {
            return await _dbSet.Where(u => u.EstatusOrden == EstatusOrden.Activo)
                                .Include(orden => orden.Usuario)
                                .ToListAsync();
        }
        public async Task<IEnumerable<Orden>> ObtenerTodosDetallesAsync()
        {
            return await _dbSet.Where(u => u.EstatusOrden == EstatusOrden.Activo)
                                .Include(orden => orden.Usuario)
                                .Include(orden => orden.DetalleOrden)
                                .ToListAsync();
        }

    }
}
