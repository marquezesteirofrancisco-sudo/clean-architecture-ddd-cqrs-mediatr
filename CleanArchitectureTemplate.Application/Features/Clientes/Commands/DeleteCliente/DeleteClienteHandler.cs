using CleanArchitectureTemplate.Domain.Interfaces;
using MediatR;
 

namespace CleanArchitectureTemplate.Application.Features.Clientes.Commands.DeleteCliente
{
    internal class DeleteClienteHandler: IRequestHandler<DeleteClienteCommand>
    {
        
        private readonly IClientesRepository _clientRepository;

        public DeleteClienteHandler(IClientesRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<Unit> Handle(DeleteClienteCommand request, CancellationToken cancellationToken)
        {

            await _clientRepository.DeleteAsync(request.Id);

            return Unit.Value;
        }
    }
}
