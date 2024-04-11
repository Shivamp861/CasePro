using Domaincasepro.Queries;
using Microsoft.AspNetCore.Mvc;
using Modelcasepro.RequestModel;

namespace CasePro.Controllers
{
    public class ActivityController : Controller
    {
        private readonly ActivityQueryHandler _handler;
        public ActivityController(ActivityQueryHandler handler)
        {
            _handler = handler;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ActivitiesList()
        {
            try
            {
                
                List<ActivityViewModel> activities = _handler.ExecuteGetList();
                return View(activities);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while Get List of Activities: " + ex.Message);
            }


        }

        public IActionResult CreateActivity(int id) {
            try
            {
                return View(new Modelcasepro.ViewModel.Activity {
					ActivityId = id
				});
            }
            catch (Exception ex)
            {
				throw new Exception("An error occurred: " + ex.Message);
			}
        }
        public IActionResult ProductDetails(int id) 
        {
            return View();
        }
        public IActionResult ResourseDetails(int id)
        {
            return View();
        }
    }
}
