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
    public  ActionResult GetAllCategories(){
        var categories = _context.Categories.Include(c=>c.Products).ToArray();
    return Ok(categories);
    }
    [HttpGet("{id}")]
    public ActionResult GetCategories(int id){
        var category = _context.Categories.Find(id);
        _context.Entry(category).Collection(c => c.Products).Load();
        return Ok(category);
    }
        
    }
}