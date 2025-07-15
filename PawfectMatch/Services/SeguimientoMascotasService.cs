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
    /// Gestiona las operaciones CRUD para la entidad 'SeguimientoMascotas'.
    /// Este servicio se encarga de la lógica de negocio para registrar y administrar
    /// el seguimiento post-adopción de las mascotas.
    /// </summary>
    public class SeguimientoMascotasService(IDbContextFactory<ApplicationDbContext> dbFactory) : ICRUD<SeguimientoMascotas>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory = dbFactory;

        #region Create / Update Operations

        /// <summary>
        /// Inserta un nuevo registro de seguimiento en la base de datos.
        /// </summary>
        /// <param name="elem">El objeto 'SeguimientoMascotas' a ser insertado.</param>
        /// <returns>True si la inserción fue exitosa.</returns>
        public async Task<bool> InsertAsync(SeguimientoMascotas elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            ctx.SeguimientoMascotas.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Actualiza un registro de seguimiento existente de forma altamente eficiente.
        /// Este método está optimizado para no cargar datos relacionados innecesarios.
        /// </summary>
        /// <param name="elem">El objeto 'SeguimientoMascotas' con los datos actualizados.</param>
        /// <returns>True si la actualización fue exitosa.</returns>
        public async Task<bool> UpdateAsync(SeguimientoMascotas elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            // Usamos FindAsync para obtener la entidad por su PK. Es la forma más eficiente
            // y evita cargar las 3 tablas relacionadas innecesariamente para esta operación.
            var seguimiento = await ctx.SeguimientoMascotas.FindAsync(elem.SeguimientoID);

            if (seguimiento is null)
            {
                return false; // No se puede actualizar un seguimiento que no existe.
            }

            // SetValues copia todas las propiedades escalares, haciendo el código robusto y mantenible.
            ctx.Entry(seguimiento).CurrentValues.SetValues(elem);

            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Guarda un registro de seguimiento (Upsert): Lo inserta si es nuevo o lo actualiza si ya existe.
        /// </summary>
        /// <param name="elem">El registro de seguimiento a guardar.</param>
        /// <returns>True si la operación de guardado fue exitosa.</returns>
        public async Task<bool> SaveAsync(SeguimientoMascotas elem)
        {
            // Determina la operación en memoria para mayor eficiencia.
            return elem.SeguimientoID == 0
                ? await InsertAsync(elem)
                : await UpdateAsync(elem);
        }

        #endregion

        #region Read Operations

        /// <summary>
        /// Obtiene una lista de seguimientos que cumplen un criterio, incluyendo la persona responsable.
        /// </summary>
        /// <param name="criteria">La expresión para filtrar los seguimientos.</param>
        /// <returns>Una lista de objetos 'SeguimientoMascotas'.</returns>
        public async Task<List<SeguimientoMascotas>> ListAsync(Expression<Func<SeguimientoMascotas, bool>> criteria)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.SeguimientoMascotas
                .Include(s => s.Persona)
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        /// <summary>
        /// Busca un registro de seguimiento por su ID, cargando todos sus datos relacionados.
        /// </summary>
        /// <param name="id">El ID del seguimiento a buscar.</param>
        /// <returns>El objeto 'SeguimientoMascotas' encontrado, o uno nuevo si no se encuentra.</returns>
        public async Task<SeguimientoMascotas> SearchByIdAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.SeguimientoMascotas
                .Include(s => s.MascotaAdopcion)
                .Include(s => s.Persona)
                .Include(s => s.EstadoMascota)
                .FirstOrDefaultAsync(s => s.SeguimientoID == id) ?? new SeguimientoMascotas();
        }

        /// <summary>
        /// Comprueba si existe un seguimiento con el ID especificado.
        /// </summary>
        /// <param name="id">El ID del seguimiento a verificar.</param>
        /// <returns>True si el seguimiento existe, de lo contrario False.</returns>
        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.SeguimientoMascotas.AnyAsync(s => s.SeguimientoID == id);
        }

        #endregion

        #region Delete Operations

        /// <summary>
        /// Realiza un borrado lógico (soft delete) de un registro de seguimiento.
        /// </summary>
        /// <param name="id">El ID del seguimiento a marcar como borrado.</param>
        /// <returns>True si la operación fue exitosa.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            var seguimiento = await ctx.SeguimientoMascotas.FindAsync(id);
            if (seguimiento is null)
            {
                return false;
            }

            seguimiento.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        #endregion
    }
}