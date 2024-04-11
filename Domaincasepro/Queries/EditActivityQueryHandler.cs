using Domaincasepro.Repository;
using Modelcasepro.Entities;
using Modelcasepro.ResponseModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaincasepro.Queries
{
    public class EditActivityQueryHandler
    {
        private readonly CaseproDbContext _context;
        public EditActivityQueryHandler(CaseproDbContext context)
        {
            _context = context;
        }
        public EditActivityResponseModel EditActivityHandler(int activityid, IActivityRepository activityRepo)
        {
            try
            {
                var Res = activityRepo.GetActivityById(activityid);
                EditActivityResponseModel activityinfo = MapToViewModel(Res);

                return activityinfo;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while Get List of Trailerdetails: " + ex.Message);
            }

        }

        private EditActivityResponseModel MapToViewModel(ActivityTable activity)
        {
            string notes = string.Join("; ", activity.ActivityNotes.Select(note => note.Notes));
            var res = activity.ActivityDetails.FirstOrDefault();
            var ImageResult  = activity.ActivityImages.FirstOrDefault();

            string meetingsite = string.Join("; ", activity.ActivityDetails.Select(note => note.MeetingSite));
            string image = string.Join("; ", activity.ActivityImages.Select(note => note.ImageName));

            var productViewModels = activity.ActivityProductDetails.Select(product =>
        new Productviewmodel
        {
            Shift = product.Shift,
            Date = Convert.ToDateTime(product.Date),
            SummaryOfWorks = product.SummaryOfWorks
        }).ToList();

            var customerViewModels = activity.ActivityCustomerTables.Select(customer =>
       new CustomerviewModel
       {
           CustomerName = customer.Name,
           ContactNo = customer.ContactNo,
           
       }).ToList();

            var ResourseViewModels = activity.ActivityResourcesDetails.Select(resourse =>
       new ResourceviewModel
       {
           ResourceShift = resourse.Shift,
           ResourceDate = Convert.ToDateTime(resourse.Date),
           Name = resourse.Name,
           ResourceType = resourse.ResourceType,
           Comments = resourse.Comments,
           DayNight = resourse.DayNight,

       }).ToList();
            var Signoffviewmodel = activity.ActivitySignOffdetails.Select(Signoff =>
       new Signoffviewmodel
       {
           CompetionDate = Convert.ToDateTime(Signoff.CompetionDate),
           PrintName = Signoff.PrintName,
           SignOffDate = Convert.ToDateTime(Signoff.Date),
           Signature = Signoff.Signature,
           
          

       }).ToList();
            var Trailerviewmodel = activity.ActivityTrailerTables.Select(Trailer =>
       new Trailerviewmodel
       {
           TrailerSupplier = Trailer.TrailerSupplier,
           TrailerNumber = Trailer.TrailerNumber,
           Quantity = Trailer.Quantity,
           VehicleReg = Trailer.VehicleReg,
           LoadDepot = Trailer.LoadDepot,
           LoadedTippedBy = Trailer.LoadedTippedBy,


       }).ToList();

			return new EditActivityResponseModel
			{
				ActivityId = activity.Id,
				CustomerOrderNumber = activity.CustomerOrderNumber,
				SageOrderNumber = activity.SageOrderNumber,
				HcorderNumber = activity.HcorderNumber,
				ActivityType = activity.ActivitType,
				DateRaised = DateTime.Parse(activity.DateRaised),
				RaisedBy = activity.RaisedBy,
				OutofhoursEmrgContact = activity.OutorhoursEmrgContact,
				NearestAE = activity.NearestAE,
				SiteAddress = activity.SiteAddress,
				Notes = notes,
				MeetingSite = res?.MeetingSite,
				LabourSupplier = res?.LabourSupplier,
				SupplierContact = res?.SupplierContact,
				NoOfPersoneSupplied = res?.NoOfPersoneSupplied,
				BarrierType = res?.BarrierType,
				BarrierQty = res?.BarrierQty,
				BarrierStartAndFinishLocation = res?.BarrierStartAndFinishLocation,
				BarrierPerformance = res?.BarrierPerformance,
				LengthOfRuns = res?.LengthOfRuns,
				AnchoringDetails = res?.AnchoringDetails,
				Isapermittobreakgroundrequired = res?.Isapermittobreakgroundrequired,
				ChainLiftingequipmenttobeused = res?.ChainLiftingequipmenttobeused,
				LiftingEquipmentUsed = res?.LiftingEquipmentUsed,
				IncidentReporting = res?.IncidentReporting,
				OtherResourcesEquipmentUsed = res?.OtherResourcesEquipmentUsed,
				AnySpecialInstructions = res?.AnySpecialInstructions,
				AllRelevantActivityRams = res?.AllRelevantActivityRams,
				AnyNearMissOccurrences = res?.AnyNearMissOccurrences,
				BarrierConditionChecks = res?.BarrierConditionChecks,
				Startandfinishtime = res?.Startandfinishtime,
				ImageName = ImageResult?.ImageName,
				UploadPath = ImageResult?.UploadPath,
				customerModel = customerViewModels,
				ProductModel = productViewModels,
				ResouorceModel = ResourseViewModels,
				SignoffModel = Signoffviewmodel,
				TrailerModel = Trailerviewmodel,


			};
		}
    }
}
