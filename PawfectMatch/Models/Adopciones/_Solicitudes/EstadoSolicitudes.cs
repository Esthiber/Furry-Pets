using System.ComponentModel.DataAnnotations;

namespace PawfectMatch.Models.Adopciones._Solicitudes
{
    public class EstadoSolicitudes
    {
        [Key]
        public int EstadoSolicitudId { get; set; }

        public string Nombre { get; set; } = null!;
    }
}
