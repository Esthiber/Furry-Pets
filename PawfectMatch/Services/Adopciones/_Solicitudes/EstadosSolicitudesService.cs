using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models.Adopciones._Solicitudes;
using System.Linq.Expressions;

namespace PawfectMatch.Services._Solicitudes
{
    public class EstadosSolicitudesService(IDbContextFactory<ApplicationDbContext> DbFactory) : ICRUD<EstadoSolicitudes>
    {
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.EstadoSolicitudes
                .AsNoTracking()
                .Where(m => m.EstadoSolicitudId == id)
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.EstadoSolicitudes
                .AnyAsync(m => m.EstadoSolicitudId == id);
        }

        public async Task<bool> InsertAsync(EstadoSolicitudes elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.EstadoSolicitudes.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<List<EstadoSolicitudes>> ListAsync(Expression<Func<EstadoSolicitudes, bool>> criteria)
        {await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.EstadoSolicitudes
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
            
        }

        public async Task<bool> SaveAsync(EstadoSolicitudes elem)
        {
            if (await ExistAsync(elem.EstadoSolicitudId))
            {
                return await InsertAsync(elem);
            }
            else
            {
                return await UpdateAsync(elem);
            }
        }

        public async Task<EstadoSolicitudes> SearchByIdAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.EstadoSolicitudes
                .FirstOrDefaultAsync(a => a.EstadoSolicitudId == id) ?? new();
        }

        public async Task<bool> UpdateAsync(EstadoSolicitudes elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.EstadoSolicitudes.Update(elem);
            return await ctx.SaveChangesAsync() > 0;
        }
    }
}
