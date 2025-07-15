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
        /// Inserta una nueva asignación de rol en la base de datos.
        /// </summary>
        /// <param name="elem">El objeto 'PersonasRoles' a ser insertado.</param>
        /// <returns>True si la inserción fue exitosa.</returns>
        public async Task<bool> InsertAsync(PersonasRoles elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            ctx.PersonasRoles.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Actualiza una asignación de rol existente en la base de datos.
        /// </summary>
        /// <param name="elem">El objeto 'PersonasRoles' con los datos actualizados.</param>
        /// <returns>True si la actualización fue exitosa.</returns>
        public async Task<bool> UpdateAsync(PersonasRoles elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            var existingPersonaRol = await ctx.PersonasRoles.FindAsync(elem.PersonasRolesID);

            if (existingPersonaRol is null)
            {
                return false; // No se puede actualizar una asignación que no existe.
            }

            // Uso de SetValues para una actualización robusta y mantenible.
            ctx.Entry(existingPersonaRol).CurrentValues.SetValues(elem);

            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Guarda una asignación de rol (Upsert): La inserta si es nueva o la actualiza si ya existe.
        /// </summary>
        /// <param name="elem">La asignación de rol a guardar.</param>
        /// <returns>True si la operación de guardado fue exitosa.</returns>
        public async Task<bool> SaveAsync(PersonasRoles elem)
        {
            // Determina la operación en memoria (Insert vs Update) basándose en el ID,
            // lo que evita una consulta extra a la base de datos.
            return elem.PersonasRolesID == 0
                ? await InsertAsync(elem)
                : await UpdateAsync(elem);
        }

        #endregion

        #region Read Operations

        /// <summary>
        /// Obtiene una lista de asignaciones de roles que cumplen con un criterio de búsqueda.
        /// Carga de forma anticipada la entidad relacionada 'Persona'.
        /// </summary>
        /// <param name="criteria">Una expresión lambda para filtrar las asignaciones.</param>
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
        /// Busca una asignación de rol por su ID, incluyendo su persona relacionada.
        /// </summary>
        /// <param name="id">El ID de la asignación a buscar.</param>
        /// <returns>
        /// El objeto 'PersonasRoles' encontrado, o un nuevo objeto vacío si no se encuentra.
        /// </returns>
        public async Task<PersonasRoles> SearchByIdAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.PersonasRoles
                .Include(p => p.Persona)
                .FirstOrDefaultAsync(p => p.PersonasRolesID == id) ?? new PersonasRoles();
        }

        /// <summary>
        /// Comprueba si existe una asignación de rol con el ID especificado.
        /// </summary>
        /// <param name="id">El ID de la asignación a verificar.</param>
        /// <returns>True si la asignación existe, de lo contrario False.</returns>
        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.PersonasRoles.AnyAsync(p => p.PersonasRolesID == id);
        }

        #endregion

        #region Delete Operations

        /// <summary>
        /// Realiza un borrado lógico (soft delete) de una asignación de rol.
        /// </summary>
        /// <param name="id">El ID de la asignación a marcar como borrada.</param>
        /// <returns>True si la operación fue exitosa.</returns>
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