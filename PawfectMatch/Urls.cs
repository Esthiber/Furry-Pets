
namespace PawfectMatch
{
    public static class Urls
    {
        public static class Mascotas
        {
            public const string Index = "/mascotas";
            public const string Crear = "/mascotas/crear";
            public const string Editar = "/mascotas/editar/";
            public const string Detalle = "/mascotas/";
        }

        public static class Solicitudes
        {
            public const string Index = "/solicitudes";
            public const string Crear = "/solicitudes/crear";
            public const string Editar = "/solicitudes/Editar";
            public const string Detalle = "/solicitudes/id/";
        }

        public static class Account
        {
            public const string Index = "/usuarios";
            public const string Login = "Account/Login";
            public const string Register = "Account/Register";
            public const string Manage = "Account/Manage";
        }

        public static class Configuracion
        {
            public const string Index = "/configuracion";
            public const string Reportes = "/configuracion/reportes";
            public const string Backup = "/configuracion/backup";
        }
    }
}
