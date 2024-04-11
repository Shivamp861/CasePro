using Microsoft.EntityFrameworkCore;
using Modelcasepro.Entities;
using Modelcasepro.RequestModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaincasepro.Repository
{
    public class ActivityRepository : IActivityRepository
    {
        private CaseproDbContext _context;
        public ActivityRepository(CaseproDbContext context)
        {
            this._context = context;
        }
        public async Task<ActivityTable> AddOrUpdateActivity(ActivityTable activityinfo)
        {
            _context.ActivityTables.Add(activityinfo);
            await _context.SaveChangesAsync();

            // After saving, the ID should be populated
            return activityinfo;
        }

        public ActivityTable GetActivityById(int activityid)
        {
            return _context.ActivityTables.Include("ActivityCustomerTables").Include("ActivityDetails").Include("ActivityImages").Include("ActivityNotes").Include("ActivityProductDetails").Include("ActivityResourcesDetails").Include("ActivitySignOffdetails").Include("ActivityTrailerTables").Where(x => x.Id == activityid).FirstOrDefault();
        }

        //public List<ActivityTable> GetAllList()
        //{
        //    // Retrieve all activities from the database
        //    return _context.ActivityTables.ToList();
        //}

        public bool Create(ActivityNote notes)
        {
            bool success = false;
            try
            {
                _context.ActivityNotes.Add(notes);
                _context.SaveChanges();
                success = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return success;
        }
        public ActivityTable getActivityId(ActivityTable activityid)
        {
            var latestEntity = _context.ActivityTables.OrderByDescending(x => x.Id).FirstOrDefault();
            return latestEntity;

        }

        public List<ActivityViewModel> GetAllList()
        {
            var RES = _context.ActivityTables
     .SelectMany(
         at => at.ActivityCustomerTables.DefaultIfEmpty(),
         (at, act) => new { Activity = at, ActivityCustomer = act }
     )
     .SelectMany(
         at => at.Activity.ActivityResourcesDetails.DefaultIfEmpty(),
         (at, res) => new { at.Activity, at.ActivityCustomer, ActivityResourcesDetail = res }
     )
     .Select(at => new ActivityViewModel
     {
         Id = at.Activity.Id,
         CustomerOrderNumber = at.Activity.CustomerOrderNumber,
         DateRaised = DateTime.Parse(at.Activity.DateRaised),
         ActivitType = at.Activity.ActivitType,
         SiteAddress = at.Activity.SiteAddress,
         BarrierType = at.Activity.ActivityDetails.FirstOrDefault().BarrierType,
         LabourSupplier = at.Activity.ActivityDetails.FirstOrDefault().LabourSupplier,
         TrailerNumber = at.Activity.ActivityTrailerTables.FirstOrDefault().TrailerNumber,
         CustomerName = at.ActivityCustomer.Name,
         DayNight = at.ActivityResourcesDetail.DayNight
     })
     .ToList();

            return RES;
        }

        public ActivityProductDetail getProductById(int activityId)
        {
            return _context.ActivityProductDetails.Where(x => x.ActivityId == activityId).FirstOrDefault();
        }


        //public ActivityTable GetActivityById(int activityid)
        //{
        //    return _context.ActivityTables.Include("ActivityCustomerTables").Include("ActivityDetails").Include("ActivityImages").Include("ActivityNotes").Include("ActivityProductDetails").Include("ActivityResourcesDetails").Include("ActivitySignOffdetails").Include("ActivityTrailerTables").Where(x => x.Id == activityid).FirstOrDefault();
        //}

    }
}
