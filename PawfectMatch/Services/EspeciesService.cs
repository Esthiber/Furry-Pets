using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    /// <summary>
    /// Gestiona las operaciones CRUD para la entidad 'Especies'.
    /// Este servicio se encarga de la lógica de negocio para crear, leer,
    /// actualizar y borrar las diferentes especies de animales en el sistema (ej. Perro, Gato).
    /// </summary>
    public class EspeciesService(IDbContextFactory<ApplicationDbContext> dbFactory) : ICRUD<Especies>
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
        /// Inserta una nueva especie en la base de datos.
        /// </summary>
        /// <param name="elem">El objeto 'Especies' a ser insertado.</param>
        /// <returns>True si la operación fue exitosa, de lo contrario False.</returns>
        public async Task<bool> InsertAsync(Especies elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            ctx.Especies.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Actualiza una especie existente en la base de datos.
        /// </summary>
        /// <param name="elem">El objeto 'Especies' con los datos actualizados.</param>
        /// <returns>True si la actualización fue exitosa, de lo contrario False.</returns>
        public async Task<bool> UpdateAsync(Especies elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            var existingEspecie = await ctx.Especies.FindAsync(elem.EspeciesID);
            if (existingEspecie is null)
            {
                return false; // No se puede actualizar una especie que no existe.
            }

            // Utiliza SetValues para una actualización eficiente y mantenible.
            ctx.Entry(existingEspecie).CurrentValues.SetValues(elem);

            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Guarda una especie (Upsert): La inserta si es nueva o la actualiza si ya existe.
        /// </summary>
        /// <param name="elem">La especie a guardar.</param>
        /// <returns>True si la operación de guardado fue exitosa.</returns>
        public async Task<bool> SaveAsync(Especies elem)
        {
            if (elem.EspeciesID == 0)
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
        /// Obtiene una lista de especies que cumplen con un criterio de búsqueda.
        /// </summary>
        /// <param name="criteria">Una expresión lambda para filtrar las especies.</param>
        /// <returns>Una lista de objetos 'Especies'.</returns>
        public async Task<List<Especies>> ListAsync(Expression<Func<Especies, bool>> criteria)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.Especies
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        /// <summary>
        /// Busca una especie por su ID.
        /// </summary>
        /// <param name="id">El ID de la especie a buscar.</param>
        /// <returns>El objeto 'Especies' encontrado, o un nuevo objeto vacío si no se encuentra.</returns>
        public async Task<Especies> SearchByIdAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.Especies.FindAsync(id) ?? new();
        }

        /// <summary>
        /// Comprueba si existe una especie con el ID especificado.
        /// </summary>
        /// <param name="id">El ID de la especie a verificar.</param>
        /// <returns>True si la especie existe, de lo contrario False.</returns>
        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.Especies.AnyAsync(e => e.EspeciesID == id);
        }

        #endregion

        #region Delete Operations

        /// <summary>
        /// Realiza un borrado lógico (soft delete) de una especie.
        /// La entidad no se elimina físicamente, solo se marca como 'IsDeleted = true'.
        /// </summary>
        /// <param name="id">El ID de la especie a marcar como borrada.</param>
        /// <returns>True si la operación fue exitosa.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            var especie = await ctx.Especies.FindAsync(id);
            if (especie is null)
            {
                return false;
            }

            especie.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        #endregion

        #endregion
    }
}