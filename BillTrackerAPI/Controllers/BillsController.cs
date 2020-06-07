using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BillTrackerAPI.Data.Models;
using BillTrackerAPI.Data.MongoDB;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BillTrackerAPI.Controllers
{
    [Route("api/Bills")]
    [ApiController]
    public class BillsController : BillTrackerController<BillService, Bill>
    {
        public BillsController(BillService service) : base(service)
        {
        }

        [HttpGet("user/{userId:length(24)}")]
        public async Task<ActionResult<List<Bill>>> GetBillsForUser(string userId)
        {
            return await GetAll(bill => bill.UserId == userId);

        }
    }
}
