using InventoryManagement.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using InventoryManagement.data;
using InventoryManagement.enums;
using Microsoft.AspNetCore.Authorization;

namespace InventoryManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventoryManagementController : Controller
    {
        private readonly InventoryManagamentDbContext _db;
        public InventoryManagementController(InventoryManagamentDbContext db)
        {
            _db = db;
        }
        [HttpGet("test")]
        public async Task<ActionResult> TestConnection()
        {
            return Ok("Connection Successful");
        }
        [HttpGet("ProductItems")]
        public async Task<ActionResult> GetCheckedInProductItems()
        {
            var productItems = _db.ProductItems;
            return Ok(productItems);
        }

        [HttpPost("checkIn")]
        public async Task<ActionResult> CheckInProductItem([FromBody] ProductItemModel model)
        {
            TryValidateModel(model);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _db.ProductItems.AddAsync(model);
            await _db.SaveChangesAsync();
            return Created("", model);
        }

        [HttpPost("checkOut")]
        public async Task<ActionResult> CheckOutProductItem([FromBody] string SGTIN, ReasonsForCheckout Reason)
        {
            var itemToBeCheckedOut = _db.ProductItems.SingleOrDefault(item => item.SGTIN == SGTIN);
            if(itemToBeCheckedOut == null)
            {
                return NotFound();
            } else
            {
                itemToBeCheckedOut.CheckOutDate = DateTime.Now;
                itemToBeCheckedOut.ReasonForCheckout = Reason;
                await _db.SaveChangesAsync();
                return Ok(itemToBeCheckedOut);
            }
        }

    }
}
