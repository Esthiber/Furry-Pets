using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    public class PersonasService(IDbContextFactory<ApplicationDbContext> DbFactory) : ICRUD<Personas>
    {
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Personas
                .AsNoTracking()
                .Where(p => p.PersonasID == id)
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Personas
                .AnyAsync(p => p.PersonasID == id);
        }

        public async Task<bool> InsertAsync(Personas elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.Personas.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<List<Personas>> ListAsync(Expression<Func<Personas, bool>> criteria)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Personas
                .Include(p => p.Roles)
                .Include(p => p.AdoptantesDetalles)
                .Include(p => p.Mascotas)
                .Include(p => p.Facturas)
                .Include(p => p.SolicitudesAdopciones)
                .Include(p => p.Seguimientos)
                .Include(p => p.Citas)
                .Include(p => p.HistoriasClinicas)
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync(Personas elem)
        {
            if (!await ExistAsync(elem.PersonasID))
            {
                return await InsertAsync(elem);
            }
            else
            {
                return await UpdateAsync(elem);
            }
        }

        public async Task<Personas> SearchByIdAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Personas
                .Include(p => p.Roles)
                .Include(p => p.AdoptantesDetalles)
                .Include(p => p.Mascotas)
                .Include(p => p.Facturas)
                .Include(p => p.SolicitudesAdopciones)
                .Include(p => p.Seguimientos)
                .Include(p => p.Citas)
                .Include(p => p.HistoriasClinicas)
                .FirstOrDefaultAsync(p => p.PersonasID == id) ?? new();
        }

        public async Task<bool> UpdateAsync(Personas elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var p = await ctx.Personas
                .Include(p => p.Roles)
                .Include(p => p.AdoptantesDetalles)
                .Include(p => p.Mascotas)
                .Include(p => p.Facturas)
                .Include(p => p.SolicitudesAdopciones)
                .Include(p => p.Seguimientos)
                .Include(p => p.Citas)
                .Include(p => p.HistoriasClinicas)
                .FirstOrDefaultAsync(p => p.PersonasID == elem.PersonasID);

            if (p is null) return false;

            p.Nombre = elem.Nombre;
            p.Telefono = elem.Telefono;
            p.Direccion = elem.Direccion;
            p.Sexo = elem.Sexo;
            p.Identificacion = elem.Identificacion;
            p.FechaNacimiento = elem.FechaNacimiento;
            p.Email = elem.Email;
            p.Nacionalidad = elem.Nacionalidad;
            p.EstadoCivil = elem.EstadoCivil;
            p.IsDeleted = elem.IsDeleted;

            return await ctx.SaveChangesAsync() > 0;
        }
    }
} 