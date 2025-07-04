using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    public class EstadosService(IDbContextFactory<ApplicationDbContext> DbFactory) : ICRUD<Estados>
    {
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var estado = await ctx.Estados.FindAsync(id);
            if (estado == null) return false;
            
            estado.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Estados
                .AnyAsync(e => e.EstadoID == id);
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
            if (!await ExistAsync(elem.EstadoID))
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
                .FirstOrDefaultAsync(e => e.EstadoID == id) ?? new();
        }

        public async Task<bool> UpdateAsync(Estados elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var e = await ctx.Estados
                .FirstOrDefaultAsync(e => e.EstadoID == elem.EstadoID);

            if (e is null) return false;

            e.Nombre = elem.Nombre;
            e.IsDeleted = elem.IsDeleted;

            return await ctx.SaveChangesAsync() > 0;
        }
    }
} 