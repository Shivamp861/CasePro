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
        private readonly CaseproDbContext _context;

        public TrailerTippingCommandHandler(CaseproDbContext context)
        {
            _context = context;
        }

        public TrailerTippingResponseModel AddActivityTrailerdetails(TrailerTippingRequestModel TrailerRequest, ITrailerTippingRepository TrailerRepo)
        {
            try
            {
                ActivityTrailerTable TrailerEntity = MapToEntitySite(TrailerRequest);

                ActivityTrailerTable addedTrailerdetails = TrailerRepo.AddTrailerTipping(TrailerEntity);
           

                if (TrailerEntity != null)
                {
                    return TrailerTippingResponseModel.Create(true, "Trailer Tipping successfully Added");
                }
                else
                {
                    // Something went wrong while adding the activity
                    return TrailerTippingResponseModel.Create(false, "Failed to Add Trailer Tipping");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding Yard Tipping: " + ex.Message);
            }
        }
        private ActivityTrailerTable MapToEntitySite(TrailerTippingRequestModel requestModel)
        {
            return new ActivityTrailerTable
            {
                Date = requestModel.Date,
                TrailerSupplier= requestModel.TrailerSupplier,
                TrailerNumber= requestModel.TrailerNumber,
                Quantity=requestModel.Quantity,
                VehicleReg= requestModel.VehicleReg,
                LoadDepot= requestModel.LoadDepot,
                LoadedTippedBy= requestModel.LoadedTippedBy,
                ActivityId = requestModel.Activityid

            };
        }
    }
}
