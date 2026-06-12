using MediatR;


namespace CleanArchitectureTemplate.Application.Features.Clientes.Commands.UpdateCliente
{
    public class UpdateClienteCommand : IRequest
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public int CodigoPostal { get; set; }
    }
}
