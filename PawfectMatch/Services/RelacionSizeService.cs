using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    public class RelacionSizeService(IDbContextFactory<ApplicationDbContext> DbFactory) : ICRUD<RelacionSize>
    {
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.RelacionSize
                .AsNoTracking()
                .Where(r => r.RelacionSizeID == id)
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.RelacionSize
                .AnyAsync(r => r.RelacionSizeID == id);
        }

        public async Task<bool> InsertAsync(RelacionSize elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.RelacionSize.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<List<RelacionSize>> ListAsync(Expression<Func<RelacionSize, bool>> criteria)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.RelacionSize
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync(RelacionSize elem)
        {
            if (!await ExistAsync(elem.RelacionSizeID))
            {
                return await InsertAsync(elem);
            }
            else
            {
                return await UpdateAsync(elem);
            }
        }

        public async Task<RelacionSize> SearchByIdAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.RelacionSize
                .FirstOrDefaultAsync(r => r.RelacionSizeID == id) ?? new();
        }

        public async Task<bool> UpdateAsync(RelacionSize elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var r = await ctx.RelacionSize
                .FirstOrDefaultAsync(r => r.RelacionSizeID == elem.RelacionSizeID);

            if (r is null) return false;

            r.Nombre = elem.Nombre;
            r.IsDeleted = elem.IsDeleted;

            return await ctx.SaveChangesAsync() > 0;
        }
    }
} 