using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawfectMatch.Models
{
    public class Razas
    {
        [Key]
        public int RazasID { get; set; }

        public int? EspeciesID { get; set; }

        [ForeignKey("EspeciesID")]
        public Especies? Especie { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; } = string.Empty;

        public bool IsDeleted { get; set; } = false;

        // Propiedades de navegación opcionales
        public ICollection<MascotasAdopcion>? MascotasAdopcion { get; set; }
        public ICollection<MascotasPersonas>? MascotasPersonas { get; set; }
    }
}