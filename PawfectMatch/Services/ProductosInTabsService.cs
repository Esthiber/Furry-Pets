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
    /// Gestiona las operaciones CRUD para la entidad 'ProductosInTabs'.
    /// Este servicio administra la relación entre los productos y las "pestañas" o categorías
    /// de un sistema de punto de venta (POS), incluyendo su orden de visualización.
    /// </summary>
    public class ProductosInTabsService(IDbContextFactory<ApplicationDbContext> dbFactory) : ICRUD<ProductosInTabs>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory = dbFactory;

        #region Create / Update Operations

        /// <summary>
        /// Inserta una nueva asociación de producto a una pestaña en la base de datos.
        /// </summary>
        /// <param name="elem">El objeto 'ProductosInTabs' a ser insertado.</param>
        /// <returns>True si la inserción fue exitosa.</returns>
        public async Task<bool> InsertAsync(ProductosInTabs elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            ctx.ProductosInTabs.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Actualiza una asociación existente en la base de datos de forma eficiente.
        /// </summary>
        /// <param name="elem">El objeto 'ProductosInTabs' con los datos actualizados.</param>
        /// <returns>True si la actualización fue exitosa.</returns>
        public async Task<bool> UpdateAsync(ProductosInTabs elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            var existingEntry = await ctx.ProductosInTabs.FindAsync(elem.ProductosInTabsID);

            if (existingEntry is null)
            {
                return false; // No se puede actualizar una entrada que no existe.
            }

            // Uso de SetValues para una actualización robusta y mantenible.
            ctx.Entry(existingEntry).CurrentValues.SetValues(elem);

            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Guarda una asociación (Upsert): La inserta si es nueva o la actualiza si ya existe.
        /// </summary>
        /// <param name="elem">La asociación a guardar.</param>
        /// <returns>True si la operación de guardado fue exitosa.</returns>
        public async Task<bool> SaveAsync(ProductosInTabs elem)
        {
            // Determina la operación en memoria (Insert vs Update) para mayor eficiencia.
            return elem.ProductosInTabsID == 0
                ? await InsertAsync(elem)
                : await UpdateAsync(elem);
        }

        #endregion

        #region Read Operations

        /// <summary>
        /// Obtiene una lista de asociaciones que cumplen un criterio, incluyendo datos relacionados.
        /// Los resultados se ordenan por el campo 'Orden'.
        /// </summary>
        /// <param name="criteria">La expresión para filtrar las asociaciones.</param>
        /// <returns>Una lista ordenada de objetos 'ProductosInTabs'.</returns>
        public async Task<List<ProductosInTabs>> ListAsync(Expression<Func<ProductosInTabs, bool>> criteria)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.ProductosInTabs
                .Include(p => p.VetasTab)
                .Include(p => p.Producto)
                .AsNoTracking()
                .Where(criteria)
                .OrderBy(p => p.Orden) // Se preserva la lógica de ordenamiento del negocio.
                .ToListAsync();
        }

        /// <summary>
        /// Busca una asociación por su ID, incluyendo sus datos relacionados.
        /// </summary>
        /// <param name="id">El ID de la asociación a buscar.</param>
        /// <returns>El objeto 'ProductosInTabs' encontrado, o uno nuevo si no se encuentra.</returns>
        public async Task<ProductosInTabs> SearchByIdAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.ProductosInTabs
                .Include(p => p.VetasTab)
                .Include(p => p.Producto)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProductosInTabsID == id) ?? new ProductosInTabs();
        }

        /// <summary>
        /// Comprueba si existe una asociación con el ID especificado.
        /// </summary>
        /// <param name="id">El ID de la asociación a verificar.</param>
        /// <returns>True si la asociación existe, de lo contrario False.</returns>
        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.ProductosInTabs.AnyAsync(p => p.ProductosInTabsID == id);
        }

        #endregion

        #region Delete Operations

        /// <summary>
        /// Realiza un borrado lógico (soft delete) de una asociación.
        /// </summary>
        /// <param name="id">El ID de la asociación a marcar como borrada.</param>
        /// <returns>True si la operación fue exitosa.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            var entity = await ctx.ProductosInTabs.FindAsync(id);
            if (entity is null)
            {
                return false;
            }

            entity.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        #endregion
    }
}