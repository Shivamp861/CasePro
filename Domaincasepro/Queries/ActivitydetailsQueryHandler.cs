﻿using Azure.Core;
using Domaincasepro.Repository;
using Microsoft.AspNetCore.Http;
using Modelcasepro.Entities;
using Modelcasepro.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaincasepro.Queries
{
    public class ActivitydetailsQueryHandler
    {
        public readonly IActivityRepository _activityRepo;
        private readonly IActivitydetailsRepository _repo;
        private readonly ITrailerTippingRepository _Trepo;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public ActivitydetailsQueryHandler(IActivitydetailsRepository repo, ITrailerTippingRepository trepo, IActivityRepository activityRepo, IHttpContextAccessor httpContextAccessor)
        {
            _repo = repo;
            _Trepo = trepo;
            _activityRepo = activityRepo;
            _httpContextAccessor = httpContextAccessor;
        }

        public ActivityDetail ExecuteActivityddetailsQueryById(int activityid)
        {
            return _repo.GetActivityById(activityid)??new ActivityDetail();
        }
        public List<ActivityImage> ExecuteImageByID(int activityid)
        {
            return _repo.GetActivityImage(activityid) ?? new List<ActivityImage>();
        }
        public ActivityTable ExecuteActivityByID(int activityid)
        {
            return _activityRepo.GetActivityById(activityid) ?? new ActivityTable();
        }
        public List<ActivityTrailerTable> ExecuteTrailerByID(int activityid)
        {
            return _Trepo.GetAllListById(activityid) ?? new List<ActivityTrailerTable>();
        }
        public Details GetActivitydetails(int activityId)
        {
            try
            {
                var activitydetails = ExecuteActivityddetailsQueryById(activityId);
                var activityImage = ExecuteImageByID(activityId);
                var activity = ExecuteActivityByID(activityId);
                var TrailerdetailsInfo = ExecuteTrailerByID(activityId);

                var request = _httpContextAccessor.HttpContext.Request;
                string baseUrl = $"{request.Scheme}://{request.Host}";

                List<ActivityImagesViewModel> images = activityImage.Select(activitiesImage => new ActivityImagesViewModel {
                Imgid = activitiesImage.Id,
                    UploadPath = activitiesImage.UploadPath.Replace(activitiesImage.UploadPath.Substring(0, activitiesImage.UploadPath.IndexOf("/Upload")), baseUrl),
                }).ToList();
                
                List<TrailerViewModel> Trailerdetails = TrailerdetailsInfo.Select(trailer =>
            new TrailerViewModel
            {
                id = trailer.Id,
                activityid=trailer.ActivityId,
                TrailerSupplier = trailer.TrailerSupplier,
                TrailerNumber = trailer.TrailerNumber,
                DepartFrom = trailer.DepartFrom,
                Date= trailer.Date,
                Quantity = trailer.Quantity,
                VehicleReg = trailer.VehicleReg,
                LoadDepot = trailer.LoadDepot,
                LoadedTippedBy = trailer.LoadedTippedBy,
                LoadPositioned=trailer.Loadpositioned,
                IsOutBound=trailer.IsOutBound,
            }).ToList();

                return new Details()
                {
                    MeetingSite = activitydetails.MeetingSite,
                    ActivityId = activity.Id,
                    Id = activitydetails.Id,
                    ActivityDate = activitydetails.ActivityDate,
                    Type=activity.ActivitType,
                    LabourSupplier = activitydetails.LabourSupplier,
                    SupplierContact = activitydetails.SupplierContact,
                    NoOfPersoneSupplied = activitydetails.NoOfPersoneSupplied,
                    BarrierType = activitydetails.BarrierType,
                    BarrierQty = activitydetails.BarrierQty,
                    BarrierStartAndFinishLocation = activitydetails.BarrierStartAndFinishLocation,
                    BarrierPerformance = activitydetails.BarrierPerformance,
                    LengthOfRuns = activitydetails.LengthOfRuns,
                    AnchoringDetails = activitydetails.AnchoringDetails,
                    Isapermittobreakgroundrequired = activitydetails.Isapermittobreakgroundrequired,
                    ChainLiftingequipmenttobeused = activitydetails.ChainLiftingequipmenttobeused,
                    LiftingEquipmentUsed = activitydetails.LiftingEquipmentUsed,
                    IncidentReporting = activitydetails.IncidentReporting,
                    OtherResourcesEquipmentUsed = activitydetails.OtherResourcesEquipmentUsed,
                    AnySpecialInstructions = activitydetails.AnySpecialInstructions,
                    AllRelevantActivityRams = activitydetails.AllRelevantActivityRams,
                    AnyNearMissOccurrences = activitydetails.AnyNearMissOccurrences,
                    BarrierConditionChecks = activitydetails.BarrierConditionChecks,
                    Startandfinishtime = activitydetails.Startandfinishtime,
                    //Imgid = activityImage.Id,                 
                    //UploadPath = activityImage.UploadPath,
                    activityImages = images,
                    Trailerdetails = Trailerdetails,
                    
                };
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred: " + ex.Message);
            }
        }
    }
}
