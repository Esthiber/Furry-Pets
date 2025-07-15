using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    /// <summary>
    /// Gestiona las operaciones CRUD para la entidad 'EstadosCitas'.
    /// Este servicio maneja la lógica de negocio para los diferentes estados
    /// que puede tener una cita (ej. 'Pendiente', 'Confirmada', 'Cancelada', 'Completada').
    /// </summary>
    public class EstadosCitasService(IDbContextFactory<ApplicationDbContext> dbFactory) : ICRUD<EstadosCitas>
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
        /// Inserta un nuevo estado de cita en la base de datos.
        /// </summary>
        /// <param name="elem">El objeto 'EstadosCitas' a ser insertado.</param>
        /// <returns>True si la operación fue exitosa, de lo contrario False.</returns>
        public async Task<bool> InsertAsync(EstadosCitas elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            ctx.EstadosCitas.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Actualiza un estado de cita existente en la base de datos.
        /// </summary>
        /// <param name="elem">El objeto 'EstadosCitas' con los datos actualizados.</param>
        /// <returns>True si la actualización fue exitosa, de lo contrario False.</returns>
        public async Task<bool> UpdateAsync(EstadosCitas elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            var existingState = await ctx.EstadosCitas.FindAsync(elem.EstadosCitasID);
            if (existingState is null)
            {
                return false; // No se puede actualizar un estado que no existe.
            }

            // Utiliza SetValues para una actualización eficiente y mantenible.
            ctx.Entry(existingState).CurrentValues.SetValues(elem);

            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Guarda un estado de cita (Upsert): Lo inserta si es nuevo o lo actualiza si ya existe.
        /// </summary>
        /// <param name="elem">El estado de cita a guardar.</param>
        /// <returns>True si la operación de guardado fue exitosa.</returns>
        public async Task<bool> SaveAsync(EstadosCitas elem)
        {
            if (elem.EstadosCitasID == 0)
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
        /// Obtiene una lista de estados de cita que cumplen con un criterio de búsqueda.
        /// </summary>
        /// <param name="criteria">Una expresión lambda para filtrar los estados.</param>
        /// <returns>Una lista de objetos 'EstadosCitas'.</returns>
        public async Task<List<EstadosCitas>> ListAsync(Expression<Func<EstadosCitas, bool>> criteria)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.EstadosCitas
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        /// <summary>
        /// Busca un estado de cita por su ID.
        /// </summary>
        /// <param name="id">El ID del estado a buscar.</param>
        /// <returns>El objeto 'EstadosCitas' encontrado, o un nuevo objeto vacío si no se encuentra.</returns>
        public async Task<EstadosCitas> SearchByIdAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            // FindAsync es el método más optimizado para buscar por clave primaria.
            return await ctx.EstadosCitas.FindAsync(id) ?? new();
        }

        /// <summary>
        /// Comprueba si existe un estado de cita con el ID especificado.
        /// </summary>
        /// <param name="id">El ID del estado a verificar.</param>
        /// <returns>True si el estado existe, de lo contrario False.</returns>
        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.EstadosCitas.AnyAsync(e => e.EstadosCitasID == id);
        }

        #endregion

        #region Delete Operations

        /// <summary>
        /// Realiza un borrado lógico (soft delete) de un estado de cita.
        /// </summary>
        /// <param name="id">El ID del estado a marcar como borrado.</param>
        /// <returns>True si la operación fue exitosa.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            var estadoCita = await ctx.EstadosCitas.FindAsync(id);
            if (estadoCita is null)
            {
                return false;
            }

            estadoCita.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        #endregion

        #endregion
    }
}