using System.ComponentModel.DataAnnotations;

namespace PawfectMatch.Models.POS
{
    public class CategoriasProductos
    {
        [Key]
        public int CategoriasProductosID { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        public bool IsDeleted { get; set; } = false;
    }
}