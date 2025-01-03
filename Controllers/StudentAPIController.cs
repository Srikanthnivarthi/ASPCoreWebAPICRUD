using ASPCoreWebAPICRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPCoreWebAPICRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        private readonly ProductDBContext context;

        public StudentAPIController(ProductDBContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var data = await context.Products.ToListAsync();
            return Ok(data);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await context.Products.FindAsync(id);
            if (product==null)
                return NotFound();
            return product;
        }
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
            return Ok(product);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, Product product)
        {
            if (id != product.Id)
                return BadRequest();
            context.Entry(product).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Ok(product);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await context.Products.FindAsync(id);
            if (product == null)
                return NotFound();
            context.Products.Remove(product);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
