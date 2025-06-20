using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models._Solicitudes;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services._Solicitudes
{
    public class SolicitudesAdopcionesService(IDbContextFactory<ApplicationDbContext> DbFactory) : ICRUD<SolicitudesAdopciones>
    {
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.SolicitudesAdopciones
                .Where(a => a.SolicitudAdopcionId == id)
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.SolicitudesAdopciones
                .AnyAsync(c => c.SolicitudAdopcionId == id);
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
                .Include(a => a.Adoptante)
                    .ThenInclude(_ => _.User)
                .Include(a => a.Mascota)
                    .ThenInclude(m => m.Categoria)
                .Include(a => a.Mascota)
                    .ThenInclude(m => m.RelacionSize)
                .Include(a => a.Mascota)
                    .ThenInclude(m => m.Estado)
                .Include(a => a.Mascota)
                    .ThenInclude(m => m.Sexo)
                .Include(a => a.EstadoSolicitud)
                .Where(criteria)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync(SolicitudesAdopciones elem)
        {
            if (!await ExistAsync(elem.SolicitudAdopcionId))
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
                .Include(a => a.Adoptante)
                    .ThenInclude(_ => _.User)
                .Include(a => a.Mascota)
                    .ThenInclude(m => m.Categoria)
                .Include(a => a.Mascota)
                    .ThenInclude(m => m.RelacionSize)
                .Include(a => a.Mascota)
                    .ThenInclude(m => m.Estado)
                .Include(a => a.Mascota)
                    .ThenInclude(m => m.Sexo)
                .Include(a => a.EstadoSolicitud)
                .FirstOrDefaultAsync(a => a.SolicitudAdopcionId == id) ?? new();
        }

        public async Task<bool> UpdateAsync(SolicitudesAdopciones elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.SolicitudesAdopciones.Update(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> AceptarSolicitud(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var s = await ctx.SolicitudesAdopciones
                .Include(a => a.Adoptante)
                .Include(a => a.Mascota)
                .Include(a => a.EstadoSolicitud)
                .FirstOrDefaultAsync(a => a.SolicitudAdopcionId == id);

            if (s is null) return false;

            s.EstadoSolicitudId = 2;

            return await ctx.SaveChangesAsync() > 0;
        }


        public async Task<bool> RechazarSolicitud(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var s = await ctx.SolicitudesAdopciones
                .Include(a => a.Adoptante)
                .Include(a => a.Mascota)
                .Include(a => a.EstadoSolicitud)
                .FirstOrDefaultAsync(a => a.SolicitudAdopcionId == id);

            if (s is null) return false;

            s.EstadoSolicitudId = 3; // Rechazar

            return await ctx.SaveChangesAsync() > 0;
        }
    }
}
