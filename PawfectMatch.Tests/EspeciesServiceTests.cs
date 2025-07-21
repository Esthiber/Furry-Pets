using Xunit;
using System.Threading.Tasks;
using PawfectMatch.Services;
using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models;

namespace PawfectMatch.Tests
{
    public class EspeciesServiceTests
    {
        private readonly EspeciesService _service;
        private readonly ApplicationDbContext _context;

        public EspeciesServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: $"EspeciesServiceTestDb_{Guid.NewGuid()}")
                .Options;
            var factory = new TestDbContextFactory(options);
            _service = new EspeciesService(factory);
        }

        [Fact]
        public async Task InsertAsync_ShouldAddEspecie()
        {
            // Arrange
            var especie = new Especies
            {
                Nombre = "Test Especie",
                IsDeleted = false
            };

            // Act
            var result = await _service.InsertAsync(especie);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ListAsync_ShouldReturnEspecies()
        {
            // Arrange
            var especie = new Especies
            {
                Nombre = "Test Especie List",
                IsDeleted = false
            };
            await _service.InsertAsync(especie);

            // Act
            var especies = await _service.ListAsync(e => !e.IsDeleted);

            // Assert
            Assert.NotNull(especies);
            Assert.Contains(especies, e => e.Nombre == "Test Especie List");
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateEspecie()
        {
            // Arrange
            var especie = new Especies
            {
                Nombre = "Update Especie",
                IsDeleted = false
            };
            await _service.InsertAsync(especie);
            especie.Nombre = "Updated Especie";

            // Act
            var result = await _service.UpdateAsync(especie);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldSoftDeleteEspecie()
        {
            // Arrange
            var especie = new Especies
            {
                Nombre = "Delete Especie",
                IsDeleted = false
            };
            await _service.InsertAsync(especie);

            // Act
            var result = await _service.DeleteAsync(especie.EspeciesID);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task SearchByIdAsync_ShouldReturnEspecie()
        {
            // Arrange
            var especie = new Especies
            {
                Nombre = "Search Especie",
                IsDeleted = false
            };
            await _service.InsertAsync(especie);

            // Act
            var result = await _service.SearchByIdAsync(especie.EspeciesID);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Search Especie", result.Nombre);
        }

        [Fact]
        public async Task ExistAsync_ShouldReturnTrue_WhenEspecieExists()
        {
            // Arrange
            var especie = new Especies
            {
                Nombre = "Exist Especie",
                IsDeleted = false
            };
            await _service.InsertAsync(especie);

            // Act
            var exists = await _service.ExistAsync(especie.EspeciesID);

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public async Task ExistAsync_ShouldReturnFalse_WhenEspecieDoesNotExist()
        {
            // Act
            var exists = await _service.ExistAsync(999);

            // Assert
            Assert.False(exists);
        }

        [Fact]
        public async Task SaveAsync_ShouldInsert_WhenEspecieIsNew()
        {
            // Arrange
            var especie = new Especies
            {
                EspeciesID = 0,
                Nombre = "Save New Especie",
                IsDeleted = false
            };

            // Act
            var result = await _service.SaveAsync(especie);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task SaveAsync_ShouldUpdate_WhenEspecieExists()
        {
            // Arrange
            var especie = new Especies
            {
                Nombre = "Save Update Especie",
                IsDeleted = false
            };
            await _service.InsertAsync(especie);
            especie.Nombre = "Updated via Save";

            // Act
            var result = await _service.SaveAsync(especie);

            // Assert
            Assert.True(result);
        }
    }
}