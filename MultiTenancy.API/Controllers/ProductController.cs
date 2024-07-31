using Microsoft.AspNetCore.Mvc;
using MultiTenancy.API.DTOS;

namespace MultiTenancy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService productService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await productService.GetAllAsync();
            return Ok(products);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await productService.GetByIdAsync(id);

            return product is null ? NotFound() : Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> CreatedAsync(CreateProduct dto)
        {
            Product product = new()
            {
                Name = dto.Name,
                Descreption = dto.Description,
                Rate = dto.Rate,
            };
            var createdProduct = await productService.CreatedAsync(product);
            return Ok(createdProduct);
        }
    }
}
