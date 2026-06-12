using CleanArchitectureTemplate.Application.DTOs;
using MediatR;


namespace CleanArchitectureTemplate.Application.Features.Clientes.Queries.GetClienteById
{
    public class GetClienteByIdQuery : IRequest<ClienteDTO>
    {

        public int Id { get; set; }

        public GetClienteByIdQuery(int id)
        {
            Id = id;
        }

    }
}
