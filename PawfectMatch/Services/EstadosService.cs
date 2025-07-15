using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    /// <summary>
    /// Gestiona las operaciones CRUD para la entidad genérica 'Estados'.
    /// Este servicio se encarga de la lógica de negocio para los diferentes estados
    /// que pueden ser utilizados por varias entidades en el sistema (ej. 'Activo', 'Inactivo', 'Pendiente').
    /// </summary>
    public class EstadosService(IDbContextFactory<ApplicationDbContext> dbFactory) : ICRUD<Estados>
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
        /// Inserta un nuevo estado en la base de datos.
        /// </summary>
        /// <param name="elem">El objeto 'Estados' a ser insertado.</param>
        /// <returns>True si la operación fue exitosa, de lo contrario False.</returns>
        public async Task<bool> InsertAsync(Estados elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            ctx.Estados.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Actualiza un estado existente en la base de datos.
        /// </summary>
        /// <param name="elem">El objeto 'Estados' con los datos actualizados.</param>
        /// <returns>True si la actualización fue exitosa, de lo contrario False.</returns>
        public async Task<bool> UpdateAsync(Estados elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            var existingState = await ctx.Estados.FindAsync(elem.EstadoID);
            if (existingState is null)
            {
                return false; // No se puede actualizar un estado que no existe.
            }

            // Utiliza SetValues para una actualización eficiente y mantenible de las propiedades.
            ctx.Entry(existingState).CurrentValues.SetValues(elem);

            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Guarda un estado (Upsert): Lo inserta si es nuevo o lo actualiza si ya existe.
        /// </summary>
        /// <param name="elem">El estado a guardar.</param>
        /// <returns>True si la operación de guardado fue exitosa.</returns>
        public async Task<bool> SaveAsync(Estados elem)
        {
            if (elem.EstadoID == 0)
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
        /// Obtiene una lista de estados que cumplen con un criterio de búsqueda.
        /// </summary>
        /// <param name="criteria">Una expresión lambda para filtrar los estados.</param>
        /// <returns>Una lista de objetos 'Estados'.</returns>
        public async Task<List<Estados>> ListAsync(Expression<Func<Estados, bool>> criteria)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.Estados
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        /// <summary>
        /// Busca un estado por su ID.
        /// </summary>
        /// <param name="id">El ID del estado a buscar.</param>
        /// <returns>El objeto 'Estados' encontrado, o un nuevo objeto vacío si no se encuentra.</returns>
        public async Task<Estados> SearchByIdAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            // FindAsync es el método más optimizado para buscar por clave primaria.
            return await ctx.Estados.FindAsync(id) ?? new();
        }

        /// <summary>
        /// Comprueba si existe un estado con el ID especificado.
        /// </summary>
        /// <param name="id">El ID del estado a verificar.</param>
        /// <returns>True si el estado existe, de lo contrario False.</returns>
        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.Estados.AnyAsync(e => e.EstadoID == id);
        }

        #endregion

        #region Delete Operations

        /// <summary>
        /// Realiza un borrado lógico (soft delete) de un estado.
        /// </summary>
        /// <param name="id">El ID del estado a marcar como borrado.</param>
        /// <returns>True si la operación fue exitosa.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            var estado = await ctx.Estados.FindAsync(id);
            if (estado is null)
            {
                return false;
            }

            estado.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        #endregion

        #endregion
    }
}