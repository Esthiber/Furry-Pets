using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawfectMatch.Models
{
    public class SeguimientoMascotas
    {
        [Key]
        public int SeguimientoID { get; set; }

        [Required]
        public int MascotasAdopcionID { get; set; }
        [ForeignKey("MascotasAdopcionID")]
        public MascotasAdopcion? MascotaAdopcion { get; set; }

        [Required]
        public int PersonasID { get; set; }
        [ForeignKey("PersonasID")]
        public Personas? Persona { get; set; }

        [Required]
        public DateTime FechaVista { get; set; }

        [Required]
        public int EstadoMascotaID { get; set; }
        [ForeignKey("EstadoMascotaID")]
        public EstadoMascota? EstadoMascota { get; set; }

        [StringLength(255)]
        public string? Observaciones { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}