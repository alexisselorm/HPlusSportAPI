using HPlusSport.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HPlusSport.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ShopContext _context;

        public CategoriesController(ShopContext context){
            _context = context;

            _context.Database.EnsureCreated();
        }

    // Get all categories
    [HttpGet]
    public async Task<ActionResult> GetAllCategories(){
        var categories =await _context.Categories.Include(c=>c.Products).ToArrayAsync();
    return Ok(categories);
    }
    [HttpGet("{id}")]
    public async  Task<ActionResult> GetCategories([FromRoute]int id){
        var category =await _context.Categories.FindAsync(id);
        if (category != null){

        await _context.Entry(category).Collection(c => c.Products).LoadAsync();
        return Ok(category);
        }
        else{
            return NotFound();
        }
    }
        
    }
}