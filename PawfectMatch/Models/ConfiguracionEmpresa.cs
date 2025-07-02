using System.ComponentModel.DataAnnotations;

namespace PawfectMatch.Models
{
    public class ConfiguracionEmpresa
    {
        [Key]
        public int EmpresaID { get; set; }

        [Required]
        [StringLength(150)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(20)]
        public string? Telefono { get; set; }

        [Required]
        [StringLength(20)]
        public string RNC { get; set; } = string.Empty;

        [StringLength(250)]
        public string? Direccion { get; set; }
    }
}