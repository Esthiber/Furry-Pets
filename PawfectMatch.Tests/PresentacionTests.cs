using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models._Presentacion;
using PawfectMatch.Services._Presentacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawfectMatch.Tests
{
    public class PresentacionTests : ICommonTests
    {
        [Fact]
        public async Task DeleteAsync()
        {
            // Arrange
            var factory = CrearDbFactory();
            var service = new PresentacionesService(factory);

            var presentacion = new Presentaciones
            {
                Titulo = "ToBeDeleted",
                FechaCreacion = DateTime.UtcNow,
                EsActiva = true
            };

            var diapositiva = new Diapositivas
            {
                ImageUrl = "/images/delete.jpg",
                Orden = 1
            };

            await service.InsertAsync(presentacion, new List<Diapositivas> { diapositiva });

            // Act
            var deleted = await service.DeleteAsync(presentacion.PresentacionId);

            // Assert
            Assert.True(deleted);

            await using var ctx = await factory.CreateDbContextAsync();
            var presentacionDb = await ctx.Presentaciones.FindAsync(presentacion.PresentacionId);
            var relaciones = await ctx.PresentacionesDiapositivas
                .Where(r => r.PresentacionId == presentacion.PresentacionId)
                .ToListAsync();

            Assert.Null(presentacionDb);
            Assert.Empty(relaciones);
        }

        [Fact]
        public async Task ExistAsync()
        {
            // Arrange
            var factory = CrearDbFactory();
            var service = new PresentacionesService(factory);

            var presentacion = new Presentaciones
            {
                Titulo = "Verificacion Existencia",
                FechaCreacion = DateTime.UtcNow,
                EsActiva = true
            };

            await service.InsertAsync(presentacion, new List<Diapositivas>
    {
        new Diapositivas { ImageUrl = "/img/exist.jpg", Orden = 1 }
    });

            // Act
            var exists = await service.ExistAsync(presentacion.PresentacionId);

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public async Task InsertAsync()
        {
            // Arrange
            var factory = CrearDbFactory();
            var service = new PresentacionesService(factory);

            var presentacion = new Presentaciones
            {
                Titulo = "HomeDisplay",
                FechaCreacion = DateTime.UtcNow,
                EsActiva = true
            };

            List<Diapositivas> diapositivas = new()
            {
                new Diapositivas
                {
                    IsTituloLeftActive = true,
                    Titulo_Left = "Título 1",
                    SubTitulo_Left = "Subtítulo 1",
                    ImageUrl = "/images/img1.jpg",
                    Orden = 1
                },
                new Diapositivas
                {
                    IsTituloRightActive = true,
                    Titulo_Right = "Título 2",
                    SubTitulo_Right = "Subtítulo 2",
                    ImageUrl = "/images/img2.jpg",
                    Orden = 2
                },
                new Diapositivas
                {
                    IsButtonLeftActive = true,
                    TextButton_Left = "Click aquí",
                    LinkButton_Left = "/link",
                    ImageUrl = "/images/img3.jpg",
                    Orden = 3
                }
            };

            // Act
            var result = await service.InsertAsync(presentacion, diapositivas);

            // Assert
            Assert.True(result);

            await using var context = await factory.CreateDbContextAsync();

            var presentacionDb = await context.Presentaciones
                .Include(p => p.PresentacionesDiapositivas)
                .ThenInclude(pd => pd.Diapositiva)
                .FirstOrDefaultAsync(p => p.PresentacionId == presentacion.PresentacionId);

            Assert.NotNull(presentacionDb);
            Assert.Equal("HomeDisplay", presentacionDb.Titulo);
            Assert.Equal(3, presentacionDb.PresentacionesDiapositivas.Count);
            Assert.Equal("/link", presentacionDb.PresentacionesDiapositivas.ToList()[2].Diapositiva.LinkButton_Left);
        }

        [Fact]
        public async Task ListAsync()
        {
            // Arrange
            var factory = CrearDbFactory();
            var service = new PresentacionesService(factory);

            var presentacion1 = new Presentaciones { Titulo = "Home", FechaCreacion = DateTime.UtcNow, EsActiva = true };
            var presentacion2 = new Presentaciones { Titulo = "About", FechaCreacion = DateTime.UtcNow, EsActiva = false };

            var diapositiva = new Diapositivas
            {
                ImageUrl = "/images/slide.jpg",
                Orden = 1
            };
            var diapositiva2 = new Diapositivas
            {
                ImageUrl = "/images/slide.jpg",
                Orden = 1
            };

            await service.InsertAsync(presentacion1, new List<Diapositivas> { diapositiva });
            await service.InsertAsync(presentacion2, new List<Diapositivas> { diapositiva2 });

            // Act
            var result = await service.ListAsync(p => p.EsActiva);

            // Assert
            Assert.Single(result);
            Assert.Equal("Home", result[0].Titulo);
            Assert.NotEmpty(result[0].PresentacionesDiapositivas);
        }

        [Fact]
        public async Task SaveAsync()
        {
            var factory = CrearDbFactory();
            var service = new PresentacionesService(factory);

            var presentacion = new Presentaciones
            {
                Titulo = "Nueva Presentación",
                FechaCreacion = DateTime.UtcNow,
                EsActiva = true
            };

            var diapositivas = new List<Diapositivas>
    {
        new Diapositivas { ImageUrl = "/img/new1.jpg", Orden = 1 },
        new Diapositivas { ImageUrl = "/img/new2.jpg", Orden = 2 }
    };

            var result = await service.SaveAsync(presentacion, diapositivas);

            Assert.True(result);
            Assert.True(presentacion.PresentacionId > 0);
        }

        [Fact]
        public async Task SaveAsync_UpdatesExistingPresentation()
        {
            var factory = CrearDbFactory();
            var service = new PresentacionesService(factory);

            var presentacion = new Presentaciones
            {
                Titulo = "Original",
                FechaCreacion = DateTime.UtcNow,
                EsActiva = true
            };

            await service.InsertAsync(presentacion, new List<Diapositivas>
            {
                new Diapositivas { ImageUrl = "/img/original.jpg", Orden = 1 }
            });

            // Cambiar valores
            presentacion.Titulo = "Actualizada";

            var nuevasDiapositivas = new List<Diapositivas>
            {
                new Diapositivas { ImageUrl = "/img/update1.jpg", Orden = 1 },
                new Diapositivas { ImageUrl = "/img/update2.jpg", Orden = 2 }
            };

            var result = await service.SaveAsync(presentacion, nuevasDiapositivas);

            Assert.True(result);
            var actualizado = await service.SearchByIdAsync(presentacion.PresentacionId);
            Assert.Equal("Actualizada", actualizado.Titulo);
            Assert.Equal(2, actualizado.PresentacionesDiapositivas.Count);
        }

        [Fact]
        public async Task SearchByIdAsync()
        {
            // Arrange
            var factory = CrearDbFactory();
            var service = new PresentacionesService(factory);

            var presentacion = new Presentaciones
            {
                Titulo = "Busqueda",
                FechaCreacion = DateTime.UtcNow,
                EsActiva = true
            };

            var diapositivas = new List<Diapositivas>
            {
              new Diapositivas
              {
                 ImageUrl = "/images/search.jpg",
                 Orden = 1
              }
            };

            await service.InsertAsync(presentacion, diapositivas);

            // Act
            var result = await service.SearchByIdAsync(presentacion.PresentacionId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Busqueda", result.Titulo);
            Assert.NotEmpty(result.PresentacionesDiapositivas);
            Assert.Equal("/images/search.jpg", result.PresentacionesDiapositivas.First().Diapositiva.ImageUrl);
        }

        [Fact]
        public async Task ExistAsync_ReturnsFalseIfNotExists()
        {
            // Arrange
            var factory = CrearDbFactory();
            var service = new PresentacionesService(factory);

            // Act
            var exists = await service.ExistAsync(999); // ID que no existe

            // Assert
            Assert.False(exists);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            // Arrange
            var factory = CrearDbFactory();
            var service = new PresentacionesService(factory);

            var presentacion = new Presentaciones
            {
                Titulo = "Antes",
                FechaCreacion = DateTime.UtcNow,
                EsActiva = true
            };

            var iniciales = new List<Diapositivas>
            {
                new Diapositivas { ImageUrl = "/img/old1.jpg", Orden = 1 },
                new Diapositivas { ImageUrl = "/img/old2.jpg", Orden = 2 }
            };

            await service.InsertAsync(presentacion, iniciales);

            // Act - modificar y actualizar
            presentacion.Titulo = "Después";
            presentacion.EsActiva = false;

            var nuevas = new List<Diapositivas>
            {
                new Diapositivas { ImageUrl = "/img/new1.jpg", Orden = 1 },
                new Diapositivas { ImageUrl = "/img/new2.jpg", Orden = 2 },
                new Diapositivas { ImageUrl = "/img/new3.jpg", Orden = 3 }
            };

            var actualizado = await service.UpdateAsync(presentacion, nuevas);

            // Assert
            Assert.True(actualizado);

            var result = await service.SearchByIdAsync(presentacion.PresentacionId);
            Assert.Equal("Después", result.Titulo);
            Assert.Equal(3, result.PresentacionesDiapositivas.Count);
            Assert.All(result.PresentacionesDiapositivas, pd => Assert.StartsWith("/img/new", pd.Diapositiva.ImageUrl));
        }

        [Fact]
        public async Task SetActive_OnlyOnePresentationRemainsActive()
        {
            // Arrange
            var factory = CrearDbFactory();
            var service = new PresentacionesService(factory);

            var presentacion1 = new Presentaciones { Titulo = "P1", EsActiva = true };
            var presentacion2 = new Presentaciones { Titulo = "P2", EsActiva = false };
            var presentacion3 = new Presentaciones { Titulo = "P3", EsActiva = true };

            // Act: Add presentations to the context and save changes
            var ctx = await factory.CreateDbContextAsync();
            ctx.Presentaciones.AddRange(presentacion1, presentacion2, presentacion3);
            await ctx.SaveChangesAsync();

            // Act: Set the second presentation as active
            await service.SetActive(presentacion2.PresentacionId);
            //presentacion2.EsActiva = true;
            //ctx.Presentaciones.Update(presentacion2);
            //await ctx.SaveChangesAsync();


            // Assert: Verify the state of each presentation
            var all = await service.ListAsync(p => true);

            Assert.True(all.First(p => p.Titulo == "P2").EsActiva, "Presentation P2 should be active.");
            Assert.False(all.First(p => p.Titulo == "P1").EsActiva, "Presentation P1 should be inactive.");
            Assert.False(all.First(p => p.Titulo == "P3").EsActiva, "Presentation P3 should be inactive.");
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
