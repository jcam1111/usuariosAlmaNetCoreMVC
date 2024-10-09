// Models/Persona.cs
using System;
using System.ComponentModel.DataAnnotations;

namespace UsuariosAlmaNetCoreMVC.Models
{

    public class Persona
    {
        public int Id { get; set; }

        [Required]
        public string Nombres { get; set; }

        [Required]
        public string Apellidos { get; set; }

        [Required]
        public string NumeroIdentificacion { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string TipoIdentificacion { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public virtual Usuario Usuario { get; set; }
    }

}
