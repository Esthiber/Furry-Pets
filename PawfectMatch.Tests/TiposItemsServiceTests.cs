using Xunit;
using System.Threading.Tasks;
using PawfectMatch.Services;
using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models.POS;

namespace PawfectMatch.Tests
{
    public class TiposItemsServiceTests
    {
        private readonly TiposItemsService _service;
        //private readonly ApplicationDbContext _context;

        public TiposItemsServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: $"TiposItemsServiceTestDb_{Guid.NewGuid()}")
                .Options;
            var factory = new TestDbContextFactory(options);
            _service = new TiposItemsService(factory);
        }

        [Fact]
        public async Task InsertAsync_ShouldAddTipoItem()
        {
            // Arrange
            var tipoItem = new TiposItems
            {
                Nombre = "Test Tipo Item",
                IsDeleted = false
            };

            // Act
            var result = await _service.InsertAsync(tipoItem);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ListAsync_ShouldReturnTiposItems()
        {
            // Arrange
            var tipoItem = new TiposItems
            {
                Nombre = "Test Tipo Item List",
                IsDeleted = false
            };
            await _service.InsertAsync(tipoItem);

            // Act
            var tiposItems = await _service.ListAsync(t => !t.IsDeleted);

            // Assert
            Assert.NotNull(tiposItems);
            Assert.Contains(tiposItems, t => t.Nombre == "Test Tipo Item List");
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateTipoItem()
        {
            // Arrange
            var tipoItem = new TiposItems
            {
                Nombre = "Update Tipo Item",
                IsDeleted = false
            };
            await _service.InsertAsync(tipoItem);
            tipoItem.Nombre = "Updated Tipo Item";

            // Act
            var result = await _service.UpdateAsync(tipoItem);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldSoftDeleteTipoItem()
        {
            // Arrange
            var tipoItem = new TiposItems
            {
                Nombre = "Delete Tipo Item",
                IsDeleted = false
            };
            await _service.InsertAsync(tipoItem);

            // Act
            var result = await _service.DeleteAsync(tipoItem.TiposItemsID);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task SearchByIdAsync_ShouldReturnTipoItem()
        {
            // Arrange
            var tipoItem = new TiposItems
            {
                Nombre = "Search Tipo Item",
                IsDeleted = false
            };
            await _service.InsertAsync(tipoItem);

            // Act
            var result = await _service.SearchByIdAsync(tipoItem.TiposItemsID);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Search Tipo Item", result.Nombre);
        }

        [Fact]
        public async Task ExistAsync_ShouldReturnTrue_WhenTipoItemExists()
        {
            // Arrange
            var tipoItem = new TiposItems
            {
                Nombre = "Exist Tipo Item",
                IsDeleted = false
            };
            await _service.InsertAsync(tipoItem);

            // Act
            var exists = await _service.ExistAsync(tipoItem.TiposItemsID);

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public async Task ExistAsync_ShouldReturnFalse_WhenTipoItemDoesNotExist()
        {
            // Act
            var exists = await _service.ExistAsync(999);

            // Assert
            Assert.False(exists);
        }

        [Fact]
        public async Task SaveAsync_ShouldInsert_WhenTipoItemIsNew()
        {
            // Arrange
            var tipoItem = new TiposItems
            {
                TiposItemsID = 0,
                Nombre = "Save New Tipo Item",
                IsDeleted = false
            };

            // Act
            var result = await _service.SaveAsync(tipoItem);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task SaveAsync_ShouldUpdate_WhenTipoItemExists()
        {
            // Arrange
            var tipoItem = new TiposItems
            {
                Nombre = "Save Update Tipo Item",
                IsDeleted = false
            };
            await _service.InsertAsync(tipoItem);
            tipoItem.Nombre = "Updated via Save";

            // Act
            var result = await _service.SaveAsync(tipoItem);

            // Assert
            Assert.True(result);
        }
    }
}