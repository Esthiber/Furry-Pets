
using PawfectMatch.Models.Adopciones;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawfectMatch.Models
{
    public class SolicitudesAdopciones
    {
        [Key]
        public int SolicitudesAdopcionesID { get; set; }

        [Required]
        public int PersonasID { get; set; }
        [ForeignKey("PersonasID")]
        public Personas? Persona { get; set; }

        [Required]
        public int EstadoSolicitudID { get; set; }
        [ForeignKey("EstadoSolicitudID")]
        public EstadoSolicitud? EstadoSolicitud { get; set; }

        [Required]
        public int MascotasAdopcionID { get; set; }
        [ForeignKey("MascotasAdopcionID")]
        public MascotasAdopcion? MascotaAdopcion { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        public bool IsDeleted { get; set; } = false;

        // Navegación a histórico de adopciones (si quieres relación inversa)
        public ICollection<HistorialAdopciones>? Historiales { get; set; }
    }
}