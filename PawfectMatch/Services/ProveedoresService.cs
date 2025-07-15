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
    /// Gestiona las operaciones CRUD para la entidad 'Proveedores'.
    /// Este servicio se encarga de la l�gica de negocio para administrar la informaci�n
    /// de los proveedores de productos o servicios de la empresa.
    /// </summary>
    public class ProveedoresService(IDbContextFactory<ApplicationDbContext> dbFactory) : ICRUD<Proveedores>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory = dbFactory;

        #region Create / Update Operations

        /// <summary>
        /// Inserta un nuevo proveedor en la base de datos.
        /// </summary>
        /// <param name="elem">El objeto 'Proveedores' a ser insertado.</param>
        /// <returns>True si la inserci�n fue exitosa.</returns>
        public async Task<bool> InsertAsync(Proveedores elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            ctx.Proveedores.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Actualiza un proveedor existente de forma eficiente y segura.
        /// </summary>
        /// <param name="elem">El objeto 'Proveedores' con los datos actualizados.</param>
        /// <returns>True si la actualizaci�n fue exitosa.</returns>
        public async Task<bool> UpdateAsync(Proveedores elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            // FindAsync es la forma m�s eficiente de obtener una entidad por su clave primaria.
            var proveedor = await ctx.Proveedores.FindAsync(elem.ProveedoresID);

            if (proveedor is null)
            {
                return false; // No se puede actualizar un proveedor que no existe.
            }

            // SetValues copia todas las propiedades escalares, haciendo el c�digo robusto y mantenible.
            ctx.Entry(proveedor).CurrentValues.SetValues(elem);

            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Guarda un proveedor (Upsert): Lo inserta si es nuevo o lo actualiza si ya existe.
        /// Este m�todo es m�s eficiente al evitar una consulta extra a la base de datos.
        /// </summary>
        /// <param name="elem">El proveedor a guardar.</param>
        /// <returns>True si la operaci�n de guardado fue exitosa.</returns>
        public async Task<bool> SaveAsync(Proveedores elem)
        {
            // Determina la operaci�n en memoria (Insert vs Update) bas�ndose en el ID.
            return elem.ProveedoresID == 0
                ? await InsertAsync(elem)
                : await UpdateAsync(elem);
        }

        #endregion

        #region Read Operations

        /// <summary>
        /// Obtiene una lista de proveedores que cumplen con un criterio de b�squeda.
        /// </summary>
        /// <param name="criteria">Una expresi�n lambda para filtrar los proveedores.</param>
        /// <returns>Una lista de objetos 'Proveedores'.</returns>
        public async Task<List<Proveedores>> ListAsync(Expression<Func<Proveedores, bool>> criteria)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.Proveedores
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        /// <summary>
        /// Busca un proveedor por su ID.
        /// </summary>
        /// <param name="id">El ID del proveedor a buscar.</param>
        /// <returns>
        /// El objeto 'Proveedores' encontrado, o un nuevo objeto vac�o si no se encuentra.
        /// </returns>
        public async Task<Proveedores> SearchByIdAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            // FindAsync se podr�a usar aqu� tambi�n, es ligeramente m�s eficiente que FirstOrDefaultAsync para PK.
            return await ctx.Proveedores.FindAsync(id) ?? new Proveedores();
        }

        /// <summary>
        /// Comprueba si existe un proveedor con el ID especificado.
        /// </summary>
        /// <param name="id">El ID del proveedor a verificar.</param>
        /// <returns>True si el proveedor existe, de lo contrario False.</returns>
        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.Proveedores.AnyAsync(p => p.ProveedoresID == id);
        }

        #endregion

        #region Delete Operations

        /// <summary>
        /// Realiza un borrado l�gico (soft delete) de un proveedor.
        /// </summary>
        /// <param name="id">El ID del proveedor a marcar como borrado.</param>
        /// <returns>True si la operaci�n fue exitosa.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            var proveedor = await ctx.Proveedores.FindAsync(id);
            if (proveedor is null)
            {
                return false;
            }

            proveedor.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        #endregion
    }
}