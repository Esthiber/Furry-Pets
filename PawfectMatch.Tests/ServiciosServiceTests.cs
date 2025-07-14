using Xunit;
using System.Threading.Tasks;
using PawfectMatch.Services;
using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;

namespace PawfectMatch.Tests
{
    public class ServiciosServiceTests
    {
        private readonly ServiciosService _service;
        private readonly ApplicationDbContext _context;

        public ServiciosServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "PersonasServiceTestDb")
                .Options;
            var factory = new TestDbContextFactory(options);
            _service = new ServiciosService(factory);
        }

        [Fact]
        public async Task InsertAsync_ShouldAddServicio()
        {
            var servicio = new PawfectMatch.Models.Servicios.Servicios
            {
                TiposServiciosID = 1,
                Nombre = "Test Servicio",
                Descripcion = "Desc",
                Precio = 10,
                IsDeleted = false
            };
            var result = await _service.InsertAsync(servicio);
            Assert.True(result);
        }

        [Fact]
        public async Task ListAsync_ShouldReturnServicios()
        {
            var servicios = await _service.ListAsync(s => !s.IsDeleted);
            Assert.NotNull(servicios);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateServicio()
        {
            var servicio = new PawfectMatch.Models.Servicios.Servicios
            {
                TiposServiciosID = 1,
                Nombre = "Update Servicio",
                Descripcion = "Desc",
                Precio = 10,
                IsDeleted = false
            };
            await _service.InsertAsync(servicio);
            servicio.Nombre = "Updated";
            var result = await _service.UpdateAsync(servicio);
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldSoftDeleteServicio()
        {
            var servicio = new PawfectMatch.Models.Servicios.Servicios
            {
                TiposServiciosID = 1,
                Nombre = "Delete Servicio",
                Descripcion = "Desc",
                Precio = 10,
                IsDeleted = false
            };
            await _service.InsertAsync(servicio);
            var result = await _service.DeleteAsync(servicio.ServiciosID);
            Assert.True(result);
        }
    }
}
