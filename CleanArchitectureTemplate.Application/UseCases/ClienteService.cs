using CleanArchitectureTemplate.Application.DTOs;
using CleanArchitectureTemplate.Domain.Entities;
using CleanArchitectureTemplate.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureTemplate.Application.UseCases
{
    public class ClienteService
    {

        private readonly IClientesRepository _clientesRepository;

        public ClienteService(IClientesRepository clientesRepository)
        {
            _clientesRepository = clientesRepository;
        }


        public async Task<List<ClienteDTO>> ObtenerClienteAsync()
        {
            var clientes = await _clientesRepository.GetAllAsync();

            return clientes.Select(p => new ClienteDTO
            {
                Id = p.Id,
                Direccion = p.Direccion,
                Descripcion = p.Descripcion,
                Nombre = p.Nombre,
                Ciudad = p.Ciudad,
                CodigoPostal = p.CodigoPostal,
            }).ToList();

        }

        public async Task AgregarClienteAsync(ClienteDTO clienteDTO)
        {

            // Validar el nombre del DTO
            if (string.IsNullOrEmpty(clienteDTO.Nombre))
                throw new ArgumentException("El nombre del cliente es obligatorio");

            // Validar el precio del DTO
            if (clienteDTO.CodigoPostal < 1)
                throw new ArgumentException("El C.P. del codigo postal no debe ser negativo");

            // busco el produco por el nombre por si ya existe
            var clienteExistente = await _clientesRepository.GetByNameAsync(clienteDTO.Nombre);

            // si lo ha encontrado lanzo una exception
            if (clienteExistente != null)
                throw new ArgumentException("El cliente ya existe");

            // Mapeamos el DTO a la entidad
            var cliente = new Cliente()
            {
                Direccion = clienteDTO.Direccion,
                Descripcion = clienteDTO.Descripcion,
                Nombre = clienteDTO.Nombre,
                Ciudad = clienteDTO.Ciudad,
                CodigoPostal = clienteDTO.CodigoPostal,
            };

            // añadimos el producto en la base de datos
            await _clientesRepository.AddAsync(cliente);

        }


        public async Task<ClienteDTO?> ObtenerClientePorIdAsync(int id)
        {
            // 1. Busco el producto por Id en la base de datos (con su respectivo await)
            var cliente = await _clientesRepository.GetByIdAsync(id);

            // 2. Si no lo encuentro devuelvo una excepción
            if (cliente == null)
                throw new ArgumentException($"No se ha encontrado el cliente con el ID {id}");

            // 3. Mapeo la entidad al DTO y lo devuelvo
            return new ClienteDTO
            {
                Id = cliente.Id,
                Direccion = cliente.Direccion,
                Descripcion = cliente.Descripcion,
                Nombre = cliente.Nombre,
                Ciudad = cliente.Ciudad,
                CodigoPostal = cliente.CodigoPostal,
            };
        }


        public async Task<ClienteDTO> ActualizarClienteAsync(ClienteDTO clienteDTO)
        {
            // Validar el nombre del DTO
            if (string.IsNullOrEmpty(clienteDTO.Nombre))
                throw new ArgumentException("El nombre del cliente es obligatorio");

            // Validar el precio del DTO
            if (clienteDTO.CodigoPostal < 0)
                throw new ArgumentException("El C.P. del cliente no debe ser negativo");


            // Mapeamos el DTO a la entidad
            var cliente = new Cliente()
            {
                Id = clienteDTO.Id,
                Direccion = clienteDTO.Direccion,
                Descripcion = clienteDTO.Descripcion,
                Nombre = clienteDTO.Nombre,
                Ciudad = clienteDTO.Ciudad,
                CodigoPostal = clienteDTO.CodigoPostal,
            };

            // actualizamos el producto en la base de datos
            await _clientesRepository.UpdateAsync(cliente);

            // devuelve el producto
            return clienteDTO;

        }

        public async Task EliminarClienteAsync(int id)
        {

            // 1. Valido que el Id del producto sea un numero positivo
            if (id < 0)
                throw new ArgumentException("El id del cliente no puede ser negativo");

            // 2. Busco el cliente por Id en la base de datos (con su respectivo await)
            var cliente = await _clientesRepository.GetByIdAsync(id);

            // 3. Si no lo encuentro devuelvo una excepción
            if (cliente == null)
                throw new ArgumentException($"No se ha encontrado el producto con el ID {id}");

            // 4. Elimino el proeucot en la base de datos
            await _clientesRepository.DeleteAsync(id);

        }


    }
}
