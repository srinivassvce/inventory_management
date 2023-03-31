using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductRegistration.models;
using ProductsRegistration.data;

namespace ProductRegistration.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegistrationController : ControllerBase
    {

        private readonly ILogger<RegistrationController> _logger;
        private readonly ProductsDbContext _db;

        public RegistrationController(ILogger<RegistrationController> logger, ProductsDbContext productsDb)
        {
            _logger = logger;
            _db = productsDb;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] ProductModel model)
        {
            TryValidateModel(model);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                await _db.Products.AddAsync(model);
                await _db.SaveChangesAsync();
                return Created("", new ProductModel()
                {
                  Description = model.Description,
                  GTIN = model.GTIN,
                  MaterialNumber = model.MaterialNumber,
                  ProductName = model.ProductName,
                  UnitOfMeasure = model.UnitOfMeasure,
                  Vendor = model.Vendor,
                  IsActive = true,
                });
            }
        }
        [HttpPost("Deregister")]
        [ProducesResponseType(StatusCodes.Status200OK, Type=typeof(ProductModel))]
        public async Task<ActionResult> Deregister([FromBody] string GTIN)
        {
            var product = _db.Products.SingleOrDefault(product => product.GTIN == GTIN);
            product.IsActive = false;
            await _db.SaveChangesAsync();
            return Ok(product);
        }
        [HttpGet("GetProducts")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductModel[]))]
        public async Task<ActionResult> GetProducts()
        {
            var products = _db.Products;
            return Ok(products.Where(product => product.IsActive));
        }
    }
}