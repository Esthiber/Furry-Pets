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
                    allPermissions.Add(permission.GetValue(null).ToString());
                }
            }
            return allPermissions;
        }

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

        public static class Users
        {
            public const string View = "Permissions.Users.View";
            public const string Create = "Permissions.Users.Create";
            public const string Edit = "Permissions.Users.Edit";
            public const string Delete = "Permissions.Users.Delete";
            public const string ViewDetails = "Permissions.Users.ViewDetails";
            public const string ManageRoles = "Permissions.Users.ManageRoles";
        }

        public static class Products
        {
            public const string View = "Permissions.Products.View";
            public const string Create = "Permissions.Products.Create";
            public const string Edit = "Permissions.Products.Edit";
            public const string Delete = "Permissions.Products.Delete";
            public const string ViewDetails = "Permissions.Products.ViewDetails";
            public const string ManageCategories = "Permissions.Products.ManageCategories";
        }
    }
}