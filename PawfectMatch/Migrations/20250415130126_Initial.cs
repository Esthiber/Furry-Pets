using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PawfectMatch.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    CategoriaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.CategoriaId);
                });

            migrationBuilder.CreateTable(
                name: "Diapositivas",
                columns: table => new
                {
                    DiapositivaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsTituloLeftActive = table.Column<bool>(type: "bit", nullable: false),
                    Titulo_Left = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubTitulo_Left = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsTituloRightActive = table.Column<bool>(type: "bit", nullable: false),
                    Titulo_Right = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubTitulo_Right = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsButtonLefttActive = table.Column<bool>(type: "bit", nullable: false),
                    TextButton_Left = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkButton_Left = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsButtonRighttActive = table.Column<bool>(type: "bit", nullable: false),
                    TextButton_Right = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkButton_Right = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Orden = table.Column<int>(type: "int", nullable: false),
                    Animacion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diapositivas", x => x.DiapositivaId);
                });

            migrationBuilder.CreateTable(
                name: "Estados",
                columns: table => new
                {
                    EstadoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estados", x => x.EstadoId);
                });

            migrationBuilder.CreateTable(
                name: "EstadoSolicitudes",
                columns: table => new
                {
                    EstadoSolicitudId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoSolicitudes", x => x.EstadoSolicitudId);
                });

            migrationBuilder.CreateTable(
                name: "Presentaciones",
                columns: table => new
                {
                    PresentacionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EsActiva = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presentaciones", x => x.PresentacionId);
                });

            migrationBuilder.CreateTable(
                name: "RelacionSizes",
                columns: table => new
                {
                    RelacionSizeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelacionSizes", x => x.RelacionSizeId);
                });

            migrationBuilder.CreateTable(
                name: "Servicios",
                columns: table => new
                {
                    ServicioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Costo = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicios", x => x.ServicioId);
                });

            migrationBuilder.CreateTable(
                name: "Sexos",
                columns: table => new
                {
                    SexoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sexos", x => x.SexoId);
                });

            migrationBuilder.CreateTable(
                name: "Sugerencias",
                columns: table => new
                {
                    SugerenciaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserMail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sugerencias", x => x.SugerenciaId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Adoptantes",
                columns: table => new
                {
                    AdoptanteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ocupacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adoptantes", x => x.AdoptanteId);
                    table.ForeignKey(
                        name: "FK_Adoptantes_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Razas",
                columns: table => new
                {
                    RazaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoriaId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Razas", x => x.RazaId);
                    table.ForeignKey(
                        name: "FK_Razas_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "CategoriaId");
                });

            migrationBuilder.CreateTable(
                name: "PresentacionesDiapositivas",
                columns: table => new
                {
                    PresentacionDiapositivaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PresentacionId = table.Column<int>(type: "int", nullable: false),
                    DiapositivaId = table.Column<int>(type: "int", nullable: false),
                    Orden = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PresentacionesDiapositivas", x => x.PresentacionDiapositivaId);
                    table.ForeignKey(
                        name: "FK_PresentacionesDiapositivas_Diapositivas_DiapositivaId",
                        column: x => x.DiapositivaId,
                        principalTable: "Diapositivas",
                        principalColumn: "DiapositivaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PresentacionesDiapositivas_Presentaciones_PresentacionId",
                        column: x => x.PresentacionId,
                        principalTable: "Presentaciones",
                        principalColumn: "PresentacionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mascotas",
                columns: table => new
                {
                    MascotaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoriaId = table.Column<int>(type: "int", nullable: false),
                    RazaId = table.Column<int>(type: "int", nullable: false),
                    RelacionSizeId = table.Column<int>(type: "int", nullable: false),
                    EstadoId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tamano = table.Column<double>(type: "float", nullable: false),
                    FechaNacimiento = table.Column<DateOnly>(type: "date", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SexoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mascotas", x => x.MascotaId);
                    table.ForeignKey(
                        name: "FK_Mascotas_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "CategoriaId");
                    table.ForeignKey(
                        name: "FK_Mascotas_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "EstadoId");
                    table.ForeignKey(
                        name: "FK_Mascotas_RelacionSizes_RelacionSizeId",
                        column: x => x.RelacionSizeId,
                        principalTable: "RelacionSizes",
                        principalColumn: "RelacionSizeId");
                    table.ForeignKey(
                        name: "FK_Mascotas_Sexos_SexoId",
                        column: x => x.SexoId,
                        principalTable: "Sexos",
                        principalColumn: "SexoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Citas",
                columns: table => new
                {
                    CitaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdoptanteId = table.Column<int>(type: "int", nullable: false),
                    MascotaId = table.Column<int>(type: "int", nullable: false),
                    FechaHora = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citas", x => x.CitaId);
                    table.ForeignKey(
                        name: "FK_Citas_Adoptantes_AdoptanteId",
                        column: x => x.AdoptanteId,
                        principalTable: "Adoptantes",
                        principalColumn: "AdoptanteId");
                    table.ForeignKey(
                        name: "FK_Citas_Mascotas_MascotaId",
                        column: x => x.MascotaId,
                        principalTable: "Mascotas",
                        principalColumn: "MascotaId");
                });

            migrationBuilder.CreateTable(
                name: "HistorialAdopciones",
                columns: table => new
                {
                    HistorialAdopcioneId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdoptanteId = table.Column<int>(type: "int", nullable: false),
                    MascotaId = table.Column<int>(type: "int", nullable: false),
                    FechaAdopcion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notas = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialAdopciones", x => x.HistorialAdopcioneId);
                    table.ForeignKey(
                        name: "FK_HistorialAdopciones_Adoptantes_AdoptanteId",
                        column: x => x.AdoptanteId,
                        principalTable: "Adoptantes",
                        principalColumn: "AdoptanteId");
                    table.ForeignKey(
                        name: "FK_HistorialAdopciones_Mascotas_MascotaId",
                        column: x => x.MascotaId,
                        principalTable: "Mascotas",
                        principalColumn: "MascotaId");
                });

            migrationBuilder.CreateTable(
                name: "SolicitudesAdopciones",
                columns: table => new
                {
                    SolicitudAdopcionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdoptanteId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoSolicitudId = table.Column<int>(type: "int", nullable: false),
                    MascotaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudesAdopciones", x => x.SolicitudAdopcionId);
                    table.ForeignKey(
                        name: "FK_SolicitudesAdopciones_Adoptantes_AdoptanteId",
                        column: x => x.AdoptanteId,
                        principalTable: "Adoptantes",
                        principalColumn: "AdoptanteId");
                    table.ForeignKey(
                        name: "FK_SolicitudesAdopciones_EstadoSolicitudes_EstadoSolicitudId",
                        column: x => x.EstadoSolicitudId,
                        principalTable: "EstadoSolicitudes",
                        principalColumn: "EstadoSolicitudId");
                    table.ForeignKey(
                        name: "FK_SolicitudesAdopciones_Mascotas_MascotaId",
                        column: x => x.MascotaId,
                        principalTable: "Mascotas",
                        principalColumn: "MascotaId");
                });

            migrationBuilder.CreateTable(
                name: "SolicitudesServicios",
                columns: table => new
                {
                    SolicitudesServiciosId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SolicitudAdopcionId = table.Column<int>(type: "int", nullable: false),
                    ServicioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudesServicios", x => x.SolicitudesServiciosId);
                    table.ForeignKey(
                        name: "FK_SolicitudesServicios_Servicios_ServicioId",
                        column: x => x.ServicioId,
                        principalTable: "Servicios",
                        principalColumn: "ServicioId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolicitudesServicios_SolicitudesAdopciones_SolicitudAdopcionId",
                        column: x => x.SolicitudAdopcionId,
                        principalTable: "SolicitudesAdopciones",
                        principalColumn: "SolicitudAdopcionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", null, "Admin", "ADMIN" },
                    { "2", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "CategoriaId", "Nombre" },
                values: new object[,]
                {
                    { 1, "Perros" },
                    { 2, "Gatos" }
                });

            migrationBuilder.InsertData(
                table: "EstadoSolicitudes",
                columns: new[] { "EstadoSolicitudId", "Nombre" },
                values: new object[,]
                {
                    { 1, "Pendiente" },
                    { 2, "Aprobada" },
                    { 3, "Rechazada" }
                });

            migrationBuilder.InsertData(
                table: "Estados",
                columns: new[] { "EstadoId", "Nombre" },
                values: new object[,]
                {
                    { 1, "Disponible" },
                    { 2, "Adoptado" },
                    { 3, "No Disponible" }
                });

            migrationBuilder.InsertData(
                table: "RelacionSizes",
                columns: new[] { "RelacionSizeId", "Size" },
                values: new object[,]
                {
                    { 1, "Pequeño" },
                    { 2, "Mediano" },
                    { 3, "Grande" }
                });

            migrationBuilder.InsertData(
                table: "Servicios",
                columns: new[] { "ServicioId", "Costo", "Descripcion", "Nombre" },
                values: new object[,]
                {
                    { 1, 0.0, "Documentos que certifican la propiedad de la mascota.", "Documentos de Adopción" },
                    { 2, 500.0, "Servicio de vacunación para mascotas.", "Vacunación" },
                    { 3, 300.0, "Tratamiento para eliminar parásitos internos y externos.", "Desparasitación" },
                    { 4, 150.0, "Servicio de corte de uñas para mascotas pequeñas y grandes.", "Corte de Uñas" },
                    { 5, 800.0, "Consulta general con un veterinario calificado.", "Consulta Veterinaria" }
                });

            migrationBuilder.InsertData(
                table: "Sexos",
                columns: new[] { "SexoId", "Nombre" },
                values: new object[,]
                {
                    { 1, "Macho" },
                    { 2, "Hembra" }
                });

            migrationBuilder.InsertData(
                table: "Mascotas",
                columns: new[] { "MascotaId", "CategoriaId", "Descripcion", "EstadoId", "FechaNacimiento", "FotoUrl", "Nombre", "RazaId", "RelacionSizeId", "SexoId", "Tamano" },
                values: new object[,]
                {
                    { 1, 1, "Es grande es bonito es un perro loco.", 1, new DateOnly(2025, 4, 2), "https://media.istockphoto.com/id/1465311007/es/foto/un-perro-peque%C3%B1o-sonr%C3%ADe-al-due%C3%B1o-peque%C3%B1as-mordeduras-de-mascotas-peligroso-terrier-de-juguete.jpg?s=612x612&w=0&k=20&c=nZzhW0piLl7oIrielT9JA7vAcXqaepnDhCggxD7JZ0I=", "Felipe", 5, 1, 1, 25.0 },
                    { 2, 1, "Es un perrito muy cariñoso y con mucha energia", 2, new DateOnly(2022, 8, 8), "https://th.bing.com/th/id/OIP.TfniUPx7NqEggeKV9APOZgHaE8?rs=1&pid=ImgDetMain", "Milo", 1, 3, 1, 18.0 },
                    { 3, 1, "Es un perrito que adora pasear y comer", 2, new DateOnly(2021, 6, 8), "https://http2.mlstatic.com/D_NQ_NP_660656-MLM46803871812_072021-F.jpg", "Roky", 2, 1, 1, 16.0 },
                    { 4, 1, "Es un perro jugueton y le gusta dormir", 1, new DateOnly(2024, 3, 8), "https://th.bing.com/th/id/R.69b9a203e86486a2114bc7380d204970?rik=kimEIahYLupgjQ&pid=ImgRaw&r=0&sres=1&sresct=1", "Willi", 5, 3, 1, 24.0 },
                    { 5, 1, "Es un perrito muy tierno y adora jugar", 1, new DateOnly(2024, 1, 7), "https://www.publicdomainpictures.net/pictures/180000/velka/chihuaua-puppy.jpg", "lali", 6, 1, 1, 9.0 },
                    { 6, 1, "Es un perrito muy aventurero", 1, new DateOnly(2024, 10, 22), "https://th.bing.com/th/id/OIP.NA3_b8GubIn0nfMn0w4XDAHaE8?rs=1&pid=ImgDetMain", "Mango", 1, 3, 1, 16.0 },
                    { 7, 1, "Le encanta correr y comer", 1, new DateOnly(2023, 11, 8), "https://th.bing.com/th/id/OIP.EzwbKs8GJQPv0CCPP2GMlQHaD9?rs=1&pid=ImgDetMain", "rodolf", 7, 1, 1, 9.0 },
                    { 8, 1, "Es un perro que adora el cariño y los paseos largos", 1, new DateOnly(2021, 4, 6), "https://th.bing.com/th/id/OIP.v3rbPJHS2RVJFSlRq_DXTAHaE8?rs=1&pid=ImgDetMain", "Oli", 8, 2, 1, 10.0 },
                    { 9, 1, "Es un perro con mucha energia y adora que le den cariño", 1, new DateOnly(2023, 4, 12), "https://th.bing.com/th/id/OIP.JuzlhHcoLZR61J50nJgftwHaFr?rs=1&pid=ImgDetMain", "Lalo", 6, 1, 1, 10.0 }
                });

            migrationBuilder.InsertData(
                table: "Razas",
                columns: new[] { "RazaId", "CategoriaId", "Nombre" },
                values: new object[,]
                {
                    { 1, 1, "Labrador" },
                    { 2, 1, "Bulldog" },
                    { 3, 2, "Persa" },
                    { 4, 2, "Siamés" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adoptantes_Id",
                table: "Adoptantes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Citas_AdoptanteId",
                table: "Citas",
                column: "AdoptanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Citas_MascotaId",
                table: "Citas",
                column: "MascotaId");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialAdopciones_AdoptanteId",
                table: "HistorialAdopciones",
                column: "AdoptanteId");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialAdopciones_MascotaId",
                table: "HistorialAdopciones",
                column: "MascotaId");

            migrationBuilder.CreateIndex(
                name: "IX_Mascotas_CategoriaId",
                table: "Mascotas",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Mascotas_EstadoId",
                table: "Mascotas",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Mascotas_RelacionSizeId",
                table: "Mascotas",
                column: "RelacionSizeId");

            migrationBuilder.CreateIndex(
                name: "IX_Mascotas_SexoId",
                table: "Mascotas",
                column: "SexoId");

            migrationBuilder.CreateIndex(
                name: "IX_PresentacionesDiapositivas_DiapositivaId",
                table: "PresentacionesDiapositivas",
                column: "DiapositivaId");

            migrationBuilder.CreateIndex(
                name: "IX_PresentacionesDiapositivas_PresentacionId",
                table: "PresentacionesDiapositivas",
                column: "PresentacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Razas_CategoriaId",
                table: "Razas",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesAdopciones_AdoptanteId",
                table: "SolicitudesAdopciones",
                column: "AdoptanteId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesAdopciones_EstadoSolicitudId",
                table: "SolicitudesAdopciones",
                column: "EstadoSolicitudId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesAdopciones_MascotaId",
                table: "SolicitudesAdopciones",
                column: "MascotaId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesServicios_ServicioId",
                table: "SolicitudesServicios",
                column: "ServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesServicios_SolicitudAdopcionId",
                table: "SolicitudesServicios",
                column: "SolicitudAdopcionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Citas");

            migrationBuilder.DropTable(
                name: "HistorialAdopciones");

            migrationBuilder.DropTable(
                name: "PresentacionesDiapositivas");

            migrationBuilder.DropTable(
                name: "Razas");

            migrationBuilder.DropTable(
                name: "SolicitudesServicios");

            migrationBuilder.DropTable(
                name: "Sugerencias");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Diapositivas");

            migrationBuilder.DropTable(
                name: "Presentaciones");

            migrationBuilder.DropTable(
                name: "Servicios");

            migrationBuilder.DropTable(
                name: "SolicitudesAdopciones");

            migrationBuilder.DropTable(
                name: "Adoptantes");

            migrationBuilder.DropTable(
                name: "EstadoSolicitudes");

            migrationBuilder.DropTable(
                name: "Mascotas");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Estados");

            migrationBuilder.DropTable(
                name: "RelacionSizes");

            migrationBuilder.DropTable(
                name: "Sexos");
        }
    }
}
