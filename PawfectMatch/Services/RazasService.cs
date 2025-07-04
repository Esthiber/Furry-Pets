using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    public class RazasService(IDbContextFactory<ApplicationDbContext> DbFactory) : ICRUD<Razas>
    {
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Razas
                .AsNoTracking()
                .Where(r => r.RazasID == id)
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Razas
                .AnyAsync(r => r.RazasID == id);
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
                .Include(r => r.Especie)
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync(Razas elem)
        {
            if (!await ExistAsync(elem.RazasID))
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
                .Include(r => r.Especie)
                .FirstOrDefaultAsync(r => r.RazasID == id) ?? new();
        }

        public async Task<bool> UpdateAsync(Razas elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var r = await ctx.Razas
                .Include(r => r.Especie)
                .FirstOrDefaultAsync(r => r.RazasID == elem.RazasID);

            if (r is null) return false;

            r.EspeciesID = elem.EspeciesID;
            r.Nombre = elem.Nombre;
            r.IsDeleted = elem.IsDeleted;

            return await ctx.SaveChangesAsync() > 0;
        }
    }
} 