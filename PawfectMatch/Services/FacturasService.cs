using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    public class FacturasService(IDbContextFactory<ApplicationDbContext> DbFactory) : ICRUD<Facturas>
    {
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var factura = await ctx.Facturas.FindAsync(id);
            if (factura == null) return false;
            
            factura.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Facturas
                .AnyAsync(f => f.FacturasID == id);
        }

        public async Task<bool> InsertAsync(Facturas elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.Facturas.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<List<Facturas>> ListAsync(Expression<Func<Facturas, bool>> criteria)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Facturas
                .Include(f => f.Persona)
                .Include(f => f.EstadoPago)
                .Include(f => f.Detalles)
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync(Facturas elem)
        {
            if (!await ExistAsync(elem.FacturasID))
            {
                return await InsertAsync(elem);
            }
            else
            {
                return await UpdateAsync(elem);
            }
        }

        public async Task<Facturas> SearchByIdAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Facturas
                .Include(f => f.Persona)
                .Include(f => f.EstadoPago)
                .Include(f => f.Detalles)
                .FirstOrDefaultAsync(f => f.FacturasID == id) ?? new();
        }

        public async Task<bool> UpdateAsync(Facturas elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var f = await ctx.Facturas
                .Include(f => f.Persona)
                .Include(f => f.EstadoPago)
                .Include(f => f.Detalles)
                .FirstOrDefaultAsync(f => f.FacturasID == elem.FacturasID);

            if (f is null) return false;

            f.PersonasID = elem.PersonasID;
            f.Fecha = elem.Fecha;
            f.Total = elem.Total;
            f.EstadoPagoID = elem.EstadoPagoID;
            f.IsDeleted = elem.IsDeleted;

            return await ctx.SaveChangesAsync() > 0;
        }
    }
} 