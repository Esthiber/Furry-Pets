using System.ComponentModel.DataAnnotations;

namespace PawfectMatch.Models
{
    public class TipoViviendas
    {
        [Key]
        public int TipoViviendasID { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        public bool IsDeleted { get; set; } = false;
    }
}