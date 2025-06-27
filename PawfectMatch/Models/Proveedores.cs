using System.ComponentModel.DataAnnotations;

namespace PawfectMatch.Models
{
    public class Proveedores
    {

        [Key]
        public int ProveedorId { get; set; }
        [Required]
        public string? Nombre { get; set; }
        [Required]
        public string? Direccion { get; set; }
        [Phone]
        public string? Telefono { get; set; }
        [EmailAddress]
        public string? Email { get; set; }

    }
}
