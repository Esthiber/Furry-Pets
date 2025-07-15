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
    /// Gestiona las operaciones CRUD para la entidad 'SolicitudesAdopciones'.
    /// Este servicio se encarga de la lógica de negocio para crear y administrar
    /// las solicitudes de adopción de mascotas por parte de los interesados.
    /// </summary>
    public class SolicitudesAdopcionesService(IDbContextFactory<ApplicationDbContext> dbFactory) : ICRUD<SolicitudesAdopciones>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory = dbFactory;

        #region Create / Update Operations

        /// <summary>
        /// Inserta una nueva solicitud de adopción en la base de datos.
        /// </summary>
        /// <param name="elem">El objeto 'SolicitudesAdopciones' a ser insertado.</param>
        /// <returns>True si la inserción fue exitosa.</returns>
        public async Task<bool> InsertAsync(SolicitudesAdopciones elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            ctx.SolicitudesAdopciones.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Actualiza una solicitud de adopción existente de forma altamente eficiente.
        /// Este método está optimizado para no cargar datos relacionados innecesarios.
        /// </summary>
        /// <param name="elem">El objeto 'SolicitudesAdopciones' con los datos actualizados.</param>
        /// <returns>True si la actualización fue exitosa.</returns>
        public async Task<bool> UpdateAsync(SolicitudesAdopciones elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            // Usamos FindAsync para obtener la entidad por su PK. Es la forma más eficiente
            // y evita cargar las 3 tablas relacionadas innecesariamente para esta operación.
            var solicitud = await ctx.SolicitudesAdopciones.FindAsync(elem.SolicitudesAdopcionesID);

            if (solicitud is null)
            {
                return false; // No se puede actualizar una solicitud que no existe.
            }

            // SetValues copia todas las propiedades escalares, haciendo el código robusto y mantenible.
            ctx.Entry(solicitud).CurrentValues.SetValues(elem);

            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Guarda una solicitud de adopción (Upsert): La inserta si es nueva o la actualiza si ya existe.
        /// </summary>
        /// <param name="elem">La solicitud de adopción a guardar.</param>
        /// <returns>True si la operación de guardado fue exitosa.</returns>
        public async Task<bool> SaveAsync(SolicitudesAdopciones elem)
        {
            // Determina la operación en memoria para mayor eficiencia.
            return elem.SolicitudesAdopcionesID == 0
                ? await InsertAsync(elem)
                : await UpdateAsync(elem);
        }

        #endregion

        #region Read Operations

        /// <summary>
        /// Obtiene una lista de solicitudes que cumplen un criterio, incluyendo datos relacionados.
        /// </summary>
        /// <param name="criteria">La expresión para filtrar las solicitudes.</param>
        /// <returns>Una lista de objetos 'SolicitudesAdopciones'.</returns>
        public async Task<List<SolicitudesAdopciones>> ListAsync(Expression<Func<SolicitudesAdopciones, bool>> criteria)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.SolicitudesAdopciones
                .Include(s => s.Persona)
                .Include(s => s.MascotaAdopcion)
                .Include(s => s.EstadoSolicitud)
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        /// <summary>
        /// Busca una solicitud de adopción por su ID, cargando todos sus datos relacionados.
        /// </summary>
        /// <param name="id">El ID de la solicitud a buscar.</param>
        /// <returns>El objeto 'SolicitudesAdopciones' encontrado, o uno nuevo si no se encuentra.</returns>
        public async Task<SolicitudesAdopciones> SearchByIdAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.SolicitudesAdopciones
                .Include(s => s.Persona)
                .Include(s => s.MascotaAdopcion)
                .Include(s => s.EstadoSolicitud)
                .FirstOrDefaultAsync(s => s.SolicitudesAdopcionesID == id) ?? new SolicitudesAdopciones();
        }

        /// <summary>
        /// Comprueba si existe una solicitud con el ID especificado.
        /// </summary>
        /// <param name="id">El ID de la solicitud a verificar.</param>
        /// <returns>True si la solicitud existe, de lo contrario False.</returns>
        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.SolicitudesAdopciones.AnyAsync(s => s.SolicitudesAdopcionesID == id);
        }

        #endregion

        #region Delete Operations

        /// <summary>
        /// Realiza un borrado lógico (soft delete) de una solicitud de adopción.
        /// </summary>
        /// <param name="id">El ID de la solicitud a marcar como borrada.</param>
        /// <returns>True si la operación fue exitosa.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            var solicitud = await ctx.SolicitudesAdopciones.FindAsync(id);
            if (solicitud is null)
            {
                return false;
            }

            solicitud.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        #endregion
    }
}