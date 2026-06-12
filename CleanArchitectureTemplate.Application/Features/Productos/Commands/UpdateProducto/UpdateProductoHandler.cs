using CleanArchitectureTemplate.Domain.Interfaces;
using MediatR;


namespace CleanArchitectureTemplate.Application.Features.Productos.Commands.UpdateProducto
{
    public class UpdateProductoHandler : IRequestHandler<UpdateProductoCommand>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductoHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Unit> Handle(UpdateProductoCommand request, CancellationToken cancellationToken)
        {
            // buscamos el producto por el id de la request
            var producto = await _productRepository.GetByIdAsync(request.Id);

            // si no lo encontramos lanzamos una excepticion
            if (producto == null)
                throw new Exception("Producto no encontrado");

            // actualizamos las propiedades del produccto
            producto.Nombre = request.Nombre;
            producto.Descripcion = request.Descripcion;
            producto.Precio = request.Precio;

            // llamamos al mentodo para acualizar en la base de datos
            await _productRepository.UpdateAsync(producto);

            return Unit.Value;
        }
    }
}
