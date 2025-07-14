using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models.Servicios;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    public class ServiciosService(IDbContextFactory<ApplicationDbContext> DbFactory) : ICRUD<Servicios>
    {
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var servicio = await ctx.Servicios.FindAsync(id);
            if (servicio == null) return false;
            
            servicio.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Servicios
                .AnyAsync(s => s.ServiciosID == id);
        }

        public async Task<bool> InsertAsync(Servicios elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.Servicios.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<List<Servicios>> ListAsync(Expression<Func<Servicios, bool>> criteria)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Servicios
                .Include(s => s.TipoServicio)
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync(Servicios elem)
        {
            if (!await ExistAsync(elem.ServiciosID))
            {
                return await InsertAsync(elem);
            }
            else
            {
                return await UpdateAsync(elem);
            }
        }

        public async Task<Servicios> SearchByIdAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Servicios
                .Include(s => s.TipoServicio)
                .FirstOrDefaultAsync(s => s.ServiciosID == id) ?? new();
        }

        public async Task<bool> UpdateAsync(Servicios elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var s = await ctx.Servicios
                .Include(s => s.TipoServicio)
                .FirstOrDefaultAsync(s => s.ServiciosID == elem.ServiciosID);

            if (s is null) return false;

            s.TiposServiciosID = elem.TiposServiciosID;
            s.Nombre = elem.Nombre;
            s.Descripcion = elem.Descripcion;
            s.Precio = elem.Precio;
            s.IsDeleted = elem.IsDeleted;

            return await ctx.SaveChangesAsync() > 0;
        }
    }
} 