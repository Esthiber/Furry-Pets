using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PawfectMatch.Services
{
    /// <summary>
    /// Gestiona las operaciones CRUD para la entidad 'VetasTabs'.
    /// Este servicio administra las pestañas o categorías utilizadas en la interfaz
    /// del punto de venta (POS) para organizar productos o servicios.
    /// </summary>
    public class VetasTabsService(IDbContextFactory<ApplicationDbContext> dbFactory) : ICRUD<VetasTabs>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory = dbFactory;

        #region Create / Update Operations

        /// <summary>
        /// Inserta una nueva pestaña de venta en la base de datos.
        /// </summary>
        /// <param name="elem">El objeto 'VetasTabs' a ser insertado.</param>
        /// <returns>True si la inserción fue exitosa.</returns>
        public async Task<bool> InsertAsync(VetasTabs elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            ctx.VetasTabs.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Actualiza una pestaña de venta existente de forma eficiente y segura.
        /// </summary>
        /// <param name="elem">El objeto 'VetasTabs' con los datos actualizados.</param>
        /// <returns>True si la actualización fue exitosa.</returns>
        public async Task<bool> UpdateAsync(VetasTabs elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            // FindAsync es la forma más eficiente de obtener una entidad por su clave primaria.
            var tab = await ctx.VetasTabs.FindAsync(elem.VetasTabsID);

            if (tab is null)
            {
                return false; // No se puede actualizar una pestaña que no existe.
            }

            // SetValues copia todas las propiedades escalares, haciendo el código robusto y mantenible.
            ctx.Entry(tab).CurrentValues.SetValues(elem);

            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Guarda una pestaña de venta (Upsert): La inserta si es nueva o la actualiza si ya existe.
        /// </summary>
        /// <param name="elem">La pestaña de venta a guardar.</param>
        /// <returns>True si la operación de guardado fue exitosa.</returns>
        public async Task<bool> SaveAsync(VetasTabs elem)
        {
            // Determina la operación en memoria para mayor eficiencia.
            return elem.VetasTabsID == 0
                ? await InsertAsync(elem)
                : await UpdateAsync(elem);
        }

        #endregion

        #region Read Operations

        /// <summary>
        /// Obtiene una lista de pestañas de venta que cumplen con un criterio de búsqueda.
        /// </summary>
        /// <param name="criteria">Una expresión lambda para filtrar las pestañas.</param>
        /// <returns>Una lista de objetos 'VetasTabs'.</returns>
        public async Task<List<VetasTabs>> ListAsync(Expression<Func<VetasTabs, bool>> criteria)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.VetasTabs
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        /// <summary>
        /// Busca una pestaña de venta por su ID.
        /// </summary>
        /// <param name="id">El ID de la pestaña a buscar.</param>
        /// <returns>El objeto 'VetasTabs' encontrado, o uno nuevo si no se encuentra.</returns>
        public async Task<VetasTabs> SearchByIdAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            // FindAsync es ligeramente más eficiente que FirstOrDefaultAsync para búsquedas por PK.
            return await ctx.VetasTabs.FindAsync(id) ?? new VetasTabs();
        }

        /// <summary>
        /// Comprueba si existe una pestaña de venta con el ID especificado.
        /// </summary>
        /// <param name="id">El ID de la pestaña a verificar.</param>
        /// <returns>True si la pestaña existe, de lo contrario False.</returns>
        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.VetasTabs.AnyAsync(c => c.VetasTabsID == id);
        }

        #endregion

        #region Delete Operations

        /// <summary>
        /// Realiza un borrado lógico (soft delete) de una pestaña de venta.
        /// </summary>
        /// <param name="id">El ID de la pestaña a marcar como borrada.</param>
        /// <returns>True si la operación fue exitosa.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            var tab = await ctx.VetasTabs.FindAsync(id);
            if (tab is null)
            {
                return false;
            }

            tab.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        #endregion
    }
}