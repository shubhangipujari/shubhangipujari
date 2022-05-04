using Microsoft.EntityFrameworkCore;
using ScheduleService.Context;
using ScheduleService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleService.Repository
{
    public class ScheduleRepository:ISchedule
    {
        private readonly ScheduleContext _dbContext;

        public ScheduleRepository(ScheduleContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<ScheduleDetail> GetScheduleDetails()
        {
            return _dbContext.details.ToList();
        }

        public void CreateSchedule(ScheduleDetail scheduleDetail)
        {
            _dbContext.Add(scheduleDetail);
            Save();
        }

        public void UpdateSchedule(ScheduleDetail scheduleDetail)
        {
            _dbContext.Entry(scheduleDetail).State = EntityState.Modified;
            Save();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

       

        public async Task<IEnumerable<ScheduleDetail>> searchScheduleDetails(string fromPlace, string toPlace)
        {
            IQueryable<ScheduleDetail> query = _dbContext.details;



            if (fromPlace != null)
            {
                query = query.Where(e => e.FromPlace == fromPlace);
            }
            if (toPlace != null)
            {
                query = query.Where(e => e.ToPlace == toPlace);
            }
            //if (chooseway.emp != '')
            //{
            //    query = query.Where(e => e.ChooseWay == chooseway);
            //}

            return await query.ToListAsync();
        }
    }
}
