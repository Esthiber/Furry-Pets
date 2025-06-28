using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models.Adopciones._Mascotas;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services._Mascotas
{
    public class RelacionSizesService(IDbContextFactory<ApplicationDbContext> DbFactory) : ICRUD<RelacionSizes>
    {
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.RelacionSizes
                .AsNoTracking()
                .Where(m => m.RelacionSizeId == id)
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.RelacionSizes
                .AnyAsync(m => m.RelacionSizeId == id);
        }

        public async Task<bool> InsertAsync(RelacionSizes elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.RelacionSizes.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<List<RelacionSizes>> ListAsync(Expression<Func<RelacionSizes, bool>> criteria)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.RelacionSizes
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync(RelacionSizes elem)
        {
            if (await ExistAsync(elem.RelacionSizeId))
            {
                return await InsertAsync(elem);
            }
            else
            {
                return await UpdateAsync(elem);
            }
        }

        public async Task<RelacionSizes> SearchByIdAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.RelacionSizes
                .FirstOrDefaultAsync(a => a.RelacionSizeId == id) ?? new();
        }

        public async Task<bool> UpdateAsync(RelacionSizes elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.RelacionSizes.Update(elem);
            return await ctx.SaveChangesAsync() > 0;
        }
    }
}
