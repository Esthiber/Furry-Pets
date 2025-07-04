using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    public class PersonasRolesService(IDbContextFactory<ApplicationDbContext> DbFactory) : ICRUD<PersonasRoles>
    {
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var personaRol = await ctx.PersonasRoles.FindAsync(id);
            if (personaRol == null) return false;
            
            personaRol.IsDeleted = true;
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.PersonasRoles
                .AnyAsync(p => p.PersonasRolesID == id);
        }

        public async Task<bool> InsertAsync(PersonasRoles elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.PersonasRoles.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<List<PersonasRoles>> ListAsync(Expression<Func<PersonasRoles, bool>> criteria)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.PersonasRoles
                .Include(p => p.Persona)
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync(PersonasRoles elem)
        {
            if (!await ExistAsync(elem.PersonasRolesID))
            {
                return await InsertAsync(elem);
            }
            else
            {
                return await UpdateAsync(elem);
            }
        }

        public async Task<PersonasRoles> SearchByIdAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.PersonasRoles
                .Include(p => p.Persona)
                .FirstOrDefaultAsync(p => p.PersonasRolesID == id) ?? new();
        }

        public async Task<bool> UpdateAsync(PersonasRoles elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var p = await ctx.PersonasRoles
                .Include(p => p.Persona)
                .FirstOrDefaultAsync(p => p.PersonasRolesID == elem.PersonasRolesID);

            if (p is null) return false;

            p.PersonasID = elem.PersonasID;
            p.Rol = elem.Rol;
            p.IsDeleted = elem.IsDeleted;

            return await ctx.SaveChangesAsync() > 0;
        }
    }
} 