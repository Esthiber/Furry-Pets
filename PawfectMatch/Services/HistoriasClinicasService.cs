using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    /// <summary>
    /// Gestiona las operaciones CRUD para la entidad 'HistoriasClinicas'.
    /// Este servicio se encarga de la l�gica de negocio para registrar y consultar
    /// las visitas y tratamientos m�dicos de las mascotas.
    /// </summary>
    public class HistoriasClinicasService(IDbContextFactory<ApplicationDbContext> dbFactory) : ICRUD<HistoriasClinicas>
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
        /// Inserta un nuevo registro de historia cl�nica en la base de datos.
        /// </summary>
        /// <param name="elem">El objeto 'HistoriasClinicas' a ser insertado.</param>
        /// <returns>True si la operaci�n fue exitosa, de lo contrario False.</returns>
        public async Task<bool> InsertAsync(HistoriasClinicas elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            ctx.HistoriasClinicas.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Actualiza un registro de historia cl�nica existente.
        /// </summary>
        /// <param name="elem">El objeto 'HistoriasClinicas' con los datos actualizados.</param>
        /// <returns>True si la actualizaci�n fue exitosa, de lo contrario False.</returns>
        public async Task<bool> UpdateAsync(HistoriasClinicas elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            var existingHistory = await ctx.HistoriasClinicas.FindAsync(elem.HistoriasClinicaID);
            if (existingHistory is null)
            {
                return false; // No se puede actualizar una historia que no existe.
            }

            // Utiliza SetValues para una actualizaci�n eficiente y mantenible.
            ctx.Entry(existingHistory).CurrentValues.SetValues(elem);

            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Guarda un registro de historia cl�nica (Upsert): lo inserta si es nuevo o lo actualiza si ya existe.
        /// </summary>
        /// <param name="elem">La historia cl�nica a guardar.</param>
        /// <returns>True si la operaci�n de guardado fue exitosa.</returns>
        public async Task<bool> SaveAsync(HistoriasClinicas elem)
        {
            if (elem.HistoriasClinicaID == 0)
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
        /// Obtiene una lista de historias cl�nicas que cumplen con un criterio de b�squeda.
        /// Carga de forma anticipada (Eager Loading) las entidades relacionadas 'MascotaPersona' y 'Veterinario'.
        /// </summary>
        /// <param name="criteria">Una expresi�n lambda para filtrar las historias.</param>
        /// <returns>Una lista de objetos 'HistoriasClinicas'.</returns>
        public async Task<List<HistoriasClinicas>> ListAsync(Expression<Func<HistoriasClinicas, bool>> criteria)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.HistoriasClinicas
                .Include(h => h.MascotaPersona)
                .Include(h => h.Veterinario)
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        /// <summary>
        /// Busca un registro de historia cl�nica por su ID, incluyendo sus datos relacionados.
        /// </summary>
        /// <param name="id">El ID de la historia cl�nica a buscar.</param>
        /// <returns>El objeto 'HistoriasClinicas' encontrado, o un nuevo objeto vac�o si no se encuentra.</returns>
        public async Task<HistoriasClinicas> SearchByIdAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.HistoriasClinicas
                .Include(h => h.MascotaPersona)
                .Include(h => h.Veterinario)
                .FirstOrDefaultAsync(h => h.HistoriasClinicaID == id) ?? new();
        }

        /// <summary>
        /// Comprueba si existe un registro de historia cl�nica con el ID especificado.
        /// </summary>
        /// <param name="id">El ID de la historia a verificar.</param>
        /// <returns>True si la historia existe, de lo contrario False.</returns>
        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.HistoriasClinicas.AnyAsync(h => h.HistoriasClinicaID == id);
        }

        #endregion

        #region Delete Operations

        /// <summary>
        /// Realiza un borrado l�gico (soft delete) de un registro de historia cl�nica.
        /// </summary>
        /// <param name="id">El ID de la historia a marcar como borrada.</param>
        /// <returns>True si la operaci�n fue exitosa.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            var historia = await ctx.HistoriasClinicas.FindAsync(id);
            if (historia is null)
            {
                return false;
            }

            historia.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        #endregion

        #endregion
    }
}