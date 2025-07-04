using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    public class SeguimientoMascotasService(IDbContextFactory<ApplicationDbContext> DbFactory) : ICRUD<SeguimientoMascotas>
    {
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var seguimiento = await ctx.SeguimientoMascotas.FindAsync(id);
            if (seguimiento == null) return false;
            
            seguimiento.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.SeguimientoMascotas
                .AnyAsync(s => s.SeguimientoID == id);
        }

        public async Task<bool> InsertAsync(SeguimientoMascotas elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.SeguimientoMascotas.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<List<SeguimientoMascotas>> ListAsync(Expression<Func<SeguimientoMascotas, bool>> criteria)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.SeguimientoMascotas
                .Include(s => s.Persona)
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync(SeguimientoMascotas elem)
        {
            if (!await ExistAsync(elem.SeguimientoID))
            {
                return await InsertAsync(elem);
            }
            else
            {
                return await UpdateAsync(elem);
            }
        }

        public async Task<SeguimientoMascotas> SearchByIdAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.SeguimientoMascotas
                .Include(s => s.MascotaAdopcion)
                .Include(s => s.Persona)
                .Include(s => s.EstadoMascota)
                .FirstOrDefaultAsync(s => s.SeguimientoID == id) ?? new();
        }

        public async Task<bool> UpdateAsync(SeguimientoMascotas elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var s = await ctx.SeguimientoMascotas
                .Include(s => s.MascotaAdopcion)
                .Include(s => s.Persona)
                .Include(s => s.EstadoMascota)
                .FirstOrDefaultAsync(s => s.SeguimientoID == elem.SeguimientoID);

            if (s is null) return false;

            s.MascotasAdopcionID = elem.MascotasAdopcionID;
            s.PersonasID = elem.PersonasID;
            s.FechaVista = elem.FechaVista;
            s.EstadoMascotaID = elem.EstadoMascotaID;
            s.Observaciones = elem.Observaciones;
            s.IsDeleted = elem.IsDeleted;

            return await ctx.SaveChangesAsync() > 0;
        }
    }
} 