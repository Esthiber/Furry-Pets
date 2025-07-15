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
    /// Gestiona las operaciones CRUD para la entidad 'Razas'.
    /// Este servicio se encarga de la lógica de negocio para administrar las diferentes razas de mascotas
    /// y su relación con una especie específica (ej. Perro, Gato).
    /// </summary>
    public class RazasService(IDbContextFactory<ApplicationDbContext> dbFactory) : ICRUD<Razas>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory = dbFactory;

        #region Create / Update Operations

        /// <summary>
        /// Inserta una nueva raza en la base de datos.
        /// </summary>
        /// <param name="elem">El objeto 'Razas' a ser insertado.</param>
        /// <returns>True si la inserción fue exitosa.</returns>
        public async Task<bool> InsertAsync(Razas elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            ctx.Razas.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Actualiza una raza existente de forma eficiente y segura.
        /// Este método está optimizado para no cargar datos relacionados innecesarios.
        /// </summary>
        /// <param name="elem">El objeto 'Razas' con los datos actualizados.</param>
        /// <returns>True si la actualización fue exitosa.</returns>
        public async Task<bool> UpdateAsync(Razas elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            // Usamos FindAsync para obtener la entidad por su PK, es más eficiente
            // y evita cargar la 'Especie' innecesariamente para esta operación.
            var raza = await ctx.Razas.FindAsync(elem.RazasID);

            if (raza is null)
            {
                return false; // No se puede actualizar una raza que no existe.
            }

            // SetValues copia todas las propiedades escalares, haciendo el código robusto y mantenible.
            ctx.Entry(raza).CurrentValues.SetValues(elem);

            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Guarda una raza (Upsert): La inserta si es nueva o la actualiza si ya existe.
        /// </summary>
        /// <param name="elem">La raza a guardar.</param>
        /// <returns>True si la operación de guardado fue exitosa.</returns>
        public async Task<bool> SaveAsync(Razas elem)
        {
            // Determina la operación en memoria (Insert vs Update) para mayor eficiencia.
            return elem.RazasID == 0
                ? await InsertAsync(elem)
                : await UpdateAsync(elem);
        }

        #endregion

        #region Read Operations

        /// <summary>
        /// Obtiene una lista de razas que cumplen un criterio, incluyendo su especie relacionada.
        /// </summary>
        /// <param name="criteria">La expresión para filtrar las razas.</param>
        /// <returns>Una lista de objetos 'Razas'.</returns>
        public async Task<List<Razas>> ListAsync(Expression<Func<Razas, bool>> criteria)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.Razas
                .Include(r => r.Especie)
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        /// <summary>
        /// Busca una raza por su ID, cargando su especie relacionada.
        /// </summary>
        /// <param name="id">El ID de la raza a buscar.</param>
        /// <returns>El objeto 'Razas' encontrado, o uno nuevo si no se encuentra.</returns>
        public async Task<Razas> SearchByIdAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.Razas
                .Include(r => r.Especie)
                .FirstOrDefaultAsync(r => r.RazasID == id) ?? new Razas();
        }

        /// <summary>
        /// Comprueba si existe una raza con el ID especificado.
        /// </summary>
        /// <param name="id">El ID de la raza a verificar.</param>
        /// <returns>True si la raza existe, de lo contrario False.</returns>
        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.Razas.AnyAsync(r => r.RazasID == id);
        }

        #endregion

        #region Delete Operations

        /// <summary>
        /// Realiza un borrado lógico (soft delete) de una raza.
        /// </summary>
        /// <param name="id">El ID de la raza a marcar como borrada.</param>
        /// <returns>True si la operación fue exitosa.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            var raza = await ctx.Razas.FindAsync(id);
            if (raza is null)
            {
                return false;
            }

            raza.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        #endregion
    }
}