using Modelcasepro.Entities;
using Modelcasepro.RequestModel;
using Modelcasepro.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domaincasepro.Repository
{
    public interface IActivityRepository
    {
        public ActivityTable AddOrUpdateActivity(ActivityTable activityinfo);

        public ActivityTable GetActivityById(int activityid);

		List<ActivityViewModel> GetAllList();

        ActivitySummary ActivitySummaryGetAllList(int id);


        bool DeleteActivity(int activityId);
		public (bool,bool) Create(ActivityNote notes);
        public ActivityTable getActivityId(ActivityTable activityid);
        public ActivityTable updateactivitystatus(string status, int activityid);
		public (ActivityTable, int) CloneAddOrUpdateActivity(ActivityTable activityEntity);
        public ActivityTable getActType(int deleteid);
    }
}
