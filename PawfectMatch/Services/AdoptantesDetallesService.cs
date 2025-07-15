using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models.Adopciones;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    /// <summary>
    /// Gestiona las operaciones CRUD para la entidad 'AdoptantesDetalles'.
    /// Esta clase encapsula la lógica de acceso a datos para los detalles específicos de un adoptante,
    /// como su tipo de vivienda, si tiene otras mascotas, etc.
    /// Utiliza IDbContextFactory para garantizar la seguridad de las operaciones en Blazor Server.
    /// </summary>
    public class AdoptantesDetallesService(IDbContextFactory<ApplicationDbContext> dbFactory) : ICRUD<AdoptantesDetalles>
    {
        #region Dependencies & Constructor
        /// <summary>
        /// Fábrica para crear instancias de ApplicationDbContext.
        /// </summary>
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory = dbFactory;
        #endregion

        #region ICRUD Implementation

        #region Create / Update Operations

        /// <summary>
        /// Inserta un nuevo registro de detalles de adoptante en la base de datos.
        /// </summary>
        /// <param name="elem">El objeto 'AdoptantesDetalles' a insertar.</param>
        /// <returns>True si la inserción fue exitosa, de lo contrario False.</returns>
        public async Task<bool> InsertAsync(AdoptantesDetalles elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            ctx.AdoptantesDetalles.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Actualiza un registro existente de detalles de adoptante.
        /// Este método utiliza 'SetValues' para una actualización eficiente y mantenible.
        /// </summary>
        /// <param name="elem">El objeto 'AdoptantesDetalles' con los datos actualizados.</param>
        /// <returns>True si la actualización fue exitosa, de lo contrario False.</returns>
        public async Task<bool> UpdateAsync(AdoptantesDetalles elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            var existingDetails = await ctx.AdoptantesDetalles.FindAsync(elem.AdoptantesDetallesID);
            if (existingDetails is null)
            {
                return false; // El registro a actualizar no existe.
            }

            // Copia eficientemente los valores de las propiedades del objeto de entrada
            // al objeto que está siendo rastreado por el DbContext.
            ctx.Entry(existingDetails).CurrentValues.SetValues(elem);

            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Guarda un registro de detalles de adoptante (inserta si es nuevo, actualiza si existe).
        /// </summary>
        /// <param name="elem">El registro de detalles a guardar.</param>
        /// <returns>True si la operación fue exitosa.</returns>
        public async Task<bool> SaveAsync(AdoptantesDetalles elem)
        {
            if (elem.AdoptantesDetallesID == 0)
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
        /// Recupera una lista de detalles de adoptantes según un criterio de búsqueda.
        /// Carga de forma anticipada (Eager Loading) las entidades relacionadas 'Persona' y 'TipoVivienda'.
        /// </summary>
        /// <param name="criteria">La expresión lambda para filtrar los resultados.</param>
        /// <returns>Una lista de objetos 'AdoptantesDetalles' que cumplen el criterio.</returns>
        public async Task<List<AdoptantesDetalles>> ListAsync(Expression<Func<AdoptantesDetalles, bool>> criteria)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.AdoptantesDetalles
                .Include(a => a.Persona)
                .Include(a => a.TipoVivienda)
                .AsNoTracking() // Optimización para operaciones de solo lectura.
                .Where(criteria)
                .ToListAsync();
        }

        /// <summary>
        /// Busca los detalles de un adoptante por su ID, incluyendo entidades relacionadas.
        /// </summary>
        /// <param name="id">El ID del registro de detalles a buscar.</param>
        /// <returns>El objeto 'AdoptantesDetalles' si se encuentra; de lo contrario, un nuevo objeto vacío.</returns>
        public async Task<AdoptantesDetalles> SearchByIdAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.AdoptantesDetalles
                .Include(a => a.Persona)
                .Include(a => a.TipoVivienda)
                .FirstOrDefaultAsync(a => a.AdoptantesDetallesID == id) ?? new();
        }

        /// <summary>
        /// Verifica si existe un registro de detalles de adoptante con el ID especificado.
        /// </summary>
        /// <param name="id">El ID a verificar.</param>
        /// <returns>True si el registro existe, de lo contrario False.</returns>
        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.AdoptantesDetalles.AnyAsync(a => a.AdoptantesDetallesID == id);
        }

        #endregion

        #region Delete Operations

        /// <summary>
        /// Realiza un borrado lógico (soft delete) de un registro de detalles de adoptante.
        /// </summary>
        /// <param name="id">El ID del registro a marcar como borrado.</param>
        /// <returns>True si la operación fue exitosa.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            var adoptanteDetails = await ctx.AdoptantesDetalles.FindAsync(id);
            if (adoptanteDetails == null)
            {
                return false;
            }

            adoptanteDetails.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        #endregion

        #endregion
    }
}