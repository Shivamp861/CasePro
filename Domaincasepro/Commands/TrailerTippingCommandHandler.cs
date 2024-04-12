using Domaincasepro.Repository;
using Microsoft.AspNetCore.Http;
using Modelcasepro.Entities;
using Modelcasepro.Factory;
using Modelcasepro.RequestModel;
using Modelcasepro.ResponseModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaincasepro.Commands
{
    public class TrailerTippingCommandHandler
    {
        private readonly ITrailerTippingRepository _repo;

        public TrailerTippingCommandHandler(ITrailerTippingRepository repo)
        {
            _repo = repo;
        }

        public TrailerTippingResponseModel AddActivityTrailerdetails(TrailerTippingRequestModel TrailerRequest)
        {
            try
            {
                ActivityTrailerTable TrailerEntity = MapToEntitySite(TrailerRequest);

                ActivityTrailerTable addedTrailerdetails = _repo.AddTrailerTipping(TrailerEntity);           

                if (TrailerEntity != null)
                {
                   
                    return TrailerTippingFactory.Create(true,"",addedTrailerdetails.Id);
                }
                else
                {
                    // Something went wrong while adding the activity
                    return TrailerTippingFactory.Create(false, "Failed to Add Trailer Tipping",0);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding Yard Tipping: " + ex.Message);
            }
        }
        private ActivityTrailerTable MapToEntitySite(TrailerTippingRequestModel requestModel)
        {
            bool isbound;
            if (requestModel.IsOutBound=="on")
            {
                isbound= true;
            }
            else
            {
                isbound= false;
            }
            return new ActivityTrailerTable
            {
                Date = requestModel.Date,
                TrailerSupplier= requestModel.TrailerSupplier,
                TrailerNumber= requestModel.TrailerNumber,
                Quantity=requestModel.Quantity,
                VehicleReg= requestModel.VehicleReg,
                LoadDepot= requestModel.LoadDepot,
                LoadedTippedBy= requestModel.LoadedTippedBy,
                DepartFrom=requestModel.DepartFrom,
                IsOutBound= isbound,
                Loadpositioned = requestModel.LoadPositioned,
                ActivityId = requestModel.Activityid

            };
        }
    }
}
