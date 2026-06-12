using CleanArchitectureTemplate.Domain.Interfaces;
using MediatR;
 

namespace CleanArchitectureTemplate.Application.Features.Productos.Commands.DeleteProducto
{
    internal class DeleteProductoHandler: IRequestHandler<DeleteProductoCommand>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductoHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Unit> Handle(DeleteProductoCommand request, CancellationToken cancellationToken)
        {

            await _productRepository.DeleteAsync(request.Id);

            return Unit.Value;
        }
    }
}
