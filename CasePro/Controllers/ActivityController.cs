using Domaincasepro.Commands;
using Domaincasepro.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modelcasepro.RequestModel;
using Modelcasepro.ViewModel;

namespace CasePro.Controllers
{
    [Authorize]
    public class ActivityController : Controller
    {
        private readonly ActivityQueryHandler _handler;
        private readonly ActivitydetailsQueryHandler _detailshandler;
        private readonly SiteInstallationCommandHandler _sitehandler;
        private readonly ActivityCommandHandler _activityhandler;
        private readonly CustomerCommadHandler _custhandler;
        private readonly TrailerTippingCommandHandler _Trailerhandler;
        private readonly TrailerTippingQueryHandler _tQueryhandler;
        private readonly ProductDataCommandHandler _productCommandHandler;
        private readonly ResourceDataCommandHandler _resourceCommandHandler;
        private readonly NotesCommandHandler _notescommandhandler;
        private readonly InstructorDataCommandHandler _instructcommandhandler;
        private readonly InstructQueryHandler _instructhandler;
        private readonly SignoffCommadnHandler _signoffcommandhandler;
		private readonly DeleteActivityCommandHandler _deletehandler;

		public ActivityController(ActivityQueryHandler handler, ActivitydetailsQueryHandler detailshandler, SiteInstallationCommandHandler sitehandler, ActivityCommandHandler activityhandler, CustomerCommadHandler custhandler, TrailerTippingCommandHandler trailerhandler, TrailerTippingQueryHandler tQueryhandler, ProductDataCommandHandler productCommandHandler, ResourceDataCommandHandler resourceCommandHandler, NotesCommandHandler notescommandhandler, InstructorDataCommandHandler instructcommandhandler,InstructQueryHandler instructhandler, SignoffCommadnHandler signoffcommandhandler,DeleteActivityCommandHandler deletehandler)
        {
            _handler = handler;
            _detailshandler = detailshandler;
            _sitehandler = sitehandler;
            _activityhandler = activityhandler;
            _custhandler = custhandler;
            _Trailerhandler = trailerhandler;
            _tQueryhandler = tQueryhandler;
            _productCommandHandler = productCommandHandler;
            _resourceCommandHandler = resourceCommandHandler;
            _notescommandhandler = notescommandhandler;
            _instructcommandhandler = instructcommandhandler;
            _instructhandler = instructhandler;
            _signoffcommandhandler = signoffcommandhandler;
            _deletehandler = deletehandler;

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

        public IActionResult CreateActivity(int id, int? InstructorId = 0)
        {
            try
            {
                ViewBag.InstructorId = InstructorId;
                return View(new Modelcasepro.ViewModel.Activity
                {
                    ActivityId = id
                });

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred: " + ex.Message);
            }
        }
        [HttpPost]
        public IActionResult SaveTrailerTipping(TrailerTippingRequestModel TrailerData)
        {
            try
            {
                if (TrailerData != null)
                {
                    var response = _Trailerhandler.AddActivityTrailerdetails(TrailerData);

                    TrailerTippingRequestModel res = _tQueryhandler.ExecuteTrailerQueryById(response.trid);
                    // Check the response and take appropriate action
                    if (response.IsSuccess)
                    {
                        return Json(res);
                    }
                    else
                    {
                        return Json(new { success = false, errorMessage = response.Message }); // Or return appropriate error response
                    }
                }
                else
                {
                    return Json("");
                }

            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = "An error occurred while executing Save SignOff details: " + ex.Message });
            }
        }

