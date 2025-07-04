using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    public class ProveedoresService(IDbContextFactory<ApplicationDbContext> DbFactory) : ICRUD<Proveedores>
    {
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var proveedor = await ctx.Proveedores.FindAsync(id);
            if (proveedor == null) return false;
            
            proveedor.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Proveedores
                .AnyAsync(p => p.ProveedoresID == id);
        }

        public async Task<bool> InsertAsync(Proveedores elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.Proveedores.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<List<Proveedores>> ListAsync(Expression<Func<Proveedores, bool>> criteria)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Proveedores
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync(Proveedores elem)
        {
            if (!await ExistAsync(elem.ProveedoresID))
            {
                return await InsertAsync(elem);
            }
            else
            {
                return await UpdateAsync(elem);
            }
        }

        public async Task<Proveedores> SearchByIdAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Proveedores
                .FirstOrDefaultAsync(p => p.ProveedoresID == id) ?? new();
        }

        public async Task<bool> UpdateAsync(Proveedores elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var p = await ctx.Proveedores
                .FirstOrDefaultAsync(p => p.ProveedoresID == elem.ProveedoresID);

            if (p is null) return false;

            p.Nombre = elem.Nombre;
            p.RNC = elem.RNC;
            p.Telefono = elem.Telefono;
            p.Email = elem.Email;
            p.IsDeleted = elem.IsDeleted;

            return await ctx.SaveChangesAsync() > 0;
        }
    }
} 