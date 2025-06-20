using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models._Servicios;
using PawfectMatch.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PawfectMatch.Tests
{
    public class ServiciosServiceTests
    {
        [Fact]
        public async Task InsertAsync_ShouldInsertServicio()
        {
            var factory = CrearDbFactory();
            var service = new ServiciosService(factory);

            var servicio = new Servicios { Nombre = "Vacunación", Descripcion = "Servicio de vacunas para mascotas" };
            var result = await service.InsertAsync(servicio);

            Assert.True(result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteServicio()
        {
            var factory = CrearDbFactory();
            var service = new ServiciosService(factory);

            var servicio = new Servicios { Nombre = "Corte de uñas", Descripcion = "Servicio de grooming" };
            await service.InsertAsync(servicio);

            var deleted = await service.DeleteAsync(servicio.ServicioId);
            Assert.True(deleted);
        }

        [Fact]
        public async Task ExistAsync_ShouldReturnTrueIfServicioExists()
        {
            var factory = CrearDbFactory();
            var service = new ServiciosService(factory);

            var servicio = new Servicios { Nombre = "Baño", Descripcion = "Servicio de baño para mascotas" };
            await service.InsertAsync(servicio);

            var exists = await service.ExistAsync(servicio.ServicioId);
            Assert.True(exists);
        }

        [Fact]
        public async Task ListAsync_ShouldReturnMatchingServicios()
        {
            var factory = CrearDbFactory();
            var service = new ServiciosService(factory);

            var servicio = new Servicios { Nombre = "Chequeo", Descripcion = "Chequeo general" };
            await service.InsertAsync(servicio);

            var result = await service.ListAsync(s => s.Nombre.Contains("Chequeo"));
            Assert.Contains(result, s => s.ServicioId == servicio.ServicioId);
        }

        [Fact]
        public async Task SearchByIdAsync_ShouldReturnCorrectServicio()
        {
            var factory = CrearDbFactory();
            var service = new ServiciosService(factory);

            var servicio = new Servicios { Nombre = "Desparasitación", Descripcion = "Eliminación de parásitos" };
            await service.InsertAsync(servicio);

            var result = await service.SearchByIdAsync(servicio.ServicioId);
            Assert.Equal(servicio.ServicioId, result.ServicioId);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateServicio()
        {
            var factory = CrearDbFactory();
            var service = new ServiciosService(factory);

            var servicio = new Servicios { Nombre = "Vacunación", Descripcion = "Vacunas básicas" };
            await service.InsertAsync(servicio);

            servicio.Descripcion = "Vacunas completas";
            var updated = await service.UpdateAsync(servicio);

            Assert.True(updated);
        }

        [Fact]
        public async Task SaveAsync_ShouldInsertOrUpdateServicio()
        {
            var factory = CrearDbFactory();
            var service = new ServiciosService(factory);

            var servicio = new Servicios { Nombre = "Guardería", Descripcion = "Cuidado diario" };
            var saved = await service.SaveAsync(servicio);
            Assert.True(saved);

            servicio.Descripcion = "Cuidado diario completo";
            var updated = await service.SaveAsync(servicio);
            Assert.True(updated);
        }

        private IDbContextFactory<ApplicationDbContext> CrearDbFactory()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(connection)
                .Options;

            var context = new ApplicationDbContext(options);
            context.Database.EnsureCreated();

            return new DbContextFactoryMock(options, connection);
        }

        class DbContextFactoryMock : IDbContextFactory<ApplicationDbContext>
        {
            private readonly DbContextOptions<ApplicationDbContext> _options;
            private readonly SqliteConnection _connection;

            public DbContextFactoryMock(DbContextOptions<ApplicationDbContext> options, SqliteConnection connection)
            {
                _options = options;
                _connection = connection;
            }

            public ApplicationDbContext CreateDbContext()
            {
                return new ApplicationDbContext(_options);
            }
        }
    }
}
