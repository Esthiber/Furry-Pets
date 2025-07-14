using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawfectMatch.Models.POS
{
    public class DetallesFacturas
    {
        [Key]
        public int DetallesFacturasID { get; set; }

        [Required]
        public int FacturasID { get; set; }
        [ForeignKey("FacturasID")]
        public Facturas? Factura { get; set; }

        [Required]
        public int TiposItemsID { get; set; }
        [ForeignKey("TiposItemsID")]
        public TiposItems? TipoItem { get; set; }

        [Required]
        public int ItemID { get; set; }
        // Aquí no hay una FK directa porque puede ser Producto o Servicio; manejarlo en la lógica de negocio.

        [Required]
        public int Cantidad { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioUnitario { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}