using CleanArchitectureTemplate.Application.DTOs;
using CleanArchitectureTemplate.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Application.Features.Clientes.Queries.GetAllCliente
{
    public class GetAllClientesHandler: IRequestHandler<GetAllClienteQuery, List<ClienteDTO>>
    {
        private readonly IClientesRepository _clientRepository;

        public GetAllClientesHandler(IClientesRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<List<ClienteDTO>> Handle(GetAllClienteQuery request, CancellationToken cancellationToken)
        {
            var clientes = await _clientRepository.GetAllAsync();

            return clientes.Select(p => new ClienteDTO
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Direccion = p.Direccion,
                Descripcion = p.Descripcion,
                CodigoPostal = p.CodigoPostal,
                Ciudad = p.Ciudad,
            }).ToList();
        }
    }
}
