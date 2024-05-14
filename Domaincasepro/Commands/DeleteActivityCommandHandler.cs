using Domaincasepro.Repository;
using Microsoft.EntityFrameworkCore;
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
	public class DeleteActivityCommandHandler
	{
		private readonly IActivityRepository _repo;

		public DeleteActivityCommandHandler(IActivityRepository repo)
		{
			_repo = repo;
		}

		public ActivityResponseModel DeleteHandler(int id)
		{
			try
			{

				bool res = _repo.DeleteActivity(id);

				if (res)
				{
					return ActivityResponseFactory.Create(true, "Activity Deleted Succesfully", 0, "");
				}
				else
				{
					// Something went wrong while adding the activity
					return ActivityResponseFactory.Create(false, "Failed to Delete activity", 0, "");
				}
			}
			catch (Exception ex)
			{
				throw new Exception("An error occurred while adding activity: " + ex.Message);
			}
		}
	}


}
