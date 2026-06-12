using CleanArchitectureTemplate.Application.DTOs;
using CleanArchitectureTemplate.Domain.Interfaces;
using MediatR;


namespace CleanArchitectureTemplate.Application.Features.Productos.Queries.GetProductoById
{
    public class GetProductoByIdQueryHandler : IRequestHandler<GetProductoByIdQuery, ProductDTO>
    {
        private readonly IProductRepository _productRepository;

        public GetProductoByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDTO> Handle(GetProductoByIdQuery request, CancellationToken cancellationToken)
        {
            var producto = await _productRepository.GetByIdAsync(request.Id);

            if (producto == null)
                throw new ArgumentException("El proucto no existe");

            var productoDTO = new ProductDTO
            {
                Descripcion = producto.Descripcion,
                Nombre = producto.Nombre,
                Id = producto.Id,
                Precio = producto.Precio
            };

            return productoDTO;
        }
    }
}
