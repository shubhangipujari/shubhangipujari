using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using TicketBookingService.Models;
using TicketBookingService.Repository;

namespace TicketBookingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketBookingController : ControllerBase
    {
        private readonly ITicketBooking _TicketRepository;

        public TicketBookingController(ITicketBooking TicketRepository)
        {
            _TicketRepository = TicketRepository;
        }

        [HttpGet]
        public IActionResult getTicketBooking()
        {
            var ticketBookingDetails = _TicketRepository.GetTicketBooking();
            return new OkObjectResult(ticketBookingDetails);
        }


        [HttpPost]
        [Route("createTicketBooking")]


        public IActionResult CreateTicketBooing([FromBody] FlightBookingDetail ticketBookingDetails)
        {
            using (var scope = new TransactionScope())
            {
                _TicketRepository.CreateTicketBooking(ticketBookingDetails);
                scope.Complete();
                return CreatedAtAction(nameof(getTicketBooking), ticketBookingDetails);
            }
        }

        [HttpPut]
        [Route("updateScheduleDetails")]
        public IActionResult UpdateTicketBooking([FromBody] FlightBookingDetail scheduleDetail)
        {
            if (scheduleDetail != null)
            {
                using (var scope = new TransactionScope())
                {
                    _TicketRepository.UpdateTicketBooking(scheduleDetail);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }
    }
}