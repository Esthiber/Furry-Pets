using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    /// <summary>
    /// Proporciona operaciones CRUD para la entidad 'Citas'.
    /// Este servicio gestiona toda la lógica relacionada con la programación, consulta,
    /// actualización y cancelación de citas en el sistema.
    /// </summary>
    public class CitasService(IDbContextFactory<ApplicationDbContext> dbFactory) : ICRUD<Citas>
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
        /// Registra una nueva cita en la base de datos.
        /// </summary>
        /// <param name="elem">El objeto 'Citas' a ser insertado.</param>
        /// <returns>True si la operación fue exitosa, de lo contrario False.</returns>
        public async Task<bool> InsertAsync(Citas elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            ctx.Citas.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Actualiza una cita existente en la base de datos.
        /// Este método utiliza 'SetValues' para una actualización eficiente y mantenible
        /// de las propiedades de la cita.
        /// </summary>
        /// <param name="elem">El objeto 'Citas' con los datos actualizados.</param>
        /// <returns>True si la actualización fue exitosa, de lo contrario False.</returns>
        public async Task<bool> UpdateAsync(Citas elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            var existingCita = await ctx.Citas.FindAsync(elem.CitasID);
            if (existingCita is null)
            {
                return false; // No se puede actualizar una cita que no existe.
            }

            // Copia los valores de las propiedades escalares del objeto de entrada
            // al objeto que está siendo rastreado por el DbContext.
            // Esto es mucho más mantenible que asignar cada propiedad manualmente.
            ctx.Entry(existingCita).CurrentValues.SetValues(elem);

            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Guarda una cita (Upsert): La inserta si es nueva o la actualiza si ya existe.
        /// </summary>
        /// <param name="elem">La cita a guardar.</param>
        /// <returns>True si la operación de guardado fue exitosa.</returns>
        public async Task<bool> SaveAsync(Citas elem)
        {
            // Determina si es un nuevo registro verificando si el ID es 0.
            // Esto es más eficiente que hacer una consulta previa a la BD con ExistAsync.
            if (elem.CitasID == 0)
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
        /// Obtiene una lista de citas que cumplen con un criterio de búsqueda.
        /// Carga de forma anticipada (Eager Loading) las entidades relacionadas para evitar
        /// consultas N+1. Usa AsNoTracking para mejorar el rendimiento de solo lectura.
        /// </summary>
        /// <param name="criteria">Una expresión lambda para filtrar las citas.</param>
        /// <returns>Una lista de objetos 'Citas' que cumplen el criterio.</returns>
        public async Task<List<Citas>> ListAsync(Expression<Func<Citas, bool>> criteria)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.Citas
                .Include(c => c.Persona)
                .Include(c => c.MascotaPersona)
                .Include(c => c.EstadoCita)
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        /// <summary>
        /// Busca una cita por su ID, incluyendo sus datos relacionados.
        /// </summary>
        /// <param name="id">El ID de la cita a buscar.</param>
        /// <returns>El objeto 'Citas' encontrado, o un nuevo objeto vacío si no se encuentra.</returns>
        public async Task<Citas> SearchByIdAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.Citas
                .Include(c => c.Persona)
                .Include(c => c.MascotaPersona)
                .Include(c => c.EstadoCita)
                .FirstOrDefaultAsync(c => c.CitasID == id) ?? new();
        }

        /// <summary>
        /// Comprueba si existe una cita con el ID especificado.
        /// </summary>
        /// <param name="id">El ID de la cita a verificar.</param>
        /// <returns>True si la cita existe, de lo contrario False.</returns>
        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.Citas.AnyAsync(c => c.CitasID == id);
        }

        #endregion

        #region Delete Operations

        /// <summary>
        /// Realiza un borrado lógico (soft delete) de una cita.
        /// </summary>
        /// <param name="id">El ID de la cita a marcar como borrada.</param>
        /// <returns>True si la operación fue exitosa.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            var cita = await ctx.Citas.FindAsync(id);
            if (cita is null)
            {
                return false;
            }

            cita.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        #endregion

        #endregion
    }
}