using System.ComponentModel.DataAnnotations;

namespace PawfectMatch.Models.POS
{
    public class VetasTabs
    {
        [Key]
        public int VetasTabsID { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        public string? Color { get; set; }

        public string? Icono { get; set; }

        public int Orden { get; set; } = 0;

        public bool IsDeleted { get; set; } = false;



    }
}
