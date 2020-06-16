using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BillTrackerAPI.Data.Models;
using BillTrackerAPI.Data.MongoDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BillTrackerAPI.Controllers
{
    [Route("api/Bills")]
    [ApiController]
    [Authorize]
    public class BillsController : BillTrackerController<BillService, Bill>
    {
        readonly UserService _userService;

        public BillsController(BillService service, UserService userService) : base(service)
        {
            _userService = userService;
        }

        [HttpGet("user/{userId:length(24)}")]
        public async Task<ActionResult<List<Bill>>> GetBillsForUser(string userId)
        {
            return await GetAll(bill => bill.UserId == userId);

        }

        public override Task<ActionResult<Bill>> Create(Bill item)
        {
            var user = _userService.GetById(item.UserId);
            item.UserId = null;
            return base.Create(item);
        }
    }
}
