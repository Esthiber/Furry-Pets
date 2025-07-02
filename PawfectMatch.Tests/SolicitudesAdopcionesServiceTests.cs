using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models;
using PawfectMatch.Models._Mascotas;
using PawfectMatch.Models._Solicitudes;
using PawfectMatch.Services;
using PawfectMatch.Services._Mascotas;
using PawfectMatch.Services._Solicitudes;
using Xunit;

namespace PawfectMatch.Tests
{
    public class SolicitudesAdopcionesServiceTests : ICommonTests // Clase con los metodos CRUD Base
    {
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

        [Fact]
        public async Task InsertAsync()
        {
            var factory = CrearDbFactory();
            var mascotaService = new MascotasService(factory);
            var adoptanteService = new AdoptantesService(factory);
            var estadoService = new EstadosSolicitudesService(factory);
            var service = new SolicitudesAdopcionesService(factory);

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

            // Insertar usuario adoptante y entidad adoptante
            var usuario = new ApplicationUser { Id = Guid.NewGuid().ToString(), UserName = "usuario_prueba" };
            using (var ctx = await factory.CreateDbContextAsync())
            {
                ctx.Users.Add(usuario);
                ctx.SaveChanges();
            }

            var adoptante = new Adoptantes
            {
                Nombre = "Carlos Pérez",
                Ocupacion = "Ingeniero",
                UsuarioId = usuario.Id
            };
            await adoptanteService.InsertAsync(adoptante);

            // Insertar estado de solicitud
            var estado = new EstadoSolicitudes { Nombre = "Pendiente" };
            await estadoService.InsertAsync(estado);

            var solicitud = new SolicitudesAdopciones
            {
                MascotaId = mascota.MascotaId,
                AdoptanteId = adoptante.AdoptanteId,
                EstadoSolicitudId = estado.EstadoSolicitudId,
                Fecha = DateTime.Now
            };

            // Act
            var resultado = await service.InsertAsync(solicitud);

            // Assert
            Assert.True(resultado);
        }

        [Fact]
        public async Task UpdateAsync()
        {
            var factory = CrearDbFactory();
            var mascotaService = new MascotasService(factory);
            var adoptanteService = new AdoptantesService(factory);
            var estadoService = new EstadosSolicitudesService(factory);
            var service = new SolicitudesAdopcionesService(factory);

            // Crear entidades relacionadas
            var categoria = new Categorias { Nombre = "Gato" };
            var raza = new Razas { Nombre = "Siames", Categoria = categoria };
            var relacionSize = new RelacionSizes { Size = "Pequeño" };
            var estadoMascota = new Estados { Nombre = "Activo" };
            var sexo = new Sexos { Nombre = "Hembra" };

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

            var mascota1 = new Mascotas
            {
                Nombre = "Luna",
                Descripcion = "Gatita cariñosa",
                CategoriaId = categoria.CategoriaId,
                RazaId = raza.RazaId,
                RelacionSizeId = relacionSize.RelacionSizeId,
                EstadoId = estadoMascota.EstadoId,
                SexoId = sexo.SexoId,
                Tamano = 10.0,
                FechaNacimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(-2)),
                FotoUrl = "https://ejemplo.com/luna.jpg"
            };
            await mascotaService.InsertAsync(mascota1);

            var mascota2 = new Mascotas
            {
                Nombre = "Toby",
                Descripcion = "Perro juguetón",
                CategoriaId = categoria.CategoriaId,
                RazaId = raza.RazaId,
                RelacionSizeId = relacionSize.RelacionSizeId,
                EstadoId = estadoMascota.EstadoId,
                SexoId = sexo.SexoId,
                Tamano = 15.0,
                FechaNacimiento = DateOnly.FromDateTime(DateTime.Now.AddYears(-3)),
                FotoUrl = "https://ejemplo.com/toby.jpg"
            };
            await mascotaService.InsertAsync(mascota2);

            // Crear usuario y adoptante
            var usuario = new ApplicationUser { Id = Guid.NewGuid().ToString(), UserName = "adoptante_update" };
            using (var ctx = await factory.CreateDbContextAsync())
            {
                ctx.Users.Add(usuario);
                ctx.SaveChanges();
            }

            var adoptante = new Adoptantes
            {
                Nombre = "Ana López",
                Ocupacion = "Doctora",
                UsuarioId = usuario.Id
            };
            await adoptanteService.InsertAsync(adoptante);

            // Crear estado de solicitud
            var estadoSolicitud = new EstadoSolicitudes { Nombre = "Pendiente" };
            await estadoService.InsertAsync(estadoSolicitud);

            // Insertar solicitud original
            var solicitud = new SolicitudesAdopciones
            {
                MascotaId = mascota1.MascotaId,
                AdoptanteId = adoptante.AdoptanteId,
                EstadoSolicitudId = estadoSolicitud.EstadoSolicitudId,
                Fecha = DateTime.Now
            };
            await service.InsertAsync(solicitud);

            // Act: actualizar solicitud cambiando la mascota
            solicitud.MascotaId = mascota2.MascotaId;
            var actualizada = await service.UpdateAsync(solicitud);

            // Assert
            Assert.True(actualizada);

            // Verificar que la solicitud fue actualizada en la base de datos
            var ctxFinal = await factory.CreateDbContextAsync();
            var solicitudActualizada = ctxFinal.SolicitudesAdopciones.FirstOrDefault(s => s.SolicitudAdopcionId == solicitud.SolicitudAdopcionId);
            Assert.NotNull(solicitudActualizada);
            Assert.Equal(mascota2.MascotaId, solicitudActualizada!.MascotaId);
        }

        [Fact]
        public async Task SearchByIdAsync()
        {
            var factory = CrearDbFactory();
            var mascotaService = new MascotasService(factory);
            var adoptanteService = new AdoptantesService(factory);
            var estadoService = new EstadosSolicitudesService(factory);
            var service = new SolicitudesAdopcionesService(factory);

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

            // Insertar usuario adoptante y entidad adoptante
            var usuario = new ApplicationUser { Id = Guid.NewGuid().ToString(), UserName = "usuario_prueba" };
            using (var ctx = await factory.CreateDbContextAsync())
            {
                ctx.Users.Add(usuario);
                ctx.SaveChanges();
            }

            var adoptante = new Adoptantes
            {
                Nombre = "Carlos Pérez",
                Ocupacion = "Ingeniero",
                UsuarioId = usuario.Id
            };
            await adoptanteService.InsertAsync(adoptante);

            // Insertar estado de solicitud
            var estado = new EstadoSolicitudes { Nombre = "Pendiente" };
            await estadoService.InsertAsync(estado);

            var solicitud = new SolicitudesAdopciones
            {
                MascotaId = mascota.MascotaId,
                AdoptanteId = adoptante.AdoptanteId,
                EstadoSolicitudId = estado.EstadoSolicitudId,
                Fecha = DateTime.Now
            };

            // Act
            var resultado = await service.InsertAsync(solicitud);

            var encontrada = await service.SearchByIdAsync(solicitud.SolicitudAdopcionId);
            Assert.Equal(solicitud.SolicitudAdopcionId, encontrada.SolicitudAdopcionId);
        }

        [Fact]
        public async Task ExistAsync()
        {
            var factory = CrearDbFactory();
            var mascotaService = new MascotasService(factory);
            var adoptanteService = new AdoptantesService(factory);
            var estadoService = new EstadosSolicitudesService(factory);
            var service = new SolicitudesAdopcionesService(factory);

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

            // Insertar usuario adoptante y entidad adoptante
            var usuario = new ApplicationUser { Id = Guid.NewGuid().ToString(), UserName = "usuario_prueba" };
            using (var ctx = await factory.CreateDbContextAsync())
            {
                ctx.Users.Add(usuario);
                ctx.SaveChanges();
            }

            var adoptante = new Adoptantes
            {
                Nombre = "Carlos Pérez",
                Ocupacion = "Ingeniero",
                UsuarioId = usuario.Id
            };
            await adoptanteService.InsertAsync(adoptante);

            // Insertar estado de solicitud
            var estado = new EstadoSolicitudes { Nombre = "Pendiente" };
            await estadoService.InsertAsync(estado);

            var solicitud = new SolicitudesAdopciones
            {
                MascotaId = mascota.MascotaId,
                AdoptanteId = adoptante.AdoptanteId,
                EstadoSolicitudId = estado.EstadoSolicitudId,
                Fecha = DateTime.Now
            };

            // Act
            var resultado = await service.InsertAsync(solicitud);

            Assert.True(await service.ExistAsync(solicitud.SolicitudAdopcionId));
        }

