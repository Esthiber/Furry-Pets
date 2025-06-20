using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PawfectMatch.Migrations
{
    /// <inheritdoc />
    public partial class LastCheck : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Diapositivas",
                columns: new[] { "DiapositivaId", "Animacion", "ImageUrl", "IsButtonLeftActive", "IsButtonRightActive", "IsTituloLeftActive", "IsTituloRightActive", "LinkButton_Left", "LinkButton_Right", "Orden", "SubTitulo_Left", "SubTitulo_Right", "TextButton_Left", "TextButton_Right", "Titulo_Left", "Titulo_Right" },
                values: new object[,]
                {
                    { 1, null, "https://images-ext-1.discordapp.net/external/pCPvCTsMyhwvSzSxaPZZ5PG8VfVshz1DMFeTU4VQBFo/https/images8.alphacoders.com/449/thumb-1920-449501.jpg?format=webp&width=1374&height=859", true, false, true, false, "/mascotas", null, 1, "Conectamos mascotas que necesitan un hogar con familias amorosas", null, "Explorar Mascotas", null, "Encuentra a tu compañero perfecto", null },
                    { 2, null, "https://cdn.pixabay.com/photo/2016/11/29/04/17/dog-1861839_1280.jpg", true, false, true, false, "#", null, 2, "Dale una segunda oportunidad a quienes más lo necesitan", null, "Ver Historias", null, "Adopta, no compres", null },
                    { 3, null, "https://cdn.pixabay.com/photo/2020/02/06/15/06/dog-4824485_1280.jpg", false, true, false, true, null, "#testimonials-section", 3, null, "Conoce cómo nuestras mascotas encontraron un hogar lleno de amor", null, "Leer historias", null, "Historias con final feliz" }
                });

            migrationBuilder.InsertData(
                table: "Mascotas",
                columns: new[] { "MascotaId", "CategoriaId", "Descripcion", "EstadoId", "FechaNacimiento", "FotoUrl", "Nombre", "RazaId", "RelacionSizeId", "SexoId", "Tamano" },
                values: new object[,]
                {
                    { 10, 1, "Dócil, juguetona y le encanta estar en brazos.", 2, new DateOnly(2022, 2, 14), "https://th.bing.com/th/id/OIP.yRLVHRUwgdR4OiOxTPnNDgHaE7?rs=1&pid=ImgDetMain", "Nina", 4, 2, 2, 14.0 },
                    { 11, 1, "Muy protector y obediente.", 1, new DateOnly(2020, 5, 9), "https://th.bing.com/th/id/OIP.qkqCM7eQmJwvj8AkH3u5ngHaE8?rs=1&pid=ImgDetMain", "Thor", 3, 1, 1, 20.0 },
                    { 12, 1, "Le gusta dormir y es muy tranquila.", 1, new DateOnly(2023, 9, 20), "https://th.bing.com/th/id/OIP.QcIBbq4tgrIPhU3W0XxvagHaE7?rs=1&pid=ImgDetMain", "Luna", 9, 3, 2, 12.0 },
                    { 13, 1, "Un perro valiente y sociable.", 1, new DateOnly(2022, 12, 1), "https://th.bing.com/th/id/OIP.f3eZqKFb3I53N7QOkz-xaQHaE8?rs=1&pid=ImgDetMain", "Zeus", 2, 2, 1, 22.0 },
                    { 14, 1, "Pequeñita, dulce y siempre feliz.", 2, new DateOnly(2021, 7, 17), "https://th.bing.com/th/id/OIP.F5dOGfGKePtNmliuyzYhiAHaE7?rs=1&pid=ImgDetMain", "Mimi", 5, 3, 2, 11.0 },
                    { 15, 1, "Curioso, travieso pero muy leal.", 1, new DateOnly(2023, 1, 5), "https://th.bing.com/th/id/OIP.7f62UOkFMy3dk9MK-DKrCwHaEK?rs=1&pid=ImgDetMain", "Coco", 4, 2, 1, 19.0 },
                    { 16, 1, "Tiene una energía inagotable, ama jugar con pelotas.", 1, new DateOnly(2022, 3, 15), "https://th.bing.com/th/id/OIP.hKhG7YFi7tvYoXL4o_ArAwHaE7?rs=1&pid=ImgDetMain", "Chispa", 7, 1, 2, 10.0 },
                    { 17, 1, "Fiel y tranquilo, le gusta que lo cepillen.", 1, new DateOnly(2021, 11, 25), "https://th.bing.com/th/id/OIP.EjRBsp3T_z1sjEdQAlm0eQHaFj?rs=1&pid=ImgDetMain", "Toby", 8, 2, 1, 18.0 },
                    { 18, 1, "Una perrita súper tierna, perfecta para compañía.", 1, new DateOnly(2023, 2, 10), "https://th.bing.com/th/id/OIP.ohAyzfrYc6QjFZ3v1nypIAHaE8?rs=1&pid=ImgDetMain", "Kira", 9, 1, 2, 9.0 },
                    { 19, 1, "Gran protector, adiestrado y obediente.", 2, new DateOnly(2020, 7, 1), "https://th.bing.com/th/id/OIP.sAfYcLwUDwKXwJKCdWrjWwHaE8?rs=1&pid=ImgDetMain", "Axel", 3, 3, 1, 30.0 },
                    { 20, 1, "Pequeñito pero con un ladrido potente.", 1, new DateOnly(2024, 5, 19), "https://th.bing.com/th/id/OIP.z3Q5VZ8cJ3C07IbEECtkhwHaE7?rs=1&pid=ImgDetMain", "Lilo", 6, 1, 1, 6.0 },
                    { 21, 1, "Muy consentida, le encanta dormir en almohadas.", 1, new DateOnly(2023, 6, 3), "https://th.bing.com/th/id/OIP.ESAKWAbKk7cyEJOLFnIMrAHaE6?rs=1&pid=ImgDetMain", "Nube", 2, 2, 2, 17.0 },
                    { 22, 2, "Gato tranquilo, le encanta dormir y ronronear.", 1, new DateOnly(2023, 3, 10), "https://th.bing.com/th/id/OIP.cAluVwFq_G_AUtMXrP3KYwHaE8?rs=1&pid=ImgDetMain", "Mishu", 10, 2, 2, 12.0 },
                    { 23, 2, "Muy hablador y curioso. Siempre quiere estar contigo.", 2, new DateOnly(2021, 9, 15), "https://th.bing.com/th/id/OIP.3Hu0Z6b-DUYZf_A9GlQXuwHaE8?rs=1&pid=ImgDetMain", "Simón", 11, 2, 1, 11.0 },
                    { 24, 2, "Ágil, le encanta explorar todo el lugar.", 1, new DateOnly(2022, 1, 22), "https://th.bing.com/th/id/OIP.nQwMG4DyUqVxgo7BunwYyAHaE8?rs=1&pid=ImgDetMain", "Nala", 12, 2, 2, 13.0 },
                    { 25, 2, "Gran tamaño y corazón. Muy tranquilo.", 1, new DateOnly(2020, 6, 30), "https://th.bing.com/th/id/OIP.Qo9h1u8q1zB2UrdkMhvCBAHaEK?rs=1&pid=ImgDetMain", "Ragnar", 13, 3, 1, 20.0 }
                });

            migrationBuilder.InsertData(
                table: "Presentaciones",
                columns: new[] { "PresentacionId", "Descripcion", "EsActiva", "FechaCreacion", "Titulo" },
                values: new object[] { 1, "Esta es la presentacion default. No la borre por favor.", true, new DateTime(2025, 4, 21, 19, 12, 23, 649, DateTimeKind.Local).AddTicks(8047), "Default" });

            migrationBuilder.InsertData(
                table: "Razas",
                columns: new[] { "RazaId", "CategoriaId", "Nombre" },
                values: new object[,]
                {
                    { 5, 1, "Golden Retriever" },
                    { 6, 1, "Chihuahua" },
                    { 7, 1, "Dachshund" },
                    { 8, 1, "Cocker Spaniel" },
                    { 9, 1, "Shih Tzu" },
                    { 10, 2, "Bengalí" },
                    { 11, 2, "Maine Coon" },
                    { 12, 2, "Esfinge" },
                    { 13, 2, "Azul Ruso" }
                });

            migrationBuilder.InsertData(
                table: "PresentacionesDiapositivas",
                columns: new[] { "PresentacionDiapositivaId", "DiapositivaId", "Orden", "PresentacionId" },
                values: new object[,]
                {
                    { 1, 1, 1, 1 },
                    { 2, 2, 2, 1 },
                    { 3, 3, 3, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Mascotas",
                keyColumn: "MascotaId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Mascotas",
                keyColumn: "MascotaId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Mascotas",
                keyColumn: "MascotaId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Mascotas",
                keyColumn: "MascotaId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Mascotas",
                keyColumn: "MascotaId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Mascotas",
                keyColumn: "MascotaId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Mascotas",
                keyColumn: "MascotaId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Mascotas",
                keyColumn: "MascotaId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Mascotas",
                keyColumn: "MascotaId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Mascotas",
                keyColumn: "MascotaId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Mascotas",
                keyColumn: "MascotaId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Mascotas",
                keyColumn: "MascotaId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Mascotas",
                keyColumn: "MascotaId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Mascotas",
                keyColumn: "MascotaId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Mascotas",
                keyColumn: "MascotaId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Mascotas",
                keyColumn: "MascotaId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "PresentacionesDiapositivas",
                keyColumn: "PresentacionDiapositivaId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PresentacionesDiapositivas",
                keyColumn: "PresentacionDiapositivaId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PresentacionesDiapositivas",
                keyColumn: "PresentacionDiapositivaId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Razas",
                keyColumn: "RazaId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Razas",
                keyColumn: "RazaId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Razas",
                keyColumn: "RazaId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Razas",
                keyColumn: "RazaId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Razas",
                keyColumn: "RazaId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Razas",
                keyColumn: "RazaId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Razas",
                keyColumn: "RazaId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Razas",
                keyColumn: "RazaId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Razas",
                keyColumn: "RazaId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Diapositivas",
                keyColumn: "DiapositivaId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Diapositivas",
                keyColumn: "DiapositivaId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Diapositivas",
                keyColumn: "DiapositivaId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Presentaciones",
                keyColumn: "PresentacionId",
                keyValue: 1);
        }
    }
}
