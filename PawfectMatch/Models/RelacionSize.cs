using System.ComponentModel.DataAnnotations;

namespace PawfectMatch.Models
{
    public class RelacionSize
    {
        [Key]
        public int RelacionSizeID { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; } = string.Empty;  // Ejemplo: "Pequeño", "Mediano", "Grande"

        public bool IsDeleted { get; set; } = false;

        // Navegación opcional
        public ICollection<MascotasAdopcion>? MascotasAdopcion { get; set; }
    }
}