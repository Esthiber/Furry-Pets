#region Usings
using Blazored.Toast;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PawfectMatch.Components;
using PawfectMatch.Components.Account;
using PawfectMatch.Constants;
using PawfectMatch.Data;
using PawfectMatch.Services;
// Nota: Microsoft.AspNet.Identity es un paquete obsoleto y no debería ser necesario.
// Usamos Microsoft.AspNetCore.Identity.
#endregion

var builder = WebApplication.CreateBuilder(args);

#region Blazor & Core Services
/// <summary>
/// Configuración de los servicios fundamentales de Blazor y del estado de autenticación.
/// </summary>
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents(); // Habilita el renderizado interactivo del servidor para componentes.

builder.Services.AddCascadingAuthenticationState(); // Propaga el estado de autenticación a través del árbol de componentes.
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
#endregion

#region Database Configuration
/// <summary>
/// Configuración de la conexión a la base de datos y el DbContext.
/// Se utiliza AddDbContextFactory para permitir la creación de instancias de DbContext
/// de forma segura en servicios con ámbito (scoped), especialmente en Blazor Server.
/// </summary>
var connectionString = builder.Configuration.GetConnectionString("SqlConStr")
    ?? throw new InvalidOperationException("Connection string 'SqlConStr' not found.");

builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter(); // Proporciona páginas de error detalladas para migraciones de EF.
#endregion

#region Identity & Authentication Configuration
/// <summary>
/// Configuración centralizada de ASP.NET Core Identity.
/// - Se especifica la clase de usuario (ApplicationUser) y la clase de rol (IdentityRole).
/// - Se enlaza con Entity Framework Core (AddEntityFrameworkStores).
/// - Se registran los servicios clave como UserManager, RoleManager y SignInManager.
/// - Se configuran las cookies de autenticación.
/// </summary>
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // Se permite el inicio de sesión sin confirmar el email.
    options.Password.RequireNonAlphanumeric = false; // Se simplifican los requisitos de contraseña para el desarrollo.
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

// Registra un servicio de envío de email que no hace nada, útil para desarrollo.
builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
#endregion

#region Authorization Policies Configuration
/// <summary>
/// Configuración de las políticas de autorización.
/// Utiliza la clase estática 'Permissions' para registrar dinámicamente una política
/// para cada permiso definido en la aplicación. Esto centraliza la gestión de permisos
/// y hace que el sistema sea más mantenible.
/// </summary>
builder.Services.AddAuthorization(options =>
{
    // Itera sobre todos los permisos definidos en la constante.
    foreach (var permission in Permissions.GetAllPermissions())
    {
        // Crea una política que requiere que el usuario tenga un claim con el mismo nombre que el permiso.
        options.AddPolicy(permission, policy =>
            policy.RequireClaim(permission, "true"));
    }
});
#endregion

#region Custom Application Services
/// <summary>
/// Inyección de dependencias para todos los servicios de la lógica de negocio de la aplicación.
/// Todos se registran como 'Scoped', lo que significa que se crea una nueva instancia por cada solicitud de cliente.
/// </summary>
builder.Services.AddScoped<AdoptantesDetallesService>();
builder.Services.AddScoped<CategoriasProductosService>();
builder.Services.AddScoped<CitasService>();
builder.Services.AddScoped<ConfiguracionEmpresaService>();
builder.Services.AddScoped<DetallesFacturasService>();
builder.Services.AddScoped<EspeciesService>();
builder.Services.AddScoped<EstadoMascotaService>();
builder.Services.AddScoped<EstadosCitasService>();
builder.Services.AddScoped<EstadoSolicitudService>();
builder.Services.AddScoped<EstadosPagosService>();
builder.Services.AddScoped<FacturasService>();
builder.Services.AddScoped<HistorialAdopcionesService>();
builder.Services.AddScoped<HistoriasClinicasService>();
builder.Services.AddScoped<MascotasAdopcionService>();
builder.Services.AddScoped<MascotasPersonasService>();
builder.Services.AddScoped<PagosService>();
builder.Services.AddScoped<PersonasRolesService>();
builder.Services.AddScoped<PersonasService>();
builder.Services.AddScoped<ProductosService>();
builder.Services.AddScoped<ProveedoresService>();
builder.Services.AddScoped<RazasService>();
builder.Services.AddScoped<RelacionSizeService>();
builder.Services.AddScoped<SeguimientoMascotasService>();
builder.Services.AddScoped<ServiciosService>();
builder.Services.AddScoped<SolicitudesAdopcionesService>();
builder.Services.AddScoped<TiposItemsService>();
builder.Services.AddScoped<TiposServiciosService>();
builder.Services.AddScoped<TipoViviendasService>();
builder.Services.AddScoped<VetasTabsService>();
builder.Services.AddScoped<ProductosInTabsService>();

// Registra servicios de componentes de UI de terceros.
builder.Services.AddBlazorBootstrap();
builder.Services.AddBlazoredToast();
#endregion

var app = builder.Build();

#region HTTP Request Pipeline Configuration
/// <summary>
/// Configuración del pipeline de procesamiento de solicitudes HTTP.
/// El orden de los middlewares es importante.
/// </summary>
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Habilita el servicio de archivos estáticos como CSS, JS, imágenes.
app.UseAntiforgery(); // Middleware de protección contra ataques CSRF.

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Añade los endpoints necesarios para las páginas de Identity (Login, Register, etc.).
app.MapAdditionalIdentityEndpoints();
#endregion

app.Run();