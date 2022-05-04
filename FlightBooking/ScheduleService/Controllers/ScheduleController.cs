using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScheduleService.Models;
using ScheduleService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace ScheduleService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly ISchedule _scheduleRepository;

        public ScheduleController(ISchedule sheduleRepository)
        {
            _scheduleRepository = sheduleRepository;
        }

        [HttpGet]
        public IActionResult getScheduleDetail()
        {
            var scheduleDetails = _scheduleRepository.GetScheduleDetails();
            return new OkObjectResult(scheduleDetails);
        }

        [HttpPost]
        [Route("createScheduleDetails")]
        public IActionResult CreateSchedule([FromBody] ScheduleDetail scheduleDetail)
        {
            using (var scope = new TransactionScope())
            {
                _scheduleRepository.CreateSchedule(scheduleDetail);
                scope.Complete();
                return CreatedAtAction(nameof(getScheduleDetail), scheduleDetail);
            }
        }

        [HttpPut]
        [Route("updateScheduleDetails")]
        public IActionResult UpdateSchedule([FromBody] ScheduleDetail scheduleDetail)
        {
            if (scheduleDetail != null)
            {
                using (var scope = new TransactionScope())
                {
                    _scheduleRepository.UpdateSchedule(scheduleDetail);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        [HttpGet]
        [Route("searchScheduleDetails")]

        public async Task<ActionResult<IEnumerable<ScheduleDetail>>> Search(string fromplace, string toPlace)
        {
            try
            {
                var result = await _scheduleRepository.searchScheduleDetails(fromplace, toPlace);

                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
    }
}
