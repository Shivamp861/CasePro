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
        private readonly ICustomerRepository _repo;

        public CustomerCommadHandler(ICustomerRepository repo)
        {
            _repo = repo;
        }

        public  ActivityResponseModel AddCust(CustomerRequestModel CustRequest)
        {
            try
            {
                ActivityCustomerTable CustEntity = MapToEntity(CustRequest);
                ActivityCustomerTable addedCustInfo =  _repo.AddOrUpdateCustomer(CustEntity);

                if (addedCustInfo != null)
                {
                    if (CustRequest.custid==0)
                    {
                        return ActivityResponseFactory.Create(true, "Customer Added", CustEntity.Id, "");
                    }
                    else
                    {
                        return ActivityResponseFactory.Create(true, "Customer Updated", CustEntity.Id, "");
                    }
                    
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
                ContactNo=requestModel.ContactNo,
                Id=requestModel.custid,

            };
        }
    }
}
