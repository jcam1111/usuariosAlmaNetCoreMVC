// Models/Persona.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsuariosAlmaNetCoreMVC.Models
{

    public class Persona
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Autoincrementable
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Los nombres no pueden exceder los 50 caracteres.")]
        public string Nombres { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Los apellidos no pueden exceder los 50 caracteres.")]
        public string Apellidos { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "El número de identificación no puede exceder los 30 caracteres.")]
        public string NumeroIdentificacion { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100, ErrorMessage = "El correo electrónico no puede exceder los 100 caracteres.")]
        public string Email { get; set; }
        
        [Required]
        public int TipoIdentificacionId { get; set; }
        public virtual TipoIdentificacion TipoIdentificacion { get; set; }

        public virtual ICollection<TipoIdentificacion> Personas { get; set; } = new List<TipoIdentificacion>();

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public virtual Usuario Usuario { get; set; }
    }

}