        [HttpPost]
        public IActionResult SaveDataactivitydetails([FromForm] Details Sitedata, [FromForm] IFormFile SiteImage)
        {
            try
            {
                if (Sitedata != null)
                {
                    var response = _sitehandler.AddSitedetails(Sitedata, SiteImage);

                    // Check the response and take appropriate action
                    if (response.IsSuccess)
                    {
                        return Json(new { success = true, activityId = response.ActivityId, errorMessage = response.Message });
                    }
                    else
                    {
                        return Json(new { success = false, errorMessage = response.Message }); // Or return appropriate error response
                    }
                }
                else
                {
                    return Json("");
                }


            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = "An error: " + ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Customersavedata(CustomerRequestModel model)
        {
            try
            {
                if (model != null)
                {
                    var response = _custhandler.AddCust(model);

                    // Check the response and take appropriate action
                    if (response.IsSuccess)
                    {
                        return Json(new { success = true, activityId = response.ActivityId, errorMessage = response.Message });
                    }
                    else
                    {
                        return Json(new { success = false, errorMessage = response.Message }); // Or return appropriate error response
                    }
                }
                else
                {
                    return Json("");
                }


            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = "An error occurred while executing Save Activity data: " + ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Savejobcard(JobCard model)
        {
            try
            {
                if (model != null)
                {
                    var response = _activityhandler.AddActivity(model);

                    // Check the response and take appropriate action
                    if (response.IsSuccess)
                    {
                        return Json(new { success = true, activityId = response.ActivityId, activityType = response.ActivityType, errorMessage = response.Message });
                    }
                    else
                    {
                        return Json(new { success = false, errorMessage = response.Message }); // Or return appropriate error response
                    }
                }
                else
                {
                    return Json("");
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = "An error occurred: " + ex.Message });
            }
        }

        [HttpPost]
        public ActionResult ProductData(string shift, DateTime date, string summaryOfWorks, int aid, int pid)
        {
            if (summaryOfWorks != null)
            {
                ProductDetails PD = new ProductDetails
                {
                    Date = date,
                    Shift = shift,
                    SummaryOfWorks = summaryOfWorks,
                    ActivityId = aid,


                };
                if (pid != null && pid > 0)
                {
                    var res = _productCommandHandler.update(pid, PD);
                    if (res.IsSuccess)
                    {
                        return Json(new { success = true });
                    }
                    else
                    {
                        return Json(new { success = false, errorMessage = res.Message });
                    }
                }
                else
                {
                    var response = _productCommandHandler.Create(PD);
                    if (response.IsSuccess)
                    {
                        return Json(new { success = true });
                    }
                    else
                    {
                        return Json(new { success = false, errorMessage = response.Message });
                    }
                }

            }
            else
            {

                return View("CreateActivity");
            }
        }
        [HttpPost]
        public ActionResult ResourseData(string resourcetype, string shift, string daynight, string name, string comment, int aid, int rid)
        {
            if (name != null && comment != null)
            {
                ResourseDetails RD = new ResourseDetails
                {
                    ResourceType = resourcetype,
                    Shift = shift,
                    DayNight = daynight,
                    Name = name,
                    Comments = comment,
                    ActivityId = aid,
                    Date = DateTime.Now,

                };
                if (rid != null && rid > 0)
                {
                    var res = _resourceCommandHandler.update(rid, RD);
                    if (res.IsSuccess)
                    {
                        return Json(new { success = true });
                    }
                    else
                    {
                        return Json(new { success = false, errorMessage = res.Message });
                    }
                }
                else
                {
                    var response = _resourceCommandHandler.Create(RD);
                    if (response.IsSuccess)
                    {
                        return Json(new { success = true });
                    }
                    else
                    {
                        return Json(new { success = false, errorMessage = response.Message });
                    }
                }
            }
            else
            {

                return View("CreateActivity");
            }
        }

        public IActionResult SaveNote(JobCard jobcard)
        {

            if (jobcard != null)
            {
                var noteresponse = _notescommandhandler.Create(jobcard);
                if (noteresponse.IsSuccess)
                {
                    
                    TempData["SuccessMessage"] =noteresponse.Message;
                    return RedirectToAction("CreateActivity", "Activity", new { id = noteresponse.ActivityId});
                }
                else
                {
                    return View(new { success = false, errorMessage = noteresponse.Message });
                }
            } 
            else
            {
                return RedirectToAction("CreateActivity", "Activity", new { id = jobcard.ActivityId });
            }


        }
        public IActionResult SaveSignOff([FromBody] List<SignoffRequestModel> signOffData)
        {
            try
            {
                var response = _signoffcommandhandler.AddSignoffDetailsAsync(signOffData);

                // Check the response and take appropriate action
                if (response.IsSuccess)
                {
                    int InstructorId = 0;
                    int activityId = 0;
                    foreach (var item in signOffData)
                    {
                        InstructorId = item.InstructorId;
                        activityId = item.ActivityId;
                    }
                    // var instrtuId = response.
                    var instructordata = _instructcommandhandler.UpdateMailData(InstructorId);
                    var loadinstructordata = _instructhandler.GetInstructOperationsDetails(activityId);

                    return Json(new { success = true, activityId = response.ActivityId, instructordata = loadinstructordata, errorMessage = response.Message });
                }
                else
                {
                    return Json(new { success = false, errorMessage = response.Message }); // Or return appropriate error response
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = "An error occurred while executing Save SignOff details: " + ex.Message });
            }
        }
        public IActionResult SaveInstruction(InstructOperation model)
        {
            var activityid = model.ActivityId;            
            string iname = model.Name;
            if (activityid == null && activityid == '0')
            {
                var savemaildata = _instructcommandhandler.SaveData(model);
                if (savemaildata.IsSuccess)
                {
                    var InstructorId = savemaildata.ActivityId;
                    var instructordata = _instructhandler.GetInstructOperationsDetails(activityid);
                    var getemail = _instructhandler.GetMailByName(iname);
                    string email = getemail.Email;

                    var sendmail = _instructcommandhandler.SendMail(model, activityid, InstructorId, email);
                    if (sendmail.IsSuccess)
                    {
                        var updatesend = _instructcommandhandler.updatedata();
                        if (updatesend.IsSuccess)
                        {
                            instructordata = _instructhandler.getinstructoperationsdetails(activityid);
                            TempData["SuccessMessage"] = updatesend.Message;
                            return RedirectToAction("CreateActivity", "Activity", new { id = activityid });

                        }
                        else
                        {
                            TempData["SuccessMessage"] = updatesend.Message;
                            return RedirectToAction("CreateActivity", "Activity", new { success = false});
                        }
                    }
                    else
                    {
                        TempData["SuccessMessage"] = sendmail.Message;
                        return RedirectToAction("CreateActivity", "Activity", new { data = instructordata});
                    }
                }
                else
                {
                    TempData["SuccessMessage"] = savemaildata.Message;
                    return RedirectToAction("CreateActivity", "Activity", new { success = false});
                }
            }
            else
            {
                TempData["SuccessMessage"] = "Please fill basic details";
                return RedirectToAction("CreateActivity", "Activity", new { success = false});

            }


        }

		[HttpPost]
		public IActionResult DeleteActivity(int id)
		{
			try
			{
				var response = _deletehandler.DeleteHandler(id);

				// Check the response and take appropriate action
				if (response.IsSuccess)
				{
					return Json(new { success = true, activityId = response.ActivityId, errorMessage = response.Message });
				}
				else
				{
					return Json(new { success = false, errorMessage = response.Message }); // Or return appropriate error response
				}

			}
			catch (Exception ex)
			{
				throw new Exception("An error occurred while delete Activity: " + ex.Message);
			}


			return View();
		}
	}
}
