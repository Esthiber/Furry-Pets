using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    public class EstadoMascotaService(IDbContextFactory<ApplicationDbContext> DbFactory) : ICRUD<EstadoMascota>
    {
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var estadoMascota = await ctx.EstadoMascota.FindAsync(id);
            if (estadoMascota == null) return false;
            
            estadoMascota.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.EstadoMascota
                .AnyAsync(e => e.EstadoMascotaID == id);
        }

        public async Task<bool> InsertAsync(EstadoMascota elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.EstadoMascota.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<List<EstadoMascota>> ListAsync(Expression<Func<EstadoMascota, bool>> criteria)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.EstadoMascota
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync(EstadoMascota elem)
        {
            if (!await ExistAsync(elem.EstadoMascotaID))
            {
                return await InsertAsync(elem);
            }
            else
            {
                return await UpdateAsync(elem);
            }
        }

        public async Task<EstadoMascota> SearchByIdAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.EstadoMascota
                .FirstOrDefaultAsync(e => e.EstadoMascotaID == id) ?? new();
        }

        public async Task<bool> UpdateAsync(EstadoMascota elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var e = await ctx.EstadoMascota
                .FirstOrDefaultAsync(e => e.EstadoMascotaID == elem.EstadoMascotaID);

            if (e is null) return false;

            e.Nombre = elem.Nombre;
            e.IsDeleted = elem.IsDeleted;

            return await ctx.SaveChangesAsync() > 0;
        }
    }
} 