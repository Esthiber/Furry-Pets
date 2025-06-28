using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models.Adopciones._Mascotas;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services._Mascotas
{
    public class CategoriasServices(IDbContextFactory<ApplicationDbContext> DbFactory) : ICRUD<Categorias>
    {
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Categorias
                .AsNoTracking()
                .Where(m => m.CategoriaId == id)
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Categorias
                .AnyAsync(m => m.CategoriaId == id);
        }

        public async Task<bool> InsertAsync(Categorias elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.Categorias.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<List<Categorias>> ListAsync(Expression<Func<Categorias, bool>> criteria)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Categorias           
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync(Categorias elem)
        {
            if (await ExistAsync(elem.CategoriaId))
            {
                return await InsertAsync(elem);
            }
            else
            {
                return await UpdateAsync(elem);
            }
        }

        public async Task<Categorias> SearchByIdAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Categorias
                .FirstOrDefaultAsync(a => a.CategoriaId == id) ?? new();
        }

        public async Task<bool> UpdateAsync(Categorias elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.Categorias.Update(elem);
            return await ctx.SaveChangesAsync() > 0;
        }
    }
}
