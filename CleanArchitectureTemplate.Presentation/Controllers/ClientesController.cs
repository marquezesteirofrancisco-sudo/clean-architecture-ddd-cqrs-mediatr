using CleanArchitectureTemplate.Application.DTOs;
using CleanArchitectureTemplate.Application.Features.Productos.Commands.CreateProducto;
using CleanArchitectureTemplate.Application.Features.Productos.Commands.DeleteProducto;
using CleanArchitectureTemplate.Application.Features.Productos.Commands.UpdateProducto;
using CleanArchitectureTemplate.Application.Features.Productos.Queries.GetProductoById;
using CleanArchitectureTemplate.Application.UseCases;
using Microsoft.AspNetCore.Mvc;
using System.CodeDom;

namespace CleanArchitectureTemplate.Presentation.Controllers
{
    public class ClientesController : Controller
    {
        private readonly ClienteService _clienteService;

        public ClientesController(ClienteService clienteService) => _clienteService = clienteService;

        public async Task<IActionResult> Index()
        {
            var clientes = await _clienteService.ObtenerClienteAsync();

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

            await _clienteService.AgregarClienteAsync(clienteDTO);

            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var cliente = await _clienteService.ObtenerClientePorIdAsync(id);

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

            await _clienteService.ActualizarClienteAsync(clienteDTO);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            var cliente = await _clienteService.ObtenerClientePorIdAsync(id);

            if (cliente == null)
                return NotFound();

            return View(cliente);
        }


        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarEliminar(int id)
        {
            var cliente = await _clienteService.ObtenerClientePorIdAsync(id);

            if (cliente == null)
                return NotFound();

            await _clienteService.EliminarClienteAsync(id);
            
            return RedirectToAction("Index");
        }


    }
}
