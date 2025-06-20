using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models._Mascotas;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services._Mascotas
{
    public class MascotasService(IDbContextFactory<ApplicationDbContext> DbFactory) : ICRUD<Mascotas>
    {
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Mascotas
                .AsNoTracking()
                .Where(m => m.MascotaId == id)
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Mascotas
                .AnyAsync(m => m.MascotaId == id);
        }

        public async Task<bool> InsertAsync(Mascotas elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.Mascotas.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<List<Mascotas>> ListAsync(Expression<Func<Mascotas, bool>> criteria)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Mascotas
                .Include(a => a.Categoria)
                .Include(a => a.RelacionSize)
                .Include(a => a.Estado)
                .Include(a => a.Sexo)
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync(Mascotas elem)
        {
            if (!await ExistAsync(elem.MascotaId))
            {
                return await InsertAsync(elem);
            }
            else
            {
                return await UpdateAsync(elem);
            }
        }

        public async Task<Mascotas> SearchByIdAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Mascotas
                .Include(a => a.Categoria)
                .Include(a => a.RelacionSize)
                .Include(a => a.Estado)
                .Include(a => a.Sexo)
                .FirstOrDefaultAsync(a => a.MascotaId == id) ?? new();
        }

        public async Task<bool> UpdateAsync(Mascotas elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var m = await ctx.Mascotas
                .Include(a => a.Categoria)
                .Include(a => a.Estado)
                .Include(a => a.RelacionSize)
                .Include(a => a.Sexo)
                .FirstOrDefaultAsync(a => a.MascotaId == elem.MascotaId);

            if (m is null) return false;

            m.Nombre = elem.Nombre;
            m.Descripcion = elem.Descripcion;
            m.Tamano = elem.Tamano;
            m.FechaNacimiento = elem.FechaNacimiento;
            m.FotoUrl = elem.FotoUrl;

            m.SexoId = elem.SexoId;
            m.CategoriaId = elem.CategoriaId;
            m.RazaId = elem.RazaId;
            m.EstadoId = elem.EstadoId;
            m.RelacionSizeId = elem.RelacionSizeId;

            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> AdoptarAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var s = await ctx.Mascotas
                .Include(a => a.Categoria)
                .Include(a => a.Estado)
                .Include(a => a.RelacionSize)
                .Include(a => a.Sexo)
                .FirstOrDefaultAsync(a => a.MascotaId == id);

            if (s is null) return false;

            s.EstadoId = 2;

            return await ctx.SaveChangesAsync() > 0;
        }
    }
}
