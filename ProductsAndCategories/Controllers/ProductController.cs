using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAndCategories.Data;
using ProductsAndCategories.Data.Entities;

namespace ProductsAndCategories.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        MyDbContext _dbContext;
        public ProductController(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _dbContext.Products.Include(p => p.Category).Select(p => new
            {
                ProductName = p.ProdName,
                Price = p.Price,
                CategoryName = p.Category.CategName
            }).ToListAsync();
            return Ok(products);
        }
        [HttpGet("GetAll/{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _dbContext.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.ProdId == id);
            if (product == null)
            {
                return NotFound();
            }
            var prod = new
            {
                ProductId = product.ProdId,
                Price = product.Price,
                ProductName = product.ProdName,
                CategoryName = product.Category.CategName
            };

            return Ok(prod);
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct(ProductDto productDto)
        {
            var category = await _dbContext.Category
                                  .FirstOrDefaultAsync(c => c.CategName == productDto.CategName);

            if (category == null)
            {
                return NotFound("Category not found");
            }
            var product = new Product
            {
                ProdName = productDto.ProdName,
                Price = productDto.Price,
                CategoryId = category.CategId
            };

            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();

            return Ok(new
            {
                product.ProdName,
                product.Price,
                CategoryName = category.CategName
            });
        }

        [HttpPut("UpdatePrice")]
        public async Task<IActionResult> UpdateProduct(int id, ProductDto productDto)
        {
            var product = await _dbContext.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.ProdId == id);
            if (product == null)
            {
                return NotFound("Product not found");
            }

            product.Price = productDto.Price;

            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();

            return Ok(new
            {
                product.ProdName,
                product.Price,
                CategoryName = product.Category.CategName
            });

        }

        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _dbContext.Products
                                          .FirstOrDefaultAsync(p => p.ProdId == id);

            if (product == null)
            {
                return NotFound("Product not found");
            }

            _dbContext.Products.Remove(product);

            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Product deleted successfully" });
        }
    }

}
