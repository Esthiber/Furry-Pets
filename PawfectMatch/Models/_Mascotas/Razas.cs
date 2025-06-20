using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawfectMatch.Models._Mascotas
{
    public class Razas
    {
        [Key]
        public int RazaId { get; set; }
        public int CategoriaId { get; set; }
        public string Nombre { get; set; } = null!;

        [ForeignKey("CategoriaId")]
        public Categorias Categoria { get; set; } = null!;
    }
}
