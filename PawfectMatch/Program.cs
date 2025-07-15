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
// Nota: Microsoft.AspNet.Identity es un paquete obsoleto y no deber�a ser necesario.
// Usamos Microsoft.AspNetCore.Identity.
#endregion

var builder = WebApplication.CreateBuilder(args);

#region Blazor & Core Services
/// <summary>
/// Configuraci�n de los servicios fundamentales de Blazor y del estado de autenticaci�n.
/// </summary>
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents(); // Habilita el renderizado interactivo del servidor para componentes.

builder.Services.AddCascadingAuthenticationState(); // Propaga el estado de autenticaci�n a trav�s del �rbol de componentes.
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
#endregion

#region Database Configuration
/// <summary>
/// Configuraci�n de la conexi�n a la base de datos y el DbContext.
/// Se utiliza AddDbContextFactory para permitir la creaci�n de instancias de DbContext
/// de forma segura en servicios con �mbito (scoped), especialmente en Blazor Server.
/// </summary>
var connectionString = builder.Configuration.GetConnectionString("SqlConStr")
    ?? throw new InvalidOperationException("Connection string 'SqlConStr' not found.");

builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter(); // Proporciona p�ginas de error detalladas para migraciones de EF.
#endregion

#region Identity & Authentication Configuration
/// <summary>
/// Configuraci�n centralizada de ASP.NET Core Identity.
/// - Se especifica la clase de usuario (ApplicationUser) y la clase de rol (IdentityRole).
/// - Se enlaza con Entity Framework Core (AddEntityFrameworkStores).
/// - Se registran los servicios clave como UserManager, RoleManager y SignInManager.
/// - Se configuran las cookies de autenticaci�n.
/// </summary>
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // Se permite el inicio de sesi�n sin confirmar el email.
    options.Password.RequireNonAlphanumeric = false; // Se simplifican los requisitos de contrase�a para el desarrollo.
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

// Registra un servicio de env�o de email que no hace nada, �til para desarrollo.
builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
#endregion

#region Authorization Policies Configuration
/// <summary>
/// Configuraci�n de las pol�ticas de autorizaci�n.
/// Utiliza la clase est�tica 'Permissions' para registrar din�micamente una pol�tica
/// para cada permiso definido en la aplicaci�n. Esto centraliza la gesti�n de permisos
/// y hace que el sistema sea m�s mantenible.
/// </summary>
builder.Services.AddAuthorization(options =>
{
    // Itera sobre todos los permisos definidos en la constante.
    foreach (var permission in Permissions.GetAllPermissions())
    {
        // Crea una pol�tica que requiere que el usuario tenga un claim con el mismo nombre que el permiso.
        options.AddPolicy(permission, policy =>
            policy.RequireClaim(permission, "true"));
    }
});
#endregion

#region Custom Application Services
/// <summary>
/// Inyecci�n de dependencias para todos los servicios de la l�gica de negocio de la aplicaci�n.
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
/// Configuraci�n del pipeline de procesamiento de solicitudes HTTP.
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
app.UseStaticFiles(); // Habilita el servicio de archivos est�ticos como CSS, JS, im�genes.
app.UseAntiforgery(); // Middleware de protecci�n contra ataques CSRF.

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// A�ade los endpoints necesarios para las p�ginas de Identity (Login, Register, etc.).
app.MapAdditionalIdentityEndpoints();
#endregion

app.Run();