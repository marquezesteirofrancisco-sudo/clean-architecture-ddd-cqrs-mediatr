using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Application.Features.Productos.Commands.DeleteProducto
{
    public class DeleteProductoCommand : IRequest
    {
        public int Id { get; set; }

        public DeleteProductoCommand(int id) => Id = id;
    }
}
