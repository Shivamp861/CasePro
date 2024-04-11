using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelcasepro.RequestModel
{
    public class CustomerRequestModel
    {
        [Required(ErrorMessage = "ActivityId is required")]
        public int ActivityId { get; set; }

        [Required(ErrorMessage = "CustomerName is required")]
        [StringLength(100, ErrorMessage = "CustomerName must be at most 100 characters long")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "ContactNo is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "ContactNo must be a 10-digit number")]
        public string ContactNo { get; set; }
    }
}
