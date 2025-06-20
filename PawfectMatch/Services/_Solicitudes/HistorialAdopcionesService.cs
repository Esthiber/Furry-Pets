using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models._Solicitudes;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services._Solicitudes
{
    public class HistorialAdopcionesService(IDbContextFactory<ApplicationDbContext> DbFactory) : ICRUD<HistorialAdopciones>
    {
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.HistorialAdopciones
                .AsNoTracking()
                .Where(a => a.HistorialAdopcioneId == id)
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.HistorialAdopciones
                .AnyAsync(c => c.HistorialAdopcioneId == id);
        }

        public async Task<bool> InsertAsync(HistorialAdopciones elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.HistorialAdopciones.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<List<HistorialAdopciones>> ListAsync(Expression<Func<HistorialAdopciones, bool>> criteria)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.HistorialAdopciones
                .Include(a => a.Adoptante)
                .Include(a => a.Mascota)
                    .ThenInclude(m=>m.Categoria)
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync(HistorialAdopciones elem)
        {
            if (await ExistAsync(elem.HistorialAdopcioneId))
            {
                return await InsertAsync(elem);
            }
            else
            {
                return await UpdateAsync(elem);
            }
        }

        public async Task<HistorialAdopciones> SearchByIdAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.HistorialAdopciones
                .Include(a => a.Adoptante)
                .Include(a => a.Mascota)
                .FirstOrDefaultAsync(a => a.HistorialAdopcioneId == id) ?? new();
        }

        public async Task<bool> UpdateAsync(HistorialAdopciones elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.HistorialAdopciones.Update(elem);
            return await ctx.SaveChangesAsync() > 0;
        }
    }
}
