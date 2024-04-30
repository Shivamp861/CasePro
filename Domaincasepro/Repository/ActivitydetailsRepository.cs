using Microsoft.EntityFrameworkCore;
using Modelcasepro.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domaincasepro.Repository
{
    public class ActivitydetailsRepository : IActivitydetailsRepository
    {
        private CaseproDbContext _context;
        public ActivitydetailsRepository(CaseproDbContext context)
        {
            this._context = context;
        }

        public ActivityDetail AddOrUpdateActivitydetails(ActivityDetail data)
        {
            var existing = _context.ActivityDetails.Where(x => x.ActivityId == data.ActivityId).FirstOrDefault();

            if (existing != null)
            {
                existing.Activity = data.Activity;
                existing.ActivityDate  = data.ActivityDate;
                existing.AllRelevantActivityRams = data.AllRelevantActivityRams;
                existing.NoOfPersoneSupplied = data.NoOfPersoneSupplied;
                existing.LabourSupplier = data.LabourSupplier;
                existing.BarrierQty = data.BarrierQty;
                existing.BarrierConditionChecks = data.BarrierConditionChecks;
                existing.SupplierContact = data.SupplierContact;
                existing.BarrierPerformance = data.BarrierPerformance;
                existing.AnchoringDetails = data.AnchoringDetails;
                existing.AnyNearMissOccurrences = data.AnyNearMissOccurrences;
                existing.AnySpecialInstructions = data.AnySpecialInstructions;
                existing.BarrierStartAndFinishLocation = data.BarrierStartAndFinishLocation;
                existing.BarrierType = data.BarrierType;
                existing.ChainLiftingequipmenttobeused = data.ChainLiftingequipmenttobeused;
                existing.IncidentReporting = data.IncidentReporting;
                existing.Isapermittobreakgroundrequired = data.Isapermittobreakgroundrequired;
                existing.LengthOfRuns = data.LengthOfRuns;
                existing.LiftingEquipmentUsed = data.LiftingEquipmentUsed;
                existing.MeetingSite = data.MeetingSite;
                existing.OtherResourcesEquipmentUsed = data.OtherResourcesEquipmentUsed;
                existing.Startandfinishtime = data.Startandfinishtime;
                _context.Update(existing);
            }
            else
            {
                _context.ActivityDetails.Add(data);
                return data;
            }

            _context.SaveChanges();
            return existing;
            
        }

        public ActivityImage AddOrUpdateActivityImage(ActivityImage activityimage)
        {
            var existing = _context.ActivityImages.Where(x => x.Id == activityimage.Id && x.ActivityId == activityimage.ActivityId).FirstOrDefault();

            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(activityimage);
            }
            else
            {
                _context.ActivityImages.Add(activityimage);
            }

            _context.SaveChanges();

            return activityimage;
        }

        public ActivityDetail deleteActDetail(int deleteid)
        {
            var activityDetail = _context.ActivityDetails.Where(x=>x.ActivityId ==  deleteid).FirstOrDefault();
            var imgdelete = _context.ActivityImages.Where(x=>x.ActivityId == deleteid).ToList();
            var tabledelete = _context.ActivityTrailerTables.Where(x => x.ActivityId == deleteid).ToList();
            if (activityDetail != null )
            {
                
                
                _context.ActivityDetails.Remove(activityDetail);
                _context.SaveChanges();
            }

            if (imgdelete.Any()) {
                foreach (var item in imgdelete)
                {
                    _context.ActivityImages.Remove(item);
                }
                _context.SaveChanges();
            }

            if (tabledelete.Count > 0)
            {
                foreach (var td in tabledelete)
                {
                    _context.ActivityTrailerTables.Remove(td);
                }
                _context.SaveChanges();
            }
            return activityDetail;
        }

        public ActivityDetail GetActivityById(int activityid)
        {
            return _context.ActivityDetails.Where(x => x.ActivityId == activityid).FirstOrDefault();
        }

        public List<ActivityImage> GetActivityImage(int activityid)
        {
            return _context.ActivityImages.Where(x => x.ActivityId == activityid).ToList();
        }
    }
}
