using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketBookingService.Context;
using TicketBookingService.Models;

namespace TicketBookingService.Repository
{
    public class TicketBookingRepository : ITicketBooking
    {
        private readonly TicketBookingContext _dbContext;

        public TicketBookingRepository(TicketBookingContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void CreateTicketBooking(FlightBookingDetail ticketBookinh)
        {
            _dbContext.Add(ticketBookinh);
            Save();
        }

        public IEnumerable<FlightBookingDetail> GetTicketBooking()
        {
            return _dbContext.details.ToList();
        }


        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void UpdateTicketBooking(FlightBookingDetail ticketBookinh)
        {
            _dbContext.Entry(ticketBookinh).State = EntityState.Modified;
            Save();
        }
    }
}
