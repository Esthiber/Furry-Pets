using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    public class CategoriasProductosService(IDbContextFactory<ApplicationDbContext> DbFactory) : ICRUD<CategoriasProductos>
    {
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var categoria = await ctx.CategoriasProductos.FindAsync(id);
            if (categoria == null) return false;
            
            categoria.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.CategoriasProductos
                .AnyAsync(c => c.CategoriasProductosID == id);
        }

        public async Task<bool> InsertAsync(CategoriasProductos elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.CategoriasProductos.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<List<CategoriasProductos>> ListAsync(Expression<Func<CategoriasProductos, bool>> criteria)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.CategoriasProductos
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync(CategoriasProductos elem)
        {
            if (!await ExistAsync(elem.CategoriasProductosID))
            {
                return await InsertAsync(elem);
            }
            else
            {
                return await UpdateAsync(elem);
            }
        }

        public async Task<CategoriasProductos> SearchByIdAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.CategoriasProductos
                .FirstOrDefaultAsync(c => c.CategoriasProductosID == id) ?? new();
        }

        public async Task<bool> UpdateAsync(CategoriasProductos elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var c = await ctx.CategoriasProductos
                .FirstOrDefaultAsync(c => c.CategoriasProductosID == elem.CategoriasProductosID);

            if (c is null) return false;

            c.Nombre = elem.Nombre;
            c.IsDeleted = elem.IsDeleted;

            return await ctx.SaveChangesAsync() > 0;
        }
    }
} 