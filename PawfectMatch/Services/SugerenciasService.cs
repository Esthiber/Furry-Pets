using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models.Adopciones;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    public class SugerenciasService(IDbContextFactory<ApplicationDbContext> DbFactory) : ICRUD<Sugerencias>
    {
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Sugerencias
                .AsNoTracking()
                .Where(s => s.SugerenciaId == id)
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Sugerencias
                .AnyAsync(s => s.SugerenciaId == id);
        }

        public async Task<bool> InsertAsync(Sugerencias elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.Sugerencias.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<List<Sugerencias>> ListAsync(Expression<Func<Sugerencias, bool>> criteria)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Sugerencias
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync(Sugerencias elem)
        {
            if (!await ExistAsync(elem.SugerenciaId))
            {
                return await InsertAsync(elem);
            }
            else
            {
                return await UpdateAsync(elem);
            }
        }

        public async Task<Sugerencias> SearchByIdAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Sugerencias
                .FirstOrDefaultAsync(s => s.SugerenciaId == id) ?? new();
        }

        public async Task<bool> UpdateAsync(Sugerencias elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var s = await ctx.Sugerencias
                .FirstOrDefaultAsync(s => s.SugerenciaId == elem.SugerenciaId);

            if (s is null) return false;

            s.UserMail = elem.UserMail;
            s.Descripcion = elem.Descripcion;

            return await ctx.SaveChangesAsync() > 0;
        }
    }
} 