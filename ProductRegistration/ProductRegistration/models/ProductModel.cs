using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductRegistration.models
{
    [Table("Products")]
    public class ProductModel
    {
        /*
         * 	- ProductName: string
	- GTIN: string(primary key)
	- MaterialNumber: string
	- Vendor: VendorEnum
	- Description: string
	- UnitOfMeasure: string
IsRetired: boolean*/
        public string ProductName { get; set; }
        public string Description{ get; set; }
        [Key]
        public string GTIN { get; set; }

        public int Vendor { get; set; }
        
        public string MaterialNumber { get; set; }
        public string UnitOfMeasure { get; set; }
        public Boolean IsActive { get; set; }

    }
}
