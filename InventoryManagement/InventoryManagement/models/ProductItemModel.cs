using System.ComponentModel.DataAnnotations;
using InventoryManagement.enums;
namespace InventoryManagement.models
{
    public class ProductItemModel
    {
        [Required]
        [Key]
        public string SGTIN { get; set; }
        public string GTIN { get; set; }
        public string Lot { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime CheckinDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public ReasonsForCheckout ReasonForCheckout { get; set; }

    }
}
