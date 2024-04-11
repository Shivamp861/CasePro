using Modelcasepro.Entities;

namespace Domaincasepro.Repository
{
    public interface IActivitySignOffRepository
    {
        ActivitySignOffdetail AddOrUpdateSignOffdetails(ActivitySignOffdetail signoff);
    }
}