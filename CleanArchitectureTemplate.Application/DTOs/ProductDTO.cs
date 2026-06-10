using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Application.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe introducir el nombre del producto obligatoriamente.")]
        [StringLength (100)]
        public string Nombre { get; set; }

        [Range (0, 100)]
        public decimal Precio { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaCreacion { get; set; }
    }
}
