using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;
using UsuariosAlmaNetCoreMVC.Services;

namespace UsuariosAlmaNetCoreMVC.Migrations
{
    public partial class InitialDataMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insertar datos en TipoIdentificacion
            migrationBuilder.InsertData(
                table: "TiposIdentificacion",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                { 1, "Cédula" },
                { 2, "Pasaporte" },
                { 3, "RUC" }
                });

            // Insertar datos en Usuario
            //migrationBuilder.InsertData(
            //    table: "Usuarios",
            //    columns: new[] { "Id", "NombreUsuario", "PasswordHash", "PasswordSalt", "FechaCreacion" },
            //    values: new object[,]
            //    {
            //    { 1, "usuario1", null, null, DateTime.Now },
            //    { 2, "usuario2", null, null, DateTime.Now },
            //    { 3, "usuario3", null, null, DateTime.Now }
            //    });

            //var passwordHasher = new PasswordHasher();
            byte[] hash1, salt1, hash2, salt2, hash3, salt3;

            PasswordHasher.CreatePasswordHash("password1", out hash1, out salt1);
            PasswordHasher.CreatePasswordHash("password2", out hash2, out salt2);
            PasswordHasher.CreatePasswordHash("password3", out hash3, out salt3);

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "NombreUsuario", "PasswordHash", "PasswordSalt", "FechaCreacion" },
                values: new object[,]
                {
        { 1, "usuario1", hash1, salt1, DateTime.Now },
        { 2, "usuario2", hash2, salt2, DateTime.Now },
        { 3, "usuario3", hash3, salt3, DateTime.Now }
                });

            // Insertar datos en Persona
            migrationBuilder.InsertData(
                table: "Personas",
                columns: new[] { "Id", "Nombres", "Apellidos", "NumeroIdentificacion", "Email", "TipoIdentificacionId", "FechaCreacion" },
                values: new object[,]
                {
                { 1, "Juan", "Pérez", "123456789", "juan.perez@example.com", 1, DateTime.Now },
                { 2, "María", "González", "987654321", "maria.gonzalez@example.com", 2, DateTime.Now },
                { 3, "Pedro", "López", "112233445", "pedro.lopez@example.com", 1, DateTime.Now },
                { 4, "Ana", "Martínez", "556677889", "ana.martinez@example.com", 3, DateTime.Now },
                { 5, "Luis", "Rodríguez", "223344556", "luis.rodriguez@example.com", 1, DateTime.Now },
                { 6, "Laura", "Hernández", "334455667", "laura.hernandez@example.com", 2, DateTime.Now },
                { 7, "Carlos", "García", "445566778", "carlos.garcia@example.com", 1, DateTime.Now },
                { 8, "Sofía", "Cruz", "556677889", "sofia.cruz@example.com", 3, DateTime.Now },
                { 9, "Javier", "Jiménez", "667788990", "javier.jimenez@example.com", 1, DateTime.Now },
                { 10, "Isabel", "Sánchez", "778899001", "isabel.sanchez@example.com", 2, DateTime.Now },
                { 11, "Roberto", "Vázquez", "889900112", "roberto.vazquez@example.com", 1, DateTime.Now },
                { 12, "Verónica", "Ríos", "990011223", "veronica.rios@example.com", 3, DateTime.Now },
                { 13, "Andrés", "Mendoza", "101112131", "andres.mendoza@example.com", 1, DateTime.Now },
                { 14, "Patricia", "Salazar", "202122232", "patricia.salazar@example.com", 2, DateTime.Now },
                { 15, "Fernando", "Morales", "303132333", "fernando.morales@example.com", 1, DateTime.Now }
                });
        }
    }
}