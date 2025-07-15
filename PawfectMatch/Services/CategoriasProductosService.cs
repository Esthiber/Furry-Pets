using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models.POS;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    /// <summary>
    /// Gestiona las operaciones CRUD para la entidad 'CategoriasProductos'.
    /// Esta clase se encarga de toda la lógica de negocio relacionada con la creación,
    /// lectura, actualización y borrado de las categorías de productos.
    /// </summary>
    public class CategoriasProductosService(IDbContextFactory<ApplicationDbContext> dbFactory) : ICRUD<CategoriasProductos>
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
        /// Inserta una nueva categoría de producto en la base de datos.
        /// </summary>
        /// <param name="elem">El objeto 'CategoriasProductos' a ser insertado.</param>
        /// <returns>True si la operación fue exitosa, de lo contrario False.</returns>
        public async Task<bool> InsertAsync(CategoriasProductos elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            ctx.CategoriasProductos.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Actualiza una categoría de producto existente en la base de datos.
        /// Utiliza 'FindAsync' para una búsqueda eficiente por clave primaria y 'SetValues'
        /// para una actualización mantenible de las propiedades.
        /// </summary>
        /// <param name="elem">El objeto 'CategoriasProductos' con los datos actualizados.</param>
        /// <returns>True si la actualización fue exitosa, de lo contrario False.</returns>
        public async Task<bool> UpdateAsync(CategoriasProductos elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            var existingCategory = await ctx.CategoriasProductos.FindAsync(elem.CategoriasProductosID);
            if (existingCategory is null)
            {
                return false; // No se puede actualizar una categoría que no existe.
            }

            // Copia los valores de las propiedades del objeto de entrada al objeto rastreado por EF.
            // Es más seguro y mantenible que la asignación manual.
            ctx.Entry(existingCategory).CurrentValues.SetValues(elem);

            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Guarda una categoría (Upsert): La inserta si es nueva o la actualiza si ya existe.
        /// </summary>
        /// <param name="elem">La categoría a guardar.</param>
        /// <returns>True si la operación de guardado fue exitosa.</returns>
        public async Task<bool> SaveAsync(CategoriasProductos elem)
        {
            // Se determina si es un nuevo registro verificando si el ID es 0 (valor por defecto).
            // Esto es más eficiente que hacer una consulta previa a la base de datos.
            if (elem.CategoriasProductosID == 0)
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
        /// Obtiene una lista de categorías que cumplen con un criterio de búsqueda.
        /// Utiliza 'AsNoTracking' para mejorar el rendimiento, ya que los resultados son de solo lectura.
        /// </summary>
        /// <param name="criteria">Una expresión lambda para filtrar las categorías.</param>
        /// <returns>Una lista de objetos 'CategoriasProductos'.</returns>
        public async Task<List<CategoriasProductos>> ListAsync(Expression<Func<CategoriasProductos, bool>> criteria)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.CategoriasProductos
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        /// <summary>
        /// Busca una categoría de producto por su ID.
        /// </summary>
        /// <param name="id">El ID de la categoría a buscar.</param>
        /// <returns>El objeto 'CategoriasProductos' encontrado, o un nuevo objeto vacío si no se encuentra.</returns>
        public async Task<CategoriasProductos> SearchByIdAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.CategoriasProductos
                .FirstOrDefaultAsync(c => c.CategoriasProductosID == id) ?? new();
        }

        /// <summary>
        /// Comprueba si existe una categoría con el ID especificado.
        /// </summary>
        /// <param name="id">El ID a verificar.</param>
        /// <returns>True si la categoría existe, de lo contrario False.</returns>
        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.CategoriasProductos.AnyAsync(c => c.CategoriasProductosID == id);
        }

        #endregion

        #region Delete Operations

        /// <summary>
        /// Realiza un borrado lógico (soft delete) de una categoría de producto.
        /// La entidad no se elimina físicamente, solo se marca como 'IsDeleted = true'.
        /// </summary>
        /// <param name="id">El ID de la categoría a marcar como borrada.</param>
        /// <returns>True si la operación fue exitosa.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            var categoria = await ctx.CategoriasProductos.FindAsync(id);
            if (categoria is null)
            {
                return false;
            }

            categoria.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        #endregion

        #endregion
    }
}