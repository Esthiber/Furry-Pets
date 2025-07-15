using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models.POS;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    /// <summary>
    /// Proporciona operaciones CRUD (Crear, Leer, Actualizar, Borrar) para la entidad 'Productos'.
    /// Esta clase utiliza IDbContextFactory para garantizar que cada operación utilice su propia instancia
    /// de DbContext, lo cual es una práctica segura y recomendada en entornos como Blazor Server.
    /// </summary>
    public class ProductosService(IDbContextFactory<ApplicationDbContext> dbFactory) : ICRUD<Productos>
    {
        #region Dependencies & Constructor
        /// <summary>
        /// Fábrica para crear instancias de ApplicationDbContext. Inyectada por el contenedor de dependencias.
        /// </summary>
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory = dbFactory;
        #endregion

        #region ICRUD Implementation

        #region Create / Update Operations

        /// <summary>
        /// Inserta un nuevo producto en la base de datos.
        /// </summary>
        /// <param name="elem">El objeto 'Productos' a insertar.</param>
        /// <returns>True si la operación fue exitosa, de lo contrario False.</returns>
        public async Task<bool> InsertAsync(Productos elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            ctx.Productos.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Actualiza un producto existente en la base de datos.
        /// Este método primero recupera la entidad existente para asegurar que está siendo rastreada por EF Core,
        /// y luego aplica los nuevos valores para evitar problemas de rastreo con entidades desconectadas.
        /// </summary>
        /// <param name="elem">El objeto 'Productos' con los datos actualizados.</param>
        /// <returns>True si la actualización fue exitosa, de lo contrario False.</returns>
        public async Task<bool> UpdateAsync(Productos elem)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            var existingProduct = await ctx.Productos.FindAsync(elem.ProductosID);
            if (existingProduct is null)
            {
                return false; // El producto a actualizar no existe.
            }

            // Usamos SetValues para copiar eficientemente todas las propiedades escalares
            // del objeto 'elem' al objeto 'existingProduct' que está siendo rastreado por el DbContext.
            // Esto es más mantenible que copiar cada propiedad manualmente.
            ctx.Entry(existingProduct).CurrentValues.SetValues(elem);

            return await ctx.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Guarda un producto. Se comporta como un "Upsert": si el producto no existe, lo inserta;
        /// si ya existe, lo actualiza.
        /// </summary>
        /// <param name="elem">El producto a guardar.</param>
        /// <returns>True si la operación de guardado (inserción o actualización) fue exitosa.</returns>
        public async Task<bool> SaveAsync(Productos elem)
        {
            // La lógica existente es clara. Si el rendimiento fuera crítico,
            // se podría optimizar para evitar el doble viaje a la BD, pero para la mayoría
            // de los casos de UI, esta claridad es preferible.
            if (elem.ProductosID == 0) // Es más fiable verificar si el ID es 0 para un nuevo elemento
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
        /// Recupera una lista de productos que cumplen con un criterio específico.
        /// Utiliza 'Include' para cargar datos relacionados de Categoría y Proveedor (Eager Loading).
        /// Utiliza 'AsNoTracking' para mejorar el rendimiento en operaciones de solo lectura.
        /// </summary>
        /// <param name="criteria">Una expresión lambda para filtrar los productos (ej. p => p.IsDeleted == false).</param>
        /// <returns>Una lista de productos que coinciden con el criterio.</returns>
        public async Task<List<Productos>> ListAsync(Expression<Func<Productos, bool>> criteria)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.Productos
                .Include(p => p.CategoriaProducto)
                .Include(p => p.Proveedor)
                .AsNoTracking() // Mejora el rendimiento para listas de solo lectura
                .Where(criteria)
                .ToListAsync();
        }

        /// <summary>
        /// Busca un producto por su ID, incluyendo sus datos de Categoría y Proveedor.
        /// </summary>
        /// <param name="id">El ID del producto a buscar.</param>
        /// <returns>
        /// El objeto 'Productos' si se encuentra. Si no se encuentra, devuelve un nuevo objeto 'Productos' vacío.
        /// </returns>
        /// <dev.note>
        /// Devolver un 'new()' (Null Object Pattern) puede simplificar el código del llamador al evitar
        /// comprobaciones de nulos, pero también puede ocultar un error de "no encontrado".
        /// Una alternativa sería devolver 'null' y manejarlo explícitamente en el código que llama al servicio.
        /// </dev.note>
        public async Task<Productos> SearchByIdAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.Productos
                .Include(p => p.CategoriaProducto)
                .Include(p => p.Proveedor)
                .FirstOrDefaultAsync(p => p.ProductosID == id) ?? new();
        }

        /// <summary>
        /// Comprueba si un producto con el ID especificado existe en la base de datos.
        /// </summary>
        /// <param name="id">El ID del producto a verificar.</param>
        /// <returns>True si el producto existe, de lo contrario False.</returns>
        public async Task<bool> ExistAsync(int id)
        {
            // Este método puede ser redundante si SaveAsync se ajusta para no llamarlo.
            // Se mantiene por si es utilizado en otras partes de la aplicación.
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.Productos.AnyAsync(p => p.ProductosID == id);
        }

        #endregion

        #region Delete Operations

        /// <summary>
        /// Realiza un "soft delete" (borrado lógico) de un producto.
        /// El registro no se elimina físicamente, solo se marca como borrado (IsDeleted = true).
        /// </summary>
        /// <param name="id">El ID del producto a marcar como borrado.</param>
        /// <returns>True si la operación fue exitosa.</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            var producto = await ctx.Productos.FindAsync(id);
            if (producto == null)
            {
                return false;
            }

            producto.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        #endregion

        #endregion
    }
}