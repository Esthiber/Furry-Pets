using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawfectMatch.Models.POS
{
    public class ProductosInTabs
    {
        [Key]
        public int ProductosInTabsID { get; set; }

        [Required]
        public int VetasTabsID { get; set; }
        [ForeignKey("VetasTabsID")]
        public VetasTabs? VetasTab { get; set; }

        [Required]
        public int ProductosID { get; set; }
        [ForeignKey("ProductosID")]
        public Productos? Producto { get; set; }

        public int Orden { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
