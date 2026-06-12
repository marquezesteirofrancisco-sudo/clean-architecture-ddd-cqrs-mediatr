using CleanArchitectureTemplate.Domain.Interfaces;
using MediatR;


namespace CleanArchitectureTemplate.Application.Features.Clientes.Commands.UpdateCliente
{
    public class UpdateClienteHandler : IRequestHandler<UpdateClienteCommand>
    {
        private readonly IClientesRepository _clienteRepository;

        public UpdateClienteHandler(IClientesRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<Unit> Handle(UpdateClienteCommand request, CancellationToken cancellationToken)
        {
            // buscamos el producto por el id de la request
            var cliente = await _clienteRepository.GetByIdAsync(request.Id);

            // si no lo encontramos lanzamos una excepticion
            if (cliente == null)
                throw new Exception("Cliente no encontrado");

            // actualizamos las propiedades del produccto
            cliente.Id = request.Id;
            cliente.Nombre = request.Nombre;
            cliente.Descripcion = request.Descripcion;
            cliente.Ciudad = request.Ciudad;
            cliente.CodigoPostal = request.CodigoPostal;

            // llamamos al mentodo para acualizar en la base de datos
            await _clienteRepository.UpdateAsync(cliente);

            return Unit.Value;
        }
    }
}
