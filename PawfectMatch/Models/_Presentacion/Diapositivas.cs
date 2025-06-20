using System.ComponentModel.DataAnnotations;

namespace PawfectMatch.Models._Presentacion
{
    public class Diapositivas
    {
        [Key]
        public int DiapositivaId { get; set; }

        public bool IsTituloLeftActive { get; set; } = false;
        public string? Titulo_Left { get; set; }
        public string? SubTitulo_Left { get; set; }

        public bool IsTituloRightActive { get; set; } = false;
        public string? Titulo_Right { get; set; }
        public string? SubTitulo_Right { get; set; }

        public bool IsButtonLeftActive { get; set; } = false;
        public string? TextButton_Left { get; set; }
        public string? LinkButton_Left { get; set; }

        public bool IsButtonRightActive { get; set; } = false;
        public string? TextButton_Right { get; set; }
        public string? LinkButton_Right { get; set; }

        [Required(ErrorMessage = "Imagen de fondo Requerida")]
        public string ImageUrl { get; set; } = null!;

        [Required(ErrorMessage = "Es necesario un numero de orden")]
        [Range(1, 100, ErrorMessage = "El numero de orden debe mayor a 1 y menor a 100")]
        public int Orden { get; set; }

        public string? Animacion { get; set; }

    }
}
