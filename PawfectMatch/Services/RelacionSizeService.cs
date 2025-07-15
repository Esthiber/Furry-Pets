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
    /// Gestiona las operaciones CRUD para la entidad 'RelacionSize'.
    /// Este servicio administra los diferentes tamaños (ej. Pequeño, Mediano, Grande)
    /// que se pueden asociar a las mascotas.
    /// </summary>
    public class RelacionSizeService(IDbContextFactory<ApplicationDbContext> dbFactory) : ICRUD<RelacionSize>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory = dbFactory;

        #region Create / Update Operations

        /// <summary>
        /// Inserta un nuevo tamaño en la base de datos.
        /// </summary>
        /// <param name="elem">El objeto 'RelacionSize' a ser insertado.</param>
        /// <returns>True si la inserción fue exitosa.</returns>
        public async Task<bool> InsertAsync(RelacionSize elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            ctx.RelacionSize.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Actualiza un tamaño existente de forma eficiente y segura.
        /// </summary>
        /// <param name="elem">El objeto 'RelacionSize' con los datos actualizados.</param>
        /// <returns>True si la actualización fue exitosa.</returns>
        public async Task<bool> UpdateAsync(RelacionSize elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            // FindAsync es la forma más eficiente de obtener una entidad por su clave primaria.
            var relacionSize = await ctx.RelacionSize.FindAsync(elem.RelacionSizeID);

            if (relacionSize is null)
            {
                return false; // No se puede actualizar una entidad que no existe.
            }

            // SetValues copia todas las propiedades escalares, haciendo el código robusto y mantenible.
            ctx.Entry(relacionSize).CurrentValues.SetValues(elem);

            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Guarda un tamaño (Upsert): Lo inserta si es nuevo o lo actualiza si ya existe.
        /// </summary>
        /// <param name="elem">El tamaño a guardar.</param>
        /// <returns>True si la operación de guardado fue exitosa.</returns>
        public async Task<bool> SaveAsync(RelacionSize elem)
        {
            // Determina la operación en memoria para mayor eficiencia, evitando una consulta extra.
            return elem.RelacionSizeID == 0
                ? await InsertAsync(elem)
                : await UpdateAsync(elem);
        }

        #endregion

        #region Read Operations

        /// <summary>
        /// Obtiene una lista de tamaños que cumplen con un criterio de búsqueda.
        /// </summary>
        /// <param name="criteria">Una expresión lambda para filtrar los tamaños.</param>
        /// <returns>Una lista de objetos 'RelacionSize'.</returns>
        public async Task<List<RelacionSize>> ListAsync(Expression<Func<RelacionSize, bool>> criteria)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.RelacionSize
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        /// <summary>
        /// Busca un tamaño por su ID.
        /// </summary>
        /// <param name="id">El ID del tamaño a buscar.</param>
        /// <returns>El objeto 'RelacionSize' encontrado, o uno nuevo si no se encuentra.</returns>
        public async Task<RelacionSize> SearchByIdAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            // FindAsync es ligeramente más eficiente que FirstOrDefaultAsync para búsquedas por PK.
            return await ctx.RelacionSize.FindAsync(id) ?? new RelacionSize();
        }

        /// <summary>
        /// Comprueba si existe un tamaño con el ID especificado.
        /// </summary>
        /// <param name="id">El ID del tamaño a verificar.</param>
        /// <returns>True si el tamaño existe, de lo contrario False.</returns>
        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.RelacionSize.AnyAsync(r => r.RelacionSizeID == id);
        }

        #endregion

        #region Delete Operations

        /// <summary>
        /// Realiza un borrado lógico (soft delete) de un tamaño.
        /// </summary>
        /// <param name="id">El ID del tamaño a marcar como borrado.</param>
        /// <returns>True si la operación fue exitosa.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            var relacionSize = await ctx.RelacionSize.FindAsync(id);
            if (relacionSize is null)
            {
                return false;
            }

            relacionSize.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        #endregion
    }
}