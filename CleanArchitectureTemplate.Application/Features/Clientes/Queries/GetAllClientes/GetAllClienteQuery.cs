using CleanArchitectureTemplate.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Application.Features.Clientes.Queries.GetAllCliente
{
    public class GetAllClienteQuery : IRequest<List<ClienteDTO>>{}
}
