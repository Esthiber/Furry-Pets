using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models.POS;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    /// <summary>
    /// Gestiona las operaciones CRUD para la entidad 'EstadosPagos'.
    /// Este servicio se encarga de la lógica de negocio para los diferentes estados
    /// que puede tener un pago (ej. 'Pendiente', 'Pagado', 'Cancelado', 'Reembolsado').
    /// </summary>
    public class EstadosPagosService(IDbContextFactory<ApplicationDbContext> dbFactory) : ICRUD<EstadosPagos>
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
        /// Inserta un nuevo estado de pago en la base de datos.
        /// </summary>
        /// <param name="elem">El objeto 'EstadosPagos' a ser insertado.</param>
        /// <returns>True si la operación fue exitosa, de lo contrario False.</returns>
        public async Task<bool> InsertAsync(EstadosPagos elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            ctx.EstadosPagos.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Actualiza un estado de pago existente en la base de datos.
        /// </summary>
        /// <param name="elem">El objeto 'EstadosPagos' con los datos actualizados.</param>
        /// <returns>True si la actualización fue exitosa, de lo contrario False.</returns>
        public async Task<bool> UpdateAsync(EstadosPagos elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            var existingState = await ctx.EstadosPagos.FindAsync(elem.EstadosPagosID);
            if (existingState is null)
            {
                return false; // No se puede actualizar un estado que no existe.
            }

            // Utiliza SetValues para una actualización eficiente y mantenible de las propiedades.
            ctx.Entry(existingState).CurrentValues.SetValues(elem);

            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Guarda un estado de pago (Upsert): Lo inserta si es nuevo o lo actualiza si ya existe.
        /// </summary>
        /// <param name="elem">El estado de pago a guardar.</param>
        /// <returns>True si la operación de guardado fue exitosa.</returns>
        public async Task<bool> SaveAsync(EstadosPagos elem)
        {
            if (elem.EstadosPagosID == 0)
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
        /// Obtiene una lista de estados de pago que cumplen con un criterio de búsqueda.
        /// </summary>
        /// <param name="criteria">Una expresión lambda para filtrar los estados.</param>
        /// <returns>Una lista de objetos 'EstadosPagos'.</returns>
        public async Task<List<EstadosPagos>> ListAsync(Expression<Func<EstadosPagos, bool>> criteria)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.EstadosPagos
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        /// <summary>
        /// Busca un estado de pago por su ID.
        /// </summary>
        /// <param name="id">El ID del estado a buscar.</param>
        /// <returns>El objeto 'EstadosPagos' encontrado, o un nuevo objeto vacío si no se encuentra.</returns>
        public async Task<EstadosPagos> SearchByIdAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            // FindAsync es el método más optimizado para buscar por clave primaria.
            return await ctx.EstadosPagos.FindAsync(id) ?? new();
        }

        /// <summary>
        /// Comprueba si existe un estado de pago con el ID especificado.
        /// </summary>
        /// <param name="id">El ID del estado a verificar.</param>
        /// <returns>True si el estado existe, de lo contrario False.</returns>
        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.EstadosPagos.AnyAsync(e => e.EstadosPagosID == id);
        }

        #endregion

        #region Delete Operations

        /// <summary>
        /// Realiza un borrado lógico (soft delete) de un estado de pago.
        /// </summary>
        /// <param name="id">El ID del estado a marcar como borrado.</param>
        /// <returns>True si la operación fue exitosa.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            var estadoPago = await ctx.EstadosPagos.FindAsync(id);
            if (estadoPago is null)
            {
                return false;
            }

            estadoPago.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        #endregion

        #endregion
    }
}