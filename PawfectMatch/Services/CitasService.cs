using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    public class CitasService(IDbContextFactory<ApplicationDbContext> DbFactory) : ICRUD<Citas>
    {
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Citas
                .AsNoTracking()
                .Where(c => c.CitasID == id)
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Citas
                .AnyAsync(c => c.CitasID == id);
        }

        public async Task<bool> InsertAsync(Citas elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.Citas.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<List<Citas>> ListAsync(Expression<Func<Citas, bool>> criteria)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Citas
                .Include(c => c.Persona)
                .Include(c => c.MascotaPersona)
                .Include(c => c.EstadoCita)
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync(Citas elem)
        {
            if (!await ExistAsync(elem.CitasID))
            {
                return await InsertAsync(elem);
            }
            else
            {
                return await UpdateAsync(elem);
            }
        }

        public async Task<Citas> SearchByIdAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Citas
                .Include(c => c.Persona)
                .Include(c => c.MascotaPersona)
                .Include(c => c.EstadoCita)
                .FirstOrDefaultAsync(c => c.CitasID == id) ?? new();
        }

        public async Task<bool> UpdateAsync(Citas elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var c = await ctx.Citas
                .Include(c => c.Persona)
                .Include(c => c.MascotaPersona)
                .Include(c => c.EstadoCita)
                .FirstOrDefaultAsync(c => c.CitasID == elem.CitasID);

            if (c is null) return false;

            c.PersonasID = elem.PersonasID;
            c.MascotasPersonasID = elem.MascotasPersonasID;
            c.Fecha = elem.Fecha;
            c.Hora = elem.Hora;
            c.Motivo = elem.Motivo;
            c.EstadosCitasID = elem.EstadosCitasID;
            c.IsDeleted = elem.IsDeleted;

            return await ctx.SaveChangesAsync() > 0;
        }
    }
} 