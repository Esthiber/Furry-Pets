using Microsoft.AspNetCore.Identity;
using PawfectMatch.Models;
using PawfectMatch.Models.Adopciones;
using PawfectMatch.Models.POS;
using PawfectMatch.Models.Servicios;

namespace PawfectMatch.Data
{
    public static class ApplicationSeedData
    {
        public static readonly List<Personas> seedPersonas = new()
        {
            new Personas
            {
                PersonasID = 1,
                Nombre = "Cliente Default",
                Email = "cliente@default.com",
                Telefono = "123-456-7890",
                Direccion = "Calle 123, Ciudad",
                IsDeleted = false
            },
            // 10 personas adicionales para pruebas
            new Personas { PersonasID = 2, Nombre = "Ana Pérez", Email = "ana@correo.com", Telefono = "809-111-2222", Direccion = "Calle 2", IsDeleted = false },
            new Personas { PersonasID = 3, Nombre = "Luis Gómez", Email = "luis@correo.com", Telefono = "809-333-4444", Direccion = "Calle 3", IsDeleted = false },
            new Personas { PersonasID = 4, Nombre = "María López", Email = "maria@correo.com", Telefono = "809-555-6666", Direccion = "Calle 4", IsDeleted = false },
            new Personas { PersonasID = 5, Nombre = "Carlos Ruiz", Email = "carlos@correo.com", Telefono = "809-777-8888", Direccion = "Calle 5", IsDeleted = false },
            new Personas { PersonasID = 6, Nombre = "Sofía Torres", Email = "sofia@correo.com", Telefono = "809-999-0000", Direccion = "Calle 6", IsDeleted = false },
            new Personas { PersonasID = 7, Nombre = "Pedro Sánchez", Email = "pedro@correo.com", Telefono = "809-121-2121", Direccion = "Calle 7", IsDeleted = false },
            new Personas { PersonasID = 8, Nombre = "Lucía Fernández", Email = "lucia@correo.com", Telefono = "809-232-3232", Direccion = "Calle 8", IsDeleted = false },
            new Personas { PersonasID = 9, Nombre = "Miguel Castro", Email = "miguel@correo.com", Telefono = "809-343-4343", Direccion = "Calle 9", IsDeleted = false },
            new Personas { PersonasID = 10, Nombre = "Valeria Jiménez", Email = "valeria@correo.com", Telefono = "809-454-5454", Direccion = "Calle 10", IsDeleted = false },
            new Personas { PersonasID = 11, Nombre = "Javier Ramírez", Email = "javier@correo.com", Telefono = "809-565-6565", Direccion = "Calle 11", IsDeleted = false }

        };

        public static readonly List<IdentityRole> seedIdentityRoles = new() {
          new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
          new IdentityRole { Id = "2", Name = "User", NormalizedName = "USER" }
        };

        public static readonly List<Estados> seedEstadosMascotas = new() {
            new Estados { EstadoID = 1, Nombre = "Adoptado", IsDeleted = false },
            new Estados { EstadoID = 2, Nombre = "Disponible", IsDeleted = false },
            new Estados { EstadoID = 3, Nombre = "No Disponible", IsDeleted = false }
        };

        public static readonly List<EstadoSolicitud> seedEstadoSolicitud = new() {
            new EstadoSolicitud { EstadoSolicitudID = 1, Nombre = "Aprobada", IsDeleted = false },
            new EstadoSolicitud { EstadoSolicitudID = 2, Nombre = "En Revision", IsDeleted = false },
            new EstadoSolicitud { EstadoSolicitudID = 3, Nombre = "Rechazada", IsDeleted = false },
            new EstadoSolicitud { EstadoSolicitudID = 4, Nombre = "En Espera", IsDeleted = false }
        };

        public static readonly List<EstadoMascota> seedEstadoMascota = new() {
            new EstadoMascota { EstadoMascotaID = 1, Nombre = "Sano", IsDeleted = false },
            new EstadoMascota { EstadoMascotaID = 2, Nombre = "Enfermo", IsDeleted = false }
        };

        public static readonly List<EstadosPagos> seedEstadosPagos = new()
        {
            new EstadosPagos { EstadosPagosID = 1, Nombre = "Pagado", IsDeleted = false },
            new EstadosPagos { EstadosPagosID = 2, Nombre = "Pendiente", IsDeleted = false }
        };

        public static readonly List<TiposItems> seedTiposItems = new()
        {
            new TiposItems { TiposItemsID = 1, Nombre = "Producto", IsDeleted = false },
            new TiposItems { TiposItemsID = 2, Nombre = "Servicio", IsDeleted = false }
        };

        public static readonly List<TiposServicios> seedTiposServicios = new()
        {
            new TiposServicios { TiposServiciosID = 1, Nombre = "Consulta Veterinaria", IsDeleted = false },
            new TiposServicios { TiposServiciosID = 2, Nombre = "Vacunación", IsDeleted = false }
        };

        public static readonly List<TipoViviendas> seedTipoViviendas = new()
        {
            new TipoViviendas { TipoViviendasID = 1, Nombre = "Casa", IsDeleted = false },
            new TipoViviendas { TipoViviendasID = 2, Nombre = "Apartamento", IsDeleted = false }
        };

        public static readonly List<Especies> seedEspecies = new()
        {
            new Especies { EspeciesID = 1, Nombre = "Gato", IsDeleted = false },
            new Especies { EspeciesID = 2, Nombre = "Perro", IsDeleted = false }
        };

        public static readonly List<RelacionSize> seedRelacionSize = new()
        {
            new RelacionSize { RelacionSizeID = 1, Nombre = "Pequeño", IsDeleted = false },
            new RelacionSize { RelacionSizeID = 2, Nombre = "Mediano", IsDeleted = false },
            new RelacionSize { RelacionSizeID = 3, Nombre = "Grande", IsDeleted = false }
        };

        public static readonly List<Razas> seedRazas = new()
        {
            new Razas { RazasID = 1, EspeciesID = 2, Nombre = "Chihuahua", IsDeleted = false },
            new Razas { RazasID = 2, EspeciesID = 2, Nombre = "Bulldog", IsDeleted = false },
            new Razas { RazasID = 3, EspeciesID = 1, Nombre = "Gato Naranja", IsDeleted = false }
        };

        public static readonly List<CategoriasProductos> seedCategoriasProductos = new()
        {
            new CategoriasProductos { CategoriasProductosID = 1, Nombre = "Alimentos", IsDeleted = false },
            new CategoriasProductos { CategoriasProductosID = 2, Nombre = "Accesorios", IsDeleted = false }
        };

        public static readonly List<EstadosCitas> seedEstadosCitas = new()
        {
            new EstadosCitas { EstadosCitasID = 1, Nombre = "Pendiente", IsDeleted = false },
            new EstadosCitas { EstadosCitasID = 2, Nombre = "Confirmada", IsDeleted = false },
            new EstadosCitas { EstadosCitasID = 3, Nombre = "Cancelada", IsDeleted = false }
        };

        public static readonly List<MascotasAdopcion> seedMascotasAdopcion = new()
        {

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
    },
    new MascotasAdopcion
    {
        MascotasAdopcionID = 11,
        RazasID = 1,
        RelacionSizeID = 1,
        Tamanio = 1,
        Nombre = "Toby",
        FechaNacimiento = new DateOnly(2022, 1, 1),
        Descripcion = "Perro juguetón",
        FotoURL = "",
        EstadoID = 2,
        Sexo = 'm',
        IsDeleted = false
    },
    new MascotasAdopcion
    {
        MascotasAdopcionID = 12,
        RazasID = 2,
        RelacionSizeID = 2,
        Tamanio = 2,
        Nombre = "Bella",
        FechaNacimiento = new DateOnly(2021, 2, 2),
        Descripcion = "Perra tranquila",
        FotoURL = "",
        EstadoID = 2,
        Sexo = 'f',
        IsDeleted = false
    },
    new MascotasAdopcion
    {
        MascotasAdopcionID = 13,
        RazasID = 3,
        RelacionSizeID = 1,
        Tamanio = 3,
        Nombre = "Tom",
        FechaNacimiento = new DateOnly(2020, 3, 3),
        Descripcion = "Gato curioso",
        FotoURL = "",
        EstadoID = 2,
        Sexo = 'm',
        IsDeleted = false
    },
    new MascotasAdopcion
    {
        MascotasAdopcionID = 14,
        RazasID = 1,
        RelacionSizeID = 2,
        Tamanio = 2,
        Nombre = "Lola",
        FechaNacimiento = new DateOnly(2019, 4, 4),
        Descripcion = "Perra activa",
        FotoURL = "",
        EstadoID = 2,
        Sexo = 'f',
        IsDeleted = false
    },
    new MascotasAdopcion
    {
        MascotasAdopcionID = 15,
        RazasID = 2,
        RelacionSizeID = 3,
        Tamanio = 3,
        Nombre = "Rex",
        FechaNacimiento = new DateOnly(2018, 5, 5),
        Descripcion = "Perro guardián",
        FotoURL = "",
        EstadoID = 2,
        Sexo = 'm',
        IsDeleted = false
    },
    new MascotasAdopcion
    {
        MascotasAdopcionID = 16,
        RazasID = 3,
        RelacionSizeID = 1,
        Tamanio = 1,
        Nombre = "Nina",
        FechaNacimiento = new DateOnly(2017, 6, 6),
        Descripcion = "Gata cariñosa",
        FotoURL = "",
        EstadoID = 2,
        Sexo = 'f',
        IsDeleted = false
    },
    new MascotasAdopcion
    {
        MascotasAdopcionID = 17,
        RazasID = 1,
        RelacionSizeID = 2,
        Tamanio = 2,
        Nombre = "Leo",
        FechaNacimiento = new DateOnly(2016, 7, 7),
        Descripcion = "Perro inteligente",
        FotoURL = "",
        EstadoID = 2,
        Sexo = 'm',
        IsDeleted = false
    },
    new MascotasAdopcion
    {
        MascotasAdopcionID = 18,
        RazasID = 2,
        RelacionSizeID = 3,
        Tamanio = 3,
        Nombre = "Maya",
        FechaNacimiento = new DateOnly(2015, 8, 8),
        Descripcion = "Perra fiel",
        FotoURL = "",
        EstadoID = 2,
        Sexo = 'f',
        IsDeleted = false
    },
    new MascotasAdopcion
    {
        MascotasAdopcionID = 19,
        RazasID = 3,
        RelacionSizeID = 1,
        Tamanio = 1,
        Nombre = "Simón",
        FechaNacimiento = new DateOnly(2014, 9, 9),
        Descripcion = "Gato dormilón",
        FotoURL = "",
        EstadoID = 2,
        Sexo = 'm',
        IsDeleted = false
    },
    new MascotasAdopcion
    {
        MascotasAdopcionID = 20,
        RazasID = 1,
        RelacionSizeID = 2,
        Tamanio = 2,
        Nombre = "Daisy",
        FechaNacimiento = new DateOnly(2013, 10, 10),
        Descripcion = "Perra juguetona",
        FotoURL = "",
        EstadoID = 2,
        Sexo = 'f',
        IsDeleted = false
    }
        };

