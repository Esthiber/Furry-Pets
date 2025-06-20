using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models._Mascotas;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services._Mascotas
{
    public class RazasService(IDbContextFactory<ApplicationDbContext> DbFactory) : ICRUD<Razas>
    {
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Razas
                .AsNoTracking()
                .Where(m => m.RazaId == id)
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Razas
                .AnyAsync(m => m.RazaId == id);
        }

        public async Task<bool> InsertAsync(Razas elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.Razas.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<List<Razas>> ListAsync(Expression<Func<Razas, bool>> criteria)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Razas
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync(Razas elem)
        {
            if (await ExistAsync(elem.RazaId))
            {
                return await InsertAsync(elem);
            }
            else
            {
                return await UpdateAsync(elem);
            }
        }

        public async Task<Razas> SearchByIdAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Razas
                .FirstOrDefaultAsync(a => a.RazaId == id) ?? new();
        }

        public async Task<bool> UpdateAsync(Razas elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.Razas.Update(elem);
            return await ctx.SaveChangesAsync() > 0;
        }
    }
}
