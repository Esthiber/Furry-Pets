using System.ComponentModel.DataAnnotations;

namespace PawfectMatch.Models
{
    public class Estados
    {
        [Key]
        public int EstadoID { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; } = string.Empty;  // Ejemplo: "Adoptado", "Disponible", etc.

        public bool IsDeleted { get; set; } = false;

        // Navegación opcional
        public ICollection<MascotasAdopcion>? MascotasAdopcion { get; set; }
    }
}