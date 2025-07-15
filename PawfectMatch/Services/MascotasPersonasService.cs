using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models.Servicios;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    /// <summary>
    /// Gestiona las operaciones CRUD para la entidad 'MascotasPersonas'.
    /// Este servicio se encarga de la l�gica de negocio para las mascotas que son propiedad de los clientes,
    /// registrando su informaci�n y su relaci�n con el due�o.
    /// </summary>
    public class MascotasPersonasService(IDbContextFactory<ApplicationDbContext> dbFactory) : ICRUD<MascotasPersonas>
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
        /// Inserta una nueva mascota de cliente en la base de datos.
        /// </summary>
        /// <param name="elem">El objeto 'MascotasPersonas' a ser insertado.</param>
        /// <returns>True si la operaci�n fue exitosa, de lo contrario False.</returns>
        public async Task<bool> InsertAsync(MascotasPersonas elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            ctx.MascotasPersonas.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Actualiza una mascota de cliente existente en la base de datos.
        /// </summary>
        /// <param name="elem">El objeto 'MascotasPersonas' con los datos actualizados.</param>
        /// <returns>True si la actualizaci�n fue exitosa, de lo contrario False.</returns>
        public async Task<bool> UpdateAsync(MascotasPersonas elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            var existingPet = await ctx.MascotasPersonas.FindAsync(elem.MascotasPersonasID);
            if (existingPet is null)
            {
                return false; // No se puede actualizar una mascota que no existe.
            }

            // Utiliza SetValues para una actualizaci�n eficiente y mantenible.
            ctx.Entry(existingPet).CurrentValues.SetValues(elem);

            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Guarda una mascota de cliente (Upsert): La inserta si es nueva o la actualiza si ya existe.
        /// </summary>
        /// <param name="elem">La mascota a guardar.</param>
        /// <returns>True si la operaci�n de guardado fue exitosa.</returns>
        public async Task<bool> SaveAsync(MascotasPersonas elem)
        {
            if (elem.MascotasPersonasID == 0)
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
        /// Obtiene una lista de mascotas de clientes que cumplen con un criterio de b�squeda.
        /// Carga de forma anticipada (Eager Loading) las entidades relacionadas 'Personas' y 'Razas'.
        /// </summary>
        /// <param name="criteria">Una expresi�n lambda para filtrar las mascotas.</param>
        /// <returns>Una lista de objetos 'MascotasPersonas'.</returns>
        public async Task<List<MascotasPersonas>> ListAsync(Expression<Func<MascotasPersonas, bool>> criteria)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.MascotasPersonas
                .Include(m => m.Personas)
                .Include(m => m.Razas)
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        /// <summary>
        /// Busca una mascota de cliente por su ID, incluyendo sus datos relacionados.
        /// </summary>
        /// <param name="id">El ID de la mascota a buscar.</param>
        /// <returns>El objeto 'MascotasPersonas' encontrado, o un nuevo objeto vac�o si no se encuentra.</returns>
        public async Task<MascotasPersonas> SearchByIdAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.MascotasPersonas
                .Include(m => m.Personas)
                .Include(m => m.Razas)
                .FirstOrDefaultAsync(m => m.MascotasPersonasID == id) ?? new();
        }

        /// <summary>
        /// Comprueba si existe una mascota de cliente con el ID especificado.
        /// </summary>
        /// <param name="id">El ID de la mascota a verificar.</param>
        /// <returns>True si la mascota existe, de lo contrario False.</returns>
        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.MascotasPersonas.AnyAsync(m => m.MascotasPersonasID == id);
        }

        #endregion

        #region Delete Operations

        /// <summary>
        /// Realiza un borrado l�gico (soft delete) de una mascota de cliente.
        /// </summary>
        /// <param name="id">El ID de la mascota a marcar como borrada.</param>
        /// <returns>True si la operaci�n fue exitosa.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            var mascotaPersona = await ctx.MascotasPersonas.FindAsync(id);
            if (mascotaPersona is null)
            {
                return false;
            }

            mascotaPersona.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        #endregion

        #endregion
    }
}