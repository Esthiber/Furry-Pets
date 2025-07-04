using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    public class AdoptantesDetallesService(IDbContextFactory<ApplicationDbContext> DbFactory) : ICRUD<AdoptantesDetalles>
    {
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.AdoptantesDetalles
                .AsNoTracking()
                .Where(a => a.AdoptantesDetallesID == id)
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.AdoptantesDetalles
                .AnyAsync(a => a.AdoptantesDetallesID == id);
        }

        public async Task<bool> InsertAsync(AdoptantesDetalles elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.AdoptantesDetalles.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<List<AdoptantesDetalles>> ListAsync(Expression<Func<AdoptantesDetalles, bool>> criteria)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.AdoptantesDetalles
                .Include(a => a.Persona)
                .Include(a => a.TipoVivienda)
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync(AdoptantesDetalles elem)
        {
            if (!await ExistAsync(elem.AdoptantesDetallesID))
            {
                return await InsertAsync(elem);
            }
            else
            {
                return await UpdateAsync(elem);
            }
        }

        public async Task<AdoptantesDetalles> SearchByIdAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.AdoptantesDetalles
                .Include(a => a.Persona)
                .Include(a => a.TipoVivienda)
                .FirstOrDefaultAsync(a => a.AdoptantesDetallesID == id) ?? new();
        }

        public async Task<bool> UpdateAsync(AdoptantesDetalles elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var a = await ctx.AdoptantesDetalles
                .Include(a => a.Persona)
                .Include(a => a.TipoVivienda)
                .FirstOrDefaultAsync(a => a.AdoptantesDetallesID == elem.AdoptantesDetallesID);

            if (a is null) return false;

            a.PersonasID = elem.PersonasID;
            a.TipoViviendasID = elem.TipoViviendasID;
            a.ViveEnViviendaAlquilada = elem.ViveEnViviendaAlquilada;
            a.TieneJardin = elem.TieneJardin;
            a.NotasJardin = elem.NotasJardin;
            a.TieneOtrasMascotas = elem.TieneOtrasMascotas;
            a.NotasOtrasMascotas = elem.NotasOtrasMascotas;
            a.HorasAusentes = elem.HorasAusentes;
            a.RazonAdopcion = elem.RazonAdopcion;
            a.IsDeleted = elem.IsDeleted;

            return await ctx.SaveChangesAsync() > 0;
        }
    }
} 