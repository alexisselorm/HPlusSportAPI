using Microsoft.AspNetCore.Mvc;

namespace HPlusSport.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public string GetProducts(){
            return "products";
        }
        
    }
}