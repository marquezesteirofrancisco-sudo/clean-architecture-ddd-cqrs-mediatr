using CleanArchitectureTemplate.Application.DTOs;
using CleanArchitectureTemplate.Domain.Entities;
using CleanArchitectureTemplate.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Application.Features.Clientes.Commands.CreateCliente
{
    public class CreateClienteHandler : IRequestHandler<CreateClienteCommand, int>
    {
        private readonly IClientesRepository _clientRepository;

        public CreateClienteHandler(IClientesRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<int> Handle(CreateClienteCommand request, CancellationToken cancellationToken)
        {
            // Implementacion del manejador para crear un producto
            // Aqui iria la logica para guardar el producot en la base de datos
            // Retornar el ID del producto creado


            var cliente = new Cliente()
            {
                Nombre = request.Nombre,
                Descripcion = request.Descripcion,
                CodigoPostal = request.CodigoPostal,
                Direccion = request.Direccion,
                Ciudad = request.Ciudad,
            };


            await _clientRepository.AddAsync(cliente);

            return cliente.Id;

        }
    }
}
