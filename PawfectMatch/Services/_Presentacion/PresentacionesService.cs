using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models._Presentacion;
using System.Linq.Expressions;

namespace PawfectMatch.Services._Presentacion
{
    public class PresentacionesService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;

        public PresentacionesService(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            var presentacion = await ctx.Presentaciones
                .Include(p => p.PresentacionesDiapositivas)
                    .ThenInclude(pd => pd.Diapositiva)
                .FirstOrDefaultAsync(p => p.PresentacionId == id);

            if (presentacion == null)
                return false;

            // Obtener las diapositivas para eliminarlas
            var diapositivas = presentacion.PresentacionesDiapositivas
                .Select(pd => pd.Diapositiva)
                .ToList();

            // Eliminar relaciones primero
            ctx.PresentacionesDiapositivas.RemoveRange(presentacion.PresentacionesDiapositivas);

            // Eliminar diapositivas
            ctx.Diapositivas.RemoveRange(diapositivas);

            // Luego eliminar la presentacion
            ctx.Presentaciones.Remove(presentacion);

            return await ctx.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();
            return await ctx.Presentaciones.AnyAsync(p => p.PresentacionId == id);
        }

        public async Task<bool> InsertAsync(Presentaciones presentacion, List<Diapositivas> diapositivas)
        {
            // reutiliza este mismo codigo para el update ya que es mas sencillo, y primero borrar lo creado.
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            try
            {
                // Si se activa esta presentacion, desactivar las demas
                if (presentacion.EsActiva)
                {
                    var presentacionesActivas = await ctx.Presentaciones
                        .Where(p => p.EsActiva)
                        .ToListAsync();

                    foreach (var p in presentacionesActivas)
                    {
                        p.EsActiva = false;
                        ctx.Presentaciones.Update(p);
                    }
                }

                // Agregar la presentación y guardar para obtener el ID
                ctx.Presentaciones.Add(presentacion);
                await ctx.SaveChangesAsync();

                // Agregar diapositivas y relaciones
                foreach (var diapositiva in diapositivas)
                {
                    
                    if (diapositiva.Orden <= 0)
                    {
                        diapositiva.Orden = diapositivas.IndexOf(diapositiva) + 1;
                    }

                    ctx.Diapositivas.Add(diapositiva);
                    await ctx.SaveChangesAsync(); // Guardar para obtener el ID de la diapositiva

                    // Crear la relacion
                    var relacion = new PresentacionesDiapositivas
                    {
                        PresentacionId = presentacion.PresentacionId,
                        DiapositivaId = diapositiva.DiapositivaId,
                        Orden = diapositiva.Orden
                    };

                    ctx.PresentacionesDiapositivas.Add(relacion);
                }

                return await ctx.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                // Si algo falla, intentar limpiar los datos parcialmente insertados
                if (presentacion.PresentacionId > 0)
                {
                    var entidad = await ctx.Presentaciones.FindAsync(presentacion.PresentacionId);
                    if (entidad != null)
                    {
                        ctx.Presentaciones.Remove(entidad);
                        await ctx.SaveChangesAsync();
                    }
                }

                throw; 
            }
        }

