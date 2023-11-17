using HPlusSport.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HPlusSport.API.Controllers;
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
        public async Task<ActionResult> GetAllProducts([FromQuery] ProductQueryParameters queryParameters)
        {
            IQueryable<Product> products = _context.Products;
            if (queryParameters.MinPrice != null)
            {
                products = products.Where(p => p.Price >= queryParameters.MinPrice);
            }
            if (queryParameters.MaxPrice != null)
            {
                products = products.Where(p => p.Price <= queryParameters.MaxPrice);
            }
            products = products.Skip(queryParameters.Size * (queryParameters.Page - 1))
                .Take(queryParameters.Size);
            
            return Ok(await products.ToArrayAsync());
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

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product){
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
          
            return CreatedAtAction(
            "GetProduct",
            new{ id = product.Id}, 
            product
            );
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutProduct(int id, [FromBody]Product product){
            if (id != product.Id)
            {
                return BadRequest();
            }
            _context.Entry(product).State = EntityState.Modified;

            try
            {
                       await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                 if (_context.Products.Any(p=>p.Id == id))
                 {
                    return NotFound();
                 }
                 else{
                    throw;
                 }
            }
            
            return NoContent();
     
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id){
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
            
        }

          [HttpPost]
          [Route("Delete")]
        public async Task<ActionResult> DeleteMultipleProducts([FromQuery]int[] ids){
            var products = new List<Product>();
            foreach (var id in ids)
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                products.Add(product);
            }
            _context.Products.RemoveRange(products);
            await _context.SaveChangesAsync();
            return Ok(products);
            
        }
    }
