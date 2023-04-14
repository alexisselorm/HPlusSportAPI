using HPlusSport.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HPlusSport.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ShopContext _context;
        public ProductsController(ShopContext context){
            _context = context;

            _context.Database.EnsureCreated();
        }

        // Get all products
        [HttpGet]
        public async Task<ActionResult> GetAllProducts(){
            var products= await _context.Products.ToArrayAsync();
            return Ok(products);
        }

        // Get one product
        [HttpGet("{id}")]
        public async Task<ActionResult> GetProduct(int id){ 

              var product = await _context.Products.FindAsync(id);
              if (product == null){
                return NotFound();
              }
              return Ok(product);
      
        }
        
    }
}