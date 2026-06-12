using CleanArchitectureTemplate.Application.DTOs;
using CleanArchitectureTemplate.Application.Features.Productos.Commands.CreateProducto;
using CleanArchitectureTemplate.Application.Features.Productos.Commands.DeleteProducto;
using CleanArchitectureTemplate.Application.Features.Productos.Commands.UpdateProducto;
using CleanArchitectureTemplate.Application.Features.Productos.Queries.GetAllProductos;
using CleanArchitectureTemplate.Application.Features.Productos.Queries.GetProductoById;
using CleanArchitectureTemplate.Application.UseCases;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureTemplate.Presentation.Controllers
{
    public class ProductsController : Controller
    {
        // private readonly ProductService _productService;
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        public async Task<IActionResult> Index()
        {
            //var productos = await _productService.ObtenerProductosAsync();
            var productos = await _mediator.Send(new GetAllProductosQuery());

            return View(productos);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(ProductDTO productDTO)
        {
            if (!ModelState.IsValid)
                return View(productDTO);

            // creo el objeto command para enviarlo por MediatR
            var command = new CreateProductoCommand
            {
                Descripcion = productDTO.Descripcion,
                Precio = productDTO.Precio,
                Nombre = productDTO.Nombre
            };

            //await _productService.AgregarProductoAsync(productDTO);
            await _mediator.Send(command);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            //var product = await _productService.ObtenerProductoPorIdAsync(id);
            var product = await _mediator.Send(new GetProductoByIdQuery(id)); 

            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(ProductDTO productDTO)
        {
            if (!ModelState.IsValid)
                return View(productDTO);

            // creo el objeto command para enviarlo por MediatR
            var command = new UpdateProductoCommand
            {
                Precio = productDTO.Precio,
                Descripcion = productDTO.Descripcion,
                Nombre = productDTO.Nombre,
                Id = productDTO.Id
            };

            
            await _mediator.Send(command);
            //await _productService.ActualizarProductoAsync(productDTO);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            //var producto = await _productService.ObtenerProductoPorIdAsync(id);
            var producto = await _mediator.Send(new GetProductoByIdQuery(id));

            if (producto == null)
                return NotFound();

            return View(producto);
        }

        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarEliminar(int id)
        {
            //var producto = await _productService.ObtenerProductoPorIdAsync(id);
            var producto = await _mediator.Send(new GetProductoByIdQuery(id));

            if (producto == null)
                return NotFound();

            //await _productService.EliminarProductoAsync(id);
            await _mediator.Send(new DeleteProductoCommand(id));

            return RedirectToAction("Index");
        }

    }
}
