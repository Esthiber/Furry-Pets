using System.ComponentModel.DataAnnotations;

namespace PawfectMatch.Models.POS
{
    public class TiposItems
    {
        [Key]
        public int TiposItemsID { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; } = string.Empty;

        public bool IsDeleted { get; set; } = false;

        // Relación de navegación opcional (colección de detalles de facturas)
        public ICollection<DetallesFacturas>? DetallesFacturas { get; set; }
    }
}