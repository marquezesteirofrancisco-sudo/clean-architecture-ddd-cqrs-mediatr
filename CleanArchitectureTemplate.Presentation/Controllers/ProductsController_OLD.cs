using CleanArchitectureTemplate.Application.DTOs;
using CleanArchitectureTemplate.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureTemplate.Presentation.Controllers
{
    public class ProductsController_OLD : Controller
    {
        private readonly ProductService _productService;

        public ProductsController_OLD(ProductService productService)
        {
            _productService = productService;
        }


        public async Task<IActionResult> Index()
        {
            var productos = await _productService.ObtenerProductosAsync();

            return View(productos);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear (ProductDTO productDTO)
        {
            if (!ModelState.IsValid)
                return View(productDTO);

            await _productService.AgregarProductoAsync(productDTO);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Editar (int id)
        {
            var product = await _productService.ObtenerProductoPorIdAsync(id);

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

            await _productService.ActualizarProductoAsync(productDTO);


            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar (int id)
        {
            var producto = await _productService.ObtenerProductoPorIdAsync(id);

            if (producto == null)
                return NotFound();

            return View(producto);
        }

        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarEliminar(int id)
        {
            var producto = await _productService.ObtenerProductoPorIdAsync(id);

            if (producto == null)
                return NotFound();

            await _productService.EliminarProductoAsync(id);

            return RedirectToAction("Index");
        }

    }
}
