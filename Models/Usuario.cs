// Models/Usuario.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace UsuariosAlmaNetCoreMVC.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        public string NombreUsuario { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Pass { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public int? PersonaId { get; set; }
        public virtual Persona Persona { get; set; }
    }
}
