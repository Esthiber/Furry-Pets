using PawfectMatch.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawfectMatch.Models
{
    public class Adoptantes
    {
        [Key]
        public int AdoptanteId { get; set; }

        [Required(ErrorMessage ="Nombre requerido")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage ="Ingrese su ocupacion")]
        public string Ocupacion { get; set; } = null!;

        public string UsuarioId { get; set; } = null!;

        [ForeignKey("Id")]
        public ApplicationUser User { get; set; } = null!;
    }
}
