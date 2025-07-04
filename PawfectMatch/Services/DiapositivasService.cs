using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models.Adopciones._Presentacion;
using System.Linq;
using System.Linq.Expressions;

namespace PawfectMatch.Services
{
    public class DiapositivasService(IDbContextFactory<ApplicationDbContext> DbFactory) : ICRUD<Diapositivas>
    {
        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Diapositivas
                .AsNoTracking()
                .Where(d => d.DiapositivaId == id)
                .ExecuteDeleteAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Diapositivas
                .AnyAsync(d => d.DiapositivaId == id);
        }

        public async Task<bool> InsertAsync(Diapositivas elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            ctx.Diapositivas.Add(elem);
            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<List<Diapositivas>> ListAsync(Expression<Func<Diapositivas, bool>> criteria)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Diapositivas
                .AsNoTracking()
                .Where(criteria)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync(Diapositivas elem)
        {
            if (!await ExistAsync(elem.DiapositivaId))
            {
                return await InsertAsync(elem);
            }
            else
            {
                return await UpdateAsync(elem);
            }
        }

        public async Task<Diapositivas> SearchByIdAsync(int id)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            return await ctx.Diapositivas
                .FirstOrDefaultAsync(d => d.DiapositivaId == id) ?? new();
        }

        public async Task<bool> UpdateAsync(Diapositivas elem)
        {
            await using var ctx = await DbFactory.CreateDbContextAsync();
            var d = await ctx.Diapositivas
                .FirstOrDefaultAsync(d => d.DiapositivaId == elem.DiapositivaId);

            if (d is null) return false;

            d.IsTituloLeftActive = elem.IsTituloLeftActive;
            d.Titulo_Left = elem.Titulo_Left;
            d.SubTitulo_Left = elem.SubTitulo_Left;
            d.IsTituloRightActive = elem.IsTituloRightActive;
            d.Titulo_Right = elem.Titulo_Right;
            d.SubTitulo_Right = elem.SubTitulo_Right;
            d.IsButtonLeftActive = elem.IsButtonLeftActive;
            d.TextButton_Left = elem.TextButton_Left;
            d.LinkButton_Left = elem.LinkButton_Left;
            d.IsButtonRightActive = elem.IsButtonRightActive;
            d.TextButton_Right = elem.TextButton_Right;
            d.LinkButton_Right = elem.LinkButton_Right;
            d.ImageUrl = elem.ImageUrl;
            d.Orden = elem.Orden;
            d.Animacion = elem.Animacion;

            return await ctx.SaveChangesAsync() > 0;
        }
    }
} 