using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PawfectMatch.Models;
using System.Reflection.Emit;
using PawfectMatch.Models.Adopciones._Presentacion;
using PawfectMatch.Models.Adopciones._Mascotas;
using PawfectMatch.Models.Adopciones._Solicitudes;
using PawfectMatch.Models.Adopciones._Servicios;
using PawfectMatch.Models.Adopciones;
using PawfectMatch.Models.Adopciones._Adoptantes;

namespace PawfectMatch.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    #region DbSets
    public DbSet<Citas> Citas { get; set; }

    public DbSet<HistorialAdopciones> HistorialAdopciones { get; set; }

    public DbSet<SolicitudesAdopciones> SolicitudesAdopciones { get; set; }

    public DbSet<EstadoSolicitudes> EstadoSolicitudes { get; set; }

    public DbSet<Adoptantes> Adoptantes { get; set; }

    public DbSet<Mascotas> Mascotas { get; set; }

    public DbSet<Sexos> Sexos { get; set; }

    public DbSet<Razas> Razas { get; set; }

    public DbSet<Categorias> Categorias { get; set; }

    public DbSet<RelacionSizes> RelacionSizes { get; set; }

    public DbSet<Estados> Estados { get; set; }

    public DbSet<Sugerencias> Sugerencias { get; set; }

    public DbSet<Presentaciones> Presentaciones { get; set; }
    public DbSet<PresentacionesDiapositivas> PresentacionesDiapositivas { get; set; }
    public DbSet<Diapositivas> Diapositivas { get; set; }

    public DbSet<Servicios> Servicios { get; set; }
    public DbSet<SolicitudesServicios> SolicitudesServicios { get; set; }
    public DbSet<Productos> Productos { get; set; }
    public DbSet<Carrito> Carrito { get; set; }
    public DbSet<Proveedores> Proveedores { get; set; }
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        #region Configuraciones

        modelBuilder.Entity<Mascotas>()
            .HasOne(m => m.Categoria)
            .WithMany()
            .HasForeignKey(m => m.CategoriaId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Mascotas>()
            .HasOne(m => m.RelacionSize)
            .WithMany()
            .HasForeignKey(m => m.RelacionSizeId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Mascotas>()
            .HasOne(m => m.Estado)
            .WithMany()
            .HasForeignKey(m => m.EstadoId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<SolicitudesAdopciones>()
            .HasOne(s => s.Mascota)
            .WithMany()
            .HasForeignKey(s => s.MascotaId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<HistorialAdopciones>()
            .HasOne(h => h.Mascota)
            .WithMany()
            .HasForeignKey(h => h.MascotaId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Citas>()
            .HasOne(c => c.Mascota)
            .WithMany()
            .HasForeignKey(c => c.MascotaId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Citas>()
            .HasOne(c => c.Adoptante)
            .WithMany()
            .HasForeignKey(c => c.AdoptanteId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<SolicitudesAdopciones>()
            .HasOne(s => s.Adoptante)
            .WithMany()
            .HasForeignKey(s => s.AdoptanteId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<SolicitudesAdopciones>()
            .HasOne(s => s.EstadoSolicitud)
            .WithMany()
            .HasForeignKey(s => s.EstadoSolicitudId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<HistorialAdopciones>()
            .HasOne(h => h.Adoptante)
            .WithMany()
            .HasForeignKey(h => h.AdoptanteId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<SolicitudesServicios>()
            .HasKey(ss => ss.SolicitudServicioId);

        modelBuilder.Entity<SolicitudesServicios>()
            .HasOne(ss => ss.SolicitudAdopcion)
            .WithMany(s => s.SolicitudesServicios)
            .HasForeignKey(ss => ss.SolicitudAdopcionId)
            .OnDelete(DeleteBehavior.Cascade);
        #endregion

        #region Initial Seed
        modelBuilder.Entity<Categorias>().HasData(
            new Categorias { CategoriaId = 1, Nombre = "Perros" },
            new Categorias { CategoriaId = 2, Nombre = "Gatos" }
        );

        modelBuilder.Entity<Razas>().HasData(
            new Razas { RazaId = 1, CategoriaId = 1, Nombre = "Labrador" },
            new Razas { RazaId = 2, CategoriaId = 1, Nombre = "Bulldog" },
            new Razas { RazaId = 3, CategoriaId = 2, Nombre = "Persa" },
            new Razas { RazaId = 4, CategoriaId = 2, Nombre = "Siamés" },
            new Razas { RazaId = 5, CategoriaId = 1, Nombre = "Golden Retriever" },
            new Razas { RazaId = 6, CategoriaId = 1, Nombre = "Chihuahua" },
            new Razas { RazaId = 7, CategoriaId = 1, Nombre = "Dachshund" },
            new Razas { RazaId = 8, CategoriaId = 1, Nombre = "Cocker Spaniel" },
            new Razas { RazaId = 9, CategoriaId = 1, Nombre = "Shih Tzu" },
            new Razas { RazaId = 10, CategoriaId = 2, Nombre = "Bengalí" },
            new Razas { RazaId = 11, CategoriaId = 2, Nombre = "Maine Coon" },
            new Razas { RazaId = 12, CategoriaId = 2, Nombre = "Esfinge" },
            new Razas { RazaId = 13, CategoriaId = 2, Nombre = "Azul Ruso" }

        );

        modelBuilder.Entity<RelacionSizes>().HasData(
            new RelacionSizes { RelacionSizeId = 1, Size = "Pequeño" },
            new RelacionSizes { RelacionSizeId = 2, Size = "Mediano" },
            new RelacionSizes { RelacionSizeId = 3, Size = "Grande" }
        );

        modelBuilder.Entity<Estados>().HasData(
            new Estados { EstadoId = 1, Nombre = "Disponible" },
            new Estados { EstadoId = 2, Nombre = "Adoptado" },
            new Estados { EstadoId = 3, Nombre = "No Disponible" }// Osea muerto
        );

        modelBuilder.Entity<Sexos>().HasData(
            new Sexos { SexoId = 1, Nombre = "Macho" },
            new Sexos { SexoId = 2, Nombre = "Hembra" }
        );

        modelBuilder.Entity<EstadoSolicitudes>().HasData(
            new EstadoSolicitudes { EstadoSolicitudId = 1, Nombre = "Pendiente" },
            new EstadoSolicitudes { EstadoSolicitudId = 2, Nombre = "Aprobada" },
            new EstadoSolicitudes { EstadoSolicitudId = 3, Nombre = "Rechazada" }
        );

        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole { Id = "2", Name = "User", NormalizedName = "USER" }
        );

        modelBuilder.Entity<Presentaciones>().HasData(
            new Presentaciones
            {
                PresentacionId = 1,
                Titulo = "Default",
                EsActiva = true,
                Descripcion = "Esta es la presentacion default. No la borre por favor.",
                FechaCreacion = new DateTime(2025, 1, 1)
            }
        );

        modelBuilder.Entity<Diapositivas>().HasData(
            new Diapositivas
            {
                DiapositivaId = 1,
                IsTituloLeftActive = true,
                Titulo_Left = "Encuentra a tu compañero perfecto",
                SubTitulo_Left = "Conectamos mascotas que necesitan un hogar con familias amorosas",

                IsButtonLeftActive = true,
                TextButton_Left = "Explorar Mascotas",
                LinkButton_Left = Urls.Mascotas.Index,

                ImageUrl = "https://images-ext-1.discordapp.net/external/pCPvCTsMyhwvSzSxaPZZ5PG8VfVshz1DMFeTU4VQBFo/https/images8.alphacoders.com/449/thumb-1920-449501.jpg?format=webp&width=1374&height=859",
                Orden = 1
            },
            new Diapositivas
            {
                DiapositivaId = 2,
                IsTituloLeftActive = true,
                Titulo_Left = "Adopta, no compres",
                SubTitulo_Left = "Dale una segunda oportunidad a quienes más lo necesitan",

                IsButtonLeftActive = true,
                TextButton_Left = "Ver Historias",
                LinkButton_Left = "#",

                ImageUrl = "https://c.wallhere.com/photos/27/00/1920x1200_px_dog-1643539.jpg!d",
                Orden = 2
            },
            new Diapositivas
            {
                DiapositivaId = 3,
                IsTituloLeftActive = false,
                IsTituloRightActive = true,
                Titulo_Right = "Historias con final feliz",
                SubTitulo_Right = "Conoce cómo nuestras mascotas encontraron un hogar lleno de amor",

                IsButtonRightActive = true,
                TextButton_Right = "Leer historias",
                LinkButton_Right = "#testimonials-section",

                ImageUrl = "https://images.unsplash.com/photo-1514888286974-6c03e2ca1dba?fm=jpg&q=60&w=3000&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8Y2F0fGVufDB8fDB8fHww",
                Orden = 3
            }

        );

        modelBuilder.Entity<PresentacionesDiapositivas>().HasData(
            new PresentacionesDiapositivas()
            {
                PresentacionDiapositivaId = 1,
                PresentacionId = 1,
                DiapositivaId = 1,
                Orden = 1
            },
             new PresentacionesDiapositivas()
             {
                 PresentacionDiapositivaId = 2,
                 PresentacionId = 1,
                 DiapositivaId = 2,
                 Orden = 2
             },
              new PresentacionesDiapositivas()
              {
                  PresentacionDiapositivaId = 3,
                  PresentacionId = 1,
                  DiapositivaId = 3,
                  Orden = 3
              }
        );

        modelBuilder.Entity<Servicios>().HasData(
            new Servicios
            {
                ServicioId = 1,
                Nombre = "Documentos de Adopción",
                Descripcion = "Documentos que certifican la propiedad de la mascota.",
                Costo = 0.00
            },
           new Servicios
           {
               ServicioId = 2,
               Nombre = "Vacunación",
               Descripcion = "Servicio de vacunación para mascotas.",
               Costo = 500.00
           },
           new Servicios
           {
               ServicioId = 3,
               Nombre = "Desparasitación",
               Descripcion = "Tratamiento para eliminar parásitos internos y externos.",
               Costo = 300.00
           },
           new Servicios
           {
               ServicioId = 4,
               Nombre = "Corte de Uñas",
               Descripcion = "Servicio de corte de uñas para mascotas pequeñas y grandes.",
               Costo = 150.00
           },
           new Servicios
           {
               ServicioId = 5,
               Nombre = "Consulta Veterinaria",
               Descripcion = "Consulta general con un veterinario calificado.",
               Costo = 800.00
           }
        );

        modelBuilder.Entity<Mascotas>().HasData(
        new Mascotas()
        {
            MascotaId = 1,
            CategoriaId = 1,
            RazaId = 5,
            RelacionSizeId = 1,
            EstadoId = 1,
            Nombre = "Felipe",
            Tamano = 25,
            FechaNacimiento = DateOnly.Parse("2025-04-02"),
            Descripcion = "Es grande es bonito es un perro loco.",
            FotoUrl = "https://media.istockphoto.com/id/1465311007/es/foto/un-perro-peque%C3%B1o-sonr%C3%ADe-al-due%C3%B1o-peque%C3%B1as-mordeduras-de-mascotas-peligroso-terrier-de-juguete.jpg?s=612x612&w=0&k=20&c=nZzhW0piLl7oIrielT9JA7vAcXqaepnDhCggxD7JZ0I=",
            SexoId = 1
        },
        new Mascotas()
        {
            MascotaId = 2,
            CategoriaId = 1,
            RazaId = 1,
            RelacionSizeId = 3,
            EstadoId = 2,
            Nombre = "Milo",
            Tamano = 18,
            FechaNacimiento = DateOnly.Parse("2022-08-08"),
            Descripcion = "Es un perrito muy cariñoso y con mucha energia",
            FotoUrl = "https://th.bing.com/th/id/OIP.TfniUPx7NqEggeKV9APOZgHaE8?rs=1&pid=ImgDetMain",
            SexoId = 1
        },
        new Mascotas()
        {
            MascotaId = 3,
            CategoriaId = 1,
            RazaId = 2,
            RelacionSizeId = 1,
            EstadoId = 2,
            Nombre = "Roky",
            Tamano = 16,
            FechaNacimiento = DateOnly.Parse("2021-06-08"),
            Descripcion = "Es un perrito que adora pasear y comer",
            FotoUrl = "https://http2.mlstatic.com/D_NQ_NP_660656-MLM46803871812_072021-F.jpg",
            SexoId = 1
        },
        new Mascotas()
        {
            MascotaId = 4,
            CategoriaId = 1,
            RazaId = 5,
            RelacionSizeId = 3,
            EstadoId = 1,
            Nombre = "Willi",
            Tamano = 24,
            FechaNacimiento = DateOnly.Parse("2024-03-08"),
            Descripcion = "Es un perro jugueton y le gusta dormir",
            FotoUrl = "https://th.bing.com/th/id/R.69b9a203e86486a2114bc7380d204970?rik=kimEIahYLupgjQ&pid=ImgRaw&r=0&sres=1&sresct=1",
            SexoId = 1
        },
        new Mascotas()
        {
            MascotaId = 5,
            CategoriaId = 1,
            RazaId = 6,
            RelacionSizeId = 1,
            EstadoId = 1,
            Nombre = "lali",
            Tamano = 9,
            FechaNacimiento = DateOnly.Parse("2024-01-07"),
            Descripcion = "Es un perrito muy tierno y adora jugar",
            FotoUrl = "https://www.publicdomainpictures.net/pictures/180000/velka/chihuaua-puppy.jpg",
            SexoId = 1
        },
        new Mascotas()
        {
            MascotaId = 6,
            CategoriaId = 1,
            RazaId = 1,
            RelacionSizeId = 3,
            EstadoId = 1,
            Nombre = "Mango",
            Tamano = 16,
            FechaNacimiento = DateOnly.Parse("2024-10-22 "),
            Descripcion = "Es un perrito muy aventurero",
            FotoUrl = "https://th.bing.com/th/id/OIP.NA3_b8GubIn0nfMn0w4XDAHaE8?rs=1&pid=ImgDetMain",
            SexoId = 1
        },
        new Mascotas()
        {
            MascotaId = 7,
            CategoriaId = 1,
            RazaId = 7,
            RelacionSizeId = 1,
            EstadoId = 1,
            Nombre = "rodolf",
            Tamano = 9,
            FechaNacimiento = DateOnly.Parse("2023-11-08"),
            Descripcion = "Le encanta correr y comer",
            FotoUrl = "https://th.bing.com/th/id/OIP.EzwbKs8GJQPv0CCPP2GMlQHaD9?rs=1&pid=ImgDetMain",
            SexoId = 1
        },
        new Mascotas()
        {
            MascotaId = 8,
            CategoriaId = 1,
            RazaId = 8,
            RelacionSizeId = 2,
            EstadoId = 1,
            Nombre = "Oli",
            Tamano = 10,
            FechaNacimiento = DateOnly.Parse("2021-04-06"),
            Descripcion = "Es un perro que adora el cariño y los paseos largos",
            FotoUrl = "https://th.bing.com/th/id/OIP.v3rbPJHS2RVJFSlRq_DXTAHaE8?rs=1&pid=ImgDetMain",
            SexoId = 1
        },
        new Mascotas()
        {
            MascotaId = 9,
            CategoriaId = 1,
            RazaId = 6,
            RelacionSizeId = 1,
            EstadoId = 1,
            Nombre = "Lalo",
            Tamano = 10,
            FechaNacimiento = DateOnly.Parse("2023-04-12"),
            Descripcion = "Es un perro con mucha energia y adora que le den cariño",
            FotoUrl = "https://th.bing.com/th/id/OIP.JuzlhHcoLZR61J50nJgftwHaFr?rs=1&pid=ImgDetMain",
            SexoId = 1
        },
        new Mascotas()
        {
            MascotaId = 10,
            CategoriaId = 1,
            RazaId = 4,
            RelacionSizeId = 2,
            EstadoId = 2,
            Nombre = "Nina",
            Tamano = 14,
            FechaNacimiento = DateOnly.Parse("2022-02-14"),
            Descripcion = "Dócil, juguetona y le encanta estar en brazos.",
            FotoUrl = "https://th.bing.com/th/id/OIP.yRLVHRUwgdR4OiOxTPnNDgHaE7?rs=1&pid=ImgDetMain",
            SexoId = 2
        },
        new Mascotas()
        {
            MascotaId = 11,
            CategoriaId = 1,
            RazaId = 3,
            RelacionSizeId = 1,
            EstadoId = 1,
            Nombre = "Thor",
            Tamano = 20,
            FechaNacimiento = DateOnly.Parse("2020-05-09"),
            Descripcion = "Muy protector y obediente.",
            FotoUrl = "https://th.bing.com/th/id/OIP.qkqCM7eQmJwvj8AkH3u5ngHaE8?rs=1&pid=ImgDetMain",
            SexoId = 1
        },
        new Mascotas()
        {
            MascotaId = 12,
            CategoriaId = 1,
            RazaId = 9,
            RelacionSizeId = 3,
            EstadoId = 1,
            Nombre = "Luna",
            Tamano = 12,
            FechaNacimiento = DateOnly.Parse("2023-09-20"),
            Descripcion = "Le gusta dormir y es muy tranquila.",
            FotoUrl = "https://th.bing.com/th/id/OIP.QcIBbq4tgrIPhU3W0XxvagHaE7?rs=1&pid=ImgDetMain",
            SexoId = 2
        },
         new Mascotas()
         {
             MascotaId = 13,
             CategoriaId = 1,
             RazaId = 2,
             RelacionSizeId = 2,
             EstadoId = 1,
             Nombre = "Zeus",
             Tamano = 22,
             FechaNacimiento = DateOnly.Parse("2022-12-01"),
             Descripcion = "Un perro valiente y sociable.",
             FotoUrl = "https://th.bing.com/th/id/OIP.f3eZqKFb3I53N7QOkz-xaQHaE8?rs=1&pid=ImgDetMain",
             SexoId = 1
         },
          new Mascotas()
          {
              MascotaId = 14,
              CategoriaId = 1,
              RazaId = 5,
              RelacionSizeId = 3,
              EstadoId = 2,
              Nombre = "Mimi",
              Tamano = 11,
              FechaNacimiento = DateOnly.Parse("2021-07-17"),
              Descripcion = "Pequeñita, dulce y siempre feliz.",
              FotoUrl = "https://th.bing.com/th/id/OIP.F5dOGfGKePtNmliuyzYhiAHaE7?rs=1&pid=ImgDetMain",
              SexoId = 2
          },
          new Mascotas()
          {
              MascotaId = 15,
              CategoriaId = 1,
              RazaId = 4,
              RelacionSizeId = 2,
              EstadoId = 1,
              Nombre = "Coco",
              Tamano = 19,
              FechaNacimiento = DateOnly.Parse("2023-01-05"),
              Descripcion = "Curioso, travieso pero muy leal.",
              FotoUrl = "https://th.bing.com/th/id/OIP.7f62UOkFMy3dk9MK-DKrCwHaEK?rs=1&pid=ImgDetMain",
              SexoId = 1
          },
          new Mascotas()
          {
              MascotaId = 16,
              CategoriaId = 1,
              RazaId = 7,
              RelacionSizeId = 1,
              EstadoId = 1,
              Nombre = "Chispa",
              Tamano = 10,
              FechaNacimiento = DateOnly.Parse("2022-03-15"),
              Descripcion = "Tiene una energía inagotable, ama jugar con pelotas.",
              FotoUrl = "https://th.bing.com/th/id/OIP.hKhG7YFi7tvYoXL4o_ArAwHaE7?rs=1&pid=ImgDetMain",
              SexoId = 2
          },
          new Mascotas()
          {
              MascotaId = 17,
              CategoriaId = 1,
              RazaId = 8,
              RelacionSizeId = 2,
              EstadoId = 1,
              Nombre = "Toby",
              Tamano = 18,
              FechaNacimiento = DateOnly.Parse("2021-11-25"),
              Descripcion = "Fiel y tranquilo, le gusta que lo cepillen.",
              FotoUrl = "https://th.bing.com/th/id/OIP.EjRBsp3T_z1sjEdQAlm0eQHaFj?rs=1&pid=ImgDetMain",
              SexoId = 1
          },
          new Mascotas()
          {
              MascotaId = 18,
              CategoriaId = 1,
              RazaId = 9,
              RelacionSizeId = 1,
              EstadoId = 1,
              Nombre = "Kira",
              Tamano = 9,
              FechaNacimiento = DateOnly.Parse("2023-02-10"),
              Descripcion = "Una perrita súper tierna, perfecta para compañía.",
              FotoUrl = "https://th.bing.com/th/id/OIP.ohAyzfrYc6QjFZ3v1nypIAHaE8?rs=1&pid=ImgDetMain",
              SexoId = 2
          },
          new Mascotas()
          {
              MascotaId = 19,
              CategoriaId = 1,
              RazaId = 3,
              RelacionSizeId = 3,
              EstadoId = 2,
              Nombre = "Axel",
              Tamano = 30,
              FechaNacimiento = DateOnly.Parse("2020-07-01"),
              Descripcion = "Gran protector, adiestrado y obediente.",
              FotoUrl = "https://th.bing.com/th/id/OIP.sAfYcLwUDwKXwJKCdWrjWwHaE8?rs=1&pid=ImgDetMain",
              SexoId = 1
          },
          new Mascotas()
          {
              MascotaId = 20,
              CategoriaId = 1,
              RazaId = 6,
              RelacionSizeId = 1,
              EstadoId = 1,
              Nombre = "Lilo",
              Tamano = 6,
              FechaNacimiento = DateOnly.Parse("2024-05-19"),
              Descripcion = "Pequeñito pero con un ladrido potente.",
              FotoUrl = "https://th.bing.com/th/id/OIP.z3Q5VZ8cJ3C07IbEECtkhwHaE7?rs=1&pid=ImgDetMain",
              SexoId = 1
          },
          new Mascotas()
          {
              MascotaId = 21,
              CategoriaId = 1,
              RazaId = 2,
              RelacionSizeId = 2,
              EstadoId = 1,
              Nombre = "Nube",
              Tamano = 17,
              FechaNacimiento = DateOnly.Parse("2023-06-03"),
              Descripcion = "Muy consentida, le encanta dormir en almohadas.",
              FotoUrl = "https://th.bing.com/th/id/OIP.ESAKWAbKk7cyEJOLFnIMrAHaE6?rs=1&pid=ImgDetMain",
              SexoId = 2
          },
          new Mascotas()
          {
              MascotaId = 22,
              CategoriaId = 2,
              RazaId = 10,
              RelacionSizeId = 2,
              EstadoId = 1,
              Nombre = "Mishu",
              Tamano = 12,
              FechaNacimiento = DateOnly.Parse("2023-03-10"),
              Descripcion = "Gato tranquilo, le encanta dormir y ronronear.",
              FotoUrl = "https://th.bing.com/th/id/OIP.cAluVwFq_G_AUtMXrP3KYwHaE8?rs=1&pid=ImgDetMain",
              SexoId = 2
          },
          new Mascotas()
          {
              MascotaId = 23,
              CategoriaId = 2,
              RazaId = 11,
              RelacionSizeId = 2,
              EstadoId = 2,
              Nombre = "Simón",
              Tamano = 11,
              FechaNacimiento = DateOnly.Parse("2021-09-15"),
              Descripcion = "Muy hablador y curioso. Siempre quiere estar contigo.",
              FotoUrl = "https://th.bing.com/th/id/OIP.3Hu0Z6b-DUYZf_A9GlQXuwHaE8?rs=1&pid=ImgDetMain",
              SexoId = 1
          },
          new Mascotas()
          {
              MascotaId = 24,
              CategoriaId = 2,
              RazaId = 12,
              RelacionSizeId = 2,
              EstadoId = 1,
              Nombre = "Nala",
              Tamano = 13,
              FechaNacimiento = DateOnly.Parse("2022-01-22"),
              Descripcion = "Ágil, le encanta explorar todo el lugar.",
              FotoUrl = "https://th.bing.com/th/id/OIP.nQwMG4DyUqVxgo7BunwYyAHaE8?rs=1&pid=ImgDetMain",
              SexoId = 2
          },
          new Mascotas()
          {
              MascotaId = 25,
              CategoriaId = 2,
              RazaId = 13,
              RelacionSizeId = 3,
              EstadoId = 1,
              Nombre = "Ragnar",
              Tamano = 20,
              FechaNacimiento = DateOnly.Parse("2020-06-30"),
              Descripcion = "Gran tamaño y corazón. Muy tranquilo.",
              FotoUrl = "https://th.bing.com/th/id/OIP.Qo9h1u8q1zB2UrdkMhvCBAHaEK?rs=1&pid=ImgDetMain",
              SexoId = 1
          });

        #endregion
    }
}