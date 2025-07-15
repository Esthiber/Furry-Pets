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
    /// Gestiona las operaciones CRUD para la entidad 'Pagos'.
    /// Este servicio se encarga de la lógica de negocio para registrar y administrar
    /// los pagos asociados a las facturas del sistema.
    /// </summary>
    public class PagosService(IDbContextFactory<ApplicationDbContext> dbFactory) : ICRUD<Pagos>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory = dbFactory;

        #region Create / Update Operations

        /// <summary>
        /// Inserta un nuevo pago en la base de datos.
        /// </summary>
        /// <param name="elem">El objeto 'Pagos' a ser insertado.</param>
        /// <returns>True si la inserción fue exitosa.</returns>
        public async Task<bool> InsertAsync(Pagos elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            ctx.Pagos.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Actualiza un pago existente en la base de datos de forma eficiente y segura.
        /// </summary>
        /// <param name="elem">El objeto 'Pagos' con los datos actualizados.</param>
        /// <returns>True si la actualización fue exitosa.</returns>
        public async Task<bool> UpdateAsync(Pagos elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            var existingPago = await ctx.Pagos.FindAsync(elem.PagosID);

            if (existingPago is null)
            {
                return false; // No se puede actualizar un pago que no existe.
            }

            // Uso de SetValues para una actualización robusta y fácil de mantener.
            ctx.Entry(existingPago).CurrentValues.SetValues(elem);

            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Guarda un pago (Upsert): Lo inserta si es nuevo o lo actualiza si ya existe.
        /// Este método es eficiente al determinar la operación en memoria, sin consultas extra.
        /// </summary>
        /// <param name="elem">El pago a guardar.</param>
        /// <returns>True si la operación de guardado fue exitosa.</returns>
        public async Task<bool> SaveAsync(Pagos elem)
        {
            // Determina la operación en memoria, evitando una consulta extra a la BD.
            return elem.PagosID == 0
                ? await InsertAsync(elem)
                : await UpdateAsync(elem);
        }

        #endregion

        #region Read Operations

        /// <summary>
        /// Obtiene una lista de pagos que cumplen con un criterio de búsqueda.
        /// Carga de forma anticipada la entidad relacionada 'Factura'.
        /// </summary>
        /// <param name="criteria">Una expresión lambda para filtrar los pagos.</param>
        /// <returns>Una lista de objetos 'Pagos'.</returns>
        public async Task<List<Pagos>> ListAsync(Expression<Func<Pagos, bool>> criteria)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.Pagos
                .Include(p => p.Factura)
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        /// <summary>
        /// Busca un pago por su ID, incluyendo su factura relacionada.
        /// </summary>
        /// <param name="id">El ID del pago a buscar.</param>
        /// <returns>
        /// El objeto 'Pagos' encontrado, o un nuevo objeto 'Pagos' vacío si no se encuentra,
        /// para evitar referencias nulas.
        /// </returns>
        public async Task<Pagos> SearchByIdAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.Pagos
                .Include(p => p.Factura)
                .FirstOrDefaultAsync(p => p.PagosID == id) ?? new Pagos();
        }

        /// <summary>
        /// Comprueba si existe un pago con el ID especificado.
        /// </summary>
        /// <param name="id">El ID del pago a verificar.</param>
        /// <returns>True si el pago existe, de lo contrario False.</returns>
        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.Pagos.AnyAsync(p => p.PagosID == id);
        }

        #endregion

        #region Delete Operations

        /// <summary>
        /// Realiza un borrado lógico (soft delete) de un pago.
        /// </summary>
        /// <param name="id">El ID del pago a marcar como borrado.</param>
        /// <returns>True si la operación fue exitosa.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            var pago = await ctx.Pagos.FindAsync(id);
            if (pago is null)
            {
                return false;
            }

            pago.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        #endregion
    }
}