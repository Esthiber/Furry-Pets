using Xunit;
using System.Threading.Tasks;
using PawfectMatch.Services;
using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models;

namespace PawfectMatch.Tests
{
    public class RazasServiceTests
    {
        private readonly RazasService _service;
        //private readonly ApplicationDbContext _context;

        public RazasServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: $"RazasServiceTestDb_{Guid.NewGuid()}")
                .Options;
            var factory = new TestDbContextFactory(options);
            _service = new RazasService(factory);
        }

        [Fact]
        public async Task InsertAsync_ShouldAddRaza()
        {
            // Arrange
            var raza = new Razas
            {
                Nombre = "Test Raza",
                EspeciesID = 1,
                IsDeleted = false
            };

            // Act
            var result = await _service.InsertAsync(raza);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ListAsync_ShouldReturnRazas()
        {
            // Arrange
            var raza = new Razas
            {
                Nombre = "Test Raza List",
                EspeciesID = 1,
                IsDeleted = false
            };
            await _service.InsertAsync(raza);

            // Act
            var razas = await _service.ListAsync(r => !r.IsDeleted);

            // Assert
            Assert.NotNull(razas);
            Assert.Contains(razas, r => r.Nombre == "Test Raza List");
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateRaza()
        {
            // Arrange
            var raza = new Razas
            {
                Nombre = "Update Raza",
                EspeciesID = 1,
                IsDeleted = false
            };
            await _service.InsertAsync(raza);
            raza.Nombre = "Updated Raza";

            // Act
            var result = await _service.UpdateAsync(raza);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldSoftDeleteRaza()
        {
            // Arrange
            var raza = new Razas
            {
                Nombre = "Delete Raza",
                EspeciesID = 1,
                IsDeleted = false
            };
            await _service.InsertAsync(raza);

            // Act
            var result = await _service.DeleteAsync(raza.RazasID);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task SearchByIdAsync_ShouldReturnRaza()
        {
            // Arrange
            var raza = new Razas
            {
                Nombre = "Search Raza",
                EspeciesID = 1,
                IsDeleted = false
            };
            await _service.InsertAsync(raza);

            // Act
            var result = await _service.SearchByIdAsync(raza.RazasID);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Search Raza", result.Nombre);
        }

        [Fact]
        public async Task ExistAsync_ShouldReturnTrue_WhenRazaExists()
        {
            // Arrange
            var raza = new Razas
            {
                Nombre = "Exist Raza",
                EspeciesID = 1,
                IsDeleted = false
            };
            await _service.InsertAsync(raza);

            // Act
            var exists = await _service.ExistAsync(raza.RazasID);

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public async Task ExistAsync_ShouldReturnFalse_WhenRazaDoesNotExist()
        {
            // Act
            var exists = await _service.ExistAsync(999);

            // Assert
            Assert.False(exists);
        }

        [Fact]
        public async Task SaveAsync_ShouldInsert_WhenRazaIsNew()
        {
            // Arrange
            var raza = new Razas
            {
                RazasID = 0,
                Nombre = "Save New Raza",
                EspeciesID = 1,
                IsDeleted = false
            };

            // Act
            var result = await _service.SaveAsync(raza);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task SaveAsync_ShouldUpdate_WhenRazaExists()
        {
            // Arrange
            var raza = new Razas
            {
                Nombre = "Save Update Raza",
                EspeciesID = 1,
                IsDeleted = false
            };
            await _service.InsertAsync(raza);
            raza.Nombre = "Updated via Save";

            // Act
            var result = await _service.SaveAsync(raza);

            // Assert
            Assert.True(result);
        }
    }
}