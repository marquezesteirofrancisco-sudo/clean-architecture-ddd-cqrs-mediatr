using CleanArchitectureTemplate.Application.DTOs;
using MediatR;


namespace CleanArchitectureTemplate.Application.Features.Productos.Queries.GetProductoById
{
    public class GetProductoByIdQuery : IRequest<ProductDTO>
    {

        public int Id { get; set; }

        public GetProductoByIdQuery(int id)
        {
            Id = id;
        }

    }
}
