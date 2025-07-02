using System.ComponentModel.DataAnnotations;

namespace PawfectMatch.Models
{
    public class EstadosCitas
    {
        [Key]
        public int EstadosCitasID { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; } = string.Empty;

        public bool IsDeleted { get; set; } = false;
    }
}