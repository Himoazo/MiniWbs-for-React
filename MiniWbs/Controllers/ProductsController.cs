using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniWbs.Data;
using MiniWbs.DTOs;
using MiniWbs.Models;

namespace MiniWbs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutProduct(int id, ProductDTO product)
        {
            if (!ModelState.IsValid || product is null)
            {
                return BadRequest(ModelState);
            }

            Product? editedProduct = await _context.Products.FindAsync(id);

            if (editedProduct is null)
            {
                return NotFound("Product was not found");
            }

            editedProduct.ProductName = product.ProductName;
            editedProduct.Price = product.Price;
            editedProduct.Quantity = product.Quantity;

            _context.Entry(editedProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Product>> PostProduct(ProductDTO product)
        {
            if (!ModelState.IsValid || product is null)
            {
                return BadRequest(ModelState);
            }

            Product newProduct = new()
            {
                ProductName = product.ProductName,
                Price = product.Price,
                Quantity = product.Quantity
            };

            if(product is not null)
            {
                _context.Products.Add(newProduct);
            }
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = newProduct.ID }, newProduct);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ID == id);
        }
    }
}
