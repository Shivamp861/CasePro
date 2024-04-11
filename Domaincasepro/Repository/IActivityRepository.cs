﻿using Modelcasepro.Entities;
using Modelcasepro.RequestModel;
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
        public Task<ActivityTable> AddOrUpdateActivity(ActivityTable activityinfo);

        public ActivityTable GetActivityById(int activityid);

		List<ActivityViewModel> GetAllList();


		public bool Create(ActivityNote notes);
        public ActivityTable getActivityId(ActivityTable activityid);
        public ActivityProductDetail getProductById(int activityId);
    }
}
