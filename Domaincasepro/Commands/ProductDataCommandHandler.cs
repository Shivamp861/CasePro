using Azure.Core;
using Domaincasepro.Repository;
using Modelcasepro.Entities;
using Modelcasepro.Factory;
using Modelcasepro.RequestModel;
using Modelcasepro.ResponseModel;
using Modelcasepro.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaincasepro.Commands
{

    public class ProductDataCommandHandler
    {
        private readonly IProductRepository _productRepo;

        public ProductDataCommandHandler(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }
        public ActivityResponseModel Create(ProductDetails request)
        {
            ActivityProductDetail productdata = new ActivityProductDetail
            {
                Shift = request.Shift,
                Date = request.Date,
                SummaryOfWorks = request.SummaryOfWorks,
                ActivityId = request.ActivityId,
            };
            bool product = _productRepo.Create(productdata);
            int pid = productdata.Id;
            if (product)
            {
                return ProductResponseFactory.Create(true, "Data inserted successfully", pid);

            }
            else
            {
                return ProductResponseFactory.Create(false, "something went wrong,Please try again", pid);
            }

        }

        public ActivityResponseModel update(int pid, ProductDetails request)
        {
            ActivityProductDetail productdata = new ActivityProductDetail
            {
                Shift = request.Shift,
                Date = request.Date,
                SummaryOfWorks = request.SummaryOfWorks,
                ActivityId = request.ActivityId,
            };
            bool product = _productRepo.Update(productdata, pid);
            if (product)
            {
                return ProductResponseFactory.Create(true, "Data inserted successfully", pid);

            }
            else
            {
                return ProductResponseFactory.Create(false, "something went wrong,Please try again", pid);
            }
        }
    }
}
