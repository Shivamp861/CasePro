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

        public (bool, bool) Create(ActivityNote notes)
        {
            bool success = false;
            bool Hassave = true;

            try
            {
                var existing = _context.ActivityNotes.Where(x => x.ActivityId == notes.ActivityId).FirstOrDefault();
                if (existing != null)
                {
                    existing.Notes = notes.Notes;
                    success = true;
                    Hassave = false;
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
            return (success, Hassave);
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

                var relatedTables = new List<IEnumerable<dynamic>>
        {
            _context.ActivityDetails.Where(ad => ad.ActivityId == activityId),
            _context.ActivityCustomerTables.Where(ad => ad.ActivityId == activityId),
            _context.ActivityProductDetails.Where(ad => ad.ActivityId == activityId),
            _context.ActivityResourcesDetails.Where(ad => ad.ActivityId == activityId),
            _context.ActivityImages.Where(ad => ad.ActivityId == activityId),
            _context.ActivitySignOffdetails.Where(ad => ad.ActivityId == activityId),
            _context.ActivityNotes.Where(ad => ad.ActivityId == activityId),
            _context.ActivityTrailerTables.Where(ad => ad.ActivityId == activityId),
            _context.InstructorFormDetails.Where(ad => ad.ActivityId == activityId)
        };

                foreach (var table in relatedTables)
                {
                    if (table != null && table.Any())
                    {
                        _context.RemoveRange(table);
                    }
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
                var activityDetails = _context.ActivityTables.Where(x => x.Id == id)
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
        //BarrierType = at.Activity.ActivityDetails.FirstOrDefault().BarrierType,
        //BarrierQuntity = at.Activity.ActivityDetails.FirstOrDefault().BarrierQty,
        customer = at.ActivityCustomer.Name,
        Dayornight = at.ActivityResourcesDetail.DayNight,
        SiteAddress = at.Activity.SiteAddress,
        SummaryOfWorks = at.Activity.ActivityProductDetails.FirstOrDefault().SummaryOfWorks

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


                var actid = _context.ActivityTables.Where(x => x.Id == id).FirstOrDefault();
                var sitedata = (
                    from Siteinstallations in _context.ActivityDetails.Where(x => x.ActivityId == activity.Id).DefaultIfEmpty()
                    select new Siteinstallation
                    {
                        LabourSupplier = Siteinstallations != null ? Siteinstallations.LabourSupplier : null,
                        SupplierContact = Siteinstallations != null ? Siteinstallations.SupplierContact : null,
                        NoOfPersoneSupplied = Siteinstallations != null ? Siteinstallations.NoOfPersoneSupplied : null,
                        BarrierType = Siteinstallations != null ? Siteinstallations.BarrierType : null,
                        BarrierQty = Siteinstallations != null ? Siteinstallations.BarrierQty : null,
                        BarrierStartAndFinishLocation = Siteinstallations != null ? Siteinstallations.BarrierStartAndFinishLocation : null,
                    }
                    ).ToList();




                var resouresdata = (
                    from ResourseDatas in _context.ActivityResourcesDetails.Where(x => x.ActivityId == activity.Id).DefaultIfEmpty()
                    select new ResourseData
                    {
                        Shift = ResourseDatas != null ? ResourseDatas.Shift : null,
                        Name = ResourseDatas != null ? ResourseDatas.Name : null,
                        DayNight = ResourseDatas != null ? ResourseDatas.DayNight : null,
                        ResourceType = ResourseDatas != null ? ResourseDatas.ResourceType : null,
                        Comments = ResourseDatas != null ? ResourseDatas.Comments : null,

                    }

                    ).ToList();
                // Create and return the ActivitySummary object
                return new ActivitySummary
                {
                    ActivityType = actid.ActivitType,
                    Activity = activityDetails,
                    Trailer = trailerDetails,
                    Siteinstallations = sitedata,
                    ResourseDatas = resouresdata
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

        public ActivityTable updateactivitystatus(string status, int activityid)
        {
            var updaterow = _context.ActivityTables.FirstOrDefault(x => x.Id == activityid);
            if (updaterow != null)
            {
                updaterow.ActivityStatus = status;

                _context.SaveChanges();
            }


            return updaterow;
        }

        public (ActivityTable, int) CloneAddOrUpdateActivity(ActivityTable activityEntity)
        {
            int flag1 = 0;
            var existing = _context.ActivityTables.Where(x => x.Id == activityEntity.Id).FirstOrDefault();
            if (existing != null)
            {
                if (existing.CustomerOrderNumber == activityEntity.CustomerOrderNumber && existing.SageOrderNumber == activityEntity.SageOrderNumber && existing.HcorderNumber == activityEntity.HcorderNumber && existing.ActivitType == activityEntity.ActivitType && existing.DateRaised == activityEntity.DateRaised && existing.RaisedBy == activityEntity.RaisedBy && existing.SiteAddress == activityEntity.SiteAddress && existing.OutorhoursEmrgContact == activityEntity.OutorhoursEmrgContact && existing.NearestAE == activityEntity.NearestAE)
                {
                    activityEntity.Id = 0;

                    _context.ActivityTables.Add(activityEntity);

                    flag1 = 1;
                }
                else
                {
                    _context.Entry(existing).CurrentValues.SetValues(activityEntity);
                }
                _context.SaveChanges();

            }


            return (activityEntity, flag1);
        }


        public ActivityTable getActType(int deleteid)
        {
            var activityTable = _context.ActivityTables.Where(x => x.Id == deleteid).FirstOrDefault();
            return (ActivityTable)activityTable;

        
        }
    }
}
