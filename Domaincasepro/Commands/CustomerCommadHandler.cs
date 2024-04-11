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
    public class CustomerCommadHandler
    {
        private readonly CaseproDbContext _context;

        public CustomerCommadHandler(CaseproDbContext context)
        {
            _context = context;
        }

        public  ActivityResponseModel AddCustAsync(CustomerRequestModel CustRequest, ICustomerRepository Custrepo)
        {
            try
            {
                ActivityCustomerTable CustEntity = MapToEntity(CustRequest);
                ActivityCustomerTable addedCustInfo =  Custrepo.AddOrUpdateCustomer(CustEntity);

                if (addedCustInfo != null)
                {
                    return ActivityResponseFactory.Create(true, "Customer Added", CustEntity.Id,"");
                }
                else
                {
                    // Something went wrong while adding the activity
                    return ActivityResponseFactory.Create(false, "Failed to add activity", 0, "");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding activity: " + ex.Message);
            }
        }

        private ActivityCustomerTable MapToEntity(CustomerRequestModel requestModel)
        {
            return new ActivityCustomerTable
            {
                ActivityId = requestModel.ActivityId,
                Name = requestModel.CustomerName,
                ContactNo=requestModel.ContactNo

            };
        }
    }
}
