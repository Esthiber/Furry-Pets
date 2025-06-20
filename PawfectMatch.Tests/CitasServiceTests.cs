using PawfectMatch.Models;
using PawfectMatch.Models._Mascotas;
using PawfectMatch.Services._Mascotas;
using PawfectMatch.Services._Solicitudes;
using PawfectMatch.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PawfectMatch.Data;

namespace PawfectMatch.Tests
{
    public class CitasServiceTests : ICommonTests
    {
        public async Task DeleteAsync()
        {
            var factory = CrearDbFactory();
            var mascotaService = new MascotasService(factory);
            var adoptanteService = new AdoptantesService(factory);
            var estadoService = new EstadosSolicitudesService(factory);
            var citasService = new CitasService(factory);

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

            var cita = new Citas();
            cita.AdoptanteId = adoptante.AdoptanteId;
            cita.MascotaId = mascota.MascotaId;
            var result = await citasService.InsertAsync(cita);
            bool r = await citasService.DeleteAsync(cita.CitaId);
            Assert.True(r);
        }

        public async Task ExistAsync()
        {
            var factory = CrearDbFactory();
            var mascotaService = new MascotasService(factory);
            var adoptanteService = new AdoptantesService(factory);
            var estadoService = new EstadosSolicitudesService(factory);
            var citasService = new CitasService(factory);

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

            var cita = new Citas();
            cita.AdoptanteId = adoptante.AdoptanteId;
            cita.MascotaId = mascota.MascotaId;
            var result = await citasService.InsertAsync(cita);

            bool r = await citasService.ExistAsync(cita.CitaId);
            Assert.True(r);
        }

        public async Task InsertAsync()
        {
            var factory = CrearDbFactory();
            var mascotaService = new MascotasService(factory);
            var adoptanteService = new AdoptantesService(factory);
            var estadoService = new EstadosSolicitudesService(factory);
            var citasService = new CitasService(factory);

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

            var cita = new Citas();
            cita.AdoptanteId = adoptante.AdoptanteId;
            cita.MascotaId = mascota.MascotaId;
            var result = await citasService.InsertAsync(cita);

            Assert.True(result);
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
