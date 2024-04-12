using Domaincasepro.Repository;
using Modelcasepro.RequestModel;
using Modelcasepro.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaincasepro.Queries
{
	public class ActivitySummaryQueryhandler
	{
		private readonly IActivityRepository _repo;
		public ActivitySummaryQueryhandler(IActivityRepository repo, ICustomerRepository custrepo)
		{
			_repo = repo;			
		}

		public ActivitySummary ExecuteGetList(int activityid)
		{
			try
			{
				ActivitySummary activityview = _repo.ActivitySummaryGetAllList(activityid);

				// Initialize a list to store mapped view models
				var viewModels = new List<ActivityRequestModel>();

				return activityview;
			}
			catch (Exception ex)
			{
				throw new Exception("An error occurred while Get List of Activities: " + ex.Message);
			}
			// Retrieve list of activities from the repository

		}
	}
}
