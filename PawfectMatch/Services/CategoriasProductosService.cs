using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models.POS;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    /// <summary>
    /// Gestiona las operaciones CRUD para la entidad 'CategoriasProductos'.
    /// Esta clase se encarga de toda la l�gica de negocio relacionada con la creaci�n,
    /// lectura, actualizaci�n y borrado de las categor�as de productos.
    /// </summary>
    public class CategoriasProductosService(IDbContextFactory<ApplicationDbContext> dbFactory) : ICRUD<CategoriasProductos>
    {
        #region Dependencies & Constructor
        /// <summary>
        /// F�brica para crear instancias de ApplicationDbContext, inyectada v�a constructor primario.
        /// </summary>
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory = dbFactory;
        #endregion

        #region ICRUD Implementation

        #region Create / Update Operations

        /// <summary>
        /// Inserta una nueva categor�a de producto en la base de datos.
        /// </summary>
        /// <param name="elem">El objeto 'CategoriasProductos' a ser insertado.</param>
        /// <returns>True si la operaci�n fue exitosa, de lo contrario False.</returns>
        public async Task<bool> InsertAsync(CategoriasProductos elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            ctx.CategoriasProductos.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Actualiza una categor�a de producto existente en la base de datos.
        /// Utiliza 'FindAsync' para una b�squeda eficiente por clave primaria y 'SetValues'
        /// para una actualizaci�n mantenible de las propiedades.
        /// </summary>
        /// <param name="elem">El objeto 'CategoriasProductos' con los datos actualizados.</param>
        /// <returns>True si la actualizaci�n fue exitosa, de lo contrario False.</returns>
        public async Task<bool> UpdateAsync(CategoriasProductos elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            var existingCategory = await ctx.CategoriasProductos.FindAsync(elem.CategoriasProductosID);
            if (existingCategory is null)
            {
                return false; // No se puede actualizar una categor�a que no existe.
            }

            // Copia los valores de las propiedades del objeto de entrada al objeto rastreado por EF.
            // Es m�s seguro y mantenible que la asignaci�n manual.
            ctx.Entry(existingCategory).CurrentValues.SetValues(elem);

            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Guarda una categor�a (Upsert): La inserta si es nueva o la actualiza si ya existe.
        /// </summary>
        /// <param name="elem">La categor�a a guardar.</param>
        /// <returns>True si la operaci�n de guardado fue exitosa.</returns>
        public async Task<bool> SaveAsync(CategoriasProductos elem)
        {
            // Se determina si es un nuevo registro verificando si el ID es 0 (valor por defecto).
            // Esto es m�s eficiente que hacer una consulta previa a la base de datos.
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
        /// Obtiene una lista de categor�as que cumplen con un criterio de b�squeda.
        /// Utiliza 'AsNoTracking' para mejorar el rendimiento, ya que los resultados son de solo lectura.
        /// </summary>
        /// <param name="criteria">Una expresi�n lambda para filtrar las categor�as.</param>
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
        /// Busca una categor�a de producto por su ID.
        /// </summary>
        /// <param name="id">El ID de la categor�a a buscar.</param>
        /// <returns>El objeto 'CategoriasProductos' encontrado, o un nuevo objeto vac�o si no se encuentra.</returns>
        public async Task<CategoriasProductos> SearchByIdAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.CategoriasProductos
                .FirstOrDefaultAsync(c => c.CategoriasProductosID == id) ?? new();
        }

        /// <summary>
        /// Comprueba si existe una categor�a con el ID especificado.
        /// </summary>
        /// <param name="id">El ID a verificar.</param>
        /// <returns>True si la categor�a existe, de lo contrario False.</returns>
        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.CategoriasProductos.AnyAsync(c => c.CategoriasProductosID == id);
        }

        #endregion

        #region Delete Operations

        /// <summary>
        /// Realiza un borrado l�gico (soft delete) de una categor�a de producto.
        /// La entidad no se elimina f�sicamente, solo se marca como 'IsDeleted = true'.
        /// </summary>
        /// <param name="id">El ID de la categor�a a marcar como borrada.</param>
        /// <returns>True si la operaci�n fue exitosa.</returns>
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