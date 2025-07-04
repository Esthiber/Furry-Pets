using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    public class TiposServiciosService(IDbContextFactory<ApplicationDbContext> DbFactory) : ICRUD<TiposServicios>
    {
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.TiposServicios
                .AsNoTracking()
                .Where(t => t.TiposServiciosID == id)
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.TiposServicios
                .AnyAsync(t => t.TiposServiciosID == id);
        }

        public async Task<bool> InsertAsync(TiposServicios elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.TiposServicios.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<List<TiposServicios>> ListAsync(Expression<Func<TiposServicios, bool>> criteria)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.TiposServicios
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync(TiposServicios elem)
        {
            if (!await ExistAsync(elem.TiposServiciosID))
            {
                return await InsertAsync(elem);
            }
            else
            {
                return await UpdateAsync(elem);
            }
        }

        public async Task<TiposServicios> SearchByIdAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.TiposServicios
                .FirstOrDefaultAsync(t => t.TiposServiciosID == id) ?? new();
        }

        public async Task<bool> UpdateAsync(TiposServicios elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var t = await ctx.TiposServicios
                .FirstOrDefaultAsync(t => t.TiposServiciosID == elem.TiposServiciosID);

            if (t is null) return false;

            t.Nombre = elem.Nombre;
            t.IsDeleted = elem.IsDeleted;

            return await ctx.SaveChangesAsync() > 0;
        }
    }
} 