using CleanArchitectureTemplate.Application.DTOs;
using CleanArchitectureTemplate.Application.Features.Clientes.Commands.CreateCliente;
using CleanArchitectureTemplate.Application.Features.Clientes.Commands.DeleteCliente;
using CleanArchitectureTemplate.Application.Features.Clientes.Commands.UpdateCliente;
using CleanArchitectureTemplate.Application.Features.Clientes.Queries.GetAllCliente;
using CleanArchitectureTemplate.Application.Features.Clientes.Queries.GetClienteById;
using CleanArchitectureTemplate.Application.Features.Productos.Commands.CreateProducto;
using CleanArchitectureTemplate.Application.Features.Productos.Commands.DeleteProducto;
using CleanArchitectureTemplate.Application.Features.Productos.Commands.UpdateProducto;
using CleanArchitectureTemplate.Application.Features.Productos.Queries.GetProductoById;
using CleanArchitectureTemplate.Application.UseCases;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.CodeDom;

namespace CleanArchitectureTemplate.Presentation.Controllers
{
    public class ClientesController : Controller
    {
        private readonly ClienteService _clienteService;

        private readonly IMediator _mediator;

        public ClientesController(ClienteService clienteService, IMediator mediator)
        {
            _mediator = mediator;
            _clienteService = clienteService;
        }

        public async Task<IActionResult> Index()
        {
            //var clientes = await _clienteService.ObtenerClienteAsync();
            var clientes = await _mediator.Send(new GetAllClienteQuery());

            return View(clientes);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(ClienteDTO clienteDTO)
        {
            if (!ModelState.IsValid)
                return View(clienteDTO);

            //await _clienteService.AgregarClienteAsync(clienteDTO);

            var command = new CreateClienteCommand
            {
                Nombre = clienteDTO.Nombre,
                Descripcion = clienteDTO.Descripcion,
                Ciudad = clienteDTO.Ciudad,
                CodigoPostal = clienteDTO.CodigoPostal,
                Direccion = clienteDTO.Direccion,
            };

            await _mediator.Send(command);

            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            //var cliente = await _clienteService.ObtenerClientePorIdAsync(id);
            var cliente = await _mediator.Send(new GetClienteByIdQuery(id));

            if (cliente == null)
                return NotFound();

            return View(cliente);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(ClienteDTO clienteDTO)
        {
            if (!ModelState.IsValid)
                return View(clienteDTO);

            var command = new UpdateClienteCommand
            {
                Id = clienteDTO.Id,
                Nombre = clienteDTO.Nombre,
                Descripcion = clienteDTO.Descripcion,
                Ciudad = clienteDTO.Ciudad,
                CodigoPostal = clienteDTO.CodigoPostal,
                Direccion = clienteDTO.Direccion,
            };

            await _mediator.Send(command);
            //await _clienteService.ActualizarClienteAsync(clienteDTO);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            //var cliente = await _clienteService.ObtenerClientePorIdAsync(id);
            var cliente = await _mediator.Send(new GetClienteByIdQuery(id));

            if (cliente == null)
                return NotFound();

            return View(cliente);
        }


        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarEliminar(int id)
        {
            //var cliente = await _clienteService.ObtenerClientePorIdAsync(id);
            var cliente = await _mediator.Send(new GetClienteByIdQuery(id));

            if (cliente == null)
                return NotFound();

            //await _clienteService.EliminarClienteAsync(id);
            await _mediator.Send(new DeleteClienteCommand(id));

            return RedirectToAction("Index");
        }


    }
}
