using Xunit;
using System.Threading.Tasks;
using PawfectMatch.Services;
using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models;

namespace PawfectMatch.Tests
{
    public class ProveedoresServiceTests
    {
        private readonly ProveedoresService _service;
        //private readonly ApplicationDbContextApplicationDbContext _context;

        public ProveedoresServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: $"ProveedoresServiceTestDb_{Guid.NewGuid()}")
                .Options;
            var factory = new TestDbContextFactory(options);
            _service = new ProveedoresService(factory);
        }

        [Fact]
        public async Task InsertAsync_ShouldAddProveedor()
        {
            // Arrange
            var proveedor = new Proveedores
            {
                Nombre = "Test Proveedor",
                Telefono = "123456789",
                Email = "test@proveedor.com",
                IsDeleted = false
            };

            // Act
            var result = await _service.InsertAsync(proveedor);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ListAsync_ShouldReturnProveedores()
        {
            // Arrange
            var proveedor = new Proveedores
            {
                Nombre = "Test Proveedor List",
                Telefono = "123456789",
                Email = "list@proveedor.com",

                IsDeleted = false
            };
            await _service.InsertAsync(proveedor);

            // Act
            var proveedores = await _service.ListAsync(p => !p.IsDeleted);

            // Assert
            Assert.NotNull(proveedores);
            Assert.Contains(proveedores, p => p.Nombre == "Test Proveedor List");
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateProveedor()
        {
            // Arrange
            var proveedor = new Proveedores
            {
                Nombre = "Update Proveedor",
                Telefono = "123456789",
                Email = "update@proveedor.com",

                IsDeleted = false
            };
            await _service.InsertAsync(proveedor);
            proveedor.Nombre = "Updated Proveedor";

            // Act
            var result = await _service.UpdateAsync(proveedor);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldSoftDeleteProveedor()
        {
            // Arrange
            var proveedor = new Proveedores
            {
                Nombre = "Delete Proveedor",
                Telefono = "123456789",
                Email = "delete@proveedor.com",

                IsDeleted = false
            };
            await _service.InsertAsync(proveedor);

            // Act
            var result = await _service.DeleteAsync(proveedor.ProveedoresID);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task SearchByIdAsync_ShouldReturnProveedor()
        {
            // Arrange
            var proveedor = new Proveedores
            {
                Nombre = "Search Proveedor",
                Telefono = "123456789",
                Email = "search@proveedor.com",

                IsDeleted = false
            };
            await _service.InsertAsync(proveedor);

            // Act
            var result = await _service.SearchByIdAsync(proveedor.ProveedoresID);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Search Proveedor", result.Nombre);
        }

        [Fact]
        public async Task ExistAsync_ShouldReturnTrue_WhenProveedorExists()
        {
            // Arrange
            var proveedor = new Proveedores
            {
                Nombre = "Exist Proveedor",
                Telefono = "123456789",
                Email = "exist@proveedor.com",

                IsDeleted = false
            };
            await _service.InsertAsync(proveedor);

            // Act
            var exists = await _service.ExistAsync(proveedor.ProveedoresID);

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public async Task ExistAsync_ShouldReturnFalse_WhenProveedorDoesNotExist()
        {
            // Act
            var exists = await _service.ExistAsync(999);

            // Assert
            Assert.False(exists);
        }

        [Fact]
        public async Task SaveAsync_ShouldInsert_WhenProveedorIsNew()
        {
            // Arrange
            var proveedor = new Proveedores
            {
                ProveedoresID = 0,
                Nombre = "Save New Proveedor",
                Telefono = "123456789",
                Email = "save@proveedor.com",

                IsDeleted = false
            };

            // Act
            var result = await _service.SaveAsync(proveedor);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task SaveAsync_ShouldUpdate_WhenProveedorExists()
        {
            // Arrange
            var proveedor = new Proveedores
            {
                Nombre = "Save Update Proveedor",
                Telefono = "123456789",
                Email = "saveupdate@proveedor.com",

                IsDeleted = false
            };
            await _service.InsertAsync(proveedor);
            proveedor.Nombre = "Updated via Save";

            // Act
            var result = await _service.SaveAsync(proveedor);

            // Assert
            Assert.True(result);
        }
    }
}