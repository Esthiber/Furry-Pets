using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models.POS;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    public class PagosService(IDbContextFactory<ApplicationDbContext> DbFactory) : ICRUD<Pagos>
    {
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var pago = await ctx.Pagos.FindAsync(id);
            if (pago == null) return false;
            
            pago.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Pagos
                .AnyAsync(p => p.PagosID == id);
        }

        public async Task<bool> InsertAsync(Pagos elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.Pagos.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<List<Pagos>> ListAsync(Expression<Func<Pagos, bool>> criteria)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Pagos
                .Include(p => p.Factura)
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync(Pagos elem)
        {
            if (!await ExistAsync(elem.PagosID))
            {
                return await InsertAsync(elem);
            }
            else
            {
                return await UpdateAsync(elem);
            }
        }

        public async Task<Pagos> SearchByIdAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Pagos
                .Include(p => p.Factura)
                .FirstOrDefaultAsync(p => p.PagosID == id) ?? new();
        }

        public async Task<bool> UpdateAsync(Pagos elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var p = await ctx.Pagos
                .Include(p => p.Factura)
                .FirstOrDefaultAsync(p => p.PagosID == elem.PagosID);

            if (p is null) return false;

            p.FacturasID = elem.FacturasID;
            p.Fecha = elem.Fecha;
            p.Monto = elem.Monto;
            p.MetodoPago = elem.MetodoPago;
            p.IsDeleted = elem.IsDeleted;

            return await ctx.SaveChangesAsync() > 0;
        }
    }
} 