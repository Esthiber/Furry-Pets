using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    public class EstadoSolicitudService(IDbContextFactory<ApplicationDbContext> DbFactory) : ICRUD<EstadoSolicitud>
    {
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var estadoSolicitud = await ctx.EstadoSolicitud.FindAsync(id);
            if (estadoSolicitud == null) return false;
            
            estadoSolicitud.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.EstadoSolicitud
                .AnyAsync(e => e.EstadoSolicitudID == id);
        }

        public async Task<bool> InsertAsync(EstadoSolicitud elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.EstadoSolicitud.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<List<EstadoSolicitud>> ListAsync(Expression<Func<EstadoSolicitud, bool>> criteria)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.EstadoSolicitud
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync(EstadoSolicitud elem)
        {
            if (!await ExistAsync(elem.EstadoSolicitudID))
            {
                return await InsertAsync(elem);
            }
            else
            {
                return await UpdateAsync(elem);
            }
        }

        public async Task<EstadoSolicitud> SearchByIdAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.EstadoSolicitud
                .FirstOrDefaultAsync(e => e.EstadoSolicitudID == id) ?? new();
        }

        public async Task<bool> UpdateAsync(EstadoSolicitud elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var e = await ctx.EstadoSolicitud
                .FirstOrDefaultAsync(e => e.EstadoSolicitudID == elem.EstadoSolicitudID);

            if (e is null) return false;

            e.Nombre = elem.Nombre;
            e.IsDeleted = elem.IsDeleted;

            return await ctx.SaveChangesAsync() > 0;
        }
    }
} 