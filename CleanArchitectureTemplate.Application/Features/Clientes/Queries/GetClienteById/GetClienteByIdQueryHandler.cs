using CleanArchitectureTemplate.Application.DTOs;
using CleanArchitectureTemplate.Domain.Interfaces;
using MediatR;


namespace CleanArchitectureTemplate.Application.Features.Clientes.Queries.GetClienteById
{
    public class GetClienteByIdQueryHandler : IRequestHandler<GetClienteByIdQuery, ClienteDTO>
    {
        private readonly IClientesRepository _clientesRepository;

        public GetClienteByIdQueryHandler(IClientesRepository clientesRepository)
        {
            _clientesRepository = clientesRepository;
        }

        public async Task<ClienteDTO> Handle(GetClienteByIdQuery request, CancellationToken cancellationToken)
        {
            var cliente = await _clientesRepository.GetByIdAsync(request.Id);

            if (cliente == null)
                throw new ArgumentException("El cliente no existe");

            var clienteDTO = new ClienteDTO
            {
                Descripcion = cliente.Descripcion,
                Nombre = cliente.Nombre,
                Id = cliente.Id,
                Ciudad = cliente.Ciudad,
                Direccion = cliente.Direccion,
                CodigoPostal = cliente.CodigoPostal
            };

            return clienteDTO;
        }
    }
}
