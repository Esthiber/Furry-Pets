using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models.Adopciones;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    /// <summary>
    /// Gestiona las operaciones CRUD para la entidad 'HistorialAdopciones'.
    /// Este servicio se encarga de la lógica de negocio para registrar y consultar
    /// las adopciones que han sido completadas exitosamente.
    /// </summary>
    public class HistorialAdopcionesService(IDbContextFactory<ApplicationDbContext> dbFactory) : ICRUD<HistorialAdopciones>
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
        /// Inserta un nuevo registro de historial de adopción en la base de datos.
        /// </summary>
        /// <param name="elem">El objeto 'HistorialAdopciones' a ser insertado.</param>
        /// <returns>True si la operación fue exitosa, de lo contrario False.</returns>
        public async Task<bool> InsertAsync(HistorialAdopciones elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            ctx.HistorialAdopciones.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Actualiza un registro de historial de adopción existente.
        /// </summary>
        /// <param name="elem">El objeto 'HistorialAdopciones' con los datos actualizados.</param>
        /// <returns>True si la actualización fue exitosa, de lo contrario False.</returns>
        public async Task<bool> UpdateAsync(HistorialAdopciones elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            var existingHistory = await ctx.HistorialAdopciones.FindAsync(elem.HistorialAdopcionesID);
            if (existingHistory is null)
            {
                return false; // No se puede actualizar un historial que no existe.
            }

            // Utiliza SetValues para una actualización eficiente y mantenible.
            ctx.Entry(existingHistory).CurrentValues.SetValues(elem);

            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Guarda un registro de historial (Upsert): lo inserta si es nuevo o lo actualiza si ya existe.
        /// </summary>
        /// <param name="elem">El historial de adopción a guardar.</param>
        /// <returns>True si la operación de guardado fue exitosa.</returns>
        public async Task<bool> SaveAsync(HistorialAdopciones elem)
        {
            if (elem.HistorialAdopcionesID == 0)
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
        /// Obtiene una lista de historiales de adopción que cumplen con un criterio de búsqueda.
        /// Carga de forma anticipada (Eager Loading) las entidades relacionadas.
        /// </summary>
        /// <param name="criteria">Una expresión lambda para filtrar los historiales.</param>
        /// <returns>Una lista de objetos 'HistorialAdopciones'.</returns>
        public async Task<List<HistorialAdopciones>> ListAsync(Expression<Func<HistorialAdopciones, bool>> criteria)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.HistorialAdopciones
                .Include(h => h.SolicitudAdopcion)
                .Include(h => h.MascotaAdopcion)
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        /// <summary>
        /// Busca un registro de historial por su ID, incluyendo sus datos relacionados.
        /// </summary>
        /// <param name="id">El ID del historial a buscar.</param>
        /// <returns>El objeto 'HistorialAdopciones' encontrado, o un nuevo objeto vacío si no se encuentra.</returns>
        public async Task<HistorialAdopciones> SearchByIdAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.HistorialAdopciones
                .Include(h => h.SolicitudAdopcion)
                .Include(h => h.MascotaAdopcion)
                .FirstOrDefaultAsync(h => h.HistorialAdopcionesID == id) ?? new();
        }

        /// <summary>
        /// Comprueba si existe un registro de historial con el ID especificado.
        /// </summary>
        /// <param name="id">El ID del historial a verificar.</param>
        /// <returns>True si el historial existe, de lo contrario False.</returns>
        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.HistorialAdopciones.AnyAsync(h => h.HistorialAdopcionesID == id);
        }

        #endregion

        #region Delete Operations

        /// <summary>
        /// Realiza un borrado lógico (soft delete) de un registro de historial.
        /// </summary>
        /// <param name="id">El ID del historial a marcar como borrado.</param>
        /// <returns>True si la operación fue exitosa.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            var historial = await ctx.HistorialAdopciones.FindAsync(id);
            if (historial is null)
            {
                return false;
            }

            historial.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        #endregion

        #endregion
    }
}