using CleanArchitectureTemplate.Application.DTOs;
using CleanArchitectureTemplate.Domain.Entities;
using CleanArchitectureTemplate.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Application.Features.Productos.Commands.CreateProducto
{
    public class CreateProductoHandler : IRequestHandler<CreateProductoCommand, int>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductoHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<int> Handle(CreateProductoCommand request, CancellationToken cancellationToken)
        {
            // Implementacion del manejador para crear un producto
            // Aqui iria la logica para guardar el producot en la base de datos
            // Retornar el ID del producto creado


            var producto = new Product(request.Nombre, request.Precio)
            {
                Descripcion = request.Descripcion
            };


            await _productRepository.AddAsync(producto);

            return producto.Id;

        }
    }
}
