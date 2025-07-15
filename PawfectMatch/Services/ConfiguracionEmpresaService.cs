using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    /// <summary>
    /// Gestiona las operaciones CRUD para la entidad 'ConfiguracionEmpresa'.
    /// Este servicio se utiliza para manejar los datos de configuraci�n principales de la empresa,
    /// como el nombre, la direcci�n y el RNC. Generalmente, se espera que haya un solo registro.
    /// </summary>
    public class ConfiguracionEmpresaService(IDbContextFactory<ApplicationDbContext> dbFactory) : ICRUD<ConfiguracionEmpresa>
    {
        #region Dependencies & Constructor
        /// <summary>
        /// F�brica para crear instancias de ApplicationDbContext.
        /// </summary>
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory = dbFactory;
        #endregion

        #region ICRUD Implementation

        #region Create / Update Operations

        /// <summary>
        /// Inserta un nuevo registro de configuraci�n de la empresa.
        /// </summary>
        /// <param name="elem">El objeto 'ConfiguracionEmpresa' a insertar.</param>
        /// <returns>True si la inserci�n fue exitosa.</returns>
        public async Task<bool> InsertAsync(ConfiguracionEmpresa elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            ctx.ConfiguracionEmpresa.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Actualiza un registro de configuraci�n de empresa existente.
        /// </summary>
        /// <param name="elem">El objeto 'ConfiguracionEmpresa' con los datos actualizados.</param>
        /// <returns>True si la actualizaci�n fue exitosa.</returns>
        public async Task<bool> UpdateAsync(ConfiguracionEmpresa elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            var existingConfig = await ctx.ConfiguracionEmpresa.FindAsync(elem.EmpresaID);
            if (existingConfig is null)
            {
                return false; // No se puede actualizar una configuraci�n que no existe.
            }

            // Utiliza SetValues para una actualizaci�n eficiente y mantenible.
            ctx.Entry(existingConfig).CurrentValues.SetValues(elem);

            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Guarda la configuraci�n de la empresa (Upsert): la inserta si es nueva o la actualiza si existe.
        /// </summary>
        /// <param name="elem">La configuraci�n a guardar.</param>
        /// <returns>True si la operaci�n de guardado fue exitosa.</returns>
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
        /// Aunque normalmente habr� una sola, este m�todo cumple con la interfaz ICRUD.
        /// </summary>
        /// <param name="criteria">La expresi�n lambda para filtrar los resultados.</param>
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
        /// Busca la configuraci�n de la empresa por su ID.
        /// </summary>
        /// <param name="id">El ID de la configuraci�n a buscar.</param>
        /// <returns>El objeto 'ConfiguracionEmpresa' encontrado, o un nuevo objeto vac�o si no se encuentra.</returns>
        public async Task<ConfiguracionEmpresa> SearchByIdAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.ConfiguracionEmpresa.FindAsync(id) ?? new();
        }

        /// <summary>
        /// Verifica si existe una configuraci�n de empresa con el ID especificado.
        /// </summary>
        /// <param name="id">El ID a verificar.</param>
        /// <returns>True si la configuraci�n existe, de lo contrario False.</returns>
        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.ConfiguracionEmpresa.AnyAsync(c => c.EmpresaID == id);
        }

        #endregion

        #region Delete Operations

        /// <summary>
        /// Realiza un borrado f�sico y permanente (hard delete) de la configuraci�n de la empresa.
        /// </summary>
        /// <param name="id">El ID de la configuraci�n a eliminar.</param>
        /// <returns>True si la eliminaci�n fue exitosa.</returns>
        /// <dev.note>
        /// Este m�todo utiliza 'ExecuteDeleteAsync', que genera una sentencia SQL DELETE directa
        /// sin cargar la entidad en memoria. Es muy eficiente, pero omite el change tracker de EF Core.
        /// A diferencia de otros servicios que usan borrado l�gico (IsDeleted), esta es una eliminaci�n permanente.
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