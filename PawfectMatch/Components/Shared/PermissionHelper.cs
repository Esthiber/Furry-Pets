using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace PawfectMatch.Components.Shared
{
    /// <summary>
    /// Componente helper para verificar permisos del usuario actual.
    /// Simplifica la verificación de claims/permisos en los componentes Blazor.
    /// </summary>
    public class PermissionHelper : ComponentBase
    {
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        /// <summary>
        /// Verifica si el usuario actual tiene un permiso específico.
        /// </summary>
        /// <param name="permission">El permiso a verificar (ej: Permissions.Users.Edit)</param>
        /// <returns>True si el usuario tiene el permiso, false en caso contrario</returns>
        public async Task<bool> HasPermissionAsync(string permission)
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            return authState.User.HasClaim(permission, "true");
        }

        /// <summary>
        /// Verifica si el usuario actual tiene uno o más de los permisos especificados.
        /// </summary>
        /// <param name="permissions">Array de permisos a verificar</param>
        /// <returns>True si el usuario tiene al menos uno de los permisos</returns>
        public async Task<bool> HasAnyPermissionAsync(params string[] permissions)
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            return permissions.Any(permission => authState.User.HasClaim(permission, "true"));
        }

        /// <summary>
        /// Verifica si el usuario actual tiene todos los permisos especificados.
        /// </summary>
        /// <param name="permissions">Array de permisos a verificar</param>
        /// <returns>True si el usuario tiene todos los permisos</returns>
        public async Task<bool> HasAllPermissionsAsync(params string[] permissions)
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            return permissions.All(permission => authState.User.HasClaim(permission, "true"));
        }

        /// <summary>
        /// Verifica si el usuario actual tiene un rol específico.
        /// </summary>
        /// <param name="role">El rol a verificar (ej: "Admin", "User")</param>
        /// <returns>True si el usuario tiene el rol</returns>
        public async Task<bool> IsInRoleAsync(string role)
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            return authState.User.IsInRole(role);
        }

        /// <summary>
        /// Obtiene el usuario actual.
        /// </summary>
        /// <returns>ClaimsPrincipal del usuario actual</returns>
        public async Task<ClaimsPrincipal> GetCurrentUserAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            return authState.User;
        }

        /// <summary>
        /// Obtiene el ID del usuario actual.
        /// </summary>
        /// <returns>ID del usuario o null si no está autenticado</returns>
        public async Task<string?> GetCurrentUserIdAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            return authState.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        /// <summary>
        /// Verifica si el usuario actual está autenticado.
        /// </summary>
        /// <returns>True si está autenticado</returns>
        public async Task<bool> IsAuthenticatedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            return authState.User.Identity?.IsAuthenticated ?? false;
        }
    }
}