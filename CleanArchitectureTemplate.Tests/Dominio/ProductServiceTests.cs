using CleanArchitectureTemplate.Application.DTOs;
using CleanArchitectureTemplate.Application.UseCases;
using CleanArchitectureTemplate.Domain.Entities;
using CleanArchitectureTemplate.Domain.Interfaces;
using Moq;
using NUnit.Framework;

namespace CleanArchitectureTemplate.Tests.Dominio
{
    public class ProductServiceTests
    {


        [Test]
        public async Task AgregarProducto_LanzaExcepcionSiNombreExiste()
        {
            // 1. Arrange (Preparación)
            var mockRepository = new Mock<IProductRepository>();

            // Unificamos el nombre a "hello" tanto en la entidad como en el DTO
            var productoExistente = new Product("hdaellos", 100m);

            // Configuramos el mock para que cuando el servicio valide si el nombre existe, devuelva el producto
            mockRepository
                .Setup(repo => repo.GetByNameAsync("hello"))
                .ReturnsAsync(productoExistente);

            var service = new ProductService(mockRepository.Object);

            var nuevoProducto = new ProductDTO()
            {
                Nombre = "hello",
                Precio = 200m,
                Descripcion = "Descripcion del producto"
            };

            // 2. Act (Acción) & 3. Assert (Verificación)
            // 🌟 Añadimos 'await' aquí para capturar correctamente la excepción asíncrona
            var exception = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await service.AgregarProductoAsync(nuevoProducto)
            );

            // Verificamos que el mensaje sea el esperado (corrigiendo la errata de "proruco")
            Assert.That(exception?.Message, Is.EqualTo("El producto ya existe"));
        }

    }
}
