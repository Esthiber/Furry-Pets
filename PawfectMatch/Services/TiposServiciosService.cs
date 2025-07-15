using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PawfectMatch.Services
{
    /// <summary>
    /// Gestiona las operaciones CRUD para la entidad 'TiposServicios'.
    /// Este servicio administra las categorías o tipos generales de servicios que se ofrecen,
    /// como por ejemplo 'Peluquería', 'Entrenamiento', 'Salud', etc.
    /// </summary>
    public class TiposServiciosService(IDbContextFactory<ApplicationDbContext> dbFactory) : ICRUD<TiposServicios>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory = dbFactory;

        #region Create / Update Operations

        /// <summary>
        /// Inserta un nuevo tipo de servicio en la base de datos.
        /// </summary>
        /// <param name="elem">El objeto 'TiposServicios' a ser insertado.</param>
        /// <returns>True si la inserción fue exitosa.</returns>
        public async Task<bool> InsertAsync(TiposServicios elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            ctx.TiposServicios.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Actualiza un tipo de servicio existente de forma eficiente y segura.
        /// </summary>
        /// <param name="elem">El objeto 'TiposServicios' con los datos actualizados.</param>
        /// <returns>True si la actualización fue exitosa.</returns>
        public async Task<bool> UpdateAsync(TiposServicios elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            // FindAsync es la forma más eficiente de obtener una entidad por su clave primaria.
            var tipoServicio = await ctx.TiposServicios.FindAsync(elem.TiposServiciosID);

            if (tipoServicio is null)
            {
                return false; // No se puede actualizar una entidad que no existe.
            }

            // SetValues copia todas las propiedades escalares, haciendo el código robusto y mantenible.
            ctx.Entry(tipoServicio).CurrentValues.SetValues(elem);

            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Guarda un tipo de servicio (Upsert): Lo inserta si es nuevo o lo actualiza si ya existe.
        /// </summary>
        /// <param name="elem">El tipo de servicio a guardar.</param>
        /// <returns>True si la operación de guardado fue exitosa.</returns>
        public async Task<bool> SaveAsync(TiposServicios elem)
        {
            // Determina la operación en memoria para mayor eficiencia.
            return elem.TiposServiciosID == 0
                ? await InsertAsync(elem)
                : await UpdateAsync(elem);
        }

        #endregion

        #region Read Operations

        /// <summary>
        /// Obtiene una lista de tipos de servicios que cumplen con un criterio de búsqueda.
        /// </summary>
        /// <param name="criteria">Una expresión lambda para filtrar los tipos de servicios.</param>
        /// <returns>Una lista de objetos 'TiposServicios'.</returns>
        public async Task<List<TiposServicios>> ListAsync(Expression<Func<TiposServicios, bool>> criteria)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.TiposServicios
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        /// <summary>
        /// Busca un tipo de servicio por su ID.
        /// </summary>
        /// <param name="id">El ID del tipo de servicio a buscar.</param>
        /// <returns>El objeto 'TiposServicios' encontrado, o uno nuevo si no se encuentra.</returns>
        public async Task<TiposServicios> SearchByIdAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            // FindAsync es ligeramente más eficiente que FirstOrDefaultAsync para búsquedas por PK.
            return await ctx.TiposServicios.FindAsync(id) ?? new TiposServicios();
        }

        /// <summary>
        /// Comprueba si existe un tipo de servicio con el ID especificado.
        /// </summary>
        /// <param name="id">El ID del tipo de servicio a verificar.</param>
        /// <returns>True si el tipo de servicio existe, de lo contrario False.</returns>
        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.TiposServicios.AnyAsync(t => t.TiposServiciosID == id);
        }

        #endregion

        #region Delete Operations

        /// <summary>
        /// Realiza un borrado lógico (soft delete) de un tipo de servicio.
        /// </summary>
        /// <param name="id">El ID del tipo de servicio a marcar como borrado.</param>
        /// <returns>True si la operación fue exitosa.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            var tipoServicio = await ctx.TiposServicios.FindAsync(id);
            if (tipoServicio is null)
            {
                return false;
            }

            tipoServicio.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        #endregion
    }
}