        public static readonly List<Proveedores> seedProveedores = new()
        {
            new Proveedores { ProveedoresID = 1, Nombre = "Proveedor Default", RNC = "000000000", Telefono = "809-000-0000", Email = "proveedor@default.com", IsDeleted = false }
        };

        public static readonly List<Productos> seedProductos = new()
        {
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
        ImagenUrl = "https://images.rawpixel.com/image_800/cHJpbmF0ZS9sci9pbWFnZXMvd2Vic2l0ZS8yMDIyLTExL3BmLXMxMDgtcG0tNDExMy1tb2NrdXAuanBn.jpg",
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
        ImagenUrl = "https://images.rawpixel.com/image_800/cHJpbmF0ZS9zci9pbWFnZXMvd2Vic2l0ZS8yMDIyLTExL3BmLXMxMDgtcG0tNDExMy1tb2NrdXAuanBn.jpg",
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
        ImagenUrl = "https://images.rawpixel.com/image_800/cHJpbmF0ZS9zci9pbWFnZXMvd2Vic2l0ZS8yMDIyLTExL3BmLXMxMDgtcG0tNDExMy1tb2NrdXAuanBn.jpg",
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
        ImagenUrl = "https://images.rawpixel.com/image_800/cHJpbmF0ZS9zci9pbWFnZXMvd2Vic2l0ZS8yMDIyLTExL3BmLXMxMDgtcG0tNDExMy1tb2NrdXAuanBn.jpg",
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
        ImagenUrl = "https://images.rawpixel.com/image_800/cHJpbmF0ZS9zci9pbWFnZXMvd2Vic2l0ZS8yMDIyLTExL3BmLXMxMDgtcG0tNDExMy1tb2NrdXAuanBn.jpg",
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
        ImagenUrl = "https://images.rawpixel.com/image_800/cHJpbmF0ZS9zci9pbWFnZXMvd2Vic2l0ZS8yMDIyLTExL3BmLXMxMDgtcG0tNDExMy1tb2NrdXAuanBn.jpg",
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
        ImagenUrl = "https://images.rawpixel.com/image_800/cHJpbmF0ZS9zci9pbWFnZXMvd2Vic2l0ZS8yMDIyLTExL3BmLXMxMDgtcG0tNDExMy1tb2NrdXAuanBn.jpg",
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
        ImagenUrl = "https://images.rawpixel.com/image_800/cHJpbmF0ZS9zci9pbWFnZXMvd2Vic2l0ZS8yMDIyLTExL3BmLXMxMDgtcG0tNDExMy1tb2NrdXAuanBn.jpg",
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
        ImagenUrl = "https://images.rawpixel.com/image_800/cHJpbmF0ZS9zci9pbWFnZXMvd2Vic2l0ZS8yMDIyLTExL3BmLXMxMDgtcG0tNDExMy1tb2NrdXAuanBn.jpg",
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
        ImagenUrl = "https://images.rawpixel.com/image_800/cHJpbmF0ZS9zci9pbWFnZXMvd2Vic2l0ZS8yMDIyLTExL3BmLXMxMDgtcG0tNDExMy1tb2NrdXAuanBn.jpg",
        IsDeleted = false
    },
    new Productos
    {
        ProductosID = 11,
        CategoriasProductosID = 1,
        ProveedoresID = 1,
        Nombre = "Croquetas Superiores",
        Descripcion = "Croquetas de alta calidad para perros deportivos.",
        Costo = 12.00m,
        Precio = 22.50m,
        Stock = 40,
        ImagenUrl = "https://via.placeholder.com/150",
        IsDeleted = false
    },
    new Productos
    {
        ProductosID = 12,
        CategoriasProductosID = 1,
        ProveedoresID = 1,
        Nombre = "Pasta de Atún para Gato",
        Descripcion = "Deliciosa pasta de atún para consentir a tu gato.",
        Costo = 3.00m,
        Precio = 6.00m,
        Stock = 90,
        ImagenUrl = "https://via.placeholder.com/150",
        IsDeleted = false
    },
    new Productos
    {
        ProductosID = 13,
        CategoriasProductosID = 2,
        ProveedoresID = 1,
        Nombre = "Arnés para Perro",
        Descripcion = "Arnés ajustable y cómodo para perros de todos los tamaños.",
        Costo = 15.00m,
        Precio = 28.50m,
        Stock = 35,
        ImagenUrl = "https://via.placeholder.com/150",
        IsDeleted = false
    },
    new Productos
    {
        ProductosID = 14,
        CategoriasProductosID = 2,
        ProveedoresID = 1,
        Nombre = "Juguete Interactivo para Gato",
        Descripcion = "Entretenido juguete para mantener activo a tu gato.",
        Costo = 5.00m,
        Precio = 10.99m,
        Stock = 25,
        ImagenUrl = "https://via.placeholder.com/150",
        IsDeleted = false
    },
    new Productos
    {
        ProductosID = 15,
        CategoriasProductosID = 1,
        ProveedoresID = 1,
        Nombre = "Snacks de Pollo para Perro",
        Descripcion = "Deliciosos snacks de pollo para premiar a tu perro.",
        Costo = 2.50m,
        Precio = 5.50m,
        Stock = 100,
        ImagenUrl = "https://via.placeholder.com/150",
        IsDeleted = false
    },
    new Productos
    {
        ProductosID = 16,
        CategoriasProductosID = 2,
        ProveedoresID = 1,
        Nombre = "Cepillo para Mascotas",
        Descripcion = "Cepillo de masaje para perros y gatos.",
        Costo = 8.00m,
        Precio = 15.00m,
        Stock = 60,
        ImagenUrl = "https://via.placeholder.com/150",
        IsDeleted = false
    },
    new Productos
    {
        ProductosID = 17,
        CategoriasProductosID = 1,
        ProveedoresID = 1,
        Nombre = "Suplemento Articular para Perros",
        Descripcion = "Suplemento para mejorar la salud articular de tu perro.",
        Costo = 25.00m,
        Precio = 40.00m,
        Stock = 20,
        ImagenUrl = "https://via.placeholder.com/150",
        IsDeleted = false
    },
    new Productos
    {
        ProductosID = 18,
        CategoriasProductosID = 2,
        ProveedoresID = 1,
        Nombre = "Collar anti pulgas y garrapatas",
        Descripcion = "Protege a tu mascota de pulgas y garrapatas.",
        Costo = 10.00m,
        Precio = 18.00m,
        Stock = 15,
        ImagenUrl = "https://via.placeholder.com/150",
        IsDeleted = false
    },
    new Productos
    {
        ProductosID = 19,
        CategoriasProductosID = 1,
        ProveedoresID = 1,
        Nombre = "Alimento Deshidratado para Perros",
        Descripcion = "Nutrición completa en un formato deshidratado fácil de servir.",
        Costo = 30.00m,
        Precio = 50.00m,
        Stock = 10,
        ImagenUrl = "https://via.placeholder.com/150",
        IsDeleted = false
    },
    new Productos
    {
        ProductosID = 20,
        CategoriasProductosID = 2,
        ProveedoresID = 1,
        Nombre = "Arena sanitaria para gatos",
        Descripcion = "Arena absorbente y control de olores para gatos.",
        Costo = 7.50m,
        Precio = 15.00m,
        Stock = 55,
        ImagenUrl = "https://via.placeholder.com/150",
        IsDeleted = false
    },
    new Productos
    {
        ProductosID = 21,
        CategoriasProductosID = 1,
        ProveedoresID = 1,
        Nombre = "Tratamiento Antipulgas Spot-On Perros",
        Descripcion = "Elimina y previene pulgas en perros.",
        Costo = 20.00m,
        Precio = 35.00m,
        Stock = 25,
        ImagenUrl = "https://via.placeholder.com/150",
        IsDeleted = false
    },
    new Productos
    {
        ProductosID = 22,
        CategoriasProductosID = 2,
        ProveedoresID = 1,
        Nombre = "Escapulario para Perros",
        Descripcion = "Escapulario con protección contra los malos espíritus.",
        Costo = 5.00m,
        Precio = 10.00m,
        Stock = 75,
        ImagenUrl = "https://via.placeholder.com/150",
        IsDeleted = false
    },
    new Productos
    {
        ProductosID = 23,
        CategoriasProductosID = 1,
        ProveedoresID = 1,
        Nombre = "Antibióticos para Mascotas",
        Descripcion = "Antibióticos de amplio espectro para infecciones en mascotas.",
        Costo = 35.00m,
        Precio = 55.00m,
        Stock = 5,
        ImagenUrl = "https://via.placeholder.com/150",
        IsDeleted = false
    },
    new Productos
    {
        ProductosID = 24,
        CategoriasProductosID = 2,
        ProveedoresID = 1,
        Nombre = "Cápsulas de Omega 3 para Mascotas",
        Descripcion = "Suplemento de Omega 3 para piel y pelaje saludable.",
        Costo = 15.00m,
        Precio = 25.00m,
        Stock = 30,
        ImagenUrl = "https://via.placeholder.com/150",
        IsDeleted = false
    },
    new Productos
    {
        ProductosID = 25,
        CategoriasProductosID = 1,
        ProveedoresID = 1,
        Nombre = "Suplemento Probiótico para Perros",
        Descripcion = "Mejora la salud digestiva y el sistema inmunológico.",
        Costo = 18.00m,
        Precio = 32.00m,
        Stock = 20,
        ImagenUrl = "https://via.placeholder.com/150",
        IsDeleted = false
    },
    new Productos
    {
        ProductosID = 26,
        CategoriasProductosID = 2,
        ProveedoresID = 1,
        Nombre = "Rascador de Cartón para Gato",
        Descripcion = "Rascador de cartón reciclable, atrae gatos por su olor.",
        Costo = 4.00m,
        Precio = 8.50m,
        Stock = 100,
        ImagenUrl = "https://via.placeholder.com/150",
        IsDeleted = false
    },
    new Productos
    {
        ProductosID = 27,
        CategoriasProductosID = 1,
        ProveedoresID = 1,
        Nombre = "Limpiador de Oídos para Perros",
        Descripcion = "Solución limpiadora para oídos de perros.",
        Costo = 7.00m,
        Precio = 15.00m,
        Stock = 45,
        ImagenUrl = "https://via.placeholder.com/150",
        IsDeleted = false
    },
    new Productos
    {
        ProductosID = 28,
        CategoriasProductosID = 2,
        ProveedoresID = 1,
        Nombre = "Peine Deshedding para Gato",
        Descripcion = "Reduce la caída del pelo en gatos de pelo largo.",
        Costo = 6.00m,
        Precio = 12.00m,
        Stock = 55,
        ImagenUrl = "https://via.placeholder.com/150",
        IsDeleted = false
    },
    new Productos
    {
        ProductosID = 29,
        CategoriasProductosID = 1,
        ProveedoresID = 1,
        Nombre = "Champú Antiinsectos para Mascotas",
        Descripcion = "Champú efectivo contra pulgas, garrapatas y mosquitos.",
        Costo = 11.00m,
        Precio = 22.00m,
        Stock = 35,
        ImagenUrl = "https://via.placeholder.com/150",
        IsDeleted = false
    },
    new Productos
    {
        ProductosID = 30,
        CategoriasProductosID = 2,
        ProveedoresID = 1,
        Nombre = "Bolsa de Transporte para Mascotas",
        Descripcion = "Bolsa transportadora cómoda y segura para mascotas pequeñas.",
        Costo = 20.00m,
        Precio = 35.00m,
        Stock = 15,
        ImagenUrl = "https://via.placeholder.com/150",
        IsDeleted = false
    },
    new Productos
    {
        ProductosID = 31,
        CategoriasProductosID = 1,
        ProveedoresID = 1,
        Nombre = "Alimento Balanceado para Perro Mayor",
        Descripcion = "Nutrición balanceada para perros mayores de 7 años.",
        Costo = 28.00m,
        Precio = 48.00m,
        Stock = 10,
        ImagenUrl = "https://via.placeholder.com/150",
        IsDeleted = false
    },
    new Productos
    {
        ProductosID = 32,
        CategoriasProductosID = 1,
        ProveedoresID = 1,
        Nombre = "Snack Dental para Perros",
        Descripcion = "Snack para ayudar a mantener los dientes limpios.",
        Costo = 4.00m,
        Precio = 7.50m,
        Stock = 85,
        ImagenUrl = "https://via.placeholder.com/150",
        IsDeleted = false
    },
    new Productos
    {
        ProductosID = 33,
        CategoriasProductosID = 2,
        ProveedoresID = 1,
        Nombre = "Transportin Doble Puerta",
        Descripcion = "Transportín resistente con doble puerta para fácil acceso.",
        Costo = 50.00m,
        Precio = 85.00m,
        Stock = 8,
        ImagenUrl = "https://via.placeholder.com/150",
        IsDeleted = false
    },
    new Productos
    {
        ProductosID = 34,
        CategoriasProductosID = 2,
        ProveedoresID = 1,
        Nombre = "Manta Térmica para Perros",
        Descripcion = "Manta que proporciona calor y confort a tu mascota.",
        Costo = 15.00m,
        Precio = 25.00m,
        Stock = 40,
        ImagenUrl = "https://via.placeholder.com/150",
        IsDeleted = false
    },
    new Productos
    {
        ProductosID = 35,
        CategoriasProductosID = 1,
        ProveedoresID = 1,
        Nombre = "Comida Carnivora para Gato",
        Descripcion = "Alta en proteínas, especial para gatos carnívoros.",
        Costo = 14.00m,
        Precio = 28.00m,
        Stock = 20,
        ImagenUrl = "https://via.placeholder.com/150",
        IsDeleted = false
    },
    new Productos
    {
        ProductosID = 36,
        CategoriasProductosID = 1,
        ProveedoresID = 1,
        Nombre = "Batido Nutricional para Mascotas",
        Descripcion = "Batido complemento alimenticio para mascotas.",
        Costo = 6.50m,
        Precio = 14.00m,
        Stock = 50,
        ImagenUrl = "https://via.placeholder.com/150",
        IsDeleted = false
    },
    new Productos
    {
        ProductosID = 37,
        CategoriasProductosID = 2,
        ProveedoresID = 1,
        Nombre = "Peine de Dientes Anchos",
        Descripcion = "Ideal para desenredar pelajes largos y gruesos.",
        Costo = 5.50m,
        Precio = 11.00m,
        Stock = 75,
        ImagenUrl = "https://via.placeholder.com/150",
        IsDeleted = false
    },
    new Productos
    {
        ProductosID = 38,
        CategoriasProductosID = 2,
        ProveedoresID = 1,
        Nombre = "Perfume para Mascotas",
        Descripcion = "Fragancia especial para el pelaje de tus mascotas.",
        Costo = 10.00m,
        Precio = 18.00m,
        Stock = 30,
        ImagenUrl = "https://via.placeholder.com/150",
        IsDeleted = false
    },
    new Productos
    {
        ProductosID = 39,
        CategoriasProductosID = 1,
        ProveedoresID = 1,
        Nombre = "Alimento Seco para Gato Esterilizado",
        Descripcion = "Especial para gatos esterilizados, control de peso.",
        Costo = 20.00m,
        Precio = 35.00m,
        Stock = 15,
        ImagenUrl = "https://via.placeholder.com/150",
        IsDeleted = false
    },
    new Productos
    {
        ProductosID = 40,
        CategoriasProductosID = 2,
        ProveedoresID = 1,
        Nombre = "Juguete de Plumas para Gato",
        Descripcion = "Juguete interactivo para estimular el ejercicio en gatos.",
        Costo = 3.00m,
        Precio = 7.00m,
        Stock = 95,
        ImagenUrl = "https://via.placeholder.com/150",
        IsDeleted = false
    },
    new Productos
    {
        ProductosID = 41,
        CategoriasProductosID = 1,
        ProveedoresID = 1,
        Nombre = "Suplemento de Calcio para Mascotas",
        Descripcion = "Fortalece los huesos y dientes de tu mascota.",
        Costo = 8.00m,
        Precio = 15.00m,
        Stock = 40,
        ImagenUrl = "https://via.placeholder.com/150",
        IsDeleted = false
    },
    new Productos
    {
        ProductosID = 42,
        CategoriasProductosID = 2,
        ProveedoresID = 1,
        Nombre = "Transportín Airline Approved",
        Descripcion = "Transportín aprobado por aerolíneas, seguro y cómodo.",
        Costo = 55.00m,
        Precio = 95.00m,
        Stock = 12,
        ImagenUrl = "https://via.placeholder.com/150",
        IsDeleted = false
    },
    new Productos
    {
        ProductosID = 43,
        CategoriasProductosID = 1,
        ProveedoresID = 1,
        Nombre = "Snack de Cordero para Perros",
        Descripcion = "Delicioso y saludable snack de cordero.",
        Costo = 4.00m,
        Precio = 8.00m,
        Stock = 85,
        ImagenUrl = "https://via.placeholder.com/150",
        IsDeleted = false
    },
    new Productos
    {
        ProductosID = 44,
        CategoriasProductosID = 2,
        ProveedoresID = 1,
        Nombre = "Cama ortopédica para Mascotas",
        Descripcion = "Cama ortopédica para el cuidado de las articulaciones.",
        Costo = 45.00m,
        Precio = 75.00m,
        Stock = 20,
        ImagenUrl = "https://via.placeholder.com/150",
        IsDeleted = false
    },
    new Productos
    {
        ProductosID = 45,
        CategoriasProductosID = 1,
        ProveedoresID = 1,
        Nombre = "Comida para Gato de Palmas",
        Descripcion = "Alimento natural y balanceado para gatos.",
        Costo = 18.00m,
        Precio = 30.00m,
        Stock = 15,
        ImagenUrl = "https://via.placeholder.com/150",
        IsDeleted = false
    },
    new Productos
    {
        ProductosID = 46,
        CategoriasProductosID = 2,
        ProveedoresID = 1,
        Nombre = "Bolsa de Transporte Impermeable",
        Descripcion = "Protege a tu mascota de la lluvia y la humedad.",
        Costo = 12.00m,
        Precio = 22.00m,
        Stock = 30,
        ImagenUrl = "https://via.placeholder.com/150",
        IsDeleted = false
    },
    new Productos
    {
        ProductosID = 47,
        CategoriasProductosID = 1,
        ProveedoresID = 1,
        Nombre = "Galletas de Arroz para Perros",
        Descripcion = "Snacks hipoalergénicos de arroz para perros sensibles.",
        Costo = 5.00m,
        Precio = 10.00m,
        Stock = 100,
        ImagenUrl = "https://via.placeholder.com/150",
        IsDeleted = false
    },
    new Productos
    {
        ProductosID = 48,
        CategoriasProductosID = 2,
        ProveedoresID = 1,
        Nombre = "Juguete para Perro con Comida Seca",
        Descripcion = "Juguete dispensador de comida para mantener a tu perro entretenido.",
        Costo = 10.50m,
        Precio = 19.99m,
        Stock = 25,
        ImagenUrl = "https://via.placeholder.com/150",
        IsDeleted = false
    },
    new Productos
    {
        ProductosID = 49,
        CategoriasProductosID = 1,
        ProveedoresID = 1,
        Nombre = "Pañales Desechables para Perros",
        Descripcion = "Pañales desechables para perros machos y hembras.",
        Costo = 8.00m,
        Precio = 15.00m,
        Stock = 60,
        ImagenUrl = "https://via.placeholder.com/150",
        IsDeleted = false
    },
    new Productos
    {
        ProductosID = 50,
        CategoriasProductosID = 2,
        ProveedoresID = 1,
        Nombre = "Correa de Mano para Perros Grande",
        Descripcion = "Correa resistente para perros de razas grandes.",
        Costo = 9.00m,
        Precio = 17.00m,
        Stock = 35,
        ImagenUrl = "https://via.placeholder.com/150",
        IsDeleted = false
    }
        };

        public static readonly List<ProductosInTabs> seedProductosInTabs = new()
        {
            new ProductosInTabs { ProductosInTabsID = 1, ProductosID = 1, VetasTabsID = 1, Orden = 1, IsDeleted = false },
            new ProductosInTabs { ProductosInTabsID = 2, ProductosID = 2, VetasTabsID = 1, Orden = 2, IsDeleted = false },
            new ProductosInTabs { ProductosInTabsID = 3, ProductosID = 3, VetasTabsID = 1, Orden = 3, IsDeleted = false },
            new ProductosInTabs { ProductosInTabsID = 4, ProductosID = 4, VetasTabsID = 1, Orden = 4, IsDeleted = false },
            new ProductosInTabs { ProductosInTabsID = 5, ProductosID = 5, VetasTabsID = 1, Orden = 5, IsDeleted = false },
            new ProductosInTabs { ProductosInTabsID = 6, ProductosID = 6, VetasTabsID = 2, Orden = 6, IsDeleted = false },
            new ProductosInTabs { ProductosInTabsID = 7, ProductosID = 7, VetasTabsID = 2, Orden = 7, IsDeleted = false },
            new ProductosInTabs { ProductosInTabsID = 8, ProductosID = 8, VetasTabsID = 2, Orden = 8, IsDeleted = false },
            new ProductosInTabs { ProductosInTabsID = 9, ProductosID = 9, VetasTabsID = 2, Orden = 9, IsDeleted = false },
            new ProductosInTabs { ProductosInTabsID = 10, ProductosID = 10, VetasTabsID = 2, Orden = 10, IsDeleted = false },
            new ProductosInTabs { ProductosInTabsID = 11, ProductosID = 11, VetasTabsID = 3, Orden = 11, IsDeleted = false },
            new ProductosInTabs { ProductosInTabsID = 12, ProductosID = 12, VetasTabsID = 3, Orden = 12, IsDeleted = false },
            new ProductosInTabs { ProductosInTabsID = 13, ProductosID = 13, VetasTabsID = 3, Orden = 13, IsDeleted = false },
            new ProductosInTabs { ProductosInTabsID = 14, ProductosID = 14, VetasTabsID = 3, Orden = 14, IsDeleted = false },
            new ProductosInTabs { ProductosInTabsID = 15, ProductosID = 15, VetasTabsID = 3, Orden = 15, IsDeleted = false },
            new ProductosInTabs { ProductosInTabsID = 16, ProductosID = 16, VetasTabsID = 4, Orden = 16, IsDeleted = false },
            new ProductosInTabs { ProductosInTabsID = 17, ProductosID = 17, VetasTabsID = 4, Orden = 17, IsDeleted = false },
            new ProductosInTabs { ProductosInTabsID = 18, ProductosID = 18, VetasTabsID = 4, Orden = 18, IsDeleted = false },
            new ProductosInTabs { ProductosInTabsID = 19, ProductosID = 19, VetasTabsID = 4, Orden = 19, IsDeleted = false },
            new ProductosInTabs { ProductosInTabsID = 20, ProductosID = 20, VetasTabsID = 4, Orden = 20, IsDeleted = false },
            new ProductosInTabs { ProductosInTabsID = 21, ProductosID = 21, VetasTabsID = 5, Orden = 21, IsDeleted = false },
            new ProductosInTabs { ProductosInTabsID = 22, ProductosID = 22, VetasTabsID = 5, Orden = 22, IsDeleted = false },
            new ProductosInTabs { ProductosInTabsID = 23, ProductosID = 23, VetasTabsID = 5, Orden = 23, IsDeleted = false },
            new ProductosInTabs { ProductosInTabsID = 24, ProductosID = 24, VetasTabsID = 5, Orden = 24, IsDeleted = false },
            new ProductosInTabs { ProductosInTabsID = 25, ProductosID = 25, VetasTabsID = 5, Orden = 25, IsDeleted = false },
            new ProductosInTabs { ProductosInTabsID = 26, ProductosID = 26, VetasTabsID = 1, Orden = 26, IsDeleted = false },
            new ProductosInTabs { ProductosInTabsID = 27, ProductosID = 27, VetasTabsID = 1, Orden = 27, IsDeleted = false },
            new ProductosInTabs { ProductosInTabsID = 28, ProductosID = 28, VetasTabsID = 1, Orden = 28, IsDeleted = false },
            new ProductosInTabs { ProductosInTabsID = 29, ProductosID = 29, VetasTabsID = 1, Orden = 29, IsDeleted = false },
            new ProductosInTabs { ProductosInTabsID = 30, ProductosID = 30, VetasTabsID = 1, Orden = 30, IsDeleted = false },
            new ProductosInTabs { ProductosInTabsID = 31, ProductosID = 31, VetasTabsID = 2, Orden = 31, IsDeleted = false },
            new ProductosInTabs { ProductosInTabsID = 32, ProductosID = 32, VetasTabsID = 2, Orden = 32, IsDeleted = false },
            new ProductosInTabs { ProductosInTabsID = 33, ProductosID = 33, VetasTabsID = 2, Orden = 33, IsDeleted = false },
            new ProductosInTabs { ProductosInTabsID = 34, ProductosID = 34, VetasTabsID = 2, Orden = 34, IsDeleted = false },
            new ProductosInTabs { ProductosInTabsID = 35, ProductosID = 35, VetasTabsID = 2, Orden = 35, IsDeleted = false },
            new ProductosInTabs { ProductosInTabsID = 36, ProductosID = 36, VetasTabsID = 3, Orden = 36, IsDeleted = false },
            new ProductosInTabs { ProductosInTabsID = 37, ProductosID = 37, VetasTabsID = 3, Orden = 37, IsDeleted = false },
            new ProductosInTabs { ProductosInTabsID = 38, ProductosID = 38, VetasTabsID = 3, Orden = 38, IsDeleted = false },
            new ProductosInTabs { ProductosInTabsID = 39, ProductosID = 39, VetasTabsID = 3, Orden = 39, IsDeleted = false },
            new ProductosInTabs { ProductosInTabsID = 40, ProductosID = 40, VetasTabsID = 3, Orden = 40, IsDeleted = false },
            new ProductosInTabs { ProductosInTabsID = 41, ProductosID = 41, VetasTabsID = 4, Orden = 41, IsDeleted = false },
            new ProductosInTabs { ProductosInTabsID = 42, ProductosID = 42, VetasTabsID = 4, Orden = 42, IsDeleted = false },
            new ProductosInTabs { ProductosInTabsID = 43, ProductosID = 43, VetasTabsID = 4, Orden = 43, IsDeleted = false },
            new ProductosInTabs { ProductosInTabsID = 44, ProductosID = 44, VetasTabsID = 4, Orden = 44, IsDeleted = false },
            new ProductosInTabs { ProductosInTabsID = 45, ProductosID = 45, VetasTabsID = 4, Orden = 45, IsDeleted = false },
            new ProductosInTabs { ProductosInTabsID = 46, ProductosID = 46, VetasTabsID = 5, Orden = 46, IsDeleted = false },
            new ProductosInTabs { ProductosInTabsID = 47, ProductosID = 47, VetasTabsID = 5, Orden = 47, IsDeleted = false },
            new ProductosInTabs { ProductosInTabsID = 48, ProductosID = 48, VetasTabsID = 5, Orden = 48, IsDeleted = false },
            new ProductosInTabs { ProductosInTabsID = 49, ProductosID = 49, VetasTabsID = 5, Orden = 49, IsDeleted = false },
            new ProductosInTabs { ProductosInTabsID = 50, ProductosID = 50, VetasTabsID = 5, Orden = 50, IsDeleted = false }
        };
        
        public static readonly List<VetasTabs> seedVetasTabs = new()
        {
            new VetasTabs { VetasTabsID = 1, Color = "#FF5733", Icono = "home", Nombre = "Inicio", Orden = 1, IsDeleted = false },
            new VetasTabs { VetasTabsID = 2, Color = "#33FF57", Icono = "paw", Nombre = "Mascotas", Orden = 2, IsDeleted = false },
            new VetasTabs { VetasTabsID = 3, Color = "#3357FF", Icono = "cart", Nombre = "Productos", Orden = 3, IsDeleted = false },
            new VetasTabs { VetasTabsID = 4, Color = "#F3FF33", Icono = "star", Nombre = "Favoritos", Orden = 4, IsDeleted = false },
            new VetasTabs { VetasTabsID = 5, Color = "#FF33F3", Icono = "gift", Nombre = "Ofertas", Orden = 5, IsDeleted = false }

        };
        
        public static readonly List<MascotasPersonas> seedMascotasPersonas = new()
        {
            new MascotasPersonas { MascotasPersonasID = 1, PersonasID = 2, RazasID = 1, Nombre = "Toby", Sexo = 'm', IsDeleted = false },
            new MascotasPersonas { MascotasPersonasID = 2, PersonasID = 3, RazasID = 2, Nombre = "Bella", Sexo = 'f', IsDeleted = false },
            new MascotasPersonas { MascotasPersonasID = 3, PersonasID = 4, RazasID = 3, Nombre = "Tom", Sexo = 'm', IsDeleted = false },
            new MascotasPersonas { MascotasPersonasID = 4, PersonasID = 5, RazasID = 1, Nombre = "Lola", Sexo = 'f', IsDeleted = false },
            new MascotasPersonas { MascotasPersonasID = 5, PersonasID = 6, RazasID = 2, Nombre = "Rex", Sexo = 'm', IsDeleted = false },
            new MascotasPersonas { MascotasPersonasID = 6, PersonasID = 7, RazasID = 3, Nombre = "Nina", Sexo = 'f', IsDeleted = false },
            new MascotasPersonas { MascotasPersonasID = 7, PersonasID = 8, RazasID = 1, Nombre = "Leo", Sexo = 'm', IsDeleted = false },
            new MascotasPersonas { MascotasPersonasID = 8, PersonasID = 9, RazasID = 2, Nombre = "Maya", Sexo = 'f', IsDeleted = false },
            new MascotasPersonas { MascotasPersonasID = 9, PersonasID = 10, RazasID = 3, Nombre = "Simón", Sexo = 'm', IsDeleted = false },
            new MascotasPersonas { MascotasPersonasID = 10, PersonasID = 11, RazasID = 1, Nombre = "Daisy", Sexo = 'f', IsDeleted = false }

        };
       
        public static readonly List<SolicitudesAdopciones> seedSolicitudesAdopciones = new()
        {
            new SolicitudesAdopciones { SolicitudesAdopcionesID = 1, PersonasID = 2, MascotasAdopcionID = 11, EstadoSolicitudID = 1, IsDeleted = false },
            new SolicitudesAdopciones { SolicitudesAdopcionesID = 2, PersonasID = 3, MascotasAdopcionID = 12, EstadoSolicitudID = 2, IsDeleted = false },
            new SolicitudesAdopciones { SolicitudesAdopcionesID = 3, PersonasID = 4, MascotasAdopcionID = 13, EstadoSolicitudID = 3, IsDeleted = false },
            new SolicitudesAdopciones { SolicitudesAdopcionesID = 4, PersonasID = 5, MascotasAdopcionID = 14, EstadoSolicitudID = 4, IsDeleted = false },
            new SolicitudesAdopciones { SolicitudesAdopcionesID = 5, PersonasID = 6, MascotasAdopcionID = 15, EstadoSolicitudID = 1, IsDeleted = false },
            new SolicitudesAdopciones { SolicitudesAdopcionesID = 6, PersonasID = 7, MascotasAdopcionID = 16, EstadoSolicitudID = 2, IsDeleted = false },
            new SolicitudesAdopciones { SolicitudesAdopcionesID = 7, PersonasID = 8, MascotasAdopcionID = 17, EstadoSolicitudID = 3, IsDeleted = false },
            new SolicitudesAdopciones { SolicitudesAdopcionesID = 8, PersonasID = 9, MascotasAdopcionID = 18, EstadoSolicitudID = 4, IsDeleted = false },
            new SolicitudesAdopciones { SolicitudesAdopcionesID = 9, PersonasID = 10, MascotasAdopcionID = 19, EstadoSolicitudID = 1, IsDeleted = false },
            new SolicitudesAdopciones { SolicitudesAdopcionesID = 10, PersonasID = 11, MascotasAdopcionID = 20, EstadoSolicitudID = 2, IsDeleted = false }
        };
    }
}
