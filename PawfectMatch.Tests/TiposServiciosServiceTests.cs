using Xunit;
using System.Threading.Tasks;
using PawfectMatch.Services;
using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models.Servicios;

namespace PawfectMatch.Tests
{
    public class TiposServiciosServiceTests
    {
        private readonly TiposServiciosService _service;
        //private readonly ApplicationDbContext _context;

        public TiposServiciosServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: $"TiposServiciosServiceTestDb_{Guid.NewGuid()}")
                .Options;
            var factory = new TestDbContextFactory(options);
            _service = new TiposServiciosService(factory);
        }

        [Fact]
        public async Task InsertAsync_ShouldAddTipoServicio()
        {
            // Arrange
            var tipoServicio = new TiposServicios
            {
                Nombre = "Test Tipo Servicio",
                IsDeleted = false
            };

            // Act
            var result = await _service.InsertAsync(tipoServicio);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ListAsync_ShouldReturnTiposServicios()
        {
            // Arrange
            var tipoServicio = new TiposServicios
            {
                Nombre = "Test Tipo Servicio List",
                IsDeleted = false
            };
            await _service.InsertAsync(tipoServicio);

            // Act
            var tiposServicios = await _service.ListAsync(t => !t.IsDeleted);

            // Assert
            Assert.NotNull(tiposServicios);
            Assert.Contains(tiposServicios, t => t.Nombre == "Test Tipo Servicio List");
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateTipoServicio()
        {
            // Arrange
            var tipoServicio = new TiposServicios
            {
                Nombre = "Update Tipo Servicio",
                IsDeleted = false
            };
            await _service.InsertAsync(tipoServicio);
            tipoServicio.Nombre = "Updated Tipo Servicio";

            // Act
            var result = await _service.UpdateAsync(tipoServicio);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldSoftDeleteTipoServicio()
        {
            // Arrange
            var tipoServicio = new TiposServicios
            {
                Nombre = "Delete Tipo Servicio",
                IsDeleted = false
            };
            await _service.InsertAsync(tipoServicio);

            // Act
            var result = await _service.DeleteAsync(tipoServicio.TiposServiciosID);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task SearchByIdAsync_ShouldReturnTipoServicio()
        {
            // Arrange
            var tipoServicio = new TiposServicios
            {
                Nombre = "Search Tipo Servicio",
                IsDeleted = false
            };
            await _service.InsertAsync(tipoServicio);

            // Act
            var result = await _service.SearchByIdAsync(tipoServicio.TiposServiciosID);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Search Tipo Servicio", result.Nombre);
        }

        [Fact]
        public async Task ExistAsync_ShouldReturnTrue_WhenTipoServicioExists()
        {
            // Arrange
            var tipoServicio = new TiposServicios
            {
                Nombre = "Exist Tipo Servicio",
                IsDeleted = false
            };
            await _service.InsertAsync(tipoServicio);

            // Act
            var exists = await _service.ExistAsync(tipoServicio.TiposServiciosID);

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public async Task ExistAsync_ShouldReturnFalse_WhenTipoServicioDoesNotExist()
        {
            // Act
            var exists = await _service.ExistAsync(999);

            // Assert
            Assert.False(exists);
        }

        [Fact]
        public async Task SaveAsync_ShouldInsert_WhenTipoServicioIsNew()
        {
            // Arrange
            var tipoServicio = new TiposServicios
            {
                TiposServiciosID = 0,
                Nombre = "Save New Tipo Servicio",
                IsDeleted = false
            };

            // Act
            var result = await _service.SaveAsync(tipoServicio);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task SaveAsync_ShouldUpdate_WhenTipoServicioExists()
        {
            // Arrange
            var tipoServicio = new TiposServicios
            {
                Nombre = "Save Update Tipo Servicio",
                IsDeleted = false
            };
            await _service.InsertAsync(tipoServicio);
            tipoServicio.Nombre = "Updated via Save";

            // Act
            var result = await _service.SaveAsync(tipoServicio);

            // Assert
            Assert.True(result);
        }
    }
}