using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Services._Mascotas;
using PawfectMatch.Services._Solicitudes;
using PawfectMatch.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PawfectMatch.Models._Mascotas;

namespace PawfectMatch.Tests
{
    public class MascotasServiceTests : ICommonTests
    {
        [Fact]
        public async Task DeleteAsync()
        {
            var factory = CrearDbFactory();
            var mascotaService = new MascotasService(factory);
            var adoptanteService = new AdoptantesService(factory);
            var estadoService = new EstadosSolicitudesService(factory);
            // Insertar entidades requeridas para Mascota
            var categoria = new Categorias { Nombre = "Perro" };
            var raza = new Razas { Nombre = "Labrador", Categoria = categoria };
            var relacionSize = new RelacionSizes { Size = "Mediano" };
            var estadoMascota = new Estados { Nombre = "Disponible" };
            var sexo = new Sexos { Nombre = "Macho" };

            // Contexto para agregar entidades relacionadas
            using (var ctx = await factory.CreateDbContextAsync())
            {
                ctx.Categorias.Add(categoria);
                ctx.RelacionSizes.Add(relacionSize);
                ctx.Estados.Add(estadoMascota);
                ctx.Sexos.Add(sexo);
                ctx.SaveChanges();

                raza.CategoriaId = categoria.CategoriaId;
                ctx.Razas.Add(raza);
                ctx.SaveChanges();
            }

            var mascota = new Mascotas
            {
                Nombre = "Felix",
                Descripcion = "Una mascota",
                CategoriaId = categoria.CategoriaId,
                RazaId = raza.RazaId,
                RelacionSizeId = relacionSize.RelacionSizeId,
                EstadoId = estadoMascota.EstadoId,
                SexoId = sexo.SexoId,
                Tamano = 12.5,
                FechaNacimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(-1)),
                FotoUrl = "https://ejemplo.com/felix.jpg"
            };
            await mascotaService.InsertAsync(mascota);

            Assert.True(await mascotaService.DeleteAsync(mascota.MascotaId));
        }

        [Fact]
        public async Task ExistAsync()
        {
            var factory = CrearDbFactory();
            var mascotaService = new MascotasService(factory);
            var adoptanteService = new AdoptantesService(factory);
            var estadoService = new EstadosSolicitudesService(factory);
            // Insertar entidades requeridas para Mascota
            var categoria = new Categorias { Nombre = "Perro" };
            var raza = new Razas { Nombre = "Labrador", Categoria = categoria };
            var relacionSize = new RelacionSizes { Size = "Mediano" };
            var estadoMascota = new Estados { Nombre = "Disponible" };
            var sexo = new Sexos { Nombre = "Macho" };

            // Contexto para agregar entidades relacionadas
            using (var ctx = await factory.CreateDbContextAsync())
            {
                ctx.Categorias.Add(categoria);
                ctx.RelacionSizes.Add(relacionSize);
                ctx.Estados.Add(estadoMascota);
                ctx.Sexos.Add(sexo);
                ctx.SaveChanges();

                raza.CategoriaId = categoria.CategoriaId;
                ctx.Razas.Add(raza);
                ctx.SaveChanges();
            }

            var mascota = new Mascotas
            {
                Nombre = "Felix",
                Descripcion = "Una mascota",
                CategoriaId = categoria.CategoriaId,
                RazaId = raza.RazaId,
                RelacionSizeId = relacionSize.RelacionSizeId,
                EstadoId = estadoMascota.EstadoId,
                SexoId = sexo.SexoId,
                Tamano = 12.5,
                FechaNacimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(-1)),
                FotoUrl = "https://ejemplo.com/felix.jpg"
            };
            await mascotaService.InsertAsync(mascota);
            Assert.True(await mascotaService.ExistAsync(mascota.MascotaId));
        }

        [Fact]
        public async Task InsertAsync()
        {
            var factory = CrearDbFactory();
            var mascotaService = new MascotasService(factory);
            var adoptanteService = new AdoptantesService(factory);
            var estadoService = new EstadosSolicitudesService(factory);
            // Insertar entidades requeridas para Mascota
            var categoria = new Categorias { Nombre = "Perro" };
            var raza = new Razas { Nombre = "Labrador", Categoria = categoria };
            var relacionSize = new RelacionSizes { Size = "Mediano" };
            var estadoMascota = new Estados { Nombre = "Disponible" };
            var sexo = new Sexos { Nombre = "Macho" };

            // Contexto para agregar entidades relacionadas
            using (var ctx = await factory.CreateDbContextAsync())
            {
                ctx.Categorias.Add(categoria);
                ctx.RelacionSizes.Add(relacionSize);
                ctx.Estados.Add(estadoMascota);
                ctx.Sexos.Add(sexo);
                ctx.SaveChanges();

                raza.CategoriaId = categoria.CategoriaId;
                ctx.Razas.Add(raza);
                ctx.SaveChanges();
            }

            var mascota = new Mascotas
            {
                Nombre = "Felix",
                Descripcion = "Una mascota",
                CategoriaId = categoria.CategoriaId,
                RazaId = raza.RazaId,
                RelacionSizeId = relacionSize.RelacionSizeId,
                EstadoId = estadoMascota.EstadoId,
                SexoId = sexo.SexoId,
                Tamano = 12.5,
                FechaNacimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(-1)),
                FotoUrl = "https://ejemplo.com/felix.jpg"
            };
            var resultado = await mascotaService.InsertAsync(mascota);

            Assert.True(resultado);
        }

        [Fact]
        public async Task ListAsync()
        {
            var factory = CrearDbFactory();
            var mascotaService = new MascotasService(factory);
            var adoptanteService = new AdoptantesService(factory);
            var estadoService = new EstadosSolicitudesService(factory);
            // Insertar entidades requeridas para Mascota
            var categoria = new Categorias { Nombre = "Perro" };
            var raza = new Razas { Nombre = "Labrador", Categoria = categoria };
            var relacionSize = new RelacionSizes { Size = "Mediano" };
            var estadoMascota = new Estados { Nombre = "Disponible" };
            var sexo = new Sexos { Nombre = "Macho" };

            // Contexto para agregar entidades relacionadas
            using (var ctx = await factory.CreateDbContextAsync())
            {
                ctx.Categorias.Add(categoria);
                ctx.RelacionSizes.Add(relacionSize);
                ctx.Estados.Add(estadoMascota);
                ctx.Sexos.Add(sexo);
                ctx.SaveChanges();

                raza.CategoriaId = categoria.CategoriaId;
                ctx.Razas.Add(raza);
                ctx.SaveChanges();
            }

            var mascota = new Mascotas
            {
                Nombre = "Felix",
                Descripcion = "Una mascota",
                CategoriaId = categoria.CategoriaId,
                RazaId = raza.RazaId,
                RelacionSizeId = relacionSize.RelacionSizeId,
                EstadoId = estadoMascota.EstadoId,
                SexoId = sexo.SexoId,
                Tamano = 12.5,
                FechaNacimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(-1)),
                FotoUrl = "https://ejemplo.com/felix.jpg"
            };
            await mascotaService.InsertAsync(mascota);

            var lista = await mascotaService.ListAsync(m => true);
            Assert.Contains(lista, a => a.MascotaId == mascota.MascotaId);
        }

        [Fact]
        public async Task SaveAsync()
        {
            var factory = CrearDbFactory();
            var mascotaService = new MascotasService(factory);
            var adoptanteService = new AdoptantesService(factory);
            var estadoService = new EstadosSolicitudesService(factory);
            // Insertar entidades requeridas para Mascota
            var categoria = new Categorias { Nombre = "Perro" };
            var raza = new Razas { Nombre = "Labrador", Categoria = categoria };
            var relacionSize = new RelacionSizes { Size = "Mediano" };
            var estadoMascota = new Estados { Nombre = "Disponible" };
            var sexo = new Sexos { Nombre = "Macho" };

            // Contexto para agregar entidades relacionadas
            using (var ctx = await factory.CreateDbContextAsync())
            {
                ctx.Categorias.Add(categoria);
                ctx.RelacionSizes.Add(relacionSize);
                ctx.Estados.Add(estadoMascota);
                ctx.Sexos.Add(sexo);
                ctx.SaveChanges();

                raza.CategoriaId = categoria.CategoriaId;
                ctx.Razas.Add(raza);
                ctx.SaveChanges();
            }

            var mascota = new Mascotas
            {
                Nombre = "Felix",
                Descripcion = "Una mascota",
                CategoriaId = categoria.CategoriaId,
                RazaId = raza.RazaId,
                RelacionSizeId = relacionSize.RelacionSizeId,
                EstadoId = estadoMascota.EstadoId,
                SexoId = sexo.SexoId,
                Tamano = 12.5,
                FechaNacimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(-1)),
                FotoUrl = "https://ejemplo.com/felix.jpg"
            };
            await mascotaService.InsertAsync(mascota);

            var result = await mascotaService.SaveAsync(mascota);

            Assert.True(result);
        }
        
        [Fact]
        public async Task SearchByIdAsync()
        {
            var factory = CrearDbFactory();
            var mascotaService = new MascotasService(factory);
            var adoptanteService = new AdoptantesService(factory);
            var estadoService = new EstadosSolicitudesService(factory);
            // Insertar entidades requeridas para Mascota
            var categoria = new Categorias { Nombre = "Perro" };
            var raza = new Razas { Nombre = "Labrador", Categoria = categoria };
            var relacionSize = new RelacionSizes { Size = "Mediano" };
            var estadoMascota = new Estados { Nombre = "Disponible" };
            var sexo = new Sexos { Nombre = "Macho" };

            // Contexto para agregar entidades relacionadas
            using (var ctx = await factory.CreateDbContextAsync())
            {
                ctx.Categorias.Add(categoria);
                ctx.RelacionSizes.Add(relacionSize);
                ctx.Estados.Add(estadoMascota);
                ctx.Sexos.Add(sexo);
                ctx.SaveChanges();

                raza.CategoriaId = categoria.CategoriaId;
                ctx.Razas.Add(raza);
                ctx.SaveChanges();
            }

            var mascota = new Mascotas
            {
                Nombre = "Felix",
                Descripcion = "Una mascota",
                CategoriaId = categoria.CategoriaId,
                RazaId = raza.RazaId,
                RelacionSizeId = relacionSize.RelacionSizeId,
                EstadoId = estadoMascota.EstadoId,
                SexoId = sexo.SexoId,
                Tamano = 12.5,
                FechaNacimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(-1)),
                FotoUrl = "https://ejemplo.com/felix.jpg"
            };
            await mascotaService.InsertAsync(mascota);

            var encontrada = await mascotaService.SearchByIdAsync(mascota.MascotaId);

            Assert.Equal(mascota.MascotaId, encontrada.MascotaId);  
        }
        
        [Fact]
        public async Task UpdateAsync()
        {
            var factory = CrearDbFactory();
            var mascotaService = new MascotasService(factory);
            var adoptanteService = new AdoptantesService(factory);
            var estadoService = new EstadosSolicitudesService(factory);
            // Insertar entidades requeridas para Mascota
            var categoria = new Categorias { Nombre = "Perro" };
            var raza = new Razas { Nombre = "Labrador", Categoria = categoria };
            var relacionSize = new RelacionSizes { Size = "Mediano" };
            var estadoMascota = new Estados { Nombre = "Disponible" };
            var sexo = new Sexos { Nombre = "Macho" };

            // Contexto para agregar entidades relacionadas
            using (var ctx = await factory.CreateDbContextAsync())
            {
                ctx.Categorias.Add(categoria);
                ctx.RelacionSizes.Add(relacionSize);
                ctx.Estados.Add(estadoMascota);
                ctx.Sexos.Add(sexo);
                ctx.SaveChanges();

                raza.CategoriaId = categoria.CategoriaId;
                ctx.Razas.Add(raza);
                ctx.SaveChanges();
            }

            var mascota = new Mascotas
            {
                Nombre = "Felix",
                Descripcion = "Una mascota",
                CategoriaId = categoria.CategoriaId,
                RazaId = raza.RazaId,
                RelacionSizeId = relacionSize.RelacionSizeId,
                EstadoId = estadoMascota.EstadoId,
                SexoId = sexo.SexoId,
                Tamano = 12.5,
                FechaNacimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(-1)),
                FotoUrl = "https://ejemplo.com/felix.jpg"
            };
            await mascotaService.InsertAsync(mascota);

            mascota.Nombre = "Francisco";

            var result = await mascotaService.UpdateAsync(mascota);

            Assert.True(result);
        }

        private IDbContextFactory<ApplicationDbContext> CrearDbFactory()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(connection)
                .Options;

            // Crear una instancia inicial para ejecutar EnsureCreated
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

            // Asegúrate de cerrar la conexión al finalizar las pruebas si usas IDisposable
            // public void Dispose() => _connection.Dispose();
        }
    }
}
