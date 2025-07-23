namespace PawfectMatch.Constants
{
    /// <summary>
    /// Centraliza las rutas (URLs) de navegación de la aplicación PawfectMatch.
    /// Utiliza estas constantes en lugar de rutas hardcodeadas para facilitar el mantenimiento y evitar errores.
    /// Agrupadas por área funcional.
    /// </summary>
    public static class Urls
    {
        /// <summary>
        /// Rutas relacionadas con la gestión de mascotas para adopción.
        /// </summary>
        public static class Mascotas
        {
            public const string Index = "/mascotas";
            public const string Crear = "/mascotas/crear";
            public const string Editar = "/mascotas/editar/"; // Requiere ID al final
            public const string Detalle = "/mascotas/"; // Requiere ID al final
            public const string Catalogo = "/catalogo";
            public const string IndexAdopcion = "/mascotasadopcion";
        }

        /// <summary>
        /// Rutas para la gestión de solicitudes de adopción.
        /// </summary>
        public static class Solicitudes
        {
            public const string Index = "/solicitudes";
            public const string Crear = "/solicitudes/crear";
            public const string Editar = "/solicitudes/editar/"; // Requiere ID al final
            public const string Detalle = "/solicitudes/id/"; // Requiere ID al final
        }

        /// <summary>
        /// Rutas de autenticación y gestión de usuarios.
        /// </summary>
        public static class Account
        {
            public const string Index = "/usuarios";
            public const string Login = "/account/login";
            public const string Register = "/account/register";
            public const string Manage = "/account/manage";
            public const string UserManagement = "/user-management";
        }

        /// <summary>
        /// Rutas de configuración y administración.
        /// </summary>
        public static class Configuracion
        {
            public const string Index = "/configuracion";
            public const string Reportes = "/configuracion/reportes";
            public const string Backup = "/configuracion/backup";
        }

        /// <summary>
        /// Rutas para el punto de venta y ventas de productos.
        /// </summary>
        public static class Ventas
        {
            public const string Index = "/ventas";
            public const string CrearProducto = "/ventas/crear";
            public const string CrearTab = "/ventas/crear";
            public const string Editar = "/ventas/editar/"; // Requiere ID al final
            public const string Detalle = "/ventas/detalle/"; // Requiere ID al final
            public const string Productos = "/ventas/productos";
            public const string Tabs = "/tabs-productos";
            public const string POS = "/pos";
        }

        /// <summary>
        /// Rutas para sugerencias y feedback.
        /// </summary>
        public static class Sugerencias
        {
            public const string Index = "/sugerencias";
            public const string Crear = "/sugerencias/crear";
            public const string Detalle = "/sugerencias/detalle/"; // Requiere ID al final
        }

        /// <summary>
        /// Rutas informativas y de contenido institucional.
        /// </summary>
        public static class Informacion
        {
            public const string Nosotros = "/Nosotros";
            public const string Contacto = "/Contacto";
        }

        /// <summary>
        /// Rutas para la gestión de adoptantes.
        /// </summary>
        public static class Adoptantes
        {
            public const string Index = "/adoptantes";
        }

        /// <summary>
        /// Rutas de administración y configuración avanzada.
        /// </summary>
        public static class Administracion
        {
            public const string Panel = "/admin";
            public const string Settings = "/settings";
        }

        /// <summary>
        /// Rutas para el historial de adopciones.
        /// </summary>
        public static class Historial
        {
            public const string Index = "/historial";
            public const string Details = "/historial/details/"; // Requiere ID al final
        }

        /// <summary>
        /// Rutas de detalles (detalle por ID, para entidades principales).
        /// </summary>
        public static class Detalles
        {
            public const string Solicitud = "/Details/Solicitudes/"; // Requiere ID al final
            // Agrega más detalles si se usan en la app
        }
    }
}
