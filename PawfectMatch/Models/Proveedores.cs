using System.ComponentModel.DataAnnotations;

namespace PawfectMatch.Models
{
    public class Proveedores
    {
        [Key]
        public int ProveedoresID { get; set; }

        [Required]
        [StringLength(150)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string RNC { get; set; } = string.Empty;

        [StringLength(20)]
        public string? Telefono { get; set; }

        [StringLength(100)]
        public string? Email { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}