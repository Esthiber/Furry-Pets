namespace PawfectMatch.Constants
{
    public static class Permissions
    {
        // Método de ayuda para obtener todos los permisos (explicado más abajo)
        public static List<string> GetAllPermissions()
        {
            var allPermissions = new List<string>();
            var modules = typeof(Permissions).GetNestedTypes(); // Obtiene las clases anidadas: Pets, Users, Products

            foreach (var module in modules)
            {
                var permissions = module.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
                foreach (var permission in permissions)
                {
                    allPermissions.Add(permission.GetValue(null)!.ToString()!);
                }
            }
            return allPermissions;
        }

        // Mascotas y Adopciones
        public static class Pets
        {
            public const string View = "Permissions.Pets.View";
            public const string Create = "Permissions.Pets.Create";
            public const string Edit = "Permissions.Pets.Edit";
            public const string Delete = "Permissions.Pets.Delete";
            public const string ViewDetails = "Permissions.Pets.ViewDetails";
            public const string Adopt = "Permissions.Pets.Adopt";
            public const string ViewAdoptions = "Permissions.Pets.ViewAdoptions";
        }

        // Gestión de Usuarios y Roles
        public static class Users
        {
            public const string View = "Permissions.Users.View";
            public const string Create = "Permissions.Users.Create";
            public const string Edit = "Permissions.Users.Edit";
            public const string Delete = "Permissions.Users.Delete";
            public const string ViewDetails = "Permissions.Users.ViewDetails";
            public const string ManageRoles = "Permissions.Users.ManageRoles";
            public const string ManageClaims = "Permissions.Users.ManageClaims";
        }

        // Productos y Categorías
        public static class Products
        {
            public const string View = "Permissions.Products.View";
            public const string Create = "Permissions.Products.Create";
            public const string Edit = "Permissions.Products.Edit";
            public const string Delete = "Permissions.Products.Delete";
            public const string ViewDetails = "Permissions.Products.ViewDetails";
            public const string ManageCategories = "Permissions.Products.ManageCategories";
        }

        // Ventas y Punto de Venta
        public static class Sales
        {
            public const string View = "Permissions.Sales.View";
            public const string Create = "Permissions.Sales.Create";
            public const string Edit = "Permissions.Sales.Edit";
            public const string Delete = "Permissions.Sales.Delete";
            public const string ViewDetails = "Permissions.Sales.ViewDetails";
            public const string ManagePOS = "Permissions.Sales.ManagePOS";
        }

        // Solicitudes de Adopción
        public static class AdoptionRequests
        {
            public const string View = "Permissions.AdoptionRequests.View";
            public const string Create = "Permissions.AdoptionRequests.Create";
            public const string Edit = "Permissions.AdoptionRequests.Edit";
            public const string Delete = "Permissions.AdoptionRequests.Delete";
            public const string ViewDetails = "Permissions.AdoptionRequests.ViewDetails";
            public const string Approve = "Permissions.AdoptionRequests.Approve";
            public const string Reject = "Permissions.AdoptionRequests.Reject";
        }

        // Reportes y Backups
        public static class Reports
        {
            public const string View = "Permissions.Reports.View";
            public const string Generate = "Permissions.Reports.Generate";
            public const string Export = "Permissions.Reports.Export";
        }
        public static class Backups
        {
            public const string View = "Permissions.Backups.View";
            public const string Create = "Permissions.Backups.Create";
            public const string Restore = "Permissions.Backups.Restore";
        }

        // Configuración y Administración
        public static class Configuracion
        {
            public const string View = "Permissions.Configuracion.View";
            public const string Edit = "Permissions.Configuracion.Edit";
            public const string ManageEmpresa = "Permissions.Configuracion.ManageEmpresa";
        }
        public static class Admin
        {
            public const string AccessPanel = "Permissions.Admin.AccessPanel";
            public const string ManageSettings = "Permissions.Admin.ManageSettings";
        }

        // Sugerencias y Feedback
        public static class Sugerencias
        {
            public const string View = "Permissions.Sugerencias.View";
            public const string Create = "Permissions.Sugerencias.Create";
            public const string Edit = "Permissions.Sugerencias.Edit";
            public const string Delete = "Permissions.Sugerencias.Delete";
            public const string ViewDetails = "Permissions.Sugerencias.ViewDetails";
        }

        // Historial de Adopciones
        public static class Historial
        {
            public const string View = "Permissions.Historial.View";
            public const string ViewDetails = "Permissions.Historial.ViewDetails";
            public const string PrintCertificate = "Permissions.Historial.PrintCertificate";
        }
    }
}