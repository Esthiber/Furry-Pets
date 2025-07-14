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
                name: "CategoriasProductos",
                columns: table => new
                {
                    CategoriasProductosID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriasProductos", x => x.CategoriasProductosID);
                });

            migrationBuilder.CreateTable(
                name: "ConfiguracionEmpresa",
                columns: table => new
                {
                    EmpresaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    RNC = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfiguracionEmpresa", x => x.EmpresaID);
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
                    IsButtonLeftActive = table.Column<bool>(type: "bit", nullable: false),
                    TextButton_Left = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LinkButton_Left = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsButtonRightActive = table.Column<bool>(type: "bit", nullable: false),
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
                name: "Especies",
                columns: table => new
                {
                    EspeciesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Especies", x => x.EspeciesID);
                });

            migrationBuilder.CreateTable(
                name: "EstadoMascota",
                columns: table => new
                {
                    EstadoMascotaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoMascota", x => x.EstadoMascotaID);
                });

            migrationBuilder.CreateTable(
                name: "Estados",
                columns: table => new
                {
                    EstadoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estados", x => x.EstadoID);
                });

            migrationBuilder.CreateTable(
                name: "EstadosCitas",
                columns: table => new
                {
                    EstadosCitasID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadosCitas", x => x.EstadosCitasID);
                });

            migrationBuilder.CreateTable(
                name: "EstadoSolicitud",
                columns: table => new
                {
                    EstadoSolicitudID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoSolicitud", x => x.EstadoSolicitudID);
                });

            migrationBuilder.CreateTable(
                name: "EstadosPagos",
                columns: table => new
                {
                    EstadosPagosID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadosPagos", x => x.EstadosPagosID);
                });

            migrationBuilder.CreateTable(
                name: "Personas",
                columns: table => new
                {
                    PersonasID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Sexo = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Identificacion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaNacimiento = table.Column<DateOnly>(type: "date", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Nacionalidad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EstadoCivil = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personas", x => x.PersonasID);
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
                name: "Proveedores",
                columns: table => new
                {
                    ProveedoresID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    RNC = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.ProveedoresID);
                });

            migrationBuilder.CreateTable(
                name: "RelacionSize",
                columns: table => new
                {
                    RelacionSizeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelacionSize", x => x.RelacionSizeID);
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
                name: "TiposItems",
                columns: table => new
                {
                    TiposItemsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposItems", x => x.TiposItemsID);
                });

            migrationBuilder.CreateTable(
                name: "TiposServicios",
                columns: table => new
                {
                    TiposServiciosID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposServicios", x => x.TiposServiciosID);
                });

            migrationBuilder.CreateTable(
                name: "TipoViviendas",
                columns: table => new
                {
                    TipoViviendasID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoViviendas", x => x.TipoViviendasID);
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
                    RazasID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EspeciesID = table.Column<int>(type: "int", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Razas", x => x.RazasID);
                    table.ForeignKey(
                        name: "FK_Razas_Especies_EspeciesID",
                        column: x => x.EspeciesID,
                        principalTable: "Especies",
                        principalColumn: "EspeciesID");
                });

            migrationBuilder.CreateTable(
                name: "Facturas",
                columns: table => new
                {
                    FacturasID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonasID = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EstadoPagoID = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facturas", x => x.FacturasID);
                    table.ForeignKey(
                        name: "FK_Facturas_EstadosPagos_EstadoPagoID",
                        column: x => x.EstadoPagoID,
                        principalTable: "EstadosPagos",
                        principalColumn: "EstadosPagosID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Facturas_Personas_PersonasID",
                        column: x => x.PersonasID,
                        principalTable: "Personas",
                        principalColumn: "PersonasID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonasRoles",
                columns: table => new
                {
                    PersonasRolesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonasID = table.Column<int>(type: "int", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonasRoles", x => x.PersonasRolesID);
                    table.ForeignKey(
                        name: "FK_PersonasRoles_Personas_PersonasID",
                        column: x => x.PersonasID,
                        principalTable: "Personas",
                        principalColumn: "PersonasID",
                        onDelete: ReferentialAction.Cascade);
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
                name: "Productos",
                columns: table => new
                {
                    ProductosID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoriasProductosID = table.Column<int>(type: "int", nullable: false),
                    ProveedoresID = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Costo = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    ImagenUrl = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.ProductosID);
                    table.ForeignKey(
                        name: "FK_Productos_CategoriasProductos_CategoriasProductosID",
                        column: x => x.CategoriasProductosID,
                        principalTable: "CategoriasProductos",
                        principalColumn: "CategoriasProductosID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Productos_Proveedores_ProveedoresID",
                        column: x => x.ProveedoresID,
                        principalTable: "Proveedores",
                        principalColumn: "ProveedoresID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Servicios",
                columns: table => new
                {
                    ServiciosID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Precio = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    TiposServiciosID = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servicios", x => x.ServiciosID);
                    table.ForeignKey(
                        name: "FK_Servicios_TiposServicios_TiposServiciosID",
                        column: x => x.TiposServiciosID,
                        principalTable: "TiposServicios",
                        principalColumn: "TiposServiciosID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdoptantesDetalles",
                columns: table => new
                {
                    AdoptantesDetallesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonasID = table.Column<int>(type: "int", nullable: false),
                    TipoViviendasID = table.Column<int>(type: "int", nullable: true),
                    ViveEnViviendaAlquilada = table.Column<bool>(type: "bit", nullable: false),
                    TieneJardin = table.Column<bool>(type: "bit", nullable: false),
                    NotasJardin = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TieneOtrasMascotas = table.Column<bool>(type: "bit", nullable: false),
                    NotasOtrasMascotas = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    HorasAusentes = table.Column<int>(type: "int", nullable: true),
                    RazonAdopcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdoptantesDetalles", x => x.AdoptantesDetallesID);
                    table.ForeignKey(
                        name: "FK_AdoptantesDetalles_Personas_PersonasID",
                        column: x => x.PersonasID,
                        principalTable: "Personas",
                        principalColumn: "PersonasID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdoptantesDetalles_TipoViviendas_TipoViviendasID",
                        column: x => x.TipoViviendasID,
                        principalTable: "TipoViviendas",
                        principalColumn: "TipoViviendasID");
                });

            migrationBuilder.CreateTable(
                name: "MascotasAdopcion",
                columns: table => new
                {
                    MascotasAdopcionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RazasID = table.Column<int>(type: "int", nullable: false),
                    RelacionSizeID = table.Column<int>(type: "int", nullable: false),
                    Tamanio = table.Column<int>(type: "int", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FechaNacimiento = table.Column<DateOnly>(type: "date", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FotoURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstadoID = table.Column<int>(type: "int", nullable: false),
                    Sexo = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    EspeciesID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MascotasAdopcion", x => x.MascotasAdopcionID);
                    table.ForeignKey(
                        name: "FK_MascotasAdopcion_Especies_EspeciesID",
                        column: x => x.EspeciesID,
                        principalTable: "Especies",
                        principalColumn: "EspeciesID");
                    table.ForeignKey(
                        name: "FK_MascotasAdopcion_Estados_EstadoID",
                        column: x => x.EstadoID,
                        principalTable: "Estados",
                        principalColumn: "EstadoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MascotasAdopcion_Razas_RazasID",
                        column: x => x.RazasID,
                        principalTable: "Razas",
                        principalColumn: "RazasID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MascotasAdopcion_RelacionSize_RelacionSizeID",
                        column: x => x.RelacionSizeID,
                        principalTable: "RelacionSize",
                        principalColumn: "RelacionSizeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MascotasPersonas",
                columns: table => new
                {
                    MascotasPersonasID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonasID = table.Column<int>(type: "int", nullable: true),
                    RazasID = table.Column<int>(type: "int", nullable: true),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FechaNacimiento = table.Column<DateOnly>(type: "date", nullable: true),
                    Sexo = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    EspeciesID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MascotasPersonas", x => x.MascotasPersonasID);
                    table.ForeignKey(
                        name: "FK_MascotasPersonas_Especies_EspeciesID",
                        column: x => x.EspeciesID,
                        principalTable: "Especies",
                        principalColumn: "EspeciesID");
                    table.ForeignKey(
                        name: "FK_MascotasPersonas_Personas_PersonasID",
                        column: x => x.PersonasID,
                        principalTable: "Personas",
                        principalColumn: "PersonasID");
                    table.ForeignKey(
                        name: "FK_MascotasPersonas_Razas_RazasID",
                        column: x => x.RazasID,
                        principalTable: "Razas",
                        principalColumn: "RazasID");
                });

            migrationBuilder.CreateTable(
                name: "DetalleFacturas",
                columns: table => new
                {
                    DetallesFacturasID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacturasID = table.Column<int>(type: "int", nullable: false),
                    TiposItemsID = table.Column<int>(type: "int", nullable: false),
                    ItemID = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleFacturas", x => x.DetallesFacturasID);
                    table.ForeignKey(
                        name: "FK_DetalleFacturas_Facturas_FacturasID",
                        column: x => x.FacturasID,
                        principalTable: "Facturas",
                        principalColumn: "FacturasID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleFacturas_TiposItems_TiposItemsID",
                        column: x => x.TiposItemsID,
                        principalTable: "TiposItems",
                        principalColumn: "TiposItemsID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pagos",
                columns: table => new
                {
                    PagosID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacturasID = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MetodoPago = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagos", x => x.PagosID);
                    table.ForeignKey(
                        name: "FK_Pagos_Facturas_FacturasID",
                        column: x => x.FacturasID,
                        principalTable: "Facturas",
                        principalColumn: "FacturasID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeguimientoMascotas",
                columns: table => new
                {
                    SeguimientoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MascotasAdopcionID = table.Column<int>(type: "int", nullable: false),
                    PersonasID = table.Column<int>(type: "int", nullable: false),
                    FechaVista = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoMascotaID = table.Column<int>(type: "int", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeguimientoMascotas", x => x.SeguimientoID);
                    table.ForeignKey(
                        name: "FK_SeguimientoMascotas_EstadoMascota_EstadoMascotaID",
                        column: x => x.EstadoMascotaID,
                        principalTable: "EstadoMascota",
                        principalColumn: "EstadoMascotaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeguimientoMascotas_MascotasAdopcion_MascotasAdopcionID",
                        column: x => x.MascotasAdopcionID,
                        principalTable: "MascotasAdopcion",
                        principalColumn: "MascotasAdopcionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeguimientoMascotas_Personas_PersonasID",
                        column: x => x.PersonasID,
                        principalTable: "Personas",
                        principalColumn: "PersonasID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudesAdopciones",
                columns: table => new
                {
                    SolicitudesAdopcionesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonasID = table.Column<int>(type: "int", nullable: false),
                    EstadoSolicitudID = table.Column<int>(type: "int", nullable: false),
                    MascotasAdopcionID = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudesAdopciones", x => x.SolicitudesAdopcionesID);
                    table.ForeignKey(
                        name: "FK_SolicitudesAdopciones_EstadoSolicitud_EstadoSolicitudID",
                        column: x => x.EstadoSolicitudID,
                        principalTable: "EstadoSolicitud",
                        principalColumn: "EstadoSolicitudID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolicitudesAdopciones_MascotasAdopcion_MascotasAdopcionID",
                        column: x => x.MascotasAdopcionID,
                        principalTable: "MascotasAdopcion",
                        principalColumn: "MascotasAdopcionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolicitudesAdopciones_Personas_PersonasID",
                        column: x => x.PersonasID,
                        principalTable: "Personas",
                        principalColumn: "PersonasID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Citas",
                columns: table => new
                {
                    CitasID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonasID = table.Column<int>(type: "int", nullable: false),
                    MascotasPersonasID = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Hora = table.Column<TimeSpan>(type: "time", nullable: false),
                    Motivo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EstadosCitasID = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Citas", x => x.CitasID);
                    table.ForeignKey(
                        name: "FK_Citas_EstadosCitas_EstadosCitasID",
                        column: x => x.EstadosCitasID,
                        principalTable: "EstadosCitas",
                        principalColumn: "EstadosCitasID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Citas_MascotasPersonas_MascotasPersonasID",
                        column: x => x.MascotasPersonasID,
                        principalTable: "MascotasPersonas",
                        principalColumn: "MascotasPersonasID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Citas_Personas_PersonasID",
                        column: x => x.PersonasID,
                        principalTable: "Personas",
                        principalColumn: "PersonasID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoriasClinicas",
                columns: table => new
                {
                    HistoriasClinicaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MascotasPersonasID = table.Column<int>(type: "int", nullable: false),
                    PersonasID = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DescripcionVisita = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Diagnostico = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Tratamiento = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoriasClinicas", x => x.HistoriasClinicaID);
                    table.ForeignKey(
                        name: "FK_HistoriasClinicas_MascotasPersonas_MascotasPersonasID",
                        column: x => x.MascotasPersonasID,
                        principalTable: "MascotasPersonas",
                        principalColumn: "MascotasPersonasID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistoriasClinicas_Personas_PersonasID",
                        column: x => x.PersonasID,
                        principalTable: "Personas",
                        principalColumn: "PersonasID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistorialAdopciones",
                columns: table => new
                {
                    HistorialAdopcionesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SolicitudesAdopcionesID = table.Column<int>(type: "int", nullable: false),
                    MascotasAdopcionID = table.Column<int>(type: "int", nullable: false),
                    FechaAdopcion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notas = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    SolicitudesAdopcionesID1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistorialAdopciones", x => x.HistorialAdopcionesID);
                    table.ForeignKey(
                        name: "FK_HistorialAdopciones_MascotasAdopcion_MascotasAdopcionID",
                        column: x => x.MascotasAdopcionID,
                        principalTable: "MascotasAdopcion",
                        principalColumn: "MascotasAdopcionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistorialAdopciones_SolicitudesAdopciones_SolicitudesAdopcionesID",
                        column: x => x.SolicitudesAdopcionesID,
                        principalTable: "SolicitudesAdopciones",
                        principalColumn: "SolicitudesAdopcionesID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistorialAdopciones_SolicitudesAdopciones_SolicitudesAdopcionesID1",
                        column: x => x.SolicitudesAdopcionesID1,
                        principalTable: "SolicitudesAdopciones",
                        principalColumn: "SolicitudesAdopcionesID");
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
                table: "CategoriasProductos",
                columns: new[] { "CategoriasProductosID", "IsDeleted", "Nombre" },
                values: new object[,]
                {
                    { 1, false, "Alimentos" },
                    { 2, false, "Accesorios" }
                });

            migrationBuilder.InsertData(
                table: "ConfiguracionEmpresa",
                columns: new[] { "EmpresaID", "Direccion", "Nombre", "RNC", "Telefono" },
                values: new object[] { 1, "Av. Principal 123, Ciudad", "Veterinaria PawfectMatch", "101000099", "809-123-4567" });

            migrationBuilder.InsertData(
                table: "Especies",
                columns: new[] { "EspeciesID", "IsDeleted", "Nombre" },
                values: new object[,]
                {
                    { 1, false, "Gato" },
                    { 2, false, "Perro" }
                });

            migrationBuilder.InsertData(
                table: "EstadoMascota",
                columns: new[] { "EstadoMascotaID", "IsDeleted", "Nombre" },
                values: new object[,]
                {
                    { 1, false, "Sano" },
                    { 2, false, "Enfermo" }
                });

            migrationBuilder.InsertData(
                table: "EstadoSolicitud",
                columns: new[] { "EstadoSolicitudID", "IsDeleted", "Nombre" },
                values: new object[,]
                {
                    { 1, false, "Aprobada" },
                    { 2, false, "En Revisi�n" },
                    { 3, false, "Rechazada" },
                    { 4, false, "En Espera" }
                });

            migrationBuilder.InsertData(
                table: "Estados",
                columns: new[] { "EstadoID", "IsDeleted", "Nombre" },
                values: new object[,]
                {
                    { 1, false, "Adoptado" },
                    { 2, false, "Disponible" },
                    { 3, false, "No Disponible" }
                });

            migrationBuilder.InsertData(
                table: "EstadosCitas",
                columns: new[] { "EstadosCitasID", "IsDeleted", "Nombre" },
                values: new object[,]
                {
                    { 1, false, "Pendiente" },
                    { 2, false, "Confirmada" },
                    { 3, false, "Cancelada" }
                });

            migrationBuilder.InsertData(
                table: "EstadosPagos",
                columns: new[] { "EstadosPagosID", "IsDeleted", "Nombre" },
                values: new object[,]
                {
                    { 1, false, "Pagado" },
                    { 2, false, "Pendiente" }
                });

            migrationBuilder.InsertData(
                table: "Proveedores",
                columns: new[] { "ProveedoresID", "Email", "IsDeleted", "Nombre", "RNC", "Telefono" },
                values: new object[,]
                {
                    { 1, "ventas@petzone.com", false, "Distribuidora PetZone", "132456789", "809-888-5555" },
                    { 2, "contacto@petplus.com", false, "PetPlus Suplidores", "101112233", "809-777-4444" },
                    { 3, "info@mascotienda.com", false, "Mascotienda SRL", "110220330", "809-666-3333" }
                });

            migrationBuilder.InsertData(
                table: "RelacionSize",
                columns: new[] { "RelacionSizeID", "IsDeleted", "Nombre" },
                values: new object[,]
                {
                    { 1, false, "Peque�o" },
                    { 2, false, "Mediano" },
                    { 3, false, "Grande" }
                });

            migrationBuilder.InsertData(
                table: "TipoViviendas",
                columns: new[] { "TipoViviendasID", "IsDeleted", "Nombre" },
                values: new object[,]
                {
                    { 1, false, "Casa" },
                    { 2, false, "Apartamento" }
                });

            migrationBuilder.InsertData(
                table: "TiposItems",
                columns: new[] { "TiposItemsID", "IsDeleted", "Nombre" },
                values: new object[,]
                {
                    { 1, false, "Producto" },
                    { 2, false, "Servicio" }
                });

            migrationBuilder.InsertData(
                table: "TiposServicios",
                columns: new[] { "TiposServiciosID", "IsDeleted", "Nombre" },
                values: new object[,]
                {
                    { 1, false, "Consulta Veterinaria" },
                    { 2, false, "Vacunaci�n" }
                });

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "ProductosID", "CategoriasProductosID", "Costo", "Descripcion", "ImagenUrl", "IsDeleted", "Nombre", "Precio", "ProveedoresID", "Stock" },
                values: new object[,]
                {
                    { 1, 1, 10.00m, "Croquetas nutritivas para perros adultos.", "https://images.rawpixel.com/image_800/cHJpdmF0ZS9sci9pbWFnZXMvd2Vic2l0ZS8yMDIyLTExL3BmLXMxMDgtcG0tNDExMy1tb2NrdXAuanBn.jpg", false, "Croquetas Premium", 18.99m, 1, 50 },
                    { 2, 1, 2.50m, "Lata de comida gourmet para gatos exigentes.", "https://images.rawpixel.com/image_800/cHJpdmF0ZS9sci9pbWFnZXMvd2Vic2l0ZS8yMDIyLTExL3BmLXMxMDgtcG0tNDExMy1tb2NrdXAuanBn.jpg", false, "Comida Húmeda para Gato", 4.99m, 1, 100 },
                    { 3, 2, 15.00m, "Collar rastreador para mascotas medianas.", "https://images.rawpixel.com/image_800/cHJpdmF0ZS9sci9pbWFnZXMvd2Vic2l0ZS8yMDIyLTExL3BmLXMxMDgtcG0tNDExMy1tb2NrdXAuanBn.jpg", false, "Collar con GPS", 29.99m, 1, 25 },
                    { 4, 2, 12.00m, "Cama acolchonada ideal para gatos y perros pequeños.", "https://images.rawpixel.com/image_800/cHJpdmF0ZS9sci9pbWFnZXMvd2Vic2l0ZS8yMDIyLTExL3BmLXMxMDgtcG0tNDExMy1tb2NrdXAuanBn.jpg", false, "Cama para Mascotas", 24.99m, 1, 30 },
                    { 5, 1, 3.00m, "Galletas orgánicas para entrenamiento.", "https://images.rawpixel.com/image_800/cHJpdmF0ZS9sci9pbWFnZXMvd2Vic2l0ZS8yMDIyLTExL3BmLXMxMDgtcG0tNDExMy1tb2NrdXAuanBn.jpg", false, "Snack Natural", 6.50m, 1, 80 },
                    { 6, 2, 1.80m, "Pelota para perros que emite sonidos al morderla.", "https://images.rawpixel.com/image_800/cHJpdmF0ZS9sci9pbWFnZXMvd2Vic2l0ZS8yMDIyLTExL3BmLXMxMDgtcG0tNDExMy1tb2NrdXAuanBn.jpg", false, "Juguete Pelota Sonora", 4.00m, 1, 60 },
                    { 7, 1, 5.00m, "Suplemento lácteo para crías sin madre.", "https://images.rawpixel.com/image_800/cHJpdmF0ZS9sci9pbWFnZXMvd2Vic2l0ZS8yMDIyLTExL3BmLXMxMDgtcG0tNDExMy1tb2NrdXAuanBn.jpg", false, "Leche para Cachorros", 9.99m, 1, 45 },
                    { 8, 2, 6.00m, "Correa extensible hasta 5 metros.", "https://images.rawpixel.com/image_800/cHJpdmF0ZS9sci9pbWFnZXMvd2Vic2l0ZS8yMDIyLTExL3BmLXMxMDgtcG0tNDExMy1tb2NrdXAuanBn.jpg", false, "Correa Retráctil", 12.99m, 1, 40 },
                    { 9, 2, 8.00m, "Rascador vertical de sisal natural.", "https://images.rawpixel.com/image_800/cHJpdmF0ZS9sci9pbWFnZXMvd2Vic2l0ZS8yMDIyLTExL3BmLXMxMDgtcG0tNDExMy1tb2NrdXAuanBn.jpg", false, "Rascador para Gato", 15.50m, 1, 20 },
                    { 10, 1, 4.00m, "Complemento vitamínico para perros y gatos.", "https://images.rawpixel.com/image_800/cHJpdmF0ZS9sci9pbWFnZXMvd2Vic2l0ZS8yMDIyLTExL3BmLXMxMDgtcG0tNDExMy1tb2NrdXAuanBn.jpg", false, "Vitaminas para Mascotas", 8.50m, 1, 70 }
                });

            migrationBuilder.InsertData(
                table: "Razas",
                columns: new[] { "RazasID", "EspeciesID", "IsDeleted", "Nombre" },
                values: new object[,]
                {
                    { 1, 2, false, "Chihuahua" },
                    { 2, 2, false, "Bulldog" },
                    { 3, 1, false, "Gato Naranja" }
                });

            migrationBuilder.InsertData(
                table: "MascotasAdopcion",
                columns: new[] { "MascotasAdopcionID", "Descripcion", "EspeciesID", "EstadoID", "FechaNacimiento", "FotoURL", "IsDeleted", "Nombre", "RazasID", "RelacionSizeID", "Sexo", "Tamanio" },
                values: new object[,]
                {
                    { 1, "Cachorrita juguetona y muy cariñosa con niños.", null, 2, new DateOnly(2023, 5, 10), "https://images.unsplash.com/photo-1583511655826-05700d52f4ae", false, "Luna", 1, 1, "f", 1 },
                    { 2, "Perro guardián y muy leal. Ideal para casas grandes.", null, 2, new DateOnly(2022, 11, 15), "https://images.unsplash.com/photo-1601758123927-196f76f75097", false, "Rocky", 2, 2, "m", 2 },
                    { 3, "Gatita rescatada muy tranquila y sociable.", null, 2, new DateOnly(2024, 2, 1), "https://images.unsplash.com/photo-1592194996308-7b43878e84a6", false, "Mia", 3, 1, "f", 3 },
                    { 4, "Perro fuerte, entrenado y excelente para seguridad.", null, 2, new DateOnly(2021, 8, 20), "https://images.unsplash.com/photo-1583511655826-05700d52f4ae", false, "Zeus", 2, 3, "m", 5 },
                    { 5, "Muy energética y necesita mucho ejercicio diario.", null, 2, new DateOnly(2023, 1, 30), "https://images.unsplash.com/photo-1601758003122-58eacb8e3ed1", false, "Nala", 1, 2, "f", 5 },
                    { 6, "Gatito curioso y muy juguetón.", null, 2, new DateOnly(2024, 4, 15), "https://images.unsplash.com/photo-1592194996308-7b43878e84a6", false, "Simba", 3, 1, "m", 2 },
                    { 7, "Gran danés amoroso y obediente.", null, 2, new DateOnly(2022, 6, 18), "https://images.unsplash.com/photo-1583511655826-05700d52f4ae", false, "Thor", 2, 3, "m", 4 },
                    { 8, "Gatita blanca, ideal para compañía.", null, 2, new DateOnly(2023, 7, 9), "https://images.unsplash.com/photo-1580377969203-4ec1eac6d5fe", false, "Lili", 3, 1, "f", 5 },
                    { 9, "Obediente y perfecto para familias con niños.", null, 2, new DateOnly(2021, 9, 12), "https://images.unsplash.com/photo-1558788353-f76d92427f16", false, "Max", 1, 2, "m", 5 },
                    { 10, "Gatita siamesa muy elegante.", null, 2, new DateOnly(2024, 3, 22), "https://images.unsplash.com/photo-1574158622682-e40e69881006", false, "Cleo", 3, 1, "f", 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdoptantesDetalles_PersonasID",
                table: "AdoptantesDetalles",
                column: "PersonasID");

            migrationBuilder.CreateIndex(
                name: "IX_AdoptantesDetalles_TipoViviendasID",
                table: "AdoptantesDetalles",
                column: "TipoViviendasID");

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
                name: "IX_Citas_EstadosCitasID",
                table: "Citas",
                column: "EstadosCitasID");

            migrationBuilder.CreateIndex(
                name: "IX_Citas_MascotasPersonasID",
                table: "Citas",
                column: "MascotasPersonasID");

            migrationBuilder.CreateIndex(
                name: "IX_Citas_PersonasID",
                table: "Citas",
                column: "PersonasID");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleFacturas_FacturasID",
                table: "DetalleFacturas",
                column: "FacturasID");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleFacturas_TiposItemsID",
                table: "DetalleFacturas",
                column: "TiposItemsID");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_EstadoPagoID",
                table: "Facturas",
                column: "EstadoPagoID");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_PersonasID",
                table: "Facturas",
                column: "PersonasID");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialAdopciones_MascotasAdopcionID",
                table: "HistorialAdopciones",
                column: "MascotasAdopcionID");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialAdopciones_SolicitudesAdopcionesID",
                table: "HistorialAdopciones",
                column: "SolicitudesAdopcionesID");

            migrationBuilder.CreateIndex(
                name: "IX_HistorialAdopciones_SolicitudesAdopcionesID1",
                table: "HistorialAdopciones",
                column: "SolicitudesAdopcionesID1");

            migrationBuilder.CreateIndex(
                name: "IX_HistoriasClinicas_MascotasPersonasID",
                table: "HistoriasClinicas",
                column: "MascotasPersonasID");

            migrationBuilder.CreateIndex(
                name: "IX_HistoriasClinicas_PersonasID",
                table: "HistoriasClinicas",
                column: "PersonasID");

            migrationBuilder.CreateIndex(
                name: "IX_MascotasAdopcion_EspeciesID",
                table: "MascotasAdopcion",
                column: "EspeciesID");

            migrationBuilder.CreateIndex(
                name: "IX_MascotasAdopcion_EstadoID",
                table: "MascotasAdopcion",
                column: "EstadoID");

            migrationBuilder.CreateIndex(
                name: "IX_MascotasAdopcion_RazasID",
                table: "MascotasAdopcion",
                column: "RazasID");

            migrationBuilder.CreateIndex(
                name: "IX_MascotasAdopcion_RelacionSizeID",
                table: "MascotasAdopcion",
                column: "RelacionSizeID");

            migrationBuilder.CreateIndex(
                name: "IX_MascotasPersonas_EspeciesID",
                table: "MascotasPersonas",
                column: "EspeciesID");

            migrationBuilder.CreateIndex(
                name: "IX_MascotasPersonas_PersonasID",
                table: "MascotasPersonas",
                column: "PersonasID");

            migrationBuilder.CreateIndex(
                name: "IX_MascotasPersonas_RazasID",
                table: "MascotasPersonas",
                column: "RazasID");

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_FacturasID",
                table: "Pagos",
                column: "FacturasID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonasRoles_PersonasID",
                table: "PersonasRoles",
                column: "PersonasID");

            migrationBuilder.CreateIndex(
                name: "IX_PresentacionesDiapositivas_DiapositivaId",
                table: "PresentacionesDiapositivas",
                column: "DiapositivaId");

            migrationBuilder.CreateIndex(
                name: "IX_PresentacionesDiapositivas_PresentacionId",
                table: "PresentacionesDiapositivas",
                column: "PresentacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_CategoriasProductosID",
                table: "Productos",
                column: "CategoriasProductosID");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_ProveedoresID",
                table: "Productos",
                column: "ProveedoresID");

            migrationBuilder.CreateIndex(
                name: "IX_Razas_EspeciesID",
                table: "Razas",
                column: "EspeciesID");

            migrationBuilder.CreateIndex(
                name: "IX_SeguimientoMascotas_EstadoMascotaID",
                table: "SeguimientoMascotas",
                column: "EstadoMascotaID");

            migrationBuilder.CreateIndex(
                name: "IX_SeguimientoMascotas_MascotasAdopcionID",
                table: "SeguimientoMascotas",
                column: "MascotasAdopcionID");

            migrationBuilder.CreateIndex(
                name: "IX_SeguimientoMascotas_PersonasID",
                table: "SeguimientoMascotas",
                column: "PersonasID");

            migrationBuilder.CreateIndex(
                name: "IX_Servicios_TiposServiciosID",
                table: "Servicios",
                column: "TiposServiciosID");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesAdopciones_EstadoSolicitudID",
                table: "SolicitudesAdopciones",
                column: "EstadoSolicitudID");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesAdopciones_MascotasAdopcionID",
                table: "SolicitudesAdopciones",
                column: "MascotasAdopcionID");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesAdopciones_PersonasID",
                table: "SolicitudesAdopciones",
                column: "PersonasID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdoptantesDetalles");

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
                name: "ConfiguracionEmpresa");

            migrationBuilder.DropTable(
                name: "DetalleFacturas");

            migrationBuilder.DropTable(
                name: "HistorialAdopciones");

            migrationBuilder.DropTable(
                name: "HistoriasClinicas");

            migrationBuilder.DropTable(
                name: "Pagos");

            migrationBuilder.DropTable(
                name: "PersonasRoles");

            migrationBuilder.DropTable(
                name: "PresentacionesDiapositivas");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "SeguimientoMascotas");

            migrationBuilder.DropTable(
                name: "Servicios");

            migrationBuilder.DropTable(
                name: "Sugerencias");

            migrationBuilder.DropTable(
                name: "TipoViviendas");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "EstadosCitas");

            migrationBuilder.DropTable(
                name: "TiposItems");

            migrationBuilder.DropTable(
                name: "SolicitudesAdopciones");

            migrationBuilder.DropTable(
                name: "MascotasPersonas");

            migrationBuilder.DropTable(
                name: "Facturas");

            migrationBuilder.DropTable(
                name: "Diapositivas");

            migrationBuilder.DropTable(
                name: "Presentaciones");

            migrationBuilder.DropTable(
                name: "CategoriasProductos");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "EstadoMascota");

            migrationBuilder.DropTable(
                name: "TiposServicios");

            migrationBuilder.DropTable(
                name: "EstadoSolicitud");

            migrationBuilder.DropTable(
                name: "MascotasAdopcion");

            migrationBuilder.DropTable(
                name: "EstadosPagos");

            migrationBuilder.DropTable(
                name: "Personas");

            migrationBuilder.DropTable(
                name: "Estados");

            migrationBuilder.DropTable(
                name: "Razas");

            migrationBuilder.DropTable(
                name: "RelacionSize");

            migrationBuilder.DropTable(
                name: "Especies");
        }
    }
}
