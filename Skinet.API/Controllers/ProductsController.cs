using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Skinet.Application.Interfaces;
using Skinet.Application.Specifications;
using Skinet.Entities.Product;

namespace Skinet.API.Controllers;

public class ProductsController(IUnitOfWork unit) : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetProducts([FromQuery] ProductSpecParams specParams)
    {
        var spec = new ProductSpecification(specParams);
        return await CreatePagedResult(
            unit.Repository<Product>(),
            spec,
            specParams.PageIndex,
            specParams.PageSize
        );
    }

    [HttpGet("{id:int}")] // api/products/2
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await unit.Repository<Product>().GetByIdAsync(id);

        if (product == null)
            return NotFound();

        return product;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        unit.Repository<Product>().Add(product);

        if (await unit.Complete())
        {
            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        return BadRequest("Problem creating product");
    }

    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProduct(int id, Product product)
    {
        if (product.Id != id || !ProductExists(id))
            return BadRequest("Cannot update this product");

        unit.Repository<Product>().Update(product);

        if (await unit.Complete())
        {
            return NoContent();
        }

        return BadRequest("Problem updating the product");
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await unit.Repository<Product>().GetByIdAsync(id);

        if (product == null)
            return NotFound();

        unit.Repository<Product>().Delete(product);

        if (await unit.Complete())
        {
            return NoContent();
        }

        return BadRequest("Problem deleting the product");
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
    {
        var spec = new BrandListSpecification();
        var brands = await unit.Repository<Product>().ListAsync(spec);
        return Ok(brands);
    }

    [HttpGet("categories")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetCategories()
    {
        var spec = new CategoryListSpecification();
        var categories = await unit.Repository<Product>().ListAsync(spec);
        return Ok(categories);
    }

    private bool ProductExists(int id)
    {
        return unit.Repository<Product>().Exists(id);
    }
}
