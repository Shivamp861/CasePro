using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelcasepro.ViewModel
{
    public class ProductDetails
    {
        public int ActivityId { get; set; }
        [Required(ErrorMessage = "Shift is required")]
        public string? Shift { get; set; }

        [Required(ErrorMessage = "Date is required")]
        public DateTime? Date { get; set; }

        [Required(ErrorMessage = "SummaryOfWorks is required")]
        public string? SummaryOfWorks { get; set; }
    }
}
