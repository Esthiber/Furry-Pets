using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawfectMatch.Models.POS
{
    public class Productos
    {
        [Key]
        public int ProductosID { get; set; }

        [Required]
        public int CategoriasProductosID { get; set; }
        [ForeignKey(nameof(CategoriasProductosID))]
        public CategoriasProductos? CategoriaProducto { get; set; }

        [Required]
        public int ProveedoresID { get; set; }
        [ForeignKey(nameof(ProveedoresID))]
        public Proveedores? Proveedor { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Descripcion { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Costo { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Precio { get; set; }

        [Required]
        public int Stock { get; set; }

        [StringLength(255)]
        public string? ImagenUrl { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}