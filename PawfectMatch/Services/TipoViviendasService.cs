using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    public class TipoViviendasService(IDbContextFactory<ApplicationDbContext> DbFactory) : ICRUD<TipoViviendas>
    {
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.TipoViviendas
                .AsNoTracking()
                .Where(t => t.TipoViviendasID == id)
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.TipoViviendas
                .AnyAsync(t => t.TipoViviendasID == id);
        }

        public async Task<bool> InsertAsync(TipoViviendas elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.TipoViviendas.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<List<TipoViviendas>> ListAsync(Expression<Func<TipoViviendas, bool>> criteria)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.TipoViviendas
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync(TipoViviendas elem)
        {
            if (!await ExistAsync(elem.TipoViviendasID))
            {
                return await InsertAsync(elem);
            }
            else
            {
                return await UpdateAsync(elem);
            }
        }

        public async Task<TipoViviendas> SearchByIdAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.TipoViviendas
                .FirstOrDefaultAsync(t => t.TipoViviendasID == id) ?? new();
        }

        public async Task<bool> UpdateAsync(TipoViviendas elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var t = await ctx.TipoViviendas
                .FirstOrDefaultAsync(t => t.TipoViviendasID == elem.TipoViviendasID);

            if (t is null) return false;

            t.Nombre = elem.Nombre;
            t.IsDeleted = elem.IsDeleted;

            return await ctx.SaveChangesAsync() > 0;
        }
    }
} 