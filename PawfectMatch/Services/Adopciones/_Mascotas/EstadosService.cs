using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models.Adopciones._Mascotas;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services._Mascotas
{
    public class EstadosService(IDbContextFactory<ApplicationDbContext> DbFactory) : ICRUD<Estados>
    {
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Estados
                .AsNoTracking()
                .Where(m => m.EstadoId == id)
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Estados
                .AnyAsync(m => m.EstadoId == id);
        }

        public async Task<bool> InsertAsync(Estados elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.Estados.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<List<Estados>> ListAsync(Expression<Func<Estados, bool>> criteria)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Estados
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync(Estados elem)
        {
            if (await ExistAsync(elem.EstadoId))
            {
                return await InsertAsync(elem);
            }
            else
            {
                return await UpdateAsync(elem);
            }
        }

        public async Task<Estados> SearchByIdAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Estados
                .FirstOrDefaultAsync(a => a.EstadoId == id) ?? new();
        }

        public async Task<bool> UpdateAsync(Estados elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.Estados.Update(elem);
            return await ctx.SaveChangesAsync() > 0;
        }
    }
}