        public async Task<List<Presentaciones>> ListAsync(Expression<Func<Presentaciones, bool>> criteria)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            return await ctx.Presentaciones
                .Include(p => p.PresentacionesDiapositivas)
                    .ThenInclude(pd => pd.Diapositiva)
                .Where(criteria)
                .ToListAsync();
        }

        public async Task<bool> SaveAsync(Presentaciones presentacion, List<Diapositivas> diapositivas)
        {
            if (presentacion.PresentacionId == 0)
            {
                return await InsertAsync(presentacion, diapositivas);
            }
            else
            {
                return await UpdateAsync(presentacion, diapositivas);
            }
        }

        public async Task<Presentaciones> SearchByIdAsync(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            var presentacion = await ctx.Presentaciones
                .Include(p => p.PresentacionesDiapositivas)
                    .ThenInclude(pd => pd.Diapositiva)
                .FirstOrDefaultAsync(p => p.PresentacionId == id);

            if (presentacion == null)
            {
                throw new KeyNotFoundException($"Presentación con ID {id} no encontrada.");
            }

            // Ordenar las diapositivas por el campo Orden
            presentacion.PresentacionesDiapositivas = presentacion.PresentacionesDiapositivas
                .OrderBy(pd => pd.Orden)
                .ToList();

            return presentacion;
        }

        public async Task<bool> UpdateAsync(Presentaciones presentacion, List<Diapositivas> nuevasDiapositivas)
        {
            // para poder actualizar primero hay que borrar las diapositivas, demasiados fallos actualizando.
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            var existing = await ctx.Presentaciones
                .Include(p => p.PresentacionesDiapositivas)
                    .ThenInclude(pd => pd.Diapositiva)
                .FirstOrDefaultAsync(p => p.PresentacionId == presentacion.PresentacionId);

            if (existing == null)
                return false;

            try
            {
                // Si se activa esta presentación, desactivar las demás
                if (presentacion.EsActiva && !existing.EsActiva)
                {
                    var presentacionesActivas = await ctx.Presentaciones
                        .Where(p => p.EsActiva && p.PresentacionId != presentacion.PresentacionId)
                        .ToListAsync();

                    foreach (var p in presentacionesActivas)
                    {
                        p.EsActiva = false;
                        ctx.Presentaciones.Update(p);
                    }
                }

                // Actualizar campos basicos
                existing.Titulo = presentacion.Titulo;
                existing.Descripcion = presentacion.Descripcion;
                existing.EsActiva = presentacion.EsActiva;

                // Obtener las diapositivas actuales para eliminarlas
                var diapositivasActuales = existing.PresentacionesDiapositivas
                    .Select(pd => pd.Diapositiva)
                    .ToList();

                // Eliminar relaciones
                ctx.PresentacionesDiapositivas.RemoveRange(existing.PresentacionesDiapositivas);

                // Eliminar diapositivas antiguas
                ctx.Diapositivas.RemoveRange(diapositivasActuales);

                await ctx.SaveChangesAsync();

                // Insertar nuevas diapositivas y relaciones
                foreach (var diapositiva in nuevasDiapositivas)
                {
                    if (diapositiva.Orden <= 0)
                    {
                        diapositiva.Orden = nuevasDiapositivas.IndexOf(diapositiva) + 1;
                    }

                    diapositiva.DiapositivaId = 0;

                    ctx.Diapositivas.Add(diapositiva);
                    await ctx.SaveChangesAsync(); 

                    // Crear la relación
                    var relacion = new PresentacionesDiapositivas
                    {
                        PresentacionId = existing.PresentacionId,
                        DiapositivaId = diapositiva.DiapositivaId,
                        Orden = diapositiva.Orden
                    };

                    ctx.PresentacionesDiapositivas.Add(relacion);
                }

                return await ctx.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                await ctx.DisposeAsync();
                throw; 
            }
        }

        public async Task<Presentaciones?> GetActiveAsync()
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            return await ctx.Presentaciones
                .Include(p => p.PresentacionesDiapositivas)
                    .ThenInclude(pd => pd.Diapositiva)
                .FirstOrDefaultAsync(p => p.EsActiva);
        }

        public async Task<bool> SetActive(int id)
        {
            await using var ctx = await _dbFactory.CreateDbContextAsync();

            // Desactivar todas las presentaciones activas
            var presentacionesActivas = await ctx.Presentaciones
                .Where(p => p.EsActiva)
                .ToListAsync();

            foreach (var p in presentacionesActivas)
            {
                p.EsActiva = false;
                ctx.Presentaciones.Update(p);
            }

            // Activar manualmente la presentacin seleccionada
            var presentacion = await ctx.Presentaciones
                .FirstOrDefaultAsync(p => p.PresentacionId == id);

            if (presentacion == null)
            {
                throw new KeyNotFoundException($"Presentación con ID {id} no encontrada.");
            }

            presentacion.EsActiva = true;
            ctx.Presentaciones.Update(presentacion);

            return await ctx.SaveChangesAsync() > 0;
        }
    }
}