using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;
using PawfectMatch.Models;
using PawfectMatch.Models._Mascotas;
using PawfectMatch.Models._Servicios;
using PawfectMatch.Models.Adopciones._Servicios;
using PawfectMatch.Models.Adopciones._Solicitudes;
using PawfectMatch.Models.Servicios;
using PawfectMatch.Services;
using PawfectMatch.Services._Mascotas;
using PawfectMatch.Services._Solicitudes;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace PawfectMatch.Tests.Services
{
    public class SolicitudesServiciosServiceTests : ICommonTests
    {
        [Fact]
        public async Task DeleteAsync()
        {
            var factory = CrearDbFactory();
            var SolicitudesService = new SolicitudesAdopcionesService(factory);
            var mascotaService = new MascotasService(factory);
            var adoptanteService = new AdoptantesService(factory);
            var estadoService = new EstadosSolicitudesService(factory);
            var ServiciosService = new ServiciosService(factory);
            var solicitudServiciosService = new SolicitudesServiciosService(factory);


            var categoria = new Categorias { Nombre = "Perro" };
            var raza = new Razas { Nombre = "Labrador", Categoria = categoria };
            var relacionSize = new RelacionSizes { Size = "Mediano" };
            var estadoMascota = new Estados { Nombre = "Disponible" };
            var sexo = new Sexos { Nombre = "Macho" };

            var dataServicios = new List<Servicios>() {
                new(){
                    Nombre="",Descripcion="",Costo=0
                }
            };

            var dataSolicitudes = new List<SolicitudesAdopciones>()
            {
                new()
                {
                    SolicitudAdopcionId = 1,
                    MascotaId = 1,
                    AdoptanteId = 1,
                    EstadoSolicitudId = 1,
                Fecha = DateTime.Now
                }
            };

            var estado = new EstadoSolicitudes { Nombre = "Pendiente" };
            await estadoService.InsertAsync(estado);

            using (var ctx = await factory.CreateDbContextAsync())
            {
                ctx.Servicios.AddRange(dataServicios);
                ctx.SaveChanges();

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

            var servicioSolicitud = new SolicitudesServicios()
            {
                SolicitudAdopcionId = 1,
                ServicioId = 1
            };
            var result = await solicitudServiciosService.InsertAsync(servicioSolicitud);
            Assert.True(result);

        }

        public async Task ExistAsync()
        {
            throw new NotImplementedException();
        }

        public async Task InsertAsync()
        {
            throw new NotImplementedException();
        }

        public async Task ListAsync()
        {
            throw new NotImplementedException();
        }

        public async Task SaveAsync()
        {
            throw new NotImplementedException();
        }

        public async Task SearchByIdAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync()
        {
            throw new NotImplementedException();
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
