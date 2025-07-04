using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    public class EstadosCitasService(IDbContextFactory<ApplicationDbContext> DbFactory) : ICRUD<EstadosCitas>
    {
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.EstadosCitas
                .AsNoTracking()
                .Where(e => e.EstadosCitasID == id)
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.EstadosCitas
                .AnyAsync(e => e.EstadosCitasID == id);
        }

        public async Task<bool> InsertAsync(EstadosCitas elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.EstadosCitas.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<List<EstadosCitas>> ListAsync(Expression<Func<EstadosCitas, bool>> criteria)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.EstadosCitas
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync(EstadosCitas elem)
        {
            if (!await ExistAsync(elem.EstadosCitasID))
            {
                return await InsertAsync(elem);
            }
            else
            {
                return await UpdateAsync(elem);
            }
        }

        public async Task<EstadosCitas> SearchByIdAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.EstadosCitas
                .FirstOrDefaultAsync(e => e.EstadosCitasID == id) ?? new();
        }

        public async Task<bool> UpdateAsync(EstadosCitas elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var e = await ctx.EstadosCitas
                .FirstOrDefaultAsync(e => e.EstadosCitasID == elem.EstadosCitasID);

            if (e is null) return false;

            e.Nombre = elem.Nombre;
            e.IsDeleted = elem.IsDeleted;

            return await ctx.SaveChangesAsync() > 0;
        }
    }
} 