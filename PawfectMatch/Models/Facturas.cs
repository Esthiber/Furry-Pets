using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace PawfectMatch.Models
{
    public class Facturas
    {
        [Key]
        public int FacturasID { get; set; }

        [Required]
        public int PersonasID { get; set; }

        [ForeignKey("PersonasID")]
        public Personas? Persona { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }

        [Required]
        public int EstadoPagoID { get; set; }

        [ForeignKey("EstadoPagoID")]
        public EstadosPagos? EstadoPago { get; set; }

        public bool IsDeleted { get; set; } = false;

        // Relaciones de navegación
        public ICollection<Pagos>? Pagos { get; set; }
        public ICollection<DetallesFacturas>? Detalles { get; set; }
    }
}