using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelcasepro.RequestModel
{
    public class ActivityRequestModel
    {
        public int ActivityId { get; set; }

        [Required(ErrorMessage = "CustomerOrderNumber is required")]
        public string CustomerOrderNumber { get; set; }

        [Required(ErrorMessage = "SageOrderNumber is required")]
        public string SageOrderNumber { get; set; }

        [Required(ErrorMessage = "HcorderNumber is required")]
        public string? HcorderNumber { get; set; }

        [Required(ErrorMessage = "ActivityType is required")]
        public string ActivityType { get; set; }

        [Required(ErrorMessage = "DateRaised is required")]
        [DataType(DataType.Date, ErrorMessage = "DateRaised must be a valid date")]
        public DateTime DateRaised { get; set; }

        [Required(ErrorMessage = "RaisedBy is required")]
        public string RaisedBy { get; set; }

        [Required(ErrorMessage = "Emergency Contact is required")]        
        public string OutofhoursEmrgContact { get; set; }

        [Required(ErrorMessage = "NearestAE is required")]
        public string NearestAE { get; set; }

        //[Required(ErrorMessage = "Notes is required")]
        //public string Notes { get; set; }

        [Required(ErrorMessage = "SiteAddress is required")]
        public string SiteAddress { get; set; }

	

		//public List<CustomerModel> Customerdetails { get; set; } = new List<CustomerModel>();
		//public List<SignoffModel> Signoffdetails { get; set; } = new List<SignoffModel>();

		//public class CustomerModel
		//{
		//    public string CustomerName { get; set; }
		//    public string ContactNo { get; set; }
		//}
		//public class SignoffModel
		//{
		//    public DateTime CompetionDate { get; set; }
		//    public string Signature { get; set; }
		//    public string PrintName { get; set; }
		//    public DateTime Date { get; set; }
		//}

	
	}
}
