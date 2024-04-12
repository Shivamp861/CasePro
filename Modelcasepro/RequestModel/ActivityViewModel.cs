using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelcasepro.RequestModel
{
	public class ActivityViewModel
	{
		public int Id { get; set; }
		public string CustomerOrderNumber { get; set; }
		public DateTime DateRaised { get; set; }
		public string ActivitType { get; set; }
		public string SiteAddress { get; set; }
		public string BarrierType { get; set; }
		public string LabourSupplier { get; set; }
		public string TrailerNumber { get; set; }
		public string CustomerName { get; set; }
		public string DayNight { get; set; }
        public string activityDate { get; set; }
    }
}