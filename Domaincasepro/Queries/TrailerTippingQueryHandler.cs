using Domaincasepro.Repository;
using Modelcasepro.Entities;
using Modelcasepro.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Domaincasepro.Queries
{
    public class TrailerTippingQueryHandler
    {
        private readonly ITrailerTippingRepository _repo;
        public TrailerTippingQueryHandler(ITrailerTippingRepository repo)
        {
            _repo = repo;
        }
        public TrailerTippingRequestModel ExecuteTrailerQueryById(int Trailerid)
        {
            try
            {
                var Res = _repo.getActivityTrailerId(Trailerid);
                TrailerTippingRequestModel Trailerdetails = MapToViewModel(Res);

                return Trailerdetails;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while Get List of Trailerdetails: " + ex.Message);
            }

        }
        private TrailerTippingRequestModel MapToViewModel(ActivityTrailerTable activity)
        {
            string isoutbond;
            if (activity.IsOutBound == true)
            {
                isoutbond = "on";
            }
            else
            {
                isoutbond = "off";
            }


            return new TrailerTippingRequestModel
            {
                Date = Convert.ToDateTime(activity.Date),
                TrailerSupplier = activity.TrailerSupplier,
                TrailerNumber = activity.TrailerNumber,
                Quantity = activity.Quantity,
                VehicleReg = activity.VehicleReg,
                LoadDepot = activity.LoadDepot,
                LoadedTippedBy = activity.LoadedTippedBy,
                DepartFrom = activity.DepartFrom,
                IsOutBound = isoutbond,
                LoadPositioned=activity.Loadpositioned,
                Activityid = activity.ActivityId,                
            };
        }
    }
}
