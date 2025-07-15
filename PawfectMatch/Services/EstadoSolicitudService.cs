using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    /// <summary>
    /// Gestiona las operaciones CRUD para la entidad 'EstadoSolicitud'.
    /// Este servicio se encarga de la lógica de negocio para los diferentes estados
    /// que puede tener una solicitud de adopción (ej. 'Enviada', 'En Revisión', 'Aprobada', 'Rechazada').
    /// </summary>
    public class EstadoSolicitudService(IDbContextFactory<ApplicationDbContext> dbFactory) : ICRUD<EstadoSolicitud>
    {
        #region Dependencies & Constructor
        /// <summary>
        /// Fábrica para crear instancias de ApplicationDbContext, inyectada vía constructor primario.
        /// </summary>
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory = dbFactory;
        #endregion

        #region ICRUD Implementation

        #region Create / Update Operations

        /// <summary>
        /// Inserta un nuevo estado de solicitud en la base de datos.
        /// </summary>
        /// <param name="elem">El objeto 'EstadoSolicitud' a ser insertado.</param>
        /// <returns>True si la operación fue exitosa, de lo contrario False.</returns>
        public async Task<bool> InsertAsync(EstadoSolicitud elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            ctx.EstadoSolicitud.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Actualiza un estado de solicitud existente en la base de datos.
        /// </summary>
        /// <param name="elem">El objeto 'EstadoSolicitud' con los datos actualizados.</param>
        /// <returns>True si la actualización fue exitosa, de lo contrario False.</returns>
        public async Task<bool> UpdateAsync(EstadoSolicitud elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            var existingState = await ctx.EstadoSolicitud.FindAsync(elem.EstadoSolicitudID);
            if (existingState is null)
            {
                return false; // No se puede actualizar un estado que no existe.
            }

            // Utiliza SetValues para una actualización eficiente y mantenible de las propiedades.
            ctx.Entry(existingState).CurrentValues.SetValues(elem);

            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Guarda un estado de solicitud (Upsert): Lo inserta si es nuevo o lo actualiza si ya existe.
        /// </summary>
        /// <param name="elem">El estado de solicitud a guardar.</param>
        /// <returns>True si la operación de guardado fue exitosa.</returns>
        public async Task<bool> SaveAsync(EstadoSolicitud elem)
        {
            if (elem.EstadoSolicitudID == 0)
            {
                return await InsertAsync(elem);
            }
            else
            {
                return await UpdateAsync(elem);
            }
        }

        #endregion

        #region Read Operations

        /// <summary>
        /// Obtiene una lista de estados de solicitud que cumplen con un criterio de búsqueda.
        /// </summary>
        /// <param name="criteria">Una expresión lambda para filtrar los estados.</param>
        /// <returns>Una lista de objetos 'EstadoSolicitud'.</returns>
        public async Task<List<EstadoSolicitud>> ListAsync(Expression<Func<EstadoSolicitud, bool>> criteria)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.EstadoSolicitud
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        /// <summary>
        /// Busca un estado de solicitud por su ID.
        /// </summary>
        /// <param name="id">El ID del estado a buscar.</param>
        /// <returns>El objeto 'EstadoSolicitud' encontrado, o un nuevo objeto vacío si no se encuentra.</returns>
        public async Task<EstadoSolicitud> SearchByIdAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            // FindAsync es el método más optimizado para buscar por clave primaria.
            return await ctx.EstadoSolicitud.FindAsync(id) ?? new();
        }

        /// <summary>
        /// Comprueba si existe un estado de solicitud con el ID especificado.
        /// </summary>
        /// <param name="id">El ID del estado a verificar.</param>
        /// <returns>True si el estado existe, de lo contrario False.</returns>
        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.EstadoSolicitud.AnyAsync(e => e.EstadoSolicitudID == id);
        }

        #endregion

        #region Delete Operations

        /// <summary>
        /// Realiza un borrado lógico (soft delete) de un estado de solicitud.
        /// </summary>
        /// <param name="id">El ID del estado a marcar como borrado.</param>
        /// <returns>True si la operación fue exitosa.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            var estadoSolicitud = await ctx.EstadoSolicitud.FindAsync(id);
            if (estadoSolicitud is null)
            {
                return false;
            }

            estadoSolicitud.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        #endregion

        #endregion
    }
}