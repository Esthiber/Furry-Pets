using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models.POS;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    public class VetasTabsService(IDbContextFactory<ApplicationDbContext> DbFactory) : ICRUD<VetasTabs>
    {
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var tab = await ctx.VetasTabs.FindAsync(id);
            if (tab == null) return false;

            tab.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.VetasTabs
                .AnyAsync(c => c.VetasTabsID == id);
        }

        public async Task<bool> InsertAsync(VetasTabs elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.VetasTabs.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<List<VetasTabs>> ListAsync(Expression<Func<VetasTabs, bool>> criteria)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.VetasTabs
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync(VetasTabs elem)
        {
            if (!await ExistAsync(elem.VetasTabsID))
            {
                return await InsertAsync(elem);
            }
            else
            {
                return await UpdateAsync(elem);
            }
        }

        public async Task<VetasTabs> SearchByIdAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.VetasTabs .AsNoTracking()
                .FirstOrDefaultAsync(c => c.VetasTabsID == id) ?? new();
        }

        public async Task<bool> UpdateAsync(VetasTabs elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var c = await ctx.VetasTabs
                .FirstOrDefaultAsync(c => c.VetasTabsID == elem.VetasTabsID);

            if (c is null) return false;

            c.Nombre = elem.Nombre;
            c.Color = elem.Color;   
            c.Icono = elem.Icono;
            c.Orden = elem.Orden;
            c.IsDeleted = elem.IsDeleted;

            return await ctx.SaveChangesAsync() > 0;
        }
    }
}
