using Blazored.Toast;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PawfectMatch.Components;
using PawfectMatch.Components.Account;
using PawfectMatch.Data;
using PawfectMatch.Services;
using PawfectMatch.Services._Mascotas;
using PawfectMatch.Services._Presentacion;
using PawfectMatch.Services._Solicitudes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
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

// Estos servicios. 
builder.Services.AddScoped<RazasService>();
builder.Services.AddScoped<CategoriasServices>();
builder.Services.AddScoped<MascotasService>();
builder.Services.AddScoped<HistorialAdopcionesService>();
builder.Services.AddScoped<SolicitudesAdopcionesService>();
builder.Services.AddScoped<AdoptantesService>();
builder.Services.AddScoped<CitasService>();
builder.Services.AddScoped<EstadosService>();
builder.Services.AddScoped<SexosService>();
builder.Services.AddScoped<EstadosSolicitudesService>();
builder.Services.AddScoped<RelacionSizesService>();
builder.Services.AddScoped<PresentacionesService>();
builder.Services.AddScoped<ServiciosService>();
builder.Services.AddScoped<SolicitudesServiciosService>();
builder.Services.AddScoped<SugerenciasService>();

builder.Services.AddBlazorBootstrap();
builder.Services.AddBlazoredToast();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();



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
