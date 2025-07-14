using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models.Adopciones;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    public class MascotasAdopcionService(IDbContextFactory<ApplicationDbContext> DbFactory) : ICRUD<MascotasAdopcion>
    {
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var mascota = await ctx.MascotasAdopcion.FindAsync(id);
            if (mascota == null) return false;
            
            mascota.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.MascotasAdopcion
                .AnyAsync(m => m.MascotasAdopcionID == id);
        }

        public async Task<bool> InsertAsync(MascotasAdopcion elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.MascotasAdopcion.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<List<MascotasAdopcion>> ListAsync(Expression<Func<MascotasAdopcion, bool>> criteria)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.MascotasAdopcion
                .Include(m => m.Razas)
                .Include(m => m.Estado)
                .Include(m => m.RelacionSize)
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync(MascotasAdopcion elem)
        {
            if (!await ExistAsync(elem.MascotasAdopcionID))
            {
                return await InsertAsync(elem);
            }
            else
            {
                return await UpdateAsync(elem);
            }
        }

        public async Task<MascotasAdopcion> SearchByIdAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.MascotasAdopcion
                .Include(m => m.Razas)
                .Include(m => m.Estado)
                .Include(m => m.RelacionSize)
                .FirstOrDefaultAsync(m => m.MascotasAdopcionID == id) ?? new();
        }

        public async Task<bool> UpdateAsync(MascotasAdopcion elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var m = await ctx.MascotasAdopcion
                .Include(m => m.Razas)
                .Include(m => m.Estado)
                .Include(m => m.RelacionSize)
                .FirstOrDefaultAsync(m => m.MascotasAdopcionID == elem.MascotasAdopcionID);

            if (m is null) return false;

            m.Nombre = elem.Nombre;
            m.Descripcion = elem.Descripcion;
            m.Tamanio = elem.Tamanio;
            m.FechaNacimiento = elem.FechaNacimiento;
            m.FotoURL = elem.FotoURL;
            m.RazasID = elem.RazasID;
            m.EstadoID = elem.EstadoID;
            m.RelacionSizeID = elem.RelacionSizeID;
            m.Sexo = elem.Sexo;
            m.IsDeleted = elem.IsDeleted;

            return await ctx.SaveChangesAsync() > 0;
        }
    }
} 