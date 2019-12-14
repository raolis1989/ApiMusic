using JMusik.Data.Contratos;
using JMusik.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JMusik.Data.Repository
{
    public class PerfilesRepository : IGenericoRepository<Perfil>
    {
        private readonly TiendaDbContext _context;
        private readonly ILogger<PerfilesRepository> _logger;
        private DbSet<Perfil> _dbset;

        public PerfilesRepository(TiendaDbContext context, ILogger<PerfilesRepository> logger)
        {
            _context = context;
            _logger = logger;
            _dbset = _context.Set<Perfil>();
        }

        public async Task<bool> Actualizar(Perfil entity)
        {
            _dbset.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            try
            {

                return await _context.SaveChangesAsync() > 0 ? true : false;
            }
            catch(Exception excepcion)
            {
                _logger.LogError($"Error en {nameof(Actualizar)}: " + excepcion.Message);
            }
            return false;
        }

        public async Task<Perfil> Agregar(Perfil entity)
        {
            _dbset.Add(entity);
            try
            {
                 await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Error en {nameof(Agregar)}:" + ex.Message);
                return null;
            }
            return entity;
        }

        public async  Task<bool> Eliminar(int id)
        {
            var entity = await _dbset.SingleOrDefaultAsync(u => u.Id == id);
            _dbset.Remove(entity);
            try
            {
                return await _context.SaveChangesAsync() > 0 ? true : false;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(Eliminar)}: " + ex.Message);
               
            }
            return false;
        }

        public async Task<Perfil> ObtenerAsync(int id)
        {
            return await _dbset.SingleOrDefaultAsync(c => c.Id == id); 
        }
        public async Task<IEnumerable<Perfil>> ObtenerTodosAsync()
        {
            return await _dbset.ToListAsync();
        }
    }
}
