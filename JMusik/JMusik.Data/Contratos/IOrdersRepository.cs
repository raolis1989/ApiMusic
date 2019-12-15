using JMusik.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JMusik.Data.Contratos
{
    public interface IOrdersRepository :IGenericoRepository<Orden>
    {
        Task<IEnumerable<Orden>> ObtenerTodosDetallesAsync();
        Task<Orden> ObtenerConDetallesAsync(int id);
    }
}
