using Domaincasepro.Repository;
using Modelcasepro.Entities;
using Modelcasepro.Factory;
using Modelcasepro.RequestModel;
using Modelcasepro.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaincasepro.Commands
{
    public class TrailerLoadCommandHandler
    {
        private readonly CaseproDbContext _context;

        public TrailerLoadCommandHandler(CaseproDbContext context)
        {
            _context = context;
        }

        public TrailerTippingResponseModel AddActivityTrailerdetails(TrailerLoadRequestModel trailerData, ITrailerTippingRepository trilerTippingRepository)
        {

            try
            {
                ActivityTrailerTable TrailerEntity = MapToEntitySite(trailerData);

                ActivityTrailerTable addedTrailerdetails = trilerTippingRepository.AddTrailerTipping(TrailerEntity);
                var trid =addedTrailerdetails.Id;

                if (TrailerEntity != null)
                {
                    return TrailerTippingFactory.Create(true, "Trailer Loading successfully Added", trid);
                }
                else
                {

                    return TrailerTippingFactory.Create(false, "Failed to Add Trailer Loading", trid);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding Trailer Loading: " + ex.Message);
            }
            return null;
        }

        private ActivityTrailerTable MapToEntitySite(TrailerLoadRequestModel requestModel)
        {
            bool isbound=true;
            if (requestModel.IsOutBound == "on")
            {
                isbound=true;
            }
            else
            {
                isbound = false;
            }

            return new ActivityTrailerTable
            {
                Date = requestModel.Date,
                TrailerSupplier = requestModel.TrailerSupplier,
                TrailerNumber = requestModel.TrailerNumber,
                Loadpositioned = requestModel.LoadPositioned,
                Quantity = requestModel.Quantity,
                VehicleReg = requestModel.VehicleReg,
                DepartFrom = requestModel.DepartFrom,
                LoadDepot = requestModel.LoadDepot,
                LoadedTippedBy = requestModel.LoadedTippedBy,
                IsOutBound = isbound,
                ActivityId = requestModel.Activityid

            };
        }
    }
    
   

   
}
