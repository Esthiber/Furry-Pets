using Blazored.Toast;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PawfectMatch.Components;
using PawfectMatch.Components.Account;
using PawfectMatch.Constants;
using PawfectMatch.Data;
using PawfectMatch.Models;
using PawfectMatch.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<Microsoft.AspNetCore.Identity.UserManager<ApplicationUser>>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("SqlConStr") ?? throw new InvalidOperationException("Connection string 'SqlConStr' not found.");
builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

#region Injeccion de Servicios

builder.Services.AddScoped<AdoptantesDetallesService>();
builder.Services.AddScoped<CategoriasProductosService>();
builder.Services.AddScoped<CitasService>();
builder.Services.AddScoped<ConfiguracionEmpresaService>();
builder.Services.AddScoped<DetallesFacturasService>();
builder.Services.AddScoped<DiapositivasService>();
builder.Services.AddScoped<EspeciesService>();
builder.Services.AddScoped<EstadoMascotaService>();
builder.Services.AddScoped<EstadosCitasService>();
builder.Services.AddScoped<EstadoSolicitudService>();
builder.Services.AddScoped<EstadosPagosService>();
builder.Services.AddScoped<FacturasService>();
builder.Services.AddScoped<HistorialAdopcionesService>();
builder.Services.AddScoped<HistoriasClinicasService>();
builder.Services.AddScoped<MascotasAdopcionService>();          // Ya estaba, OK
builder.Services.AddScoped<MascotasPersonasService>();         // Ya estaba, OK
builder.Services.AddScoped<PagosService>();
builder.Services.AddScoped<PersonasRolesService>();
builder.Services.AddScoped<PersonasService>();
builder.Services.AddScoped<PresentacionesDiapositivasService>();
builder.Services.AddScoped<PresentacionesService>();
builder.Services.AddScoped<ProductosService>();
builder.Services.AddScoped<ProveedoresService>();
builder.Services.AddScoped<RazasService>();
builder.Services.AddScoped<RelacionSizeService>();
builder.Services.AddScoped<SeguimientoMascotasService>();
builder.Services.AddScoped<ServiciosService>();
builder.Services.AddScoped<SolicitudesAdopcionesService>();
builder.Services.AddScoped<SugerenciasService>();
builder.Services.AddScoped<TiposItemsService>();
builder.Services.AddScoped<TiposServiciosService>();
builder.Services.AddScoped<TipoViviendasService>();
builder.Services.AddScoped<VetasTabsService>();
builder.Services.AddScoped<ProductosInTabsService>();

builder.Services.AddBlazorBootstrap();
builder.Services.AddBlazoredToast();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

#endregion

// --- CONFIGURACION DE AUTORIZACION ACTUALIZADA ---
builder.Services.AddAuthorization(options =>
{
    foreach (var permission in Permissions.GetAllPermissions())
    {
        options.AddPolicy(permission, policy =>
            policy.RequireClaim(permission, "true"));
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
