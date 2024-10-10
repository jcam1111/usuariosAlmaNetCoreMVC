// Models/Usuario.cs
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsuariosAlmaNetCoreMVC.Models
{
    public class Usuario : IdentityUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Autoincrementable
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "El nombre de usuario no puede exceder los 50 caracteres.")]
        public string Nombre { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public int? PersonaId { get; set; }
        public virtual Persona Persona { get; set; }
    }
}
