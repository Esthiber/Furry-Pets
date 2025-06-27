using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    public class ProductoService(IDbContextFactory<ApplicationDbContext> DbFactory) : ICRUD<Productos>
    {
        public async Task<bool> InsertAsync(Productos producto)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.Productos.Add(producto);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Productos producto)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.Productos.Update(producto);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Productos
                .AsNoTracking()
                .Where(p => p.ProductoId == id)
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<Productos> SearchByIdAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Productos
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProductoId == id) ?? new();
        }

        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Productos.AnyAsync(p => p.ProductoId == id);
        }

        public async Task<bool> SaveAsync(Productos producto)
        {
            if (await ExistAsync(producto.ProductoId))
                return await UpdateAsync(producto);
            else
                return await InsertAsync(producto);
        }

        public async Task<List<Productos>> ListAsync(Expression<Func<Productos, bool>> criteria)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Productos
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        // ---------- Métodos adicionales que no están en ICRUD pero son útiles ----------

        public async Task<List<Productos>> ListarTodosAsync()
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Productos.AsNoTracking().ToListAsync();
        }

        public async Task<bool> ActualizarStock(int productoId, int cantidad)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var producto = await ctx.Productos.FindAsync(productoId);

            if (producto == null) return false;

            producto.Stock += cantidad;
            if (producto.Stock < 0) producto.Stock = 0;

            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExisteProducto(int id, string? nombre)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Productos.AnyAsync(p => p.ProductoId == id || p.Nombre == nombre);
        }

        public async Task<List<Productos>> ListarProducto(Expression<Func<Productos, bool>>? filtro = null)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            var query = contexto.Productos.AsNoTracking();
            return filtro == null ? await query.ToListAsync() : await query.Where(filtro).ToListAsync();
        }

        public async Task<int> ObtenerTotalUnidadesEnCarrito()
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Carrito.SumAsync(item => item.Cantidad);
        }

        public async Task AgregarAlCarrito(int productoId, int cantidad)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            var producto = await contexto.Productos.FindAsync(productoId);
            if (producto == null)
            {
                throw new Exception("Producto no encontrado");
            }

            var carritoItem = await contexto.Carrito.FirstOrDefaultAsync(c => c.ProductoId == productoId);
            if (carritoItem == null)
            {
                carritoItem = new Carrito
                {
                    ProductoId = productoId,
                    Cantidad = cantidad
                };
                contexto.Carrito.Add(carritoItem);
            }
            else
            {
                carritoItem.Cantidad += cantidad;
            }

            await contexto.SaveChangesAsync();
        }

        public async Task<int> ObtenerCantidadProductosDistintosEnCarrito()
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Carrito.CountAsync(); // Cuenta por producto distinto
        }


    }
}
