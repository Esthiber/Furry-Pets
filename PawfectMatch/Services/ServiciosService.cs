using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models._Servicios;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    public class ServiciosService(IDbContextFactory<ApplicationDbContext> DbFactory)
    {
        public async Task<bool> InsertAsync(Servicios elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.Servicios.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Servicios
                .AsNoTracking()
                .Where(s => s.ServicioId == id)
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Servicios.AnyAsync(s => s.ServicioId == id);
        }

        public async Task<List<Servicios>> ListAsync(Expression<Func<Servicios, bool>> criteria)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Servicios
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        public async Task<Servicios> SearchByIdAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Servicios.FirstOrDefaultAsync(s => s.ServicioId == id) ?? new();
        }

        public async Task<bool> UpdateAsync(Servicios elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.Servicios.Update(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> SaveAsync(Servicios elem)
        {
            if (!await ExistAsync(elem.ServicioId))
            {
                return await InsertAsync(elem);
            }
            else
            {
                return await UpdateAsync(elem);
            }
        }
    }
}
