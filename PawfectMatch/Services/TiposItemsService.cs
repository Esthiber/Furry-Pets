using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models.POS;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    public class TiposItemsService(IDbContextFactory<ApplicationDbContext> DbFactory) : ICRUD<TiposItems>
    {
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var tipoItem = await ctx.TiposItems.FindAsync(id);
            if (tipoItem == null) return false;
            
            tipoItem.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.TiposItems
                .AnyAsync(t => t.TiposItemsID == id);
        }

        public async Task<bool> InsertAsync(TiposItems elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.TiposItems.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<List<TiposItems>> ListAsync(Expression<Func<TiposItems, bool>> criteria)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.TiposItems
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync(TiposItems elem)
        {
            if (!await ExistAsync(elem.TiposItemsID))
            {
                return await InsertAsync(elem);
            }
            else
            {
                return await UpdateAsync(elem);
            }
        }

        public async Task<TiposItems> SearchByIdAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.TiposItems
                .FirstOrDefaultAsync(t => t.TiposItemsID == id) ?? new();
        }

        public async Task<bool> UpdateAsync(TiposItems elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var t = await ctx.TiposItems
                .FirstOrDefaultAsync(t => t.TiposItemsID == elem.TiposItemsID);

            if (t is null) return false;

            t.Nombre = elem.Nombre;
            t.IsDeleted = elem.IsDeleted;

            return await ctx.SaveChangesAsync() > 0;
        }
    }
} 