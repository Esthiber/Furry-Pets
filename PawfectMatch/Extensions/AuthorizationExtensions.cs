using Microsoft.AspNetCore.Authorization;
using PawfectMatch.Constants;

namespace PawfectMatch.Extensions
{
    /// <summary>
    /// Extensiones para configurar autorización y permisos en la aplicación.
    /// </summary>
    public static class AuthorizationExtensions
    {
        /// <summary>
        /// Configura todas las políticas de autorización basadas en los permisos definidos.
        /// </summary>
        /// <param name="options">Opciones de autorización</param>
        public static void ConfigurePermissionPolicies(this AuthorizationOptions options)
        {
            // Registrar dinámicamente todas las políticas basadas en permisos
            var allPermissions = Permissions.GetAllPermissions();
            
            foreach (var permission in allPermissions)
            {
                options.AddPolicy(permission, policy =>
                    policy.RequireClaim(permission, "true"));
            }

            // Políticas adicionales combinadas
            options.AddPolicy("AdminOrUserManager", policy =>
                policy.RequireAssertion(context =>
                    context.User.IsInRole("Admin") ||
                    context.User.HasClaim(Permissions.Users.ManageRoles, "true")));

            options.AddPolicy("CanManageProducts", policy =>
                policy.RequireAssertion(context =>
                    context.User.HasClaim(Permissions.Products.Create, "true") ||
                    context.User.HasClaim(Permissions.Products.Edit, "true") ||
                    context.User.HasClaim(Permissions.Products.Delete, "true")));

            options.AddPolicy("CanManagePets", policy =>
                policy.RequireAssertion(context =>
                    context.User.HasClaim(Permissions.Pets.Create, "true") ||
                    context.User.HasClaim(Permissions.Pets.Edit, "true") ||
                    context.User.HasClaim(Permissions.Pets.Delete, "true")));
        }

        /// <summary>
        /// Configura políticas de autorización para roles básicos.
        /// </summary>
        /// <param name="options">Opciones de autorización</param>
        public static void ConfigureRolePolicies(this AuthorizationOptions options)
        {
            options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
            options.AddPolicy("UserOrAdmin", policy => policy.RequireRole("User", "Admin"));
            options.AddPolicy("AuthenticatedUser", policy => policy.RequireAuthenticatedUser());
        }
    }
}