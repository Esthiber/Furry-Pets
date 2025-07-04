using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    public class ConfiguracionEmpresaService(IDbContextFactory<ApplicationDbContext> DbFactory) : ICRUD<ConfiguracionEmpresa>
    {
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.ConfiguracionEmpresa
                .AsNoTracking()
                .Where(c => c.EmpresaID == id)
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.ConfiguracionEmpresa
                .AnyAsync(c => c.EmpresaID == id);
        }

        public async Task<bool> InsertAsync(ConfiguracionEmpresa elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.ConfiguracionEmpresa.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<List<ConfiguracionEmpresa>> ListAsync(Expression<Func<ConfiguracionEmpresa, bool>> criteria)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.ConfiguracionEmpresa
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync(ConfiguracionEmpresa elem)
        {
            if (!await ExistAsync(elem.EmpresaID))
            {
                return await InsertAsync(elem);
            }
            else
            {
                return await UpdateAsync(elem);
            }
        }

        public async Task<ConfiguracionEmpresa> SearchByIdAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.ConfiguracionEmpresa
                .FirstOrDefaultAsync(c => c.EmpresaID == id) ?? new();
        }

        public async Task<bool> UpdateAsync(ConfiguracionEmpresa elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var c = await ctx.ConfiguracionEmpresa
                .FirstOrDefaultAsync(c => c.EmpresaID == elem.EmpresaID);

            if (c is null) return false;

            c.Nombre = elem.Nombre;
            c.Direccion = elem.Direccion;
            c.Telefono = elem.Telefono;
            c.RNC = elem.RNC;

            return await ctx.SaveChangesAsync() > 0;
        }
    }
} 