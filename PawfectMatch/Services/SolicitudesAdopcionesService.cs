using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    public class SolicitudesAdopcionesService(IDbContextFactory<ApplicationDbContext> DbFactory) : ICRUD<SolicitudesAdopciones>
    {
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var solicitud = await ctx.SolicitudesAdopciones.FindAsync(id);
            if (solicitud == null) return false;
            
            solicitud.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.SolicitudesAdopciones
                .AnyAsync(s => s.SolicitudesAdopcionesID == id);
        }

        public async Task<bool> InsertAsync(SolicitudesAdopciones elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.SolicitudesAdopciones.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<List<SolicitudesAdopciones>> ListAsync(Expression<Func<SolicitudesAdopciones, bool>> criteria)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.SolicitudesAdopciones
                .Include(s => s.Persona)
                .Include(s => s.MascotaAdopcion)
                .Include(s => s.EstadoSolicitud)
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync(SolicitudesAdopciones elem)
        {
            if (!await ExistAsync(elem.SolicitudesAdopcionesID))
            {
                return await InsertAsync(elem);
            }
            else
            {
                return await UpdateAsync(elem);
            }
        }

        public async Task<SolicitudesAdopciones> SearchByIdAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.SolicitudesAdopciones
                .Include(s => s.Persona)
                .Include(s => s.MascotaAdopcion)
                .Include(s => s.EstadoSolicitud)
                .FirstOrDefaultAsync(s => s.SolicitudesAdopcionesID == id) ?? new();
        }

        public async Task<bool> UpdateAsync(SolicitudesAdopciones elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var s = await ctx.SolicitudesAdopciones
                .Include(s => s.Persona)
                .Include(s => s.MascotaAdopcion)
                .Include(s => s.EstadoSolicitud)
                .FirstOrDefaultAsync(s => s.SolicitudesAdopcionesID == elem.SolicitudesAdopcionesID);

            if (s is null) return false;

            s.PersonasID = elem.PersonasID;
            s.MascotasAdopcionID = elem.MascotasAdopcionID;
            s.Fecha = elem.Fecha;
            s.EstadoSolicitudID = elem.EstadoSolicitudID;
            s.IsDeleted = elem.IsDeleted;

            return await ctx.SaveChangesAsync() > 0;
        }
    }
} 