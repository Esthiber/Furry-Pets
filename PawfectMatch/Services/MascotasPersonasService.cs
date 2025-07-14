using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models.Servicios;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    public class MascotasPersonasService(IDbContextFactory<ApplicationDbContext> DbFactory) : ICRUD<MascotasPersonas>
    {
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var mascotaPersona = await ctx.MascotasPersonas.FindAsync(id);
            if (mascotaPersona == null) return false;
            
            mascotaPersona.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.MascotasPersonas
                .AnyAsync(m => m.MascotasPersonasID == id);
        }

        public async Task<bool> InsertAsync(MascotasPersonas elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.MascotasPersonas.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<List<MascotasPersonas>> ListAsync(Expression<Func<MascotasPersonas, bool>> criteria)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.MascotasPersonas
                .Include(m => m.Personas)
                .Include(m => m.Razas)
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync(MascotasPersonas elem)
        {
            if (!await ExistAsync(elem.MascotasPersonasID))
            {
                return await InsertAsync(elem);
            }
            else
            {
                return await UpdateAsync(elem);
            }
        }

        public async Task<MascotasPersonas> SearchByIdAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.MascotasPersonas
                .Include(m => m.Personas)
                .Include(m => m.Razas)
                .FirstOrDefaultAsync(m => m.MascotasPersonasID == id) ?? new();
        }

        public async Task<bool> UpdateAsync(MascotasPersonas elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var m = await ctx.MascotasPersonas
                .Include(m => m.Personas)
                .Include(m => m.Razas)
                .FirstOrDefaultAsync(m => m.MascotasPersonasID == elem.MascotasPersonasID);

            if (m is null) return false;

            m.PersonasID = elem.PersonasID;
            m.Nombre = elem.Nombre;
            m.RazasID = elem.RazasID;
            m.FechaNacimiento = elem.FechaNacimiento;
            m.IsDeleted = elem.IsDeleted;

            return await ctx.SaveChangesAsync() > 0;
        }
    }
} 