using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models.Adopciones;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    /// <summary>
    /// Gestiona las operaciones CRUD para la entidad 'MascotasAdopcion'.
    /// Este servicio es central para manejar el catálogo de mascotas disponibles para adopción,
    /// incluyendo su información detallada y relaciones con razas, estados, etc.
    /// </summary>
    public class MascotasAdopcionService(IDbContextFactory<ApplicationDbContext> dbFactory) : ICRUD<MascotasAdopcion>
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
        /// Inserta una nueva mascota de adopción en la base de datos.
        /// </summary>
        /// <param name="elem">El objeto 'MascotasAdopcion' a ser insertado.</param>
        /// <returns>True si la operación fue exitosa, de lo contrario False.</returns>
        public async Task<bool> InsertAsync(MascotasAdopcion elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            ctx.MascotasAdopcion.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Actualiza una mascota de adopción existente en la base de datos.
        /// </summary>
        /// <param name="elem">El objeto 'MascotasAdopcion' con los datos actualizados.</param>
        /// <returns>True si la actualización fue exitosa, de lo contrario False.</returns>
        public async Task<bool> UpdateAsync(MascotasAdopcion elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            var existingPet = await ctx.MascotasAdopcion.FindAsync(elem.MascotasAdopcionID);
            if (existingPet is null)
            {
                return false; // No se puede actualizar una mascota que no existe.
            }

            // Utiliza SetValues para una actualización eficiente y mantenible de las propiedades.
            ctx.Entry(existingPet).CurrentValues.SetValues(elem);

            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Guarda una mascota (Upsert): La inserta si es nueva o la actualiza si ya existe.
        /// </summary>
        /// <param name="elem">La mascota a guardar.</param>
        /// <returns>True si la operación de guardado fue exitosa.</returns>
        public async Task<bool> SaveAsync(MascotasAdopcion elem)
        {
            if (elem.MascotasAdopcionID == 0)
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
        /// Obtiene una lista de mascotas de adopción que cumplen con un criterio de búsqueda.
        /// Carga de forma anticipada (Eager Loading) las entidades relacionadas, incluyendo
        /// la relación anidada 'Especie' a través de 'Razas'.
        /// </summary>
        /// <param name="criteria">Una expresión lambda para filtrar las mascotas.</param>
        /// <returns>Una lista de objetos 'MascotasAdopcion'.</returns>
        public async Task<List<MascotasAdopcion>> ListAsync(Expression<Func<MascotasAdopcion, bool>> criteria)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.MascotasAdopcion
                .Include(m => m.Razas)
                    .ThenInclude(r => r!.Especie) // Carga la especie a través de la raza
                .Include(m => m.Estado)
                .Include(m => m.RelacionSize)
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        /// <summary>
        /// Busca una mascota de adopción por su ID, incluyendo todos sus datos relacionados.
        /// </summary>
        /// <param name="id">El ID de la mascota a buscar.</param>
        /// <returns>El objeto 'MascotasAdopcion' encontrado, o un nuevo objeto vacío si no se encuentra.</returns>
        public async Task<MascotasAdopcion> SearchByIdAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.MascotasAdopcion
                .Include(m => m.Razas)
                    .ThenInclude(r => r!.Especie)
                .Include(m => m.Estado)
                .Include(m => m.RelacionSize)
                .FirstOrDefaultAsync(m => m.MascotasAdopcionID == id) ?? new();
        }

        /// <summary>
        /// Comprueba si existe una mascota de adopción con el ID especificado.
        /// </summary>
        /// <param name="id">El ID de la mascota a verificar.</param>
        /// <returns>True si la mascota existe, de lo contrario False.</returns>
        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.MascotasAdopcion.AnyAsync(m => m.MascotasAdopcionID == id);
        }

        #endregion

        #region Delete Operations

        /// <summary>
        /// Realiza un borrado lógico (soft delete) de una mascota de adopción.
        /// </summary>
        /// <param name="id">El ID de la mascota a marcar como borrada.</param>
        /// <returns>True si la operación fue exitosa.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            var mascota = await ctx.MascotasAdopcion.FindAsync(id);
            if (mascota is null)
            {
                return false;
            }

            mascota.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        #endregion

        #endregion
    }
}