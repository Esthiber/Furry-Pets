using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    public class CitasService(IDbContextFactory<ApplicationDbContext> DbFactory) : ICRUD<Citas>
    {
        public async Task<bool> InsertAsync(Citas elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.Citas.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Citas
                 .AsNoTracking()
                 .Where(c => c.CitaId == id)
                 .ExecuteDeleteAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Citas
                .AnyAsync(c => c.CitaId == id);
        }

        public async Task<List<Citas>> ListAsync(Expression<Func<Citas, bool>> criteria)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Citas
                .Include(c => c.Adoptante)
                .Include(a => a.Mascota)
                    .ThenInclude(m => m.Categoria)
                .Include(a => a.Mascota)
                    .ThenInclude(m => m.RelacionSize)
                .Include(a => a.Mascota)
                    .ThenInclude(m => m.Estado)
                .Include(a => a.Mascota)
                    .ThenInclude(m => m.Sexo)
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();

        }

        public async Task<bool> SaveAsync(Citas elem)
        {
            if (await ExistAsync(elem.CitaId))
            {
                return await InsertAsync(elem);
            }
            else
            {
                return await UpdateAsync(elem);
            }
        }

        public async Task<Citas> SearchByIdAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Citas
                .Include(c => c.Adoptante)
                .Include(a => a.Mascota)
                    .ThenInclude(m => m.Categoria)
                .Include(a => a.Mascota)
                    .ThenInclude(m => m.RelacionSize)
                .Include(a => a.Mascota)
                    .ThenInclude(m => m.Estado)
                .Include(a => a.Mascota)
                    .ThenInclude(m => m.Sexo)
                .FirstOrDefaultAsync(c => c.CitaId == id) ?? new();
        }

        public async Task<bool> UpdateAsync(Citas elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.Citas.Update(elem);
            return await ctx.SaveChangesAsync() > 0;
        }
    }
}