        [Fact]
        public async Task ListAsync()
        {
            var factory = CrearDbFactory();
            var mascotaService = new MascotasService(factory);
            var adoptanteService = new AdoptantesService(factory);
            var estadoService = new EstadosSolicitudesService(factory);
            var service = new SolicitudesAdopcionesService(factory);

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

            // Insertar usuario adoptante y entidad adoptante
            var usuario = new ApplicationUser { Id = Guid.NewGuid().ToString(), UserName = "usuario_prueba" };
            using (var ctx = await factory.CreateDbContextAsync())
            {
                ctx.Users.Add(usuario);
                ctx.SaveChanges();
            }

            var adoptante = new Adoptantes
            {
                Nombre = "Carlos Pérez",
                Ocupacion = "Ingeniero",
                UsuarioId = usuario.Id
            };
            await adoptanteService.InsertAsync(adoptante);

            // Insertar estado de solicitud
            var estado = new EstadoSolicitudes { Nombre = "Pendiente" };
            await estadoService.InsertAsync(estado);

            var solicitud = new SolicitudesAdopciones
            {
                MascotaId = mascota.MascotaId,
                AdoptanteId = adoptante.AdoptanteId,
                EstadoSolicitudId = estado.EstadoSolicitudId,
                Fecha = DateTime.Now
            };

            // Act
            var resultado = await service.InsertAsync(solicitud);

            var lista = await service.ListAsync(s => true);
            Assert.Contains(lista, s => s.SolicitudAdopcionId == solicitud.SolicitudAdopcionId);
        }

        [Fact]
        public async Task SaveAsync()
        {
            var factory = CrearDbFactory();
            var mascotaService = new MascotasService(factory);
            var adoptanteService = new AdoptantesService(factory);
            var estadoService = new EstadosSolicitudesService(factory);
            var service = new SolicitudesAdopcionesService(factory);

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

            // Insertar usuario adoptante y entidad adoptante
            var usuario = new ApplicationUser { Id = Guid.NewGuid().ToString(), UserName = "usuario_prueba" };
            using (var ctx = await factory.CreateDbContextAsync())
            {
                ctx.Users.Add(usuario);
                ctx.SaveChanges();
            }

            var adoptante = new Adoptantes
            {
                Nombre = "Carlos Pérez",
                Ocupacion = "Ingeniero",
                UsuarioId = usuario.Id
            };
            await adoptanteService.InsertAsync(adoptante);

            // Insertar estado de solicitud
            var estado = new EstadoSolicitudes { Nombre = "Pendiente" };
            await estadoService.InsertAsync(estado);

            var solicitud = new SolicitudesAdopciones
            {
                MascotaId = mascota.MascotaId,
                AdoptanteId = adoptante.AdoptanteId,
                EstadoSolicitudId = estado.EstadoSolicitudId,
                Fecha = DateTime.Now
            };

            // Act
            var resultado = await service.InsertAsync(solicitud);

            Assert.True(await service.SaveAsync(solicitud));
        }

        [Fact]
        public async Task DeleteAsync()
        {
            var factory = CrearDbFactory();
            var mascotaService = new MascotasService(factory);
            var adoptanteService = new AdoptantesService(factory);
            var estadoService = new EstadosSolicitudesService(factory);
            var service = new SolicitudesAdopcionesService(factory);

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

            // Insertar usuario adoptante y entidad adoptante
            var usuario = new ApplicationUser { Id = Guid.NewGuid().ToString(), UserName = "usuario_prueba" };
            using (var ctx = await factory.CreateDbContextAsync())
            {
                ctx.Users.Add(usuario);
                ctx.SaveChanges();
            }

            var adoptante = new Adoptantes
            {
                Nombre = "Carlos Pérez",
                Ocupacion = "Ingeniero",
                UsuarioId = usuario.Id
            };
            await adoptanteService.InsertAsync(adoptante);

            // Insertar estado de solicitud
            var estado = new EstadoSolicitudes { Nombre = "Pendiente" };
            await estadoService.InsertAsync(estado);

            var solicitud = new SolicitudesAdopciones
            {
                MascotaId = mascota.MascotaId,
                AdoptanteId = adoptante.AdoptanteId,
                EstadoSolicitudId = estado.EstadoSolicitudId,
                Fecha = DateTime.Now
            };

            // Act
            var resultado = await service.InsertAsync(solicitud);

            var deleted = await service.DeleteAsync(solicitud.SolicitudAdopcionId);
            Assert.True(deleted);
        }

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
