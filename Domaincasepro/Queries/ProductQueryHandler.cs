using Domaincasepro.Repository;
using Modelcasepro.Entities;
using Modelcasepro.RequestModel;
using Modelcasepro.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaincasepro.Queries
{
    public class ProductQueryHandler
    {
        public readonly IProductRepository _productRepo;
        public ProductQueryHandler(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }


        public List<ProductDetails> GetProductDetails(int activityId)
        {
            try
            {
                var activity = ExecuteProductQueryById(activityId);
                List<ProductDetails> productDetails = activity.Select(productDetails => new ProductDetails
                {
                    Shift = productDetails.Shift,
                    SummaryOfWorks = productDetails.SummaryOfWorks,
                    Id = productDetails.Id,
                    ActivityId = activityId,
                    Date = productDetails.Date,
                }).ToList();
                //return new ProductDetails()
                //{
                //    Shift = activity.Shift,
                //    Date = activity.Date,
                //    SummaryOfWorks = activity.SummaryOfWorks,
                //    ActivityId = activity.Id,
                //};
                return productDetails;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred: " + ex.Message);
            }
        }


        private List<ActivityProductDetail> ExecuteProductQueryById(int activityId)
        {
            //return _repo.GetActivityById(activityid) ?? new ActivityTable();
            return _productRepo.getProductById(activityId) ?? new List<ActivityProductDetail>();
        }

    }
}
