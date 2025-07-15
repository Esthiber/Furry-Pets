using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PawfectMatch.Services
{
    /// <summary>
    /// Gestiona las operaciones CRUD para la entidad 'PersonasRoles'.
    /// Este servicio se encarga de asignar y administrar los roles de las personas (usuarios, clientes, etc.)
    /// dentro del sistema.
    /// </summary>
    public class PersonasRolesService(IDbContextFactory<ApplicationDbContext> dbFactory) : ICRUD<PersonasRoles>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory = dbFactory;

        #region Create / Update Operations

        /// <summary>
        /// Inserta una nueva asignaci�n de rol en la base de datos.
        /// </summary>
        /// <param name="elem">El objeto 'PersonasRoles' a ser insertado.</param>
        /// <returns>True si la inserci�n fue exitosa.</returns>
        public async Task<bool> InsertAsync(PersonasRoles elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            ctx.PersonasRoles.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Actualiza una asignaci�n de rol existente en la base de datos.
        /// </summary>
        /// <param name="elem">El objeto 'PersonasRoles' con los datos actualizados.</param>
        /// <returns>True si la actualizaci�n fue exitosa.</returns>
        public async Task<bool> UpdateAsync(PersonasRoles elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            var existingPersonaRol = await ctx.PersonasRoles.FindAsync(elem.PersonasRolesID);

            if (existingPersonaRol is null)
            {
                return false; // No se puede actualizar una asignaci�n que no existe.
            }

            // Uso de SetValues para una actualizaci�n robusta y mantenible.
            ctx.Entry(existingPersonaRol).CurrentValues.SetValues(elem);

            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Guarda una asignaci�n de rol (Upsert): La inserta si es nueva o la actualiza si ya existe.
        /// </summary>
        /// <param name="elem">La asignaci�n de rol a guardar.</param>
        /// <returns>True si la operaci�n de guardado fue exitosa.</returns>
        public async Task<bool> SaveAsync(PersonasRoles elem)
        {
            // Determina la operaci�n en memoria (Insert vs Update) bas�ndose en el ID,
            // lo que evita una consulta extra a la base de datos.
            return elem.PersonasRolesID == 0
                ? await InsertAsync(elem)
                : await UpdateAsync(elem);
        }

        #endregion

        #region Read Operations

        /// <summary>
        /// Obtiene una lista de asignaciones de roles que cumplen con un criterio de b�squeda.
        /// Carga de forma anticipada la entidad relacionada 'Persona'.
        /// </summary>
        /// <param name="criteria">Una expresi�n lambda para filtrar las asignaciones.</param>
        /// <returns>Una lista de objetos 'PersonasRoles'.</returns>
        public async Task<List<PersonasRoles>> ListAsync(Expression<Func<PersonasRoles, bool>> criteria)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.PersonasRoles
                .Include(p => p.Persona)
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        /// <summary>
        /// Busca una asignaci�n de rol por su ID, incluyendo su persona relacionada.
        /// </summary>
        /// <param name="id">El ID de la asignaci�n a buscar.</param>
        /// <returns>
        /// El objeto 'PersonasRoles' encontrado, o un nuevo objeto vac�o si no se encuentra.
        /// </returns>
        public async Task<PersonasRoles> SearchByIdAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.PersonasRoles
                .Include(p => p.Persona)
                .FirstOrDefaultAsync(p => p.PersonasRolesID == id) ?? new PersonasRoles();
        }

        /// <summary>
        /// Comprueba si existe una asignaci�n de rol con el ID especificado.
        /// </summary>
        /// <param name="id">El ID de la asignaci�n a verificar.</param>
        /// <returns>True si la asignaci�n existe, de lo contrario False.</returns>
        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.PersonasRoles.AnyAsync(p => p.PersonasRolesID == id);
        }

        #endregion

        #region Delete Operations

        /// <summary>
        /// Realiza un borrado l�gico (soft delete) de una asignaci�n de rol.
        /// </summary>
        /// <param name="id">El ID de la asignaci�n a marcar como borrada.</param>
        /// <returns>True si la operaci�n fue exitosa.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            var personaRol = await ctx.PersonasRoles.FindAsync(id);
            if (personaRol is null)
            {
                return false;
            }

            personaRol.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        #endregion
    }
}