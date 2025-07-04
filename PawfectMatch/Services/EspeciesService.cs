using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    public class EspeciesService(IDbContextFactory<ApplicationDbContext> DbFactory) : ICRUD<Especies>
    {
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var especie = await ctx.Especies.FindAsync(id);
            if (especie == null) return false;
            
            especie.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Especies
                .AnyAsync(e => e.EspeciesID == id);
        }

        public async Task<bool> InsertAsync(Especies elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.Especies.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<List<Especies>> ListAsync(Expression<Func<Especies, bool>> criteria)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Especies
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync(Especies elem)
        {
            if (!await ExistAsync(elem.EspeciesID))
            {
                return await InsertAsync(elem);
            }
            else
            {
                return await UpdateAsync(elem);
            }
        }

        public async Task<Especies> SearchByIdAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Especies
                .FirstOrDefaultAsync(e => e.EspeciesID == id) ?? new();
        }

        public async Task<bool> UpdateAsync(Especies elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var e = await ctx.Especies
                .FirstOrDefaultAsync(e => e.EspeciesID == elem.EspeciesID);

            if (e is null) return false;

            e.Nombre = elem.Nombre;
            e.IsDeleted = elem.IsDeleted;

            return await ctx.SaveChangesAsync() > 0;
        }
    }
} 