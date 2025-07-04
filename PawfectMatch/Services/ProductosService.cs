using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    public class ProductosService(IDbContextFactory<ApplicationDbContext> DbFactory) : ICRUD<Productos>
    {
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var producto = await ctx.Productos.FindAsync(id);
            if (producto == null) return false;
            
            producto.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Productos
                .AnyAsync(p => p.ProductosID == id);
        }

        public async Task<bool> InsertAsync(Productos elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.Productos.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<List<Productos>> ListAsync(Expression<Func<Productos, bool>> criteria)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Productos
                .Include(p => p.CategoriaProducto)
                .Include(p => p.Proveedor)
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync(Productos elem)
        {
            if (!await ExistAsync(elem.ProductosID))
            {
                return await InsertAsync(elem);
            }
            else
            {
                return await UpdateAsync(elem);
            }
        }

        public async Task<Productos> SearchByIdAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Productos
                .Include(p => p.CategoriaProducto)
                .Include(p => p.Proveedor)
                .FirstOrDefaultAsync(p => p.ProductosID == id) ?? new();
        }

        public async Task<bool> UpdateAsync(Productos elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var p = await ctx.Productos
                .Include(p => p.CategoriaProducto)
                .Include(p => p.Proveedor)
                .FirstOrDefaultAsync(p => p.ProductosID == elem.ProductosID);

            if (p is null) return false;

            p.CategoriasProductosID = elem.CategoriasProductosID;
            p.ProveedoresID = elem.ProveedoresID;
            p.Nombre = elem.Nombre;
            p.Descripcion = elem.Descripcion;
            p.Costo = elem.Costo;
            p.Precio = elem.Precio;
            p.Stock = elem.Stock;
            p.ImagenUrl = elem.ImagenUrl;
            p.IsDeleted = elem.IsDeleted;

            return await ctx.SaveChangesAsync() > 0;
        }
    }
} 