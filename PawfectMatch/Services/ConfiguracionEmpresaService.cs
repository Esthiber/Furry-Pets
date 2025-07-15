using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    /// <summary>
    /// Gestiona las operaciones CRUD para la entidad 'ConfiguracionEmpresa'.
    /// Este servicio se utiliza para manejar los datos de configuración principales de la empresa,
    /// como el nombre, la dirección y el RNC. Generalmente, se espera que haya un solo registro.
    /// </summary>
    public class ConfiguracionEmpresaService(IDbContextFactory<ApplicationDbContext> dbFactory) : ICRUD<ConfiguracionEmpresa>
    {
        #region Dependencies & Constructor
        /// <summary>
        /// Fábrica para crear instancias de ApplicationDbContext.
        /// </summary>
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory = dbFactory;
        #endregion

        #region ICRUD Implementation

        #region Create / Update Operations

        /// <summary>
        /// Inserta un nuevo registro de configuración de la empresa.
        /// </summary>
        /// <param name="elem">El objeto 'ConfiguracionEmpresa' a insertar.</param>
        /// <returns>True si la inserción fue exitosa.</returns>
        public async Task<bool> InsertAsync(ConfiguracionEmpresa elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            ctx.ConfiguracionEmpresa.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Actualiza un registro de configuración de empresa existente.
        /// </summary>
        /// <param name="elem">El objeto 'ConfiguracionEmpresa' con los datos actualizados.</param>
        /// <returns>True si la actualización fue exitosa.</returns>
        public async Task<bool> UpdateAsync(ConfiguracionEmpresa elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            var existingConfig = await ctx.ConfiguracionEmpresa.FindAsync(elem.EmpresaID);
            if (existingConfig is null)
            {
                return false; // No se puede actualizar una configuración que no existe.
            }

            // Utiliza SetValues para una actualización eficiente y mantenible.
            ctx.Entry(existingConfig).CurrentValues.SetValues(elem);

            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Guarda la configuración de la empresa (Upsert): la inserta si es nueva o la actualiza si existe.
        /// </summary>
        /// <param name="elem">La configuración a guardar.</param>
        /// <returns>True si la operación de guardado fue exitosa.</returns>
        public async Task<bool> SaveAsync(ConfiguracionEmpresa elem)
        {
            if (elem.EmpresaID == 0)
            {
                return await InsertAsync(elem);
            }
            else
            {
                return await UpdateAsync(elem);
            }
        }

        #endregion

        #region Read Operations

        /// <summary>
        /// Obtiene una lista de configuraciones de empresa que cumplen un criterio.
        /// Aunque normalmente habrá una sola, este método cumple con la interfaz ICRUD.
        /// </summary>
        /// <param name="criteria">La expresión lambda para filtrar los resultados.</param>
        /// <returns>Una lista de objetos 'ConfiguracionEmpresa'.</returns>
        public async Task<List<ConfiguracionEmpresa>> ListAsync(Expression<Func<ConfiguracionEmpresa, bool>> criteria)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.ConfiguracionEmpresa
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        /// <summary>
        /// Busca la configuración de la empresa por su ID.
        /// </summary>
        /// <param name="id">El ID de la configuración a buscar.</param>
        /// <returns>El objeto 'ConfiguracionEmpresa' encontrado, o un nuevo objeto vacío si no se encuentra.</returns>
        public async Task<ConfiguracionEmpresa> SearchByIdAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.ConfiguracionEmpresa.FindAsync(id) ?? new();
        }

        /// <summary>
        /// Verifica si existe una configuración de empresa con el ID especificado.
        /// </summary>
        /// <param name="id">El ID a verificar.</param>
        /// <returns>True si la configuración existe, de lo contrario False.</returns>
        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.ConfiguracionEmpresa.AnyAsync(c => c.EmpresaID == id);
        }

        #endregion

        #region Delete Operations

        /// <summary>
        /// Realiza un borrado físico y permanente (hard delete) de la configuración de la empresa.
        /// </summary>
        /// <param name="id">El ID de la configuración a eliminar.</param>
        /// <returns>True si la eliminación fue exitosa.</returns>
        /// <dev.note>
        /// Este método utiliza 'ExecuteDeleteAsync', que genera una sentencia SQL DELETE directa
        /// sin cargar la entidad en memoria. Es muy eficiente, pero omite el change tracker de EF Core.
        /// A diferencia de otros servicios que usan borrado lógico (IsDeleted), esta es una eliminación permanente.
        /// </dev.note>
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.ConfiguracionEmpresa
                .Where(c => c.EmpresaID == id)
                .ExecuteDeleteAsync() > 0;
        }

        #endregion

        #endregion
    }
}