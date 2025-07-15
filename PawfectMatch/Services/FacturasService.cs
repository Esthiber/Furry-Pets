using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models.POS;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    /// <summary>
    /// Gestiona las operaciones CRUD para la entidad 'Facturas'.
    /// Este servicio se encarga de la lógica de negocio para crear, leer,
    /// actualizar y anular (soft delete) facturas completas, incluyendo sus líneas de detalle.
    /// </summary>
    public class FacturasService(IDbContextFactory<ApplicationDbContext> dbFactory) : ICRUD<Facturas>
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
        /// Inserta una nueva factura, incluyendo sus detalles, en la base de datos.
        /// </summary>
        /// <param name="elem">El objeto 'Facturas' a ser insertado. Se asume que su propiedad 'Detalles' ya está poblada.</param>
        /// <returns>True si la operación fue exitosa, de lo contrario False.</returns>
        public async Task<bool> InsertAsync(Facturas elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            ctx.Facturas.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Actualiza una factura existente. Este método maneja la actualización de las propiedades
        /// de la factura y la sincronización de sus líneas de detalle (añadir, actualizar, eliminar).
        /// </summary>
        /// <param name="elem">El objeto 'Facturas' con los datos actualizados.</param>
        /// <returns>True si la actualización fue exitosa, de lo contrario False.</returns>
        public async Task<bool> UpdateAsync(Facturas elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            var existingFactura = await ctx.Facturas
                .Include(f => f.Detalles) // Cargar los detalles existentes para compararlos
                .FirstOrDefaultAsync(f => f.FacturasID == elem.FacturasID);

            if (existingFactura is null)
            {
                return false; // No se puede actualizar una factura que no existe.
            }

            // 1. Actualizar las propiedades de la factura principal
            ctx.Entry(existingFactura).CurrentValues.SetValues(elem);

            // 2. Sincronizar las líneas de detalle
            // Eliminar detalles que ya no están en la nueva lista
            var detallesAEliminar = existingFactura.Detalles
                .Where(d => !elem.Detalles.Any(newD => newD.DetallesFacturasID == d.DetallesFacturasID))
                .ToList();
            ctx.DetalleFacturas.RemoveRange(detallesAEliminar);

            // Actualizar o añadir nuevos detalles
            foreach (var detalle in elem.Detalles)
            {
                var existingDetalle = existingFactura.Detalles
                    .FirstOrDefault(d => d.DetallesFacturasID == detalle.DetallesFacturasID);

                if (existingDetalle != null)
                {
                    // El detalle ya existe, así que lo actualizamos
                    ctx.Entry(existingDetalle).CurrentValues.SetValues(detalle);
                }
                else
                {
                    // Es un nuevo detalle, así que lo añadimos a la factura existente
                    existingFactura.Detalles.Add(detalle);
                }
            }

            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Guarda una factura (Upsert): La inserta si es nueva o la actualiza si ya existe.
        /// </summary>
        /// <param name="elem">La factura a guardar.</param>
        /// <returns>True si la operación de guardado fue exitosa.</returns>
        public async Task<bool> SaveAsync(Facturas elem)
        {
            if (elem.FacturasID == 0)
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
        /// Obtiene una lista de facturas que cumplen con un criterio de búsqueda.
        /// Carga de forma anticipada las entidades relacionadas 'Persona', 'EstadoPago' y 'Detalles'.
        /// </summary>
        /// <param name="criteria">Una expresión lambda para filtrar las facturas.</param>
        /// <returns>Una lista de objetos 'Facturas'.</returns>
        public async Task<List<Facturas>> ListAsync(Expression<Func<Facturas, bool>> criteria)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.Facturas
                .Include(f => f.Persona)
                .Include(f => f.EstadoPago)
                .Include(f => f.Detalles)
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        /// <summary>
        /// Busca una factura por su ID, incluyendo todos sus datos relacionados.
        /// </summary>
        /// <param name="id">El ID de la factura a buscar.</param>
        /// <returns>El objeto 'Facturas' encontrado, o un nuevo objeto vacío si no se encuentra.</returns>
        public async Task<Facturas> SearchByIdAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.Facturas
                .Include(f => f.Persona)
                .Include(f => f.EstadoPago)
                .Include(f => f.Detalles)
                .FirstOrDefaultAsync(f => f.FacturasID == id) ?? new();
        }

        /// <summary>
        /// Comprueba si existe una factura con el ID especificado.
        /// </summary>
        /// <param name="id">El ID de la factura a verificar.</param>
        /// <returns>True si la factura existe, de lo contrario False.</returns>
        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.Facturas.AnyAsync(f => f.FacturasID == id);
        }

        #endregion

        #region Delete Operations

        /// <summary>
        /// Realiza un borrado lógico (soft delete) de una factura.
        /// </summary>
        /// <dev.note>
        /// Esta operación solo marca la cabecera de la factura como borrada.
        /// No afecta el estado 'IsDeleted' de sus líneas de detalle.
        /// </dev.note>
        /// <param name="id">El ID de la factura a marcar como borrada.</param>
        /// <returns>True si la operación fue exitosa.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            var factura = await ctx.Facturas.FindAsync(id);
            if (factura is null)
            {
                return false;
            }

            factura.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        #endregion

        #endregion
    }
}