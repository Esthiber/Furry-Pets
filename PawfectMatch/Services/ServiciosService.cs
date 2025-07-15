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
    /// Gestiona las operaciones CRUD para la entidad 'Servicios'.
    /// Este servicio se encarga de la lógica de negocio para administrar los diferentes servicios
    /// que ofrece la organización (ej. peluquería, entrenamiento, etc.).
    /// </summary>
    public class ServiciosService(IDbContextFactory<ApplicationDbContext> dbFactory) : ICRUD<Servicios>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory = dbFactory;

        #region Create / Update Operations

        /// <summary>
        /// Inserta un nuevo servicio en la base de datos.
        /// </summary>
        /// <param name="elem">El objeto 'Servicios' a ser insertado.</param>
        /// <returns>True si la inserción fue exitosa.</returns>
        public async Task<bool> InsertAsync(Servicios elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            ctx.Servicios.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Actualiza un servicio existente de forma eficiente y segura.
        /// Este método está optimizado para no cargar datos relacionados innecesarios.
        /// </summary>
        /// <param name="elem">El objeto 'Servicios' con los datos actualizados.</param>
        /// <returns>True si la actualización fue exitosa.</returns>
        public async Task<bool> UpdateAsync(Servicios elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            // Usamos FindAsync para obtener la entidad por su PK. Es la forma más eficiente
            // y evita cargar 'TipoServicio' innecesariamente para esta operación.
            var servicio = await ctx.Servicios.FindAsync(elem.ServiciosID);

            if (servicio is null)
            {
                return false; // No se puede actualizar un servicio que no existe.
            }

            // SetValues copia todas las propiedades escalares, haciendo el código robusto y mantenible.
            ctx.Entry(servicio).CurrentValues.SetValues(elem);

            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Guarda un servicio (Upsert): Lo inserta si es nuevo o lo actualiza si ya existe.
        /// </summary>
        /// <param name="elem">El servicio a guardar.</param>
        /// <returns>True si la operación de guardado fue exitosa.</returns>
        public async Task<bool> SaveAsync(Servicios elem)
        {
            // Determina la operación en memoria para mayor eficiencia.
            return elem.ServiciosID == 0
                ? await InsertAsync(elem)
                : await UpdateAsync(elem);
        }

        #endregion

        #region Read Operations

        /// <summary>
        /// Obtiene una lista de servicios que cumplen un criterio, incluyendo su tipo de servicio.
        /// </summary>
        /// <param name="criteria">La expresión para filtrar los servicios.</param>
        /// <returns>Una lista de objetos 'Servicios'.</returns>
        public async Task<List<Servicios>> ListAsync(Expression<Func<Servicios, bool>> criteria)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.Servicios
                .Include(s => s.TipoServicio)
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        /// <summary>
        /// Busca un servicio por su ID, cargando su tipo de servicio relacionado.
        /// </summary>
        /// <param name="id">El ID del servicio a buscar.</param>
        /// <returns>El objeto 'Servicios' encontrado, o uno nuevo si no se encuentra.</returns>
        public async Task<Servicios> SearchByIdAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.Servicios
                .Include(s => s.TipoServicio)
                .FirstOrDefaultAsync(s => s.ServiciosID == id) ?? new Servicios();
        }

        /// <summary>
        /// Comprueba si existe un servicio con el ID especificado.
        /// </summary>
        /// <param name="id">El ID del servicio a verificar.</param>
        /// <returns>True si el servicio existe, de lo contrario False.</returns>
        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.Servicios.AnyAsync(s => s.ServiciosID == id);
        }

        #endregion

        #region Delete Operations

        /// <summary>
        /// Realiza un borrado lógico (soft delete) de un servicio.
        /// </summary>
        /// <param name="id">El ID del servicio a marcar como borrado.</param>
        /// <returns>True si la operación fue exitosa.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            var servicio = await ctx.Servicios.FindAsync(id);
            if (servicio is null)
            {
                return false;
            }

            servicio.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        #endregion
    }
}