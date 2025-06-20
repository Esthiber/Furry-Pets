using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    public class AdoptantesService(IDbContextFactory<ApplicationDbContext> DbFactory) : ICRUD<Adoptantes>
    {
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Adoptantes
                .AsNoTracking()
                .Where(a => a.AdoptanteId == id)
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Adoptantes
                .AnyAsync(c => c.AdoptanteId == id);
        }

        public async Task<bool> InsertAsync(Adoptantes elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.Adoptantes.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<List<Adoptantes>> ListAsync(Expression<Func<Adoptantes, bool>> criteria)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Adoptantes
                .Include(a=>a.User)
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync(Adoptantes elem)
        {
            if (await ExistAsync(elem.AdoptanteId))
            {
                return await InsertAsync(elem);
            }
            else
            {
                return await UpdateAsync(elem);
            }
        }

        public async Task<Adoptantes> SearchByIdAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Adoptantes
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.AdoptanteId == id) ?? new();
        }

        public async Task<bool> UpdateAsync(Adoptantes elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.Adoptantes.Update(elem);
            return await ctx.SaveChangesAsync() > 0;
        }
    }
}
