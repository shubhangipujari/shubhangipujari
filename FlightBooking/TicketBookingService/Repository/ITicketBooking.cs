using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketBookingService.Models;

namespace TicketBookingService.Repository
{
   public interface ITicketBooking
    {
        IEnumerable<FlightBookingDetail> GetTicketBooking();

        void CreateTicketBooking(FlightBookingDetail ticketBooking);
        // void DeleteUser(int userId);
        void UpdateTicketBooking(FlightBookingDetail ticketBooking);

        void Save();
    }
}

   