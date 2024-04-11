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
		public DateTime? DateRaised { get; set; }

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

	}
}
