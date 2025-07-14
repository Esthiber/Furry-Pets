using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PawfectMatch.Models;
using PawfectMatch.Models.Adopciones;
using PawfectMatch.Models.Adopciones._Presentacion;

namespace PawfectMatch.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    #region DbSets
    // Adopciones y Seguimiento
    public DbSet<SolicitudesAdopciones> SolicitudesAdopciones { get; set; }
    public DbSet<EstadoSolicitud> EstadoSolicitud { get; set; }
    public DbSet<HistorialAdopciones> HistorialAdopciones { get; set; }
    public DbSet<MascotasAdopcion> MascotasAdopcion { get; set; }
    public DbSet<SeguimientoMascotas> SeguimientoMascotas { get; set; }

    // Mascotas y Veterinaria
    public DbSet<EstadoMascota> EstadoMascota { get; set; }
    public DbSet<Especies> Especies { get; set; }
    public DbSet<Razas> Razas { get; set; }
    public DbSet<RelacionSize> RelacionSize { get; set; }
    public DbSet<Estados> Estados { get; set; }
    public DbSet<MascotasPersonas> MascotasPersonas { get; set; }
    public DbSet<HistoriasClinicas> HistoriasClinicas { get; set; }
    public DbSet<Servicios> Servicios { get; set; }
    public DbSet<TiposServicios> TiposServicios { get; set; }

    // Facturaci�n
    public DbSet<Facturas> Facturas { get; set; }
    public DbSet<EstadosPagos> EstadosPagos { get; set; }
    public DbSet<Pagos> Pagos { get; set; }
    public DbSet<DetallesFacturas> DetalleFacturas { get; set; }
    public DbSet<TiposItems> TiposItems { get; set; }

    // Personas y Adopcion
    public DbSet<PersonasRoles> PersonasRoles { get; set; }
    public DbSet<Personas> Personas { get; set; }
    public DbSet<AdoptantesDetalles> AdoptantesDetalles { get; set; }
    public DbSet<TipoViviendas> TipoViviendas { get; set; }

    // Productos / Inventario
    public DbSet<Productos> Productos { get; set; }
    public DbSet<CategoriasProductos> CategoriasProductos { get; set; }
    public DbSet<Proveedores> Proveedores { get; set; }

    // Citas
    public DbSet<Citas> Citas { get; set; }
    public DbSet<EstadosCitas> EstadosCitas { get; set; }

    // Configuraci�n
    public DbSet<ConfiguracionEmpresa> ConfiguracionEmpresa { get; set; }

    // Presentaciones
    public DbSet<Presentaciones> Presentaciones { get; set; }
    public DbSet<Diapositivas> Diapositivas { get; set; }
    public DbSet<PresentacionesDiapositivas> PresentacionesDiapositivas { get; set; }

    // Sugerencias
    public DbSet<Sugerencias> Sugerencias { get; set; }
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        #region Configuraciones

        // Soft Delete Query Filter Example
        modelBuilder.Entity<Estados>()
            .HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<MascotasAdopcion>()
            .HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Especies>()
            .HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Razas>()
            .HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<RelacionSize>()
            .HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<EstadoMascota>()
            .HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<SeguimientoMascotas>()
            .HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Facturas>()
            .HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<EstadosPagos>()
            .HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Pagos>()
            .HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<DetallesFacturas>()
            .HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<TiposItems>()
            .HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Servicios>()
            .HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<TiposServicios>()
            .HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Productos>()
            .HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<CategoriasProductos>()
            .HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Proveedores>()
            .HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Citas>()
            .HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<EstadosCitas>()
            .HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Personas>()
            .HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<PersonasRoles>()
            .HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<AdoptantesDetalles>()
            .HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<TipoViviendas>()
            .HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<HistoriasClinicas>()
            .HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<SolicitudesAdopciones>()
            .HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<EstadoSolicitud>()
            .HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<HistorialAdopciones>()
            .HasQueryFilter(e => !e.IsDeleted);
        modelBuilder.Entity<Razas>()
            .HasQueryFilter(e => !e.IsDeleted);



        modelBuilder.Entity<ConfiguracionEmpresa>();// Solo un registro, no soft delete

        // Relaciones y restricciones adicionales si las hay:

        // HistorialAdopciones
        modelBuilder.Entity<HistorialAdopciones>()
            .HasOne(r => r.MascotaAdopcion)
            .WithMany()
            .HasForeignKey(r => r.MascotasAdopcionID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<HistorialAdopciones>()
           .HasOne(r => r.SolicitudAdopcion)
           .WithMany()
           .HasForeignKey(r => r.SolicitudesAdopcionesID)
           .OnDelete(DeleteBehavior.Restrict);

        //modelBuilder.Entity<Productos>()
        //    .HasOne(p => p.CategoriaProducto)
        //    .WithMany()
        //    .HasForeignKey(p => p.CategoriasProductosID)
        //    .OnDelete(DeleteBehavior.Restrict);

        //modelBuilder.Entity<Productos>()
        //    .HasOne(p => p.Proveedor)
        //    .WithMany()
        //    .HasForeignKey(p => p.ProveedoresID)
        //    .OnDelete(DeleteBehavior.Restrict);

        //modelBuilder.Entity<Servicios>()
        //    .HasOne(s => s.TipoServicio)
        //    .WithMany()
        //    .HasForeignKey(s => s.TiposServiciosID)
        //    .OnDelete(DeleteBehavior.Restrict);

        //modelBuilder.Entity<MascotasAdopcion>()
        //    .HasOne(m => m.Razas)
        //    .WithMany()
        //    .HasForeignKey(m => m.RazasID)
        //    .OnDelete(DeleteBehavior.Restrict);

        //modelBuilder.Entity<MascotasAdopcion>()
        //    .HasOne(m => m.Estado)
        //    .WithMany(e => e.MascotasAdopcion)
        //    .HasForeignKey(m => m.EstadoID)
        //    .OnDelete(DeleteBehavior.Restrict);

        //modelBuilder.Entity<MascotasAdopcion>()
        //    .HasOne(m => m.RelacionSize)
        //    .WithMany()
        //    .HasForeignKey(m => m.RelacionSizeID)
        //    .OnDelete(DeleteBehavior.Restrict);

        //// Razas con Especies
        //modelBuilder.Entity<Razas>()
        //    .HasOne(r => r.Especie)
        //    .WithMany()
        //    .HasForeignKey(r => r.EspeciesID)
        //    .OnDelete(DeleteBehavior.Restrict);

        //// MascotasPersonas con Razas
        //modelBuilder.Entity<MascotasPersonas>()
        //    .HasOne(mp => mp.Razas)
        //    .WithMany(r => r.MascotasPersonas)
        //    .HasForeignKey(mp => mp.RazasID)
        //    .OnDelete(DeleteBehavior.Restrict);

        //// MascotasPersonas con Personas
        //modelBuilder.Entity<MascotasPersonas>()
        //    .HasOne(mp => mp.Personas)
        //    .WithMany()
        //    .HasForeignKey(mp => mp.PersonasID)
        //    .OnDelete(DeleteBehavior.SetNull);

        //// Razas con MascotasAdopcion (navegaci�n inversa)
        //modelBuilder.Entity<Razas>()
        //    .HasMany(r => r.MascotasAdopcion)
        //    .WithOne(m => m.Razas)
        //    .HasForeignKey(m => m.RazasID)
        //    .OnDelete(DeleteBehavior.Restrict);

        //// SolicitudesAdopciones con MascotasAdopcion
        //modelBuilder.Entity<SolicitudesAdopciones>()
        //    .HasOne(sa => sa.MascotaAdopcion)
        //    .WithMany()
        //    .HasForeignKey(sa => sa.MascotasAdopcionID)
        //    .OnDelete(DeleteBehavior.Restrict);

        //// SolicitudesAdopciones con Personas
        //modelBuilder.Entity<SolicitudesAdopciones>()
        //    .HasOne(sa => sa.Persona)
        //    .WithMany()
        //    .HasForeignKey(sa => sa.PersonasID)
        //    .OnDelete(DeleteBehavior.Restrict);

        //// SolicitudesAdopciones con EstadoSolicitud
        //modelBuilder.Entity<SolicitudesAdopciones>()
        //    .HasOne(sa => sa.EstadoSolicitud)
        //    .WithMany()
        //    .HasForeignKey(sa => sa.EstadoSolicitudID)
        //    .OnDelete(DeleteBehavior.Restrict);

        //// HistorialAdopciones con MascotasAdopcion
        //modelBuilder.Entity<HistorialAdopciones>()
        //    .HasOne(ha => ha.MascotaAdopcion)
        //    .WithMany()
        //    .HasForeignKey(ha => ha.MascotasAdopcionID)
        //    .OnDelete(DeleteBehavior.Restrict);

        //// HistorialAdopciones con SolicitudesAdopciones
        //modelBuilder.Entity<HistorialAdopciones>()
        //    .HasOne(ha => ha.SolicitudAdopcion)
        //    .WithMany()
        //    .HasForeignKey(ha => ha.SolicitudesAdopcionesID)
        //    .OnDelete(DeleteBehavior.Restrict);

        //// SeguimientoMascotas con MascotasAdopcion
        //modelBuilder.Entity<SeguimientoMascotas>()
        //    .HasOne(sm => sm.MascotaAdopcion)
        //    .WithMany()
        //    .HasForeignKey(sm => sm.MascotasAdopcionID)
        //    .OnDelete(DeleteBehavior.Restrict);

        //// SeguimientoMascotas con Personas
        //modelBuilder.Entity<SeguimientoMascotas>()
        //    .HasOne(sm => sm.Persona)
        //    .WithMany()
        //    .HasForeignKey(sm => sm.PersonasID)
        //    .OnDelete(DeleteBehavior.Restrict);

        //// SeguimientoMascotas con EstadoMascota
        //modelBuilder.Entity<SeguimientoMascotas>()
        //    .HasOne(sm => sm.EstadoMascota)
        //    .WithMany()
        //    .HasForeignKey(sm => sm.EstadoMascotaID)
        //    .OnDelete(DeleteBehavior.Restrict);

        //// AdoptantesDetalles con Personas
        //modelBuilder.Entity<AdoptantesDetalles>()
        //    .HasOne(ad => ad.Persona)
        //    .WithMany()
        //    .HasForeignKey(ad => ad.PersonasID)
        //    .OnDelete(DeleteBehavior.Restrict);

        //// AdoptantesDetalles con TipoViviendas
        //modelBuilder.Entity<AdoptantesDetalles>()
        //    .HasOne(ad => ad.TipoVivienda)
        //    .WithMany()
        //    .HasForeignKey(ad => ad.TipoViviendasID)
        //    .OnDelete(DeleteBehavior.Restrict);

        //// PersonasRoles con Personas
        //modelBuilder.Entity<PersonasRoles>()
        //    .HasOne(pr => pr.Persona)
        //    .WithMany()
        //    .HasForeignKey(pr => pr.PersonasID)
        //    .OnDelete(DeleteBehavior.Restrict);

        //// HistoriasClinicas con MascotasPersonas
        //modelBuilder.Entity<HistoriasClinicas>()
        //    .HasOne(hc => hc.MascotaPersona)
        //    .WithMany()
        //    .HasForeignKey(hc => hc.MascotasPersonasID)
        //    .OnDelete(DeleteBehavior.Restrict);

        //// HistoriasClinicas con Personas (veterinario)
        //modelBuilder.Entity<HistoriasClinicas>()
        //    .HasOne(hc => hc.Veterinario)
        //    .WithMany()
        //    .HasForeignKey(hc => hc.PersonasID)
        //    .OnDelete(DeleteBehavior.Restrict);

        //// Citas con Personas
        //modelBuilder.Entity<Citas>()
        //    .HasOne(c => c.Persona)
        //    .WithMany()
        //    .HasForeignKey(c => c.PersonasID)
        //    .OnDelete(DeleteBehavior.Restrict);

        //// Citas con MascotasPersonas
        //modelBuilder.Entity<Citas>()
        //    .HasOne(c => c.MascotaPersona)
        //    .WithMany()
        //    .HasForeignKey(c => c.MascotasPersonasID)
        //    .OnDelete(DeleteBehavior.Restrict);

        //// Citas con EstadosCitas
        //modelBuilder.Entity<Citas>()
        //    .HasOne(c => c.EstadoCita)
        //    .WithMany()
        //    .HasForeignKey(c => c.EstadosCitasID)
        //    .OnDelete(DeleteBehavior.Restrict);

        //// Facturas con Personas
        //modelBuilder.Entity<Facturas>()
        //    .HasOne(f => f.Persona)
        //    .WithMany()
        //    .HasForeignKey(f => f.PersonasID)
        //    .OnDelete(DeleteBehavior.Restrict);

        //// Facturas con EstadosPagos
        //modelBuilder.Entity<Facturas>()
        //    .HasOne(f => f.EstadoPago)
        //    .WithMany()
        //    .HasForeignKey(f => f.EstadoPagoID)
        //    .OnDelete(DeleteBehavior.Restrict);

        //// Pagos con Facturas
        //modelBuilder.Entity<Pagos>()
        //    .HasOne(p => p.Factura)
        //    .WithMany()
        //    .HasForeignKey(p => p.FacturasID)
        //    .OnDelete(DeleteBehavior.Restrict);

        //// DetalleFacturas con Factura
        //modelBuilder.Entity<DetallesFacturas>()
        //    .HasOne(df => df.Factura)
        //    .WithMany()
        //    .HasForeignKey(df => df.FacturasID)
        //    .OnDelete(DeleteBehavior.Restrict);

        //// DetalleFacturas con TiposItems
        //modelBuilder.Entity<DetallesFacturas>()
        //    .HasOne(df => df.TipoItem)
        //    .WithMany()
        //    .HasForeignKey(df => df.TiposItemsID)
        //    .OnDelete(DeleteBehavior.Restrict);


        // Puedes a�adir relaciones faltantes o espec�ficas para tu dominio.
        // Chequea la cardinalidad y los requerimientos de borrado en cascada por tus reglas de negocio.

        // Puedes agregar configuraciones similares para los modelos restantes si necesitas especificar relaciones/cascadas/restricciones particulares.
        #endregion

        #region Initial Seed

        // Roles
        modelBuilder.Entity<IdentityRole>().HasData(
          new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
          new IdentityRole { Id = "2", Name = "User", NormalizedName = "USER" }
      );

        // Estados (MascotasAdopcion)
        modelBuilder.Entity<Estados>().HasData(
            new Estados { EstadoID = 1, Nombre = "Adoptado", IsDeleted = false },
            new Estados { EstadoID = 2, Nombre = "Disponible", IsDeleted = false },
            new Estados { EstadoID = 3, Nombre = "No Disponible", IsDeleted = false }
        );

        // EstadoSolicitud
        modelBuilder.Entity<EstadoSolicitud>().HasData(
            new EstadoSolicitud { EstadoSolicitudID = 1, Nombre = "Aprobada", IsDeleted = false },
            new EstadoSolicitud { EstadoSolicitudID = 2, Nombre = "En Revisi�n", IsDeleted = false },
            new EstadoSolicitud { EstadoSolicitudID = 3, Nombre = "Rechazada", IsDeleted = false },
            new EstadoSolicitud { EstadoSolicitudID = 4, Nombre = "En Espera", IsDeleted = false }
        );

        // EstadoMascota
        modelBuilder.Entity<EstadoMascota>().HasData(
            new EstadoMascota { EstadoMascotaID = 1, Nombre = "Sano", IsDeleted = false },
            new EstadoMascota { EstadoMascotaID = 2, Nombre = "Enfermo", IsDeleted = false }
        );

        // EstadosPagos
        modelBuilder.Entity<EstadosPagos>().HasData(
            new EstadosPagos { EstadosPagosID = 1, Nombre = "Pagado", IsDeleted = false },
            new EstadosPagos { EstadosPagosID = 2, Nombre = "Pendiente", IsDeleted = false }
        );

        // TiposItems
        modelBuilder.Entity<TiposItems>().HasData(
            new TiposItems { TiposItemsID = 1, Nombre = "Producto", IsDeleted = false },
            new TiposItems { TiposItemsID = 2, Nombre = "Servicio", IsDeleted = false }
        );

        // TiposServicios
        modelBuilder.Entity<TiposServicios>().HasData(
            new TiposServicios { TiposServiciosID = 1, Nombre = "Consulta Veterinaria", IsDeleted = false },
            new TiposServicios { TiposServiciosID = 2, Nombre = "Vacunaci�n", IsDeleted = false }
        );

        // TipoViviendas
        modelBuilder.Entity<TipoViviendas>().HasData(
            new TipoViviendas { TipoViviendasID = 1, Nombre = "Casa", IsDeleted = false },
            new TipoViviendas { TipoViviendasID = 2, Nombre = "Apartamento", IsDeleted = false }
        );

        // Especies y Razas (ejemplo m�nimo)
        modelBuilder.Entity<Especies>().HasData(
            new Especies { EspeciesID = 1, Nombre = "Gato", IsDeleted = false },
            new Especies { EspeciesID = 2, Nombre = "Perro", IsDeleted = false }
        );

        modelBuilder.Entity<RelacionSize>().HasData(
            new RelacionSize { RelacionSizeID = 1, Nombre = "Peque�o", IsDeleted = false },
            new RelacionSize { RelacionSizeID = 2, Nombre = "Mediano", IsDeleted = false },
            new RelacionSize { RelacionSizeID = 3, Nombre = "Grande", IsDeleted = false }
        );

        modelBuilder.Entity<Razas>().HasData(
            new Razas { RazasID = 1, EspeciesID = 2, Nombre = "Chihuahua", IsDeleted = false },
            new Razas { RazasID = 2, EspeciesID = 2, Nombre = "Bulldog", IsDeleted = false },
            new Razas { RazasID = 3, EspeciesID = 1, Nombre = "Gato Naranja", IsDeleted = false }
        );

        // Categor�as de productos
        modelBuilder.Entity<CategoriasProductos>().HasData(
            new CategoriasProductos { CategoriasProductosID = 1, Nombre = "Alimentos", IsDeleted = false },
            new CategoriasProductos { CategoriasProductosID = 2, Nombre = "Accesorios", IsDeleted = false }
        );

        // Estados de la cita
        modelBuilder.Entity<EstadosCitas>().HasData(
            new EstadosCitas { EstadosCitasID = 1, Nombre = "Pendiente", IsDeleted = false },
            new EstadosCitas { EstadosCitasID = 2, Nombre = "Confirmada", IsDeleted = false },
            new EstadosCitas { EstadosCitasID = 3, Nombre = "Cancelada", IsDeleted = false }
        );

        modelBuilder.Entity<MascotasAdopcion>().HasData(
    new MascotasAdopcion
    {
        MascotasAdopcionID = 1,
        RazasID = 1,
        RelacionSizeID = 1,
        Tamanio = 1,
        Nombre = "Luna",
        FechaNacimiento = new DateOnly(2023, 5, 10),
        Descripcion = "Cachorrita juguetona y muy cariñosa con niños.",
        FotoURL = "https://images.unsplash.com/photo-1583511655826-05700d52f4ae",
        EstadoID = 2,
        Sexo = 'f',
        IsDeleted = false
    },
    new MascotasAdopcion
    {
        MascotasAdopcionID = 2,
        RazasID = 2,
        RelacionSizeID = 2,
        Tamanio = 2,
        Nombre = "Rocky",
        FechaNacimiento = new DateOnly(2022, 11, 15),
        Descripcion = "Perro guardián y muy leal. Ideal para casas grandes.",
        FotoURL = "https://images.unsplash.com/photo-1601758123927-196f76f75097",
        EstadoID = 2,
        Sexo = 'm',
        IsDeleted = false
    },
    new MascotasAdopcion
    {
        MascotasAdopcionID = 3,
        RazasID = 3,
        RelacionSizeID = 1,
        Tamanio = 3,
        Nombre = "Mia",
        FechaNacimiento = new DateOnly(2024, 2, 1),
        Descripcion = "Gatita rescatada muy tranquila y sociable.",
        FotoURL = "https://images.unsplash.com/photo-1592194996308-7b43878e84a6",
        EstadoID = 2,
        Sexo = 'f',
        IsDeleted = false
    },
    new MascotasAdopcion
    {
        MascotasAdopcionID = 4,
        RazasID = 2,
        RelacionSizeID = 3,
        Tamanio = 5,
        Nombre = "Zeus",
        FechaNacimiento = new DateOnly(2021, 8, 20),
        Descripcion = "Perro fuerte, entrenado y excelente para seguridad.",
        FotoURL = "https://images.unsplash.com/photo-1583511655826-05700d52f4ae",
        EstadoID = 2,
        Sexo = 'm',
        IsDeleted = false
    },
    new MascotasAdopcion
    {
        MascotasAdopcionID = 5,
        RazasID = 1,
        RelacionSizeID = 2,
        Tamanio = 5,
        Nombre = "Nala",
        FechaNacimiento = new DateOnly(2023, 1, 30),
        Descripcion = "Muy energética y necesita mucho ejercicio diario.",
        FotoURL = "https://images.unsplash.com/photo-1601758003122-58eacb8e3ed1",
        EstadoID = 2,
        Sexo = 'f',
        IsDeleted = false
    },
    new MascotasAdopcion
    {
        MascotasAdopcionID = 6,
        RazasID = 3,
        RelacionSizeID = 1,
        Tamanio = 2,
        Nombre = "Simba",
        FechaNacimiento = new DateOnly(2024, 4, 15),
        Descripcion = "Gatito curioso y muy juguetón.",
        FotoURL = "https://images.unsplash.com/photo-1592194996308-7b43878e84a6",
        EstadoID = 2,
        Sexo = 'm',
        IsDeleted = false
    },
    new MascotasAdopcion
    {
        MascotasAdopcionID = 7,
        RazasID = 2,
        RelacionSizeID = 3,
        Tamanio = 4,
        Nombre = "Thor",
        FechaNacimiento = new DateOnly(2022, 6, 18),
        Descripcion = "Gran danés amoroso y obediente.",
        FotoURL = "https://images.unsplash.com/photo-1583511655826-05700d52f4ae",
        EstadoID = 2,
        Sexo = 'm',
        IsDeleted = false
    },
    new MascotasAdopcion
    {
        MascotasAdopcionID = 8,
        RazasID = 3,
        RelacionSizeID = 1,
        Tamanio = 5,
        Nombre = "Lili",
        FechaNacimiento = new DateOnly(2023, 7, 9),
        Descripcion = "Gatita blanca, ideal para compañía.",
        FotoURL = "https://images.unsplash.com/photo-1580377969203-4ec1eac6d5fe",
        EstadoID = 2,
        Sexo = 'f',
        IsDeleted = false
    },
    new MascotasAdopcion
    {
        MascotasAdopcionID = 9,
        RazasID = 1,
        RelacionSizeID = 2,
        Tamanio = 5,
        Nombre = "Max",
        FechaNacimiento = new DateOnly(2021, 9, 12),
        Descripcion = "Obediente y perfecto para familias con niños.",
        FotoURL = "https://images.unsplash.com/photo-1558788353-f76d92427f16",
        EstadoID = 2,
        Sexo = 'm',
        IsDeleted = false
    },
    new MascotasAdopcion
    {
        MascotasAdopcionID = 10,
        RazasID = 3,
        RelacionSizeID = 1,
        Tamanio = 4,
        Nombre = "Cleo",
        FechaNacimiento = new DateOnly(2024, 3, 22),
        Descripcion = "Gatita siamesa muy elegante.",
        FotoURL = "https://images.unsplash.com/photo-1574158622682-e40e69881006",
        EstadoID = 2,
        Sexo = 'f',
        IsDeleted = false
    }
);

        // Productos
        modelBuilder.Entity<Productos>().HasData(
    new Productos
    {
        ProductosID = 1,
        CategoriasProductosID = 1,
        ProveedoresID = 1,
        Nombre = "Croquetas Premium",
        Descripcion = "Croquetas nutritivas para perros adultos.",
        Costo = 10.00m,
        Precio = 18.99m,
        Stock = 50,
        ImagenUrl = "https://images.rawpixel.com/image_800/cHJpdmF0ZS9sci9pbWFnZXMvd2Vic2l0ZS8yMDIyLTExL3BmLXMxMDgtcG0tNDExMy1tb2NrdXAuanBn.jpg",
        IsDeleted = false
    },
    new Productos
    {
        ProductosID = 2,
        CategoriasProductosID = 1,
        ProveedoresID = 1,
        Nombre = "Comida Húmeda para Gato",
        Descripcion = "Lata de comida gourmet para gatos exigentes.",
        Costo = 2.50m,
        Precio = 4.99m,
        Stock = 100,
        ImagenUrl = "https://images.rawpixel.com/image_800/cHJpdmF0ZS9sci9pbWFnZXMvd2Vic2l0ZS8yMDIyLTExL3BmLXMxMDgtcG0tNDExMy1tb2NrdXAuanBn.jpg",
        IsDeleted = false
    },
    new Productos
    {
        ProductosID = 3,
        CategoriasProductosID = 2,
        ProveedoresID = 1,
        Nombre = "Collar con GPS",
        Descripcion = "Collar rastreador para mascotas medianas.",
        Costo = 15.00m,
        Precio = 29.99m,
        Stock = 25,
        ImagenUrl = "https://images.rawpixel.com/image_800/cHJpdmF0ZS9sci9pbWFnZXMvd2Vic2l0ZS8yMDIyLTExL3BmLXMxMDgtcG0tNDExMy1tb2NrdXAuanBn.jpg",
        IsDeleted = false
    },
    new Productos
    {
        ProductosID = 4,
        CategoriasProductosID = 2,
        ProveedoresID = 1,
        Nombre = "Cama para Mascotas",
        Descripcion = "Cama acolchonada ideal para gatos y perros pequeños.",
        Costo = 12.00m,
        Precio = 24.99m,
        Stock = 30,
        ImagenUrl = "https://images.rawpixel.com/image_800/cHJpdmF0ZS9sci9pbWFnZXMvd2Vic2l0ZS8yMDIyLTExL3BmLXMxMDgtcG0tNDExMy1tb2NrdXAuanBn.jpg",
        IsDeleted = false
    },
    new Productos
    {
        ProductosID = 5,
        CategoriasProductosID = 1,
        ProveedoresID = 1,
        Nombre = "Snack Natural",
        Descripcion = "Galletas orgánicas para entrenamiento.",
        Costo = 3.00m,
        Precio = 6.50m,
        Stock = 80,
        ImagenUrl = "https://images.rawpixel.com/image_800/cHJpdmF0ZS9sci9pbWFnZXMvd2Vic2l0ZS8yMDIyLTExL3BmLXMxMDgtcG0tNDExMy1tb2NrdXAuanBn.jpg",
        IsDeleted = false
    },
    new Productos
    {
        ProductosID = 6,
        CategoriasProductosID = 2,
        ProveedoresID = 1,
        Nombre = "Juguete Pelota Sonora",
        Descripcion = "Pelota para perros que emite sonidos al morderla.",
        Costo = 1.80m,
        Precio = 4.00m,
        Stock = 60,
        ImagenUrl = "https://images.rawpixel.com/image_800/cHJpdmF0ZS9sci9pbWFnZXMvd2Vic2l0ZS8yMDIyLTExL3BmLXMxMDgtcG0tNDExMy1tb2NrdXAuanBn.jpg",
        IsDeleted = false
    },
    new Productos
    {
        ProductosID = 7,
        CategoriasProductosID = 1,
        ProveedoresID = 1,
        Nombre = "Leche para Cachorros",
        Descripcion = "Suplemento lácteo para crías sin madre.",
        Costo = 5.00m,
        Precio = 9.99m,
        Stock = 45,
        ImagenUrl = "https://images.rawpixel.com/image_800/cHJpdmF0ZS9sci9pbWFnZXMvd2Vic2l0ZS8yMDIyLTExL3BmLXMxMDgtcG0tNDExMy1tb2NrdXAuanBn.jpg",
        IsDeleted = false
    },
    new Productos
    {
        ProductosID = 8,
        CategoriasProductosID = 2,
        ProveedoresID = 1,
        Nombre = "Correa Retráctil",
        Descripcion = "Correa extensible hasta 5 metros.",
        Costo = 6.00m,
        Precio = 12.99m,
        Stock = 40,
        ImagenUrl = "https://images.rawpixel.com/image_800/cHJpdmF0ZS9sci9pbWFnZXMvd2Vic2l0ZS8yMDIyLTExL3BmLXMxMDgtcG0tNDExMy1tb2NrdXAuanBn.jpg",
        IsDeleted = false
    },
    new Productos
    {
        ProductosID = 9,
        CategoriasProductosID = 2,
        ProveedoresID = 1,
        Nombre = "Rascador para Gato",
        Descripcion = "Rascador vertical de sisal natural.",
        Costo = 8.00m,
        Precio = 15.50m,
        Stock = 20,
        ImagenUrl = "https://images.rawpixel.com/image_800/cHJpdmF0ZS9sci9pbWFnZXMvd2Vic2l0ZS8yMDIyLTExL3BmLXMxMDgtcG0tNDExMy1tb2NrdXAuanBn.jpg",
        IsDeleted = false
    },
    new Productos
    {
        ProductosID = 10,
        CategoriasProductosID = 1,
        ProveedoresID = 1,
        Nombre = "Vitaminas para Mascotas",
        Descripcion = "Complemento vitamínico para perros y gatos.",
        Costo = 4.00m,
        Precio = 8.50m,
        Stock = 70,
        ImagenUrl = "https://images.rawpixel.com/image_800/cHJpdmF0ZS9sci9pbWFnZXMvd2Vic2l0ZS8yMDIyLTExL3BmLXMxMDgtcG0tNDExMy1tb2NrdXAuanBn.jpg",
        IsDeleted = false
    }
);

        modelBuilder.Entity<Proveedores>().HasData(
    new Proveedores
    {
        ProveedoresID = 1,
        Nombre = "Distribuidora PetZone",
        RNC = "132456789",
        Telefono = "809-888-5555",
        Email = "ventas@petzone.com",
        IsDeleted = false
    },
    new Proveedores
    {
        ProveedoresID = 2,
        Nombre = "PetPlus Suplidores",
        RNC = "101112233",
        Telefono = "809-777-4444",
        Email = "contacto@petplus.com",
        IsDeleted = false
    },
    new Proveedores
    {
        ProveedoresID = 3,
        Nombre = "Mascotienda SRL",
        RNC = "110220330",
        Telefono = "809-666-3333",
        Email = "info@mascotienda.com",
        IsDeleted = false
    }
);



        // Configuraci�n de empresa: Solo se admite un registro
        modelBuilder.Entity<ConfiguracionEmpresa>().HasData(
            new ConfiguracionEmpresa
            {
                EmpresaID = 1,
                Nombre = "Veterinaria PawfectMatch",
                Telefono = "809-123-4567",
                RNC = "101000099",
                Direccion = "Av. Principal 123, Ciudad"
            }
        );

        // ------ Ajustar/a�adir seeds seg�n tus necesidades -----

        #endregion
    }
}