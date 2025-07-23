using Xunit;
using System.Threading.Tasks;
using PawfectMatch.Services;
using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models;
using System.Linq;
using System.Collections.Generic;

namespace PawfectMatch.Tests
{
    public class ConfiguracionEmpresaServiceTests
    {
        private readonly ConfiguracionEmpresaService _service;
        private readonly ApplicationDbContext _context;

        public ConfiguracionEmpresaServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: $"ConfiguracionEmpresaServiceTestDb_{System.Guid.NewGuid()}")
                .Options;
            var factory = new TestDbContextFactory(options);
            _service = new ConfiguracionEmpresaService(factory);
            _context = factory.CreateDbContext();
        }

        [Fact]
        public async Task InsertAsync_ShouldAddConfiguracionEmpresa()
        {
            var config = new ConfiguracionEmpresa
            {
                Nombre = "Empresa Test",
                Telefono = "123456789",
                RNC = "123456789",
                Direccion = "Direccion Test"
            };
            var result = await _service.InsertAsync(config);
            Assert.True(result);
            Assert.True(_context.ConfiguracionEmpresa.Any(e => e.Nombre == "Empresa Test"));
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateConfiguracionEmpresa()
        {
            var config = new ConfiguracionEmpresa
            {
                Nombre = "Empresa Update",
                Telefono = "987654321",
                RNC = "987654321",
                Direccion = "Direccion Update"
            };
            await _service.InsertAsync(config);
            var inserted = _context.ConfiguracionEmpresa.First();
            inserted.Nombre = "Empresa Updated";
            var result = await _service.UpdateAsync(inserted);
            Assert.True(result);
            var updated = _context.ConfiguracionEmpresa.Find(inserted.EmpresaID);
            Assert.Equal("Empresa Updated", updated.Nombre);
        }

        [Fact]
        public async Task SaveAsync_ShouldInsertOrUpdate()
        {
            var config = new ConfiguracionEmpresa
            {
                Nombre = "Empresa Save",
                Telefono = "111222333",
                RNC = "111222333",
                Direccion = "Direccion Save"
            };
            // Insert
            var resultInsert = await _service.SaveAsync(config);
            Assert.True(resultInsert);
            var inserted = _context.ConfiguracionEmpresa.FirstOrDefault(e => e.Nombre == "Empresa Save");
            Assert.NotNull(inserted);
            // Update
            inserted.Nombre = "Empresa Save Updated";
            var resultUpdate = await _service.SaveAsync(inserted);
            Assert.True(resultUpdate);
            var updated = _context.ConfiguracionEmpresa.Find(inserted.EmpresaID);
            Assert.Equal("Empresa Save Updated", updated.Nombre);
        }

        [Fact]
        public async Task ListAsync_ShouldReturnFilteredList()
        {
            _context.ConfiguracionEmpresa.Add(new ConfiguracionEmpresa { Nombre = "Empresa1", Telefono = "1", RNC = "1", Direccion = "Dir1" });
            _context.ConfiguracionEmpresa.Add(new ConfiguracionEmpresa { Nombre = "Empresa2", Telefono = "2", RNC = "2", Direccion = "Dir2" });
            await _context.SaveChangesAsync();
            var list = await _service.ListAsync(e => e.Nombre.Contains("Empresa"));
            Assert.NotNull(list);
            Assert.True(list.Count >= 2);
        }

        [Fact]
        public async Task SearchByIdAsync_ShouldReturnByIdOrNew()
        {
            var config = new ConfiguracionEmpresa { Nombre = "EmpresaSearch", Telefono = "333", RNC = "333", Direccion = "DirSearch" };
            await _service.InsertAsync(config);
            var inserted = _context.ConfiguracionEmpresa.First(e => e.Nombre == "EmpresaSearch");
            var found = await _service.SearchByIdAsync(inserted.EmpresaID);
            Assert.NotNull(found);
            Assert.Equal("EmpresaSearch", found.Nombre);
            var notFound = await _service.SearchByIdAsync(9999);
            Assert.NotNull(notFound);
        }

        [Fact]
        public async Task ExistAsync_ShouldReturnTrueOrFalse()
        {
            var config = new ConfiguracionEmpresa { Nombre = "EmpresaExist", Telefono = "444", RNC = "444", Direccion = "DirExist" };
            await _service.InsertAsync(config);
            var inserted = _context.ConfiguracionEmpresa.First(e => e.Nombre == "EmpresaExist");
            var exists = await _service.ExistAsync(inserted.EmpresaID);
            Assert.True(exists);
            var notExists = await _service.ExistAsync(9999);
            Assert.False(notExists);
        }

        [Fact]
        public async Task DeleteAsync_ShouldHardDelete()
        {
            var config = new ConfiguracionEmpresa { Nombre = "EmpresaDelete", Telefono = "555", RNC = "555", Direccion = "DirDelete" };
            await _service.InsertAsync(config);
            var inserted = _context.ConfiguracionEmpresa.First(e => e.Nombre == "EmpresaDelete");
            var result = await _service.DeleteAsync(inserted.EmpresaID);
            Assert.True(result);
            Assert.False(_context.ConfiguracionEmpresa.Any(e => e.EmpresaID == inserted.EmpresaID));
        }
    }
}
