using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebHost.DataAccess;
using WebHost.Models;

namespace WebHost.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductReviewsController : Controller
    {
        private readonly ProductDbContext _dbContext;

        public ProductReviewsController(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int? productId)
        {
            var query = _dbContext.ProductReviews.AsQueryable();

            if (productId != null)
            {
                query = query.Where(x => x.ProductId == productId);
            }

            return Ok(await query.ToArrayAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductReview review)
        {
            try
            {
                _dbContext.ProductReviews.Add(review);

                await _dbContext.SaveChangesAsync();

                return StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
    }
}
