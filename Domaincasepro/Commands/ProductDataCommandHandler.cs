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
    public class ProductDataCommandHandler
    {
        public static ActivityResponseModel Create(ProductRequestModel request, ActivityTable getresponse, IProductRepository productRepository)
        {
            ActivityProductDetail productdata = new ActivityProductDetail
            {
                Shift = request.Shift,
                Date = request.Date,
                SummaryOfWorks = request.SummaryOfWorks,
                ActivityId=getresponse.Id,
            };
            bool product  = productRepository.Create(productdata);
            if (product )
            {
                return ProductResponseFactory.Create(true, "Data inserted successfully");

            }
            else
            {
                return ProductResponseFactory.Create(false, "something went wrong,Please try again");
            }
           
        }
        
    }
}
