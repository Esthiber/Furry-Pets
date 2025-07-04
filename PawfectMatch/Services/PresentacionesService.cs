using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models.Adopciones._Presentacion;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    public class PresentacionesService(IDbContextFactory<ApplicationDbContext> DbFactory) : ICRUD<Presentaciones>
    {
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Presentaciones
                .AsNoTracking()
                .Where(p => p.PresentacionId == id)
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Presentaciones
                .AnyAsync(p => p.PresentacionId == id);
        }

        public async Task<bool> InsertAsync(Presentaciones elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.Presentaciones.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<List<Presentaciones>> ListAsync(Expression<Func<Presentaciones, bool>> criteria)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Presentaciones
                .Include(p => p.PresentacionesDiapositivas)
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync(Presentaciones elem)
        {
            if (!await ExistAsync(elem.PresentacionId))
            {
                return await InsertAsync(elem);
            }
            else
            {
                return await UpdateAsync(elem);
            }
        }

        public async Task<Presentaciones> SearchByIdAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Presentaciones
                .Include(p => p.PresentacionesDiapositivas)
                .FirstOrDefaultAsync(p => p.PresentacionId == id) ?? new();
        }

        public async Task<bool> UpdateAsync(Presentaciones elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var p = await ctx.Presentaciones
                .Include(p => p.PresentacionesDiapositivas)
                .FirstOrDefaultAsync(p => p.PresentacionId == elem.PresentacionId);

            if (p is null) return false;

            p.Titulo = elem.Titulo;
            p.Descripcion = elem.Descripcion;
            p.FechaCreacion = elem.FechaCreacion;
            p.EsActiva = elem.EsActiva;

            return await ctx.SaveChangesAsync() > 0;
        }
    }
} 