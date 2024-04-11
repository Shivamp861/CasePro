using Domaincasepro.Repository;
using Modelcasepro.Entities;
using Modelcasepro.RequestModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Domaincasepro.Queries
{
    public class TrailerLoadingQueryHandler
    {
        private readonly CaseproDbContext _context;


        public TrailerLoadingQueryHandler(CaseproDbContext context)
        {
            _context = context;
        }
        public TrailerLoadRequestModel ExecuteTrailerQueryById(ITrailerTippingRepository TrailerRepository,int trid)
        {
            try
            {
                var Res = TrailerRepository.getActivityTrailerId(trid);
                TrailerLoadRequestModel Trailerdetails = MapToViewModel(Res);

                return Trailerdetails;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while Get List of Trailerdetails: " + ex.Message);
            }

        }

        private TrailerLoadRequestModel MapToViewModel(ActivityTrailerTable activity)
        {
            string isoutbond ;
            if (activity.IsOutBound==true)
            {
                isoutbond = "on";
            }
            else
            {
                isoutbond = "off";
            }
            
            
            return new TrailerLoadRequestModel
            {
                Date = Convert.ToDateTime(activity.Date),
                TrailerSupplier = activity.TrailerSupplier,
                TrailerNumber = activity.TrailerNumber,
                LoadPositioned = activity.Loadpositioned,
                Quantity = activity.Quantity,
                VehicleReg = activity.VehicleReg,
                DepartFrom = activity.DepartFrom,
                LoadDepot = activity.LoadDepot,
                IsOutBound= isoutbond,
                LoadedTippedBy = activity.LoadedTippedBy,
                Activityid = Convert.ToInt32(activity.Activity)

                // Map other properties as needed
            };
        }
    }
}
