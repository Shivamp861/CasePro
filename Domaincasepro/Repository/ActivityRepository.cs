using Microsoft.EntityFrameworkCore;
using Modelcasepro.Entities;
using Modelcasepro.RequestModel;
using Modelcasepro.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domaincasepro.Repository
{
    public class ActivityRepository : IActivityRepository
    {
        private CaseproDbContext _context;
        public ActivityRepository(CaseproDbContext context)
        {
            this._context = context;
        }
        public ActivityTable AddOrUpdateActivity(ActivityTable data)
        {
            var existing = _context.ActivityTables.Where(x => x.Id == data.Id).FirstOrDefault();

            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(data);
            }
            else
            {
                _context.ActivityTables.Add(data);
            }

            _context.SaveChanges();
            return data;
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
                var existing = _context.ActivityNotes.Where(x => x.ActivityId == notes.ActivityId).FirstOrDefault();
                if (existing != null)
                {
                    existing.Notes = notes.Notes;
                    success = true;
                }
                else
                {
                    _context.ActivityNotes.Add(notes);
                    success = true;
                }
                _context.SaveChanges();

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

        public bool DeleteActivity(int activityId)
        {
            bool success = false;
            try
            {
                var activity = _context.ActivityTables.Find(activityId);
                if (activity == null)
                {
                    throw new InvalidOperationException("Activity not found");
                }
                var Activitydetails = _context.ActivityDetails.Where(ad => ad.ActivityId == activityId).FirstOrDefault();
                if (Activitydetails != null)
                {
                    _context.ActivityDetails.RemoveRange(Activitydetails);
                }


                var customerdetails = _context.ActivityCustomerTables.Where(ad => ad.ActivityId == activityId).ToList();
                if (customerdetails != null)
                {
                    _context.ActivityCustomerTables.RemoveRange(customerdetails);
                }


                var productdetails = _context.ActivityProductDetails.Where(ad => ad.ActivityId == activityId).ToList();
                if (productdetails != null)
                {
                    _context.ActivityProductDetails.RemoveRange(productdetails);
                }


                var Resourcedetails = _context.ActivityResourcesDetails.Where(ad => ad.ActivityId == activityId).ToList();
                if (Resourcedetails != null)
                {
                    _context.ActivityResourcesDetails.RemoveRange(Resourcedetails);
                }


                var Imagedetails = _context.ActivityImages.Where(ad => ad.ActivityId == activityId).FirstOrDefault();
                if (Imagedetails != null)
                {
                    _context.ActivityImages.RemoveRange(Imagedetails);
                }


                var singoffdetails = _context.ActivitySignOffdetails.Where(ad => ad.ActivityId == activityId).ToList();
                if (singoffdetails != null)
                {
                    _context.ActivitySignOffdetails.RemoveRange(singoffdetails);
                }


                var Notesdetails = _context.ActivityNotes.Where(ad => ad.ActivityId == activityId).FirstOrDefault();
                if (Notesdetails != null)
                {
                    _context.ActivityNotes.RemoveRange(Notesdetails);
                }


                var Trailerdetails = _context.ActivityTrailerTables.Where(ad => ad.ActivityId == activityId).ToList();
                if (Trailerdetails != null)
                {
                    _context.ActivityTrailerTables.RemoveRange(Trailerdetails);
                }

                var InstractionOpration = _context.InstructorFormDetails.Where(ad => ad.ActivityId == activityId).ToList();
                if (InstractionOpration != null)
                {
                    _context.InstructorFormDetails.RemoveRange(InstractionOpration);
                }


                _context.ActivityTables.Remove(activity);
                _context.SaveChanges();
                success = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return success;

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
         DayNight = at.ActivityResourcesDetail.DayNight,
     })
     .ToList();

            return RES;
        }

        public ActivitySummary ActivitySummaryGetAllList(int id)
        {
            try
            {
                // Retrieve the activity by ID from the database
                var activity = _context.ActivityTables
                    .Include("ActivityCustomerTables")
                    .Include("ActivityDetails")
                    .Include("ActivityImages")
                    .Include("ActivityNotes")
                    .Include("ActivityProductDetails")
                    .Include("ActivityResourcesDetails")
                    .Include("ActivitySignOffdetails")
                    .Include("ActivityTrailerTables")
                    .Where(x => x.Id == id)
                    .FirstOrDefault();

                if (activity == null)
                {
                    // If no activity found with the provided ID, return null or handle accordingly
                    return null;
                }
                var activityDetails = _context.ActivityTables.Where(x=>x.Id==id)
    .SelectMany(
        at => at.ActivityCustomerTables.DefaultIfEmpty(),
        (at, act) => new { Activity = at, ActivityCustomer = act }
    )
    .SelectMany(
        at => at.Activity.ActivityResourcesDetails.DefaultIfEmpty(),
        (at, res) => new { at.Activity, at.ActivityCustomer, ActivityResourcesDetail = res }
    )
    .Select(at => new Activitydetails
    {              
        ActivityDate = DateTime.Parse(at.Activity.DateRaised),
        Type = at.Activity.ActivitType,        
        BarrierType = at.Activity.ActivityDetails.FirstOrDefault().BarrierType,
        BarrierQuntity = at.Activity.ActivityDetails.FirstOrDefault().BarrierQty,        
        customer = at.ActivityCustomer.Name,
        Dayornight = at.ActivityResourcesDetail.DayNight
    })
    .ToList();
                // Retrieve activity details


                // Retrieve trailer details
                var trailerDetails = (
                    from trailer in _context.ActivityTrailerTables.Where(t => t.ActivityId == activity.Id).DefaultIfEmpty()
                    select new Trailerdetails
                    {
                        Date = trailer != null ? trailer.Date : null,
                        TrailerSupplier = trailer != null ? trailer.TrailerSupplier : null,
                        TrailerNumber = trailer != null ? trailer.TrailerNumber : null,
                        Quantity = trailer != null ? trailer.Quantity : null,
                        VehicleReg = trailer != null ? trailer.VehicleReg : null,
                        LoadDepot = trailer != null ? trailer.LoadDepot : null,
                        LoadedTippedBy = trailer != null ? trailer.LoadedTippedBy : null,
                        DepartFrom = trailer != null ? trailer.DepartFrom : null,
                        IsOutBound = trailer != null ? trailer.IsOutBound : null,
                        LoadPositioned = trailer != null ? trailer.Loadpositioned : null,

                    }
                ).ToList();

                // Create and return the ActivitySummary object
                return new ActivitySummary
                {
                    Activity = activityDetails,
                    Trailer = trailerDetails
                };
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the retrieval process
                // Log the exception or perform any necessary error handling
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null; // Or throw an exception, depending on your error handling strategy
            }
        }


    }
}
