using System.ComponentModel.DataAnnotations;

namespace PawfectMatch.Models
{
    public class TiposServicios
    {
        [Key]
        public int TiposServiciosID { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; } = string.Empty;

        public bool IsDeleted { get; set; } = false;
    }
}