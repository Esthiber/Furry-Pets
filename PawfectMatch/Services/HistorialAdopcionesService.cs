using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    public class HistorialAdopcionesService(IDbContextFactory<ApplicationDbContext> DbFactory) : ICRUD<HistorialAdopciones>
    {
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var historial = await ctx.HistorialAdopciones.FindAsync(id);
            if (historial == null) return false;
            
            historial.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.HistorialAdopciones
                .AnyAsync(h => h.HistorialAdopcionesID == id);
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
                .Include(h => h.SolicitudAdopcion)
                .Include(h => h.MascotaAdopcion)
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync(HistorialAdopciones elem)
        {
            if (!await ExistAsync(elem.HistorialAdopcionesID))
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
                .Include(h => h.SolicitudAdopcion)
                .Include(h => h.MascotaAdopcion)
                .FirstOrDefaultAsync(h => h.HistorialAdopcionesID == id) ?? new();
        }

        public async Task<bool> UpdateAsync(HistorialAdopciones elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var h = await ctx.HistorialAdopciones
                .Include(h => h.SolicitudAdopcion)
                .Include(h => h.MascotaAdopcion)
                .FirstOrDefaultAsync(h => h.HistorialAdopcionesID == elem.HistorialAdopcionesID);

            if (h is null) return false;

            h.SolicitudesAdopcionesID = elem.SolicitudesAdopcionesID;
            h.MascotasAdopcionID = elem.MascotasAdopcionID;
            h.FechaAdopcion = elem.FechaAdopcion;
            h.Notas = elem.Notas;
            h.IsDeleted = elem.IsDeleted;

            return await ctx.SaveChangesAsync() > 0;
        }
    }
} 