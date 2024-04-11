using Domaincasepro.Repository;
using Modelcasepro.Entities;
using Modelcasepro.RequestModel;
using Modelcasepro.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaincasepro.Queries
{
	public class ActivityQueryHandler
	{
		private readonly IActivityRepository _repo;


		public ActivityQueryHandler(IActivityRepository repo)
		{
			_repo = repo;
		}

		public ActivityTable ExecuteActivityQueryById(int activityid)
		{
			return _repo.GetActivityById(activityid) ?? new ActivityTable();
		}

		public List<ActivityViewModel> ExecuteGetList()
		{
			try
			{
				List<ActivityViewModel> activityview = _repo.GetAllList();

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

		public JobCard GetJobCard(int activityId)
		{
			try
			{
				var activity = ExecuteActivityQueryById(activityId);
				return new JobCard()
				{
					ActivityId = activity.Id,
					ActivityType = activity.ActivitType,
					CustomerOrderNumber = activity.CustomerOrderNumber,
					DateRaised = !string.IsNullOrEmpty(activity.DateRaised) ? Convert.ToDateTime(activity.DateRaised) : null,
					HcorderNumber = activity.HcorderNumber,
					NearestAE = activity.NearestAE,
					OutofhoursEmrgContact = activity.OutorhoursEmrgContact,
					RaisedBy = activity.RaisedBy,
					SageOrderNumber = activity.SageOrderNumber,
					SiteAddress = activity.SiteAddress,
				};
			}
			catch (Exception ex)
			{
				throw new Exception("An error occurred: " + ex.Message);
			}
		}
		public ProductDetails GetProductDetails(int activityId)
		{
			try
			{
                var activity = ExecuteProductQueryById(activityId);
				return new ProductDetails()
				{
					Shift = activity.Shift,
					Date = activity.Date,
					SummaryOfWorks = activity.SummaryOfWorks,
					ActivityId = activity.Id,
				};
            }
            catch(Exception ex)
			{
                throw new Exception("An error occurred: " + ex.Message);
            }
		}

        private ActivityProductDetail ExecuteProductQueryById(int activityId)
        {
            //return _repo.GetActivityById(activityid) ?? new ActivityTable();
			return _repo.getProductById(activityId) ?? new ActivityProductDetail();
        }
    }
}
