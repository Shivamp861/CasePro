using Domaincasepro.Queries;
using Domaincasepro.Repository;
using Modelcasepro.Entities;
using Modelcasepro.Factory;
using Modelcasepro.ResponseModel;
using Modelcasepro.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaincasepro.Commands
{
    public class InstructorDataCommandHandler
    {
        private readonly IInstructOperationRepository _instructRepo;
        public InstructorDataCommandHandler(IInstructOperationRepository instructRepo)
        {
            _instructRepo = instructRepo;
        }
        public InstructorResponseModel SaveData(InstructOperation request)
        {
            InstructorFormDetail formDetail = new InstructorFormDetail
            {
                SelectedActivity = request.SelectedActivity,
                Name = request.Name,
                Date = request.Date,
                Note = request.Note,
                HasSent = false,
                HasSubmitted = false,
                ActivityId = request.ActivityId,

            };
            bool Instructor = _instructRepo.SaveMailData(formDetail);
            var id = formDetail.Id;
            if (Instructor)
            {
                return InstructorResponseFactory.Create(true, "Data inserted successfully", id);
            }
            else
            {
                return InstructorResponseFactory.Create(false, "There is somthing wrong in store data,Please try again", id);
            }
        }
        public InstructorResponseModel SendMail(InstructOperation request, int activityid, int instructorId, string email)
        {
            InstructorFormDetail formDetail = new InstructorFormDetail
            {
                SelectedActivity = request.SelectedActivity,
                Name = request.Name,
                Date = request.Date,
                Note = request.Note,
                HasSent = false,
                HasSubmitted = false,

            };


            bool Sendinstructormail = _instructRepo.SendMailData(formDetail, email, activityid, instructorId);
            var id = formDetail.Id;
            if (Sendinstructormail)
            {
                return InstructorResponseFactory.Create(true, "Mail sent successfully....!", id);
            }
            else
            {
                return InstructorResponseFactory.Create(false, "There is somthing wrong in send mail,Please try again", id);
            }


        }
        public InstructorResponseModel updatedata()
        {
            InstructorFormDetail formDetail = new InstructorFormDetail
            {
                HasSent = true,
            };
            var HasSentval = formDetail.HasSent;

            bool UpdateSentData = _instructRepo.Update(HasSentval);
            var id = formDetail.Id;
            if (UpdateSentData)
            {
                return InstructorResponseFactory.Create(true, "Data inserted successfully", id);
            }
            else
            {
                return InstructorResponseFactory.Create(false, "There is somthing wrong in update data,Please try again", id);
            }
        }

        public InstructorResponseModel UpdateMailData(int instructorId)
        {
            InstructorFormDetail formDetailmail = new InstructorFormDetail
            {
                HasSubmitted = true,
            };
            var HasSubmittedval = formDetailmail.HasSubmitted;
            var id = instructorId;
            bool UpdateMailData = _instructRepo.Updatemaildata(HasSubmittedval, id);
             

            if (UpdateMailData)
            {
                return InstructorResponseFactory.Create(true, "Data updated successfully", id);
            }
            else
            {
                return InstructorResponseFactory.Create(false, "There is somthing wrong in update data,Please try again", id);
            }
        }
    }
}
