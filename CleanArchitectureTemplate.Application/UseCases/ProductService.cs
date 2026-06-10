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
    public class ProductService
    {

        private readonly IProductRepository _productRepository;


        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<ProductDTO>> ObtenerProductosAsync()
        {
            var products = await _productRepository.GetAllAsync();

            return products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Precio = p.Precio,
                Descripcion = p.Descripcion,
                FechaCreacion = p.FechaCreacion,
            }).ToList();

        }

        public async Task AgregarProductoAsync(ProductDTO productDTO)
        {

            // Validar el nombre del DTO
            if (string.IsNullOrEmpty(productDTO.Nombre))
                throw new ArgumentException("El nombre del producto es obligatorio");

            // Validar el precio del DTO
            if (productDTO.Precio < 0)
                throw new ArgumentException("El precio del producto no debe ser negativo");

            // Mapeamos el DTO a la entidad
            var producto = new Product 
            { 
                Descripcion = productDTO.Descripcion,
                Nombre =productDTO.Nombre,
                FechaCreacion = productDTO.FechaCreacion,
                Precio = productDTO.Precio 
            };

            // añadimos el producto en la base de datos
            await _productRepository.AddAsync(producto);

        }

 
        public async Task<ProductDTO?> ObtenerProductoPorIdAsync(int id)
        {
            // 1. Busco el producto por Id en la base de datos (con su respectivo await)
            var producto = await _productRepository.GetByIdAsync(id);

            // 2. Si no lo encuentro devuelvo una excepción
            if (producto == null)
                throw new ArgumentException($"No se ha encontrado el producto con el ID {id}");

            // 3. Mapeo la entidad al DTO y lo devuelvo
            return new ProductDTO
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Precio = producto.Precio,
            };
        }


        public async Task<Product> ActualizarProductoAsync(ProductDTO productDTO)
        {
            // Validar el nombre del DTO
            if (string.IsNullOrEmpty(productDTO.Nombre))
                throw new ArgumentException("El nombre del producto es obligatorio");

            // Validar el precio del DTO
            if (productDTO.Precio < 0)
                throw new ArgumentException("El precio del producto no debe ser negativo");


            // Mapeamos el DTO a la entidad
            var producto = new Product
            {
                Descripcion = productDTO.Descripcion,
                Nombre = productDTO.Nombre,
                FechaCreacion = productDTO.FechaCreacion,
                Precio = productDTO.Precio
            };

            // actualizamos el producto en la base de datos
            await _productRepository.UpdateAsync(producto);

            // devuelve el producto
            return producto;

        }

        public async Task EliminarProductoAsync(int id)
        {

            // 1. Valido que el Id del producto sea un numero positivo
            if ( id < 0 )
                throw new ArgumentException("El id del producto no puede ser negativo");

            // 2. Busco el producto por Id en la base de datos (con su respectivo await)
            var producto = await _productRepository.GetByIdAsync(id);

            // 3. Si no lo encuentro devuelvo una excepción
            if (producto == null)
                throw new ArgumentException($"No se ha encontrado el producto con el ID {id}");

            // 4. Elimino el proeucot en la base de datos
            await _productRepository.DeleteAsync(id);

        }

    }
}
