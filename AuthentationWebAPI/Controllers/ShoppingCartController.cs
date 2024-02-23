using AuthentationWebAPI.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthentationWebAPI.Controllers
{
    [Authorize] // Apply authorization attribute to require authentication
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public ShoppingCartController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

       
    }
}
