using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsuariosAlmaNetCoreMVC.Models
{
    public class TipoIdentificacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Autoincrementable
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "El nombre del tipo de identificación no puede exceder los 50 caracteres.")]
        public string Nombre { get; set; }

        
    }
}
