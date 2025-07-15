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
    /// Gestiona las operaciones CRUD para la entidad 'Personas'.
    /// Este servicio centraliza la lógica de negocio para los perfiles de personas,
    /// que pueden ser clientes, adoptantes o personal interno.
    /// </summary>
    public class PersonasService(IDbContextFactory<ApplicationDbContext> dbFactory) : ICRUD<Personas>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory = dbFactory;

        #region Create / Update Operations

        /// <summary>
        /// Inserta una nueva persona en la base de datos.
        /// </summary>
        /// <param name="elem">El objeto 'Personas' a ser insertado.</param>
        /// <returns>True si la inserción fue exitosa.</returns>
        public async Task<bool> InsertAsync(Personas elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            ctx.Personas.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Actualiza los datos de una persona existente de forma eficiente.
        /// Este método está optimizado para no cargar datos relacionados innecesarios.
        /// </summary>
        /// <param name="elem">El objeto 'Personas' con los datos actualizados.</param>
        /// <returns>True si la actualización fue exitosa.</returns>
        public async Task<bool> UpdateAsync(Personas elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            // Usamos FindAsync para obtener la entidad por su PK. Es la forma más eficiente
            // y no carga relaciones innecesarias para una actualización de propiedades escalares.
            var persona = await ctx.Personas.FindAsync(elem.PersonasID);

            if (persona is null)
            {
                return false;
            }

            // SetValues copia todas las propiedades escalares, haciendo el código robusto y mantenible.
            ctx.Entry(persona).CurrentValues.SetValues(elem);

            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Guarda una persona (Upsert): La inserta si es nueva o la actualiza si ya existe.
        /// </summary>
        /// <param name="elem">La persona a guardar.</param>
        /// <returns>True si la operación de guardado fue exitosa.</returns>
        public async Task<bool> SaveAsync(Personas elem)
        {
            return elem.PersonasID == 0
                ? await InsertAsync(elem)
                : await UpdateAsync(elem);
        }

        #endregion

        #region Read Operations

        /// <summary>
        /// Obtiene una lista de personas que cumplen un criterio, incluyendo todas sus relaciones.
        /// ADVERTENCIA: Esta consulta puede ser muy pesada. Considerar crear métodos más
        /// específicos si no se necesitan todos los datos relacionados.
        /// </summary>
        /// <param name="criteria">La expresión para filtrar las personas.</param>
        /// <returns>Una lista de objetos 'Personas' con datos anidados.</returns>
        public async Task<List<Personas>> ListAsync(Expression<Func<Personas, bool>> criteria)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.Personas
                .Include(p => p.Roles)
                .Include(p => p.AdoptantesDetalles)
                .Include(p => p.Mascotas)
                .Include(p => p.Facturas)
                .Include(p => p.SolicitudesAdopciones)
                .Include(p => p.Seguimientos)
                .Include(p => p.Citas)
                .Include(p => p.HistoriasClinicas)
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        /// <summary>
        /// Busca una persona por su ID, cargando todas sus entidades relacionadas.
        /// </summary>
        /// <param name="id">El ID de la persona a buscar.</param>
        /// <returns>El objeto 'Personas' con todos sus datos, o uno nuevo si no se encuentra.</returns>
        public async Task<Personas> SearchByIdAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.Personas
                .Include(p => p.Roles)
                .Include(p => p.AdoptantesDetalles)
                .Include(p => p.Mascotas)
                .Include(p => p.Facturas)
                .Include(p => p.SolicitudesAdopciones)
                .Include(p => p.Seguimientos)
                .Include(p => p.Citas)
                .Include(p => p.HistoriasClinicas)
                .FirstOrDefaultAsync(p => p.PersonasID == id) ?? new Personas();
        }

        /// <summary>
        /// Comprueba si existe una persona con el ID especificado.
        /// </summary>
        /// <param name="id">El ID de la persona a verificar.</param>
        /// <returns>True si la persona existe, de lo contrario False.</returns>
        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.Personas.AnyAsync(p => p.PersonasID == id);
        }

        #endregion

        #region Delete Operations

        /// <summary>
        /// Realiza un borrado lógico (soft delete) de una persona.
        /// </summary>
        /// <param name="id">El ID de la persona a marcar como borrada.</param>
        /// <returns>True si la operación fue exitosa.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            var persona = await ctx.Personas.FindAsync(id);
            if (persona is null)
            {
                return false;
            }

            persona.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        #endregion
    }
}