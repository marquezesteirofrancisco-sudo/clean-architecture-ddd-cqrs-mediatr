using MediatR;


namespace CleanArchitectureTemplate.Application.Features.Productos.Commands.UpdateProducto
{
    public class UpdateProductoCommand : IRequest
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public decimal Precio { get; set; }

        public string Descripcion { get; set; }
    }
}
