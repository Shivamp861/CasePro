using Modelcasepro.Entities;

namespace Domaincasepro.Repository
{
    public interface ICustomerRepository
    {
        public ActivityCustomerTable AddOrUpdateCustomer(ActivityCustomerTable cusInfo);
    }
}