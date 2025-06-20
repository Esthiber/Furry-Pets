using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models._Mascotas;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services._Mascotas
{
    public class SexosService(IDbContextFactory<ApplicationDbContext> DbFactory) : ICRUD<Sexos>
    {
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Sexos
                .AsNoTracking()
                .Where(m => m.SexoId == id)
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Sexos
                .AnyAsync(m => m.SexoId == id);
        }

        public async Task<bool> InsertAsync(Sexos elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.Sexos.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<List<Sexos>> ListAsync(Expression<Func<Sexos, bool>> criteria)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Sexos
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync(Sexos elem)
        {
            if (await ExistAsync(elem.SexoId))
            {
                return await InsertAsync(elem);
            }
            else
            {
                return await UpdateAsync(elem);
            }
        }

        public async Task<Sexos> SearchByIdAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Sexos
                .FirstOrDefaultAsync(a => a.SexoId == id) ?? new();
        }

        public async Task<bool> UpdateAsync(Sexos elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.Sexos.Update(elem);
            return await ctx.SaveChangesAsync() > 0;
        }
    }
}
