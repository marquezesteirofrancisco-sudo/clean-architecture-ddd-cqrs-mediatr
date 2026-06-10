using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public decimal Precio { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaCreacion { get; set; }

        public Product(string nombre, decimal precio)
        {
            if (string.IsNullOrEmpty(nombre))
                throw new ArgumentException("El nombre no puede estar vacia", nameof(nombre));

            if (precio < 0)
                throw new ArgumentException(nameof(nombre), "El precio debe ser mayor que cero.");

            Nombre = nombre;
            Precio = precio;
        }
    }
}
