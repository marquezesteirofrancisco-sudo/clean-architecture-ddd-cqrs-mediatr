using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Application.Features.Productos.Commands.CreateProducto
{
    public class CreateProductoCommand : IRequest<int>
    {
        public string Nombre { get; set; }

        public decimal Precio { get; set; }

        public string Descripcion { get; set; }
    }
}
