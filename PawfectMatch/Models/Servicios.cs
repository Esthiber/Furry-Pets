using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawfectMatch.Models
{
    public class Servicios
    {
        [Key]
        public int ServiciosID { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Descripcion { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Precio { get; set; }

        [Required]
        public int TiposServiciosID { get; set; }
        [ForeignKey(nameof(TiposServiciosID))]
        public TiposServicios? TipoServicio { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}