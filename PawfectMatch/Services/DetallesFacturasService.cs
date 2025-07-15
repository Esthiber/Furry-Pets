using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models.POS;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    /// <summary>
    /// Gestiona las operaciones CRUD para la entidad 'DetallesFacturas'.
    /// Este servicio maneja la lógica de negocio para cada línea de detalle dentro de una factura,
    /// como el producto o servicio vendido, la cantidad y el precio.
    /// </summary>
    public class DetallesFacturasService(IDbContextFactory<ApplicationDbContext> dbFactory) : ICRUD<DetallesFacturas>
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
        /// Inserta un nuevo detalle de factura en la base de datos.
        /// </summary>
        /// <param name="elem">El objeto 'DetallesFacturas' a ser insertado.</param>
        /// <returns>True si la operación fue exitosa, de lo contrario False.</returns>
        public async Task<bool> InsertAsync(DetallesFacturas elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            ctx.DetalleFacturas.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Actualiza un detalle de factura existente en la base de datos.
        /// Utiliza 'SetValues' para una actualización eficiente y mantenible.
        /// </summary>
        /// <param name="elem">El objeto 'DetallesFacturas' con los datos actualizados.</param>
        /// <returns>True si la actualización fue exitosa, de lo contrario False.</returns>
        public async Task<bool> UpdateAsync(DetallesFacturas elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            var existingDetail = await ctx.DetalleFacturas.FindAsync(elem.DetallesFacturasID);
            if (existingDetail is null)
            {
                return false; // No se puede actualizar un detalle que no existe.
            }

            // Copia los valores de las propiedades del objeto de entrada al objeto rastreado por EF.
            ctx.Entry(existingDetail).CurrentValues.SetValues(elem);

            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Guarda un detalle de factura (Upsert): lo inserta si es nuevo o lo actualiza si ya existe.
        /// </summary>
        /// <param name="elem">El detalle de factura a guardar.</param>
        /// <returns>True si la operación de guardado fue exitosa.</returns>
        public async Task<bool> SaveAsync(DetallesFacturas elem)
        {
            if (elem.DetallesFacturasID == 0)
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
        /// Obtiene una lista de detalles de factura que cumplen con un criterio de búsqueda.
        /// Carga de forma anticipada (Eager Loading) las entidades relacionadas 'Factura' y 'TipoItem'.
        /// </summary>
        /// <param name="criteria">Una expresión lambda para filtrar los detalles.</param>
        /// <returns>Una lista de objetos 'DetallesFacturas'.</returns>
        public async Task<List<DetallesFacturas>> ListAsync(Expression<Func<DetallesFacturas, bool>> criteria)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.DetalleFacturas
                .Include(d => d.Factura)
                .Include(d => d.TipoItem)
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        /// <summary>
        /// Busca un detalle de factura por su ID, incluyendo sus datos relacionados.
        /// </summary>
        /// <param name="id">El ID del detalle de factura a buscar.</param>
        /// <returns>El objeto 'DetallesFacturas' encontrado, o un nuevo objeto vacío si no se encuentra.</returns>
        public async Task<DetallesFacturas> SearchByIdAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.DetalleFacturas
                .Include(d => d.Factura)
                .Include(d => d.TipoItem)
                .FirstOrDefaultAsync(d => d.DetallesFacturasID == id) ?? new();
        }

        /// <summary>
        /// Comprueba si existe un detalle de factura con el ID especificado.
        /// </summary>
        /// <param name="id">El ID del detalle a verificar.</param>
        /// <returns>True si el detalle existe, de lo contrario False.</returns>
        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.DetalleFacturas.AnyAsync(d => d.DetallesFacturasID == id);
        }

        #endregion

        #region Delete Operations

        /// <summary>
        /// Realiza un borrado lógico (soft delete) de un detalle de factura.
        /// </summary>
        /// <param name="id">El ID del detalle de factura a marcar como borrado.</param>
        /// <returns>True si la operación fue exitosa.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            var detalle = await ctx.DetalleFacturas.FindAsync(id);
            if (detalle is null)
            {
                return false;
            }

            detalle.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        #endregion

        #endregion
    }
}