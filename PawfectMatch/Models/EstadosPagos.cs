using System.ComponentModel.DataAnnotations;

namespace PawfectMatch.Models
{
    public class EstadosPagos
    {
        [Key]
        public int EstadosPagosID { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; } = string.Empty;

        public bool IsDeleted { get; set; } = false;

        // Relación de navegación opcional (una colección de facturas asociadas a este estado)
        public ICollection<Facturas>? Facturas { get; set; }
    }
}