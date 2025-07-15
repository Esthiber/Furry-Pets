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
    /// Gestiona las operaciones CRUD para la entidad 'TipoViviendas'.
    /// Este servicio administra los diferentes tipos de viviendas (ej. Casa, Apartamento)
    /// que pueden ser registrados en los perfiles de los adoptantes.
    /// </summary>
    public class TipoViviendasService(IDbContextFactory<ApplicationDbContext> dbFactory) : ICRUD<TipoViviendas>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory = dbFactory;

        #region Create / Update Operations

        /// <summary>
        /// Inserta un nuevo tipo de vivienda en la base de datos.
        /// </summary>
        /// <param name="elem">El objeto 'TipoViviendas' a ser insertado.</param>
        /// <returns>True si la inserción fue exitosa.</returns>
        public async Task<bool> InsertAsync(TipoViviendas elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            ctx.TipoViviendas.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Actualiza un tipo de vivienda existente de forma eficiente y segura.
        /// </summary>
        /// <param name="elem">El objeto 'TipoViviendas' con los datos actualizados.</param>
        /// <returns>True si la actualización fue exitosa.</returns>
        public async Task<bool> UpdateAsync(TipoViviendas elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            // FindAsync es la forma más eficiente de obtener una entidad por su clave primaria.
            var tipoVivienda = await ctx.TipoViviendas.FindAsync(elem.TipoViviendasID);

            if (tipoVivienda is null)
            {
                return false; // No se puede actualizar una entidad que no existe.
            }

            // SetValues copia todas las propiedades escalares, haciendo el código robusto y mantenible.
            ctx.Entry(tipoVivienda).CurrentValues.SetValues(elem);

            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Guarda un tipo de vivienda (Upsert): Lo inserta si es nuevo o lo actualiza si ya existe.
        /// </summary>
        /// <param name="elem">El tipo de vivienda a guardar.</param>
        /// <returns>True si la operación de guardado fue exitosa.</returns>
        public async Task<bool> SaveAsync(TipoViviendas elem)
        {
            // Determina la operación en memoria para mayor eficiencia.
            return elem.TipoViviendasID == 0
                ? await InsertAsync(elem)
                : await UpdateAsync(elem);
        }

        #endregion

        #region Read Operations

        /// <summary>
        /// Obtiene una lista de tipos de vivienda que cumplen con un criterio de búsqueda.
        /// </summary>
        /// <param name="criteria">Una expresión lambda para filtrar los tipos de vivienda.</param>
        /// <returns>Una lista de objetos 'TipoViviendas'.</returns>
        public async Task<List<TipoViviendas>> ListAsync(Expression<Func<TipoViviendas, bool>> criteria)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.TipoViviendas
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        /// <summary>
        /// Busca un tipo de vivienda por su ID.
        /// </summary>
        /// <param name="id">El ID del tipo de vivienda a buscar.</param>
        /// <returns>El objeto 'TipoViviendas' encontrado, o uno nuevo si no se encuentra.</returns>
        public async Task<TipoViviendas> SearchByIdAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            // FindAsync es ligeramente más eficiente que FirstOrDefaultAsync para búsquedas por PK.
            return await ctx.TipoViviendas.FindAsync(id) ?? new TipoViviendas();
        }

        /// <summary>
        /// Comprueba si existe un tipo de vivienda con el ID especificado.
        /// </summary>
        /// <param name="id">El ID del tipo de vivienda a verificar.</param>
        /// <returns>True si el tipo de vivienda existe, de lo contrario False.</returns>
        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.TipoViviendas.AnyAsync(t => t.TipoViviendasID == id);
        }

        #endregion

        #region Delete Operations

        /// <summary>
        /// Realiza un borrado lógico (soft delete) de un tipo de vivienda.
        /// </summary>
        /// <param name="id">El ID del tipo de vivienda a marcar como borrado.</param>
        /// <returns>True si la operación fue exitosa.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            var tipoVivienda = await ctx.TipoViviendas.FindAsync(id);
            if (tipoVivienda is null)
            {
                return false;
            }

            tipoVivienda.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        #endregion
    }
}