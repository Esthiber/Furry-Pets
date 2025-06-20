using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models._Servicios;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    public class SolicitudesServiciosService(IDbContextFactory<ApplicationDbContext> DbFactory) : ICRUD<SolicitudesServicios>
    {
        public async Task<bool> InsertAsync(SolicitudesServicios elem)
        {
            using var db = await DbFactory.CreateDbContextAsync();
            db.SolicitudesServicios.Add(elem);
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using var db = await DbFactory.CreateDbContextAsync();
            var entity = await db.SolicitudesServicios.FindAsync(id);
            if (entity == null) return false;

            db.SolicitudesServicios.Remove(entity);
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            using var db = await DbFactory.CreateDbContextAsync();
            return await db.SolicitudesServicios.AnyAsync(ss => ss.SolicitudServicioId == id);
        }

        public async Task<SolicitudesServicios> SearchByIdAsync(int id)
        {
            using var db = await DbFactory.CreateDbContextAsync();
            return await db.SolicitudesServicios
                          .Include(ss => ss.Servicio)
                          .Include(ss => ss.SolicitudAdopcion)
                          .FirstOrDefaultAsync(ss => ss.SolicitudServicioId == id)
                   ?? new SolicitudesServicios();
        }

        public async Task<List<SolicitudesServicios>> ListAsync(Expression<Func<SolicitudesServicios, bool>> criteria)
        {
            using var db = await DbFactory.CreateDbContextAsync();
            return await db.SolicitudesServicios
                          .Include(ss => ss.Servicio)
                          .Include(ss => ss.SolicitudAdopcion)
                          .Where(criteria)
                          .ToListAsync();
        }

        public async Task<bool> UpdateAsync(SolicitudesServicios elem)
        {
            using var db = await DbFactory.CreateDbContextAsync();
            var existing = await db.SolicitudesServicios.FindAsync(elem.SolicitudServicioId);
            if (existing == null) return false;

            db.Entry(existing).CurrentValues.SetValues(elem);
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<bool> SaveAsync(SolicitudesServicios elem)
        {
            if (await ExistAsync(elem.SolicitudServicioId))
                return await UpdateAsync(elem);
            else
                return await InsertAsync(elem);
        }
    }
}
