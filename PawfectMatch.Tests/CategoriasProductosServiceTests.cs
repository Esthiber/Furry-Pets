using Xunit;
using System.Threading.Tasks;
using PawfectMatch.Services;
using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;

namespace PawfectMatch.Tests
{
    public class CategoriasProductosServiceTests
    {
        private readonly CategoriasProductosService _service;
        private readonly ApplicationDbContext _context;

        public CategoriasProductosServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "PersonasServiceTestDb")
                .Options;
            var factory = new TestDbContextFactory(options);
            _service = new CategoriasProductosService(factory);
        }

        [Fact]
        public async Task InsertAsync_ShouldAddCategoria()
        {
            var categoria = new PawfectMatch.Models.POS.CategoriasProductos
            {
                Nombre = "Test Categoria"
            };
            var result = await _service.InsertAsync(categoria);
            Assert.True(result);
        }

        [Fact]
        public async Task ListAsync_ShouldReturnCategorias()
        {
            var categorias = await _service.ListAsync(c => !c.IsDeleted);
            Assert.NotNull(categorias);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateCategoria()
        {
            var categoria = new PawfectMatch.Models.POS.CategoriasProductos
            {
                Nombre = "Update Categoria"
            };
            await _service.InsertAsync(categoria);
            categoria.Nombre = "Updated";
            var result = await _service.UpdateAsync(categoria);
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldSoftDeleteCategoria()
        {
            var categoria = new PawfectMatch.Models.POS.CategoriasProductos
            {
                Nombre = "Delete Categoria"
            };
            await _service.InsertAsync(categoria);
            var result = await _service.DeleteAsync(categoria.CategoriasProductosID);
            Assert.True(result);
        }
    }
}
