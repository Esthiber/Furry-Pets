using System.ComponentModel.DataAnnotations;

namespace PawfectMatch.Models
{
    public class EstadoSolicitud
    {
        [Key]
        public int EstadoSolicitudID { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; } = string.Empty; // Ej: "Aprobada", "En Revisión", etc.

        public bool IsDeleted { get; set; } = false;

        // Navegación opcional
        public ICollection<SolicitudesAdopciones>? SolicitudesAdopciones { get; set; }
    }
}