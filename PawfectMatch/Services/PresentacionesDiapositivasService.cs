using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models.Adopciones._Presentacion;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    public class PresentacionesDiapositivasService(IDbContextFactory<ApplicationDbContext> DbFactory) : ICRUD<PresentacionesDiapositivas>
    {
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.PresentacionesDiapositivas
                .AsNoTracking()
                .Where(p => p.PresentacionDiapositivaId == id)
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.PresentacionesDiapositivas
                .AnyAsync(p => p.PresentacionDiapositivaId == id);
        }

        public async Task<bool> InsertAsync(PresentacionesDiapositivas elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.PresentacionesDiapositivas.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<List<PresentacionesDiapositivas>> ListAsync(Expression<Func<PresentacionesDiapositivas, bool>> criteria)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.PresentacionesDiapositivas
                .Include(p => p.Presentacion)
                .Include(p => p.Diapositiva)
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync(PresentacionesDiapositivas elem)
        {
            if (!await ExistAsync(elem.PresentacionDiapositivaId))
            {
                return await InsertAsync(elem);
            }
            else
            {
                return await UpdateAsync(elem);
            }
        }

        public async Task<PresentacionesDiapositivas> SearchByIdAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.PresentacionesDiapositivas
                .Include(p => p.Presentacion)
                .Include(p => p.Diapositiva)
                .FirstOrDefaultAsync(p => p.PresentacionDiapositivaId == id) ?? new();
        }

        public async Task<bool> UpdateAsync(PresentacionesDiapositivas elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var p = await ctx.PresentacionesDiapositivas
                .Include(p => p.Presentacion)
                .Include(p => p.Diapositiva)
                .FirstOrDefaultAsync(p => p.PresentacionDiapositivaId == elem.PresentacionDiapositivaId);

            if (p is null) return false;

            p.PresentacionId = elem.PresentacionId;
            p.DiapositivaId = elem.DiapositivaId;
            p.Orden = elem.Orden;

            return await ctx.SaveChangesAsync() > 0;
        }
    }
} 