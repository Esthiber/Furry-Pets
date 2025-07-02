using System.ComponentModel.DataAnnotations;

namespace PawfectMatch.Models
{
    public class Especies
    {
        [Key]
        public int EspeciesID { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; } = string.Empty;

        public bool IsDeleted { get; set; } = false;

        // Propiedades de navegación opcionales
        public ICollection<MascotasAdopcion>? MascotasAdopcion { get; set; }
        public ICollection<MascotasPersonas>? MascotasPersonas { get; set; }
    }
}