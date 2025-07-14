using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawfectMatch.Models.Adopciones
{
    public class HistorialAdopciones
    {
        [Key]
        public int HistorialAdopcionesID { get; set; }

        [Required]
        public int SolicitudesAdopcionesID { get; set; }
        [ForeignKey("SolicitudesAdopcionesID")]
        public SolicitudesAdopciones? SolicitudAdopcion { get; set; }

        [Required]
        public int MascotasAdopcionID { get; set; }
        [ForeignKey("MascotasAdopcionID")]
        public MascotasAdopcion? MascotaAdopcion { get; set; }

        [Required]
        public DateTime FechaAdopcion { get; set; }

        [StringLength(255)]
        public string? Notas { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}