using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    public class EstadosPagosService(IDbContextFactory<ApplicationDbContext> DbFactory) : ICRUD<EstadosPagos>
    {
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var estadoPago = await ctx.EstadosPagos.FindAsync(id);
            if (estadoPago == null) return false;
            
            estadoPago.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.EstadosPagos
                .AnyAsync(e => e.EstadosPagosID == id);
        }

        public async Task<bool> InsertAsync(EstadosPagos elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.EstadosPagos.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<List<EstadosPagos>> ListAsync(Expression<Func<EstadosPagos, bool>> criteria)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.EstadosPagos
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync(EstadosPagos elem)
        {
            if (!await ExistAsync(elem.EstadosPagosID))
            {
                return await InsertAsync(elem);
            }
            else
            {
                return await UpdateAsync(elem);
            }
        }

        public async Task<EstadosPagos> SearchByIdAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.EstadosPagos
                .FirstOrDefaultAsync(e => e.EstadosPagosID == id) ?? new();
        }

        public async Task<bool> UpdateAsync(EstadosPagos elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var e = await ctx.EstadosPagos
                .FirstOrDefaultAsync(e => e.EstadosPagosID == elem.EstadosPagosID);

            if (e is null) return false;

            e.Nombre = elem.Nombre;
            e.IsDeleted = elem.IsDeleted;

            return await ctx.SaveChangesAsync() > 0;
        }
    }
} 