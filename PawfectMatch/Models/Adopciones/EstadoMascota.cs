using System.ComponentModel.DataAnnotations;

namespace PawfectMatch.Models.Adopciones
{
    public class EstadoMascota
    {
        [Key]
        public int EstadoMascotaID { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; } = string.Empty;

        public bool IsDeleted { get; set; } = false;

        // Navegación opcional
        public ICollection<SeguimientoMascotas>? Seguimientos { get; set; }
    }
}