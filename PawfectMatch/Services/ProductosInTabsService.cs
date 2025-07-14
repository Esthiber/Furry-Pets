using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models.POS;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    public class ProductosInTabsService(IDbContextFactory<ApplicationDbContext> DbFactory) : ICRUD<ProductosInTabs>
    {
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var entity = await ctx.ProductosInTabs.FindAsync(id);
            if (entity == null) return false;

            entity.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.ProductosInTabs
                .AnyAsync(p => p.ProductosInTabsID == id);
        }

        public async Task<bool> InsertAsync(ProductosInTabs elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.ProductosInTabs.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<List<ProductosInTabs>> ListAsync(Expression<Func<ProductosInTabs, bool>> criteria)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.ProductosInTabs
                .Include(p => p.VetasTab)
                .Include(p => p.Producto)
                .AsNoTracking()
                .Where(criteria)
                .OrderBy(p => p.Orden)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync(ProductosInTabs elem)
        {
            if (!await ExistAsync(elem.ProductosInTabsID))
            {
                return await InsertAsync(elem);
            }
            else
            {
                return await UpdateAsync(elem);
            }
        }

        public async Task<ProductosInTabs> SearchByIdAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.ProductosInTabs
                .Include(p => p.VetasTab)
                .Include(p => p.Producto)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProductosInTabsID == id) ?? new();
        }

        public async Task<bool> UpdateAsync(ProductosInTabs elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var entity = await ctx.ProductosInTabs
                .FirstOrDefaultAsync(p => p.ProductosInTabsID == elem.ProductosInTabsID);

            if (entity is null) return false;

            entity.VetasTabsID = elem.VetasTabsID;
            entity.ProductosID = elem.ProductosID;
            entity.IsDeleted = elem.IsDeleted;

            return await ctx.SaveChangesAsync() > 0;
        }
    }
}
