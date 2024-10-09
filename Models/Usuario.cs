// Models/Usuario.cs
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace UsuariosAlmaNetCoreMVC.Models
{
    public class Usuario : IdentityUser
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public int? PersonaId { get; set; }
        public virtual Persona Persona { get; set; }
    }
}
