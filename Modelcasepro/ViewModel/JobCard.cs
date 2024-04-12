using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelcasepro.ViewModel
{
    public class JobCard
    {
        public int ActivityId { get; set; }
        public string CustomerOrderNumber { get; set; }
        public string SageOrderNumber { get; set; }
        public string? HcorderNumber { get; set; }
        public string ActivityType { get; set; }
        public DateTime? DateRaised { get; set; }

        public string RaisedBy { get; set; }

        public string OutofhoursEmrgContact { get; set; }
        public string NearestAE { get; set; }
        public string Notes { get; set; }
        public string SiteAddress { get; set; }

        public List<CustomerviewModel> customerModel { get; set; }

    }
    public class CustomerviewModel
    {
        public string CustomerName { get; set; }
        public string ContactNo { get; set; }
        public int custid { get; set; }
    }
}
