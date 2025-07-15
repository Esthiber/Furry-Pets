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
    /// Gestiona las operaciones CRUD para la entidad 'TiposItems'.
    /// Este servicio administra los diferentes tipos de ítems o productos
    /// que se manejan en el sistema de punto de venta (POS).
    /// </summary>
    public class TiposItemsService(IDbContextFactory<ApplicationDbContext> dbFactory) : ICRUD<TiposItems>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory = dbFactory;

        #region Create / Update Operations

        /// <summary>
        /// Inserta un nuevo tipo de ítem en la base de datos.
        /// </summary>
        /// <param name="elem">El objeto 'TiposItems' a ser insertado.</param>
        /// <returns>True si la inserción fue exitosa.</returns>
        public async Task<bool> InsertAsync(TiposItems elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            ctx.TiposItems.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Actualiza un tipo de ítem existente de forma eficiente y segura.
        /// </summary>
        /// <param name="elem">El objeto 'TiposItems' con los datos actualizados.</param>
        /// <returns>True si la actualización fue exitosa.</returns>
        public async Task<bool> UpdateAsync(TiposItems elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            // FindAsync es la forma más eficiente de obtener una entidad por su clave primaria.
            var tipoItem = await ctx.TiposItems.FindAsync(elem.TiposItemsID);

            if (tipoItem is null)
            {
                return false; // No se puede actualizar una entidad que no existe.
            }

            // SetValues copia todas las propiedades escalares, haciendo el código robusto y mantenible.
            ctx.Entry(tipoItem).CurrentValues.SetValues(elem);

            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Guarda un tipo de ítem (Upsert): Lo inserta si es nuevo o lo actualiza si ya existe.
        /// </summary>
        /// <param name="elem">El tipo de ítem a guardar.</param>
        /// <returns>True si la operación de guardado fue exitosa.</returns>
        public async Task<bool> SaveAsync(TiposItems elem)
        {
            // Determina la operación en memoria para mayor eficiencia.
            return elem.TiposItemsID == 0
                ? await InsertAsync(elem)
                : await UpdateAsync(elem);
        }

        #endregion

        #region Read Operations

        /// <summary>
        /// Obtiene una lista de tipos de ítems que cumplen con un criterio de búsqueda.
        /// </summary>
        /// <param name="criteria">Una expresión lambda para filtrar los tipos de ítems.</param>
        /// <returns>Una lista de objetos 'TiposItems'.</returns>
        public async Task<List<TiposItems>> ListAsync(Expression<Func<TiposItems, bool>> criteria)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.TiposItems
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        /// <summary>
        /// Busca un tipo de ítem por su ID.
        /// </summary>
        /// <param name="id">El ID del tipo de ítem a buscar.</param>
        /// <returns>El objeto 'TiposItems' encontrado, o uno nuevo si no se encuentra.</returns>
        public async Task<TiposItems> SearchByIdAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            // FindAsync es ligeramente más eficiente que FirstOrDefaultAsync para búsquedas por PK.
            return await ctx.TiposItems.FindAsync(id) ?? new TiposItems();
        }

        /// <summary>
        /// Comprueba si existe un tipo de ítem con el ID especificado.
        /// </summary>
        /// <param name="id">El ID del tipo de ítem a verificar.</param>
        /// <returns>True si el tipo de ítem existe, de lo contrario False.</returns>
        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.TiposItems.AnyAsync(t => t.TiposItemsID == id);
        }

        #endregion

        #region Delete Operations

        /// <summary>
        /// Realiza un borrado lógico (soft delete) de un tipo de ítem.
        /// </summary>
        /// <param name="id">El ID del tipo de ítem a marcar como borrado.</param>
        /// <returns>True si la operación fue exitosa.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            var tipoItem = await ctx.TiposItems.FindAsync(id);
            if (tipoItem is null)
            {
                return false;
            }

            tipoItem.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        #endregion
    }
}