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
        private readonly ICustomerRepository _custrepo;


        public ActivityQueryHandler(IActivityRepository repo, ICustomerRepository custrepo)
        {
            _repo = repo;
            _custrepo = custrepo;
        }

        public ActivityTable ExecuteActivityQueryById(int activityid)
        {
            return _repo.GetActivityById(activityid) ?? new ActivityTable();
        }

        public List<ActivityCustomerTable> ExecuteCustQueryById(int activityid)
        {
            return _custrepo.GetActivityById(activityid) ?? new List<ActivityCustomerTable>();
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
                var customer = ExecuteCustQueryById(activityId);
                List<CustomerviewModel> custdetails = customer.Select(c =>
                      new CustomerviewModel
                      {
                          custid = c.Id,
                          ContactNo = c.ContactNo,
                          CustomerName = c.Name
                      }).ToList();


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
                    Notes = activity.ActivityNotes.Select(x => x.Notes).FirstOrDefault(),
                    customerModel = custdetails,
                };
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred: " + ex.Message);
            }
        }

        public ActivityTable gettypefortable(int? actiId)
        {
            var activitytype = _repo.gettypefortable(actiId);
            return (ActivityTable)activitytype;
        }
    }
}
