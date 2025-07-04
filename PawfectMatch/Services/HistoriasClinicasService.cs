using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    public class HistoriasClinicasService(IDbContextFactory<ApplicationDbContext> DbFactory) : ICRUD<HistoriasClinicas>
    {
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.HistoriasClinicas
                .AsNoTracking()
                .Where(h => h.HistoriasClinicaID == id)
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.HistoriasClinicas
                .AnyAsync(h => h.HistoriasClinicaID == id);
        }

        public async Task<bool> InsertAsync(HistoriasClinicas elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.HistoriasClinicas.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<List<HistoriasClinicas>> ListAsync(Expression<Func<HistoriasClinicas, bool>> criteria)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.HistoriasClinicas
                .Include(h => h.MascotaPersona)
                .Include(h => h.Veterinario)
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync(HistoriasClinicas elem)
        {
            if (!await ExistAsync(elem.HistoriasClinicaID))
            {
                return await InsertAsync(elem);
            }
            else
            {
                return await UpdateAsync(elem);
            }
        }

        public async Task<HistoriasClinicas> SearchByIdAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.HistoriasClinicas
                .Include(h => h.MascotaPersona)
                .Include(h => h.Veterinario)
                .FirstOrDefaultAsync(h => h.HistoriasClinicaID == id) ?? new();
        }

        public async Task<bool> UpdateAsync(HistoriasClinicas elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var h = await ctx.HistoriasClinicas
                .Include(h => h.MascotaPersona)
                .Include(h => h.Veterinario)
                .FirstOrDefaultAsync(h => h.HistoriasClinicaID == elem.HistoriasClinicaID);

            if (h is null) return false;

            h.MascotasPersonasID = elem.MascotasPersonasID;
            h.PersonasID = elem.PersonasID;
            h.Fecha = elem.Fecha;
            h.DescripcionVisita = elem.DescripcionVisita;
            h.Diagnostico = elem.Diagnostico;
            h.Tratamiento = elem.Tratamiento;
            h.IsDeleted = elem.IsDeleted;

            return await ctx.SaveChangesAsync() > 0;
        }
    }
} 