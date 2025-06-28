using PawfectMatch.Models.Adopciones._Solicitudes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawfectMatch.Models.Adopciones._Servicios
{
    public class SolicitudesServicios
    {
        [Key]
        public int SolicitudServicioId { get; set; }

        public int SolicitudAdopcionId { get; set; }

        public int ServicioId { get; set; }

        [InverseProperty("SolicitudesServicios")]
        [ForeignKey("SolicitudAdopcionId")]
        virtual public SolicitudesAdopciones SolicitudAdopcion { get; set; } = null!;

        [ForeignKey("ServicioId")]
        virtual public Servicios? Servicio { get; set; }
    }
}
