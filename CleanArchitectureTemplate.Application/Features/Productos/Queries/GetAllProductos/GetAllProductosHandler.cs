using CleanArchitectureTemplate.Application.DTOs;
using CleanArchitectureTemplate.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Application.Features.Productos.Queries.GetAllProductos
{
    public class GetAllProductosHandler: IRequestHandler<GetAllProductosQuery, List<ProductDTO>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductosHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<ProductDTO>> Handle(GetAllProductosQuery request, CancellationToken cancellationToken)
        {
            var productos = await _productRepository.GetAllAsync();

            return productos.Select(p => new ProductDTO
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Precio = p.Precio,
            }).ToList();
        }
    }
}
