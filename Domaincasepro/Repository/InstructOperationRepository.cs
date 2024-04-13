using Modelcasepro.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domaincasepro.Repository
{
    public class InstructOperationRepository : IInstructOperationRepository
    {
        private CaseproDbContext _context;
        public InstructOperationRepository(CaseproDbContext context)
        {
            this._context = context;
        }
        public InstructorFormDetail GetInstructById(int activityid)
        {

            return _context.InstructorFormDetails.Where(x => x.ActivityId == activityid).FirstOrDefault();
        }

        public List<InstructorFormDetail> GetInstructorOperationsDetails(int id)
        {
            var data = _context.InstructorFormDetails.Where(x => x.ActivityId == id).ToList();
            return data;
        }

        public List<InstructorName> GetInstrutNameList()
        {
            return _context.InstructorNames.Select(x => new InstructorName
            {
                Name = x.Name,
            }).ToList();
        }

        public InstructorName GetMailByName(string iname)
        {
            return _context.InstructorNames.Where(x => x.Name == iname).FirstOrDefault(); 
        }

        public bool SaveMailData(InstructorFormDetail formDetail)
        {
            bool success = false;
            try
            {
                _context.InstructorFormDetails.Add(formDetail);
                _context.SaveChanges();
                success = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return success;
        }

        public bool SendMailData(InstructorFormDetail formDetail, string email, int activityid, int InstructorId)
        {
            try
            {
                // Sender's email credentials
                string senderEmail = "divyazinzuvadia09@gmail.com";
                string password = "dyguayajgtomdkio";

                // Configure the SMTP client
                SmtpClient client = new SmtpClient("smtp.gmail.com");
                client.Port = 587;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(senderEmail, password);

                // Create the email message
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(senderEmail);
                mailMessage.To.Add(email);
                mailMessage.Subject = " Action Required:Manager sign off";

                mailMessage.Body = $"Hello {formDetail.Name},\n\n" +
    $"You have been assigned the activity '{formDetail.SelectedActivity}' on {formDetail.Date?.Date.ToShortDateString()}.\n\n" + // Extracting only the date part
    $"Before clicking the following link to create the activity, kindly login using your credentials:\n" +
    $"https://localhost:44324/Activity/CreateActivity?id={activityid}&InstructorId={InstructorId}\n\n" + // Modified link
    $"Note: {formDetail.Note}\n\n" +
    $"Best regards,\nYour Name";


                // Optionally, you can set the email format to HTML if needed
                // mailMessage.IsBodyHtml = true;

                // Send the email
                client.Send(mailMessage);

                return true; // Email sent successfully
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"Error sending email: {ex.Message}");
                return false; // Failed to send email
            }

        }

        public bool Update(bool? hasSentval)
        {
            bool success = false;
            try
            {
                var updaterow = _context.InstructorFormDetails.OrderByDescending(x => x.Id).FirstOrDefault();
                if (updaterow != null)
                {
                    updaterow.HasSent = hasSentval;
                    _context.SaveChanges();
                    success = true;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return success;
        }

        public bool Updatemaildata(bool? hasSubmittedval, int id)
        {
            bool success = false;
            try
            {
                var instructor = _context.InstructorFormDetails.FirstOrDefault(x => x.Id == id);
                if (instructor != null)
                {
                    instructor.HasSubmitted = hasSubmittedval;
                    _context.SaveChanges();
                    success = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return success;
        }
    }
}
