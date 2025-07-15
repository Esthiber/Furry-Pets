using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models.Adopciones;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    /// <summary>
    /// Gestiona las operaciones CRUD para la entidad 'EstadoMascota'.
    /// Este servicio se encarga de la lógica de negocio para los diferentes estados
    /// que puede tener una mascota (ej. 'Disponible', 'En Proceso de Adopción', 'Adoptado').
    /// </summary>
    public class EstadoMascotaService(IDbContextFactory<ApplicationDbContext> dbFactory) : ICRUD<EstadoMascota>
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
        /// Inserta un nuevo estado de mascota en la base de datos.
        /// </summary>
        /// <param name="elem">El objeto 'EstadoMascota' a ser insertado.</param>
        /// <returns>True si la operación fue exitosa, de lo contrario False.</returns>
        public async Task<bool> InsertAsync(EstadoMascota elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            ctx.EstadoMascota.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Actualiza un estado de mascota existente en la base de datos.
        /// </summary>
        /// <param name="elem">El objeto 'EstadoMascota' con los datos actualizados.</param>
        /// <returns>True si la actualización fue exitosa, de lo contrario False.</returns>
        public async Task<bool> UpdateAsync(EstadoMascota elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            var existingState = await ctx.EstadoMascota.FindAsync(elem.EstadoMascotaID);
            if (existingState is null)
            {
                return false; // No se puede actualizar un estado que no existe.
            }

            // Utiliza SetValues para una actualización eficiente y mantenible de las propiedades.
            ctx.Entry(existingState).CurrentValues.SetValues(elem);

            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Guarda un estado de mascota (Upsert): Lo inserta si es nuevo o lo actualiza si ya existe.
        /// </summary>
        /// <param name="elem">El estado de mascota a guardar.</param>
        /// <returns>True si la operación de guardado fue exitosa.</returns>
        public async Task<bool> SaveAsync(EstadoMascota elem)
        {
            if (elem.EstadoMascotaID == 0)
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
        /// Obtiene una lista de estados de mascota que cumplen con un criterio de búsqueda.
        /// </summary>
        /// <param name="criteria">Una expresión lambda para filtrar los estados.</param>
        /// <returns>Una lista de objetos 'EstadoMascota'.</returns>
        public async Task<List<EstadoMascota>> ListAsync(Expression<Func<EstadoMascota, bool>> criteria)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.EstadoMascota
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        /// <summary>
        /// Busca un estado de mascota por su ID.
        /// </summary>
        /// <param name="id">El ID del estado a buscar.</param>
        /// <returns>El objeto 'EstadoMascota' encontrado, o un nuevo objeto vacío si no se encuentra.</returns>
        public async Task<EstadoMascota> SearchByIdAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            // FindAsync es el método más optimizado para buscar por clave primaria.
            return await ctx.EstadoMascota.FindAsync(id) ?? new();
        }

        /// <summary>
        /// Comprueba si existe un estado de mascota con el ID especificado.
        /// </summary>
        /// <param name="id">El ID del estado a verificar.</param>
        /// <returns>True si el estado existe, de lo contrario False.</returns>
        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.EstadoMascota.AnyAsync(e => e.EstadoMascotaID == id);
        }

        #endregion

        #region Delete Operations

        /// <summary>
        /// Realiza un borrado lógico (soft delete) de un estado de mascota.
        /// </summary>
        /// <param name="id">El ID del estado a marcar como borrado.</param>
        /// <returns>True si la operación fue exitosa.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            var estadoMascota = await ctx.EstadoMascota.FindAsync(id);
            if (estadoMascota is null)
            {
                return false;
            }

            estadoMascota.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        #endregion

        #endregion
    }
}