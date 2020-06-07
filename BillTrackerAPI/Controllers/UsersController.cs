using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BillTrackerAPI.Data.MongoDB;
using BillTrackerAPI.Data.Models;

namespace BillTrackerAPI.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UsersController : BillTrackerController<UserService, User>
    {
        public UsersController(UserService service) : base(service)
        {
        }
    }
}
