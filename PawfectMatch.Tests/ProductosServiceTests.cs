using Xunit;
using System.Threading.Tasks;
using PawfectMatch.Services;
using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;

namespace PawfectMatch.Tests
{
    public class ProductosServiceTests
    {
        private readonly ProductosService _service;
        private readonly ApplicationDbContext _context;

        public ProductosServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "PersonasServiceTestDb")
                .Options;
            var factory = new TestDbContextFactory(options);
            _service = new ProductosService(factory);
        }

        [Fact]
        public async Task InsertAsync_ShouldAddProducto()
        {
            var producto = new PawfectMatch.Models.POS.Productos
            {
                Nombre = "Test Producto",
                Precio = 10,
                Costo = 5,
                Stock = 10,
                CategoriasProductosID = 1,
                ProveedoresID = 1
            };
            var result = await _service.InsertAsync(producto);
            Assert.True(result);
        }

        [Fact]
        public async Task ListAsync_ShouldReturnProductos()
        {
            var productos = await _service.ListAsync(p => !p.IsDeleted);
            Assert.NotNull(productos);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateProducto()
        {
            var producto = new PawfectMatch.Models.POS.Productos
            {
                Nombre = "Update Producto",
                Precio = 10,
                Costo = 5,
                Stock = 10,
                CategoriasProductosID = 1,
                ProveedoresID = 1
            };
            await _service.InsertAsync(producto);
            producto.Nombre = "Updated";
            var result = await _service.UpdateAsync(producto);
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldSoftDeleteProducto()
        {
            var producto = new PawfectMatch.Models.POS.Productos
            {
                Nombre = "Delete Producto",
                Precio = 10,
                Costo = 5,
                Stock = 10,
                CategoriasProductosID = 1,
                ProveedoresID = 1
            };
            await _service.InsertAsync(producto);
            var result = await _service.DeleteAsync(producto.ProductosID);
            Assert.True(result);
        }
    }
}
