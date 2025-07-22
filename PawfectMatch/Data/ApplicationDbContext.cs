using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PawfectMatch.Models;
using PawfectMatch.Models.Adopciones;
using PawfectMatch.Models.POS;
using PawfectMatch.Models.Servicios;

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

    // Tabs
    public DbSet<VetasTabs> VetasTabs { get; set; }
    public DbSet<ProductosInTabs> ProductosInTabs { get; set; }

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

        // Persona Default
        modelBuilder.Entity<Personas>().HasData(ApplicationSeedData.seedPersonas);

        // Roles
        modelBuilder.Entity<IdentityRole>().HasData(ApplicationSeedData.seedIdentityRoles);

        // Estados (MascotasAdopcion)
        modelBuilder.Entity<Estados>().HasData(ApplicationSeedData.seedEstadosMascotas);

        // EstadoSolicitud
        modelBuilder.Entity<EstadoSolicitud>().HasData(ApplicationSeedData.seedEstadoSolicitud
        );

        // EstadoMascota
        modelBuilder.Entity<EstadoMascota>().HasData(ApplicationSeedData.seedEstadoMascota);

        // EstadosPagos
        modelBuilder.Entity<EstadosPagos>().HasData(ApplicationSeedData.seedEstadosPagos);

        // TiposItems
        modelBuilder.Entity<TiposItems>().HasData(ApplicationSeedData.seedTiposItems);

        // TiposServicios
        modelBuilder.Entity<TiposServicios>().HasData(ApplicationSeedData.seedTiposServicios);

        // TipoViviendas
        modelBuilder.Entity<TipoViviendas>().HasData(ApplicationSeedData.seedTipoViviendas);

        // Especies y Razas
        modelBuilder.Entity<Especies>().HasData(ApplicationSeedData.seedEspecies);

        modelBuilder.Entity<RelacionSize>().HasData(ApplicationSeedData.seedRelacionSize);

        modelBuilder.Entity<Razas>().HasData(ApplicationSeedData.seedRazas);

        // Categor�as de productos
        modelBuilder.Entity<CategoriasProductos>().HasData(ApplicationSeedData.seedCategoriasProductos);

        // Estados de la cita
        modelBuilder.Entity<EstadosCitas>().HasData(ApplicationSeedData.seedEstadosCitas);

        modelBuilder.Entity<MascotasAdopcion>().HasData(ApplicationSeedData.seedMascotasAdopcion);

        // Proveedor Default para productos
        modelBuilder.Entity<Proveedores>().HasData(ApplicationSeedData.seedProveedores);

        // Productos
        modelBuilder.Entity<Productos>().HasData(ApplicationSeedData.seedProductos);

        // Productos In Tabs
        modelBuilder.Entity<ProductosInTabs>().HasData(ApplicationSeedData.seedProductosInTabs);

        // 5 Tabs
        modelBuilder.Entity<VetasTabs>().HasData(ApplicationSeedData.seedVetasTabs);

        // 10 MascotasPersonas
        modelBuilder.Entity<MascotasPersonas>().HasData(ApplicationSeedData.seedMascotasPersonas);

        // 10 SolicitudesAdopciones
        modelBuilder.Entity<SolicitudesAdopciones>().HasData(ApplicationSeedData.seedSolicitudesAdopciones);

        // Puedes a�adir datos iniciales o de prueba para tus entidades aquí.

        #endregion

    }
}