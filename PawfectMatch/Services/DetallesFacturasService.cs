using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    public class DetallesFacturasService(IDbContextFactory<ApplicationDbContext> DbFactory) : ICRUD<DetallesFacturas>
    {
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.DetalleFacturas
                .AsNoTracking()
                .Where(d => d.DetallesFacturasID == id)
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.DetalleFacturas
                .AnyAsync(d => d.DetallesFacturasID == id);
        }

        public async Task<bool> InsertAsync(DetallesFacturas elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.DetalleFacturas.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<List<DetallesFacturas>> ListAsync(Expression<Func<DetallesFacturas, bool>> criteria)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.DetalleFacturas
                .Include(d => d.Factura)
                .Include(d => d.TipoItem)
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync(DetallesFacturas elem)
        {
            if (!await ExistAsync(elem.DetallesFacturasID))
            {
                return await InsertAsync(elem);
            }
            else
            {
                return await UpdateAsync(elem);
            }
        }

        public async Task<DetallesFacturas> SearchByIdAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.DetalleFacturas
                .Include(d => d.Factura)
                .Include(d => d.TipoItem)
                .FirstOrDefaultAsync(d => d.DetallesFacturasID == id) ?? new();
        }

        public async Task<bool> UpdateAsync(DetallesFacturas elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var d = await ctx.DetalleFacturas
                .Include(d => d.Factura)
                .Include(d => d.TipoItem)
                .FirstOrDefaultAsync(d => d.DetallesFacturasID == elem.DetallesFacturasID);

            if (d is null) return false;

            d.FacturasID = elem.FacturasID;
            d.TiposItemsID = elem.TiposItemsID;
            d.ItemID = elem.ItemID;
            d.Cantidad = elem.Cantidad;
            d.PrecioUnitario = elem.PrecioUnitario;
            d.IsDeleted = elem.IsDeleted;

            return await ctx.SaveChangesAsync() > 0;
        }
    }
} 