using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawfectMatch.Models
{
    public class Pagos
    {
        [Key]
        public int PagosID { get; set; }

        [Required]
        public int FacturasID { get; set; }

        [ForeignKey("FacturasID")]
        public Facturas? Factura { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Monto { get; set; }

        [Required]
        [StringLength(50)]
        public string MetodoPago { get; set; } = string.Empty;

        public bool IsDeleted { get; set; } = false;
    }
}