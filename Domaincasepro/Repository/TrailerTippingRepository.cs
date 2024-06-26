﻿using Microsoft.EntityFrameworkCore.Metadata;
using Modelcasepro.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Domaincasepro.Repository
{
    public class TrailerTippingRepository : ITrailerTippingRepository
    {
        private CaseproDbContext _context;
        public TrailerTippingRepository(CaseproDbContext context)
        {
            this._context = context;
        }

        public ActivityTrailerTable AddTrailerTipping(ActivityTrailerTable data)
        {
            try
            {
                var existing = _context.ActivityTrailerTables.Where(x => x.Id == data.Id && x.ActivityId == data.ActivityId).FirstOrDefault();

                if (existing != null)
                {
                    _context.Entry(existing).CurrentValues.SetValues(data);
                }
                else
                {
                    _context.ActivityTrailerTables.Add(data);
                }

                _context.SaveChanges();               

                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<ActivityTrailerTable> GetAllListById(int id)
        {
            // Retrieve all activities from the database
            return _context.ActivityTrailerTables.Where(x => x.ActivityId == id).ToList();
        }
        public ActivityTrailerTable getActivityTrailerId(int id)
        {
            var latestEntity = _context.ActivityTrailerTables.Where(x => x.Id == id).FirstOrDefault();
            return latestEntity;

        }
        public bool DeletedetailsFromCalendar(int id)
        {
            bool success = false;
            try
            {
                var activity = _context.ActivityDetails.Where(x => x.ActivityId == id).FirstOrDefault();
                if (activity == null)
                {
                    throw new InvalidOperationException("Activity not found");
                }
                else
                {
                    _context.ActivityDetails.Remove(activity);
                    _context.SaveChanges();
                    success = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while delete Activity: " + ex.Message);
            }
            return success;
        }
    }
}
