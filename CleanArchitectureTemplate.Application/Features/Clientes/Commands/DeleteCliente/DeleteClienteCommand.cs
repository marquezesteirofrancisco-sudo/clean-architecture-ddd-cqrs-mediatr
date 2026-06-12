using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Application.Features.Clientes.Commands.DeleteCliente
{
    public class DeleteClienteCommand : IRequest
    {
        public int Id { get; set; }

        public DeleteClienteCommand(int id) => Id = id;
    }
}
