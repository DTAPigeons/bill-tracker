﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BillTrackerAPI.Data.MongoDB;
using BillTrackerAPI.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace BillTrackerAPI.Controllers
{
    [Route("api/Users")]
    [ApiController]
    [Authorize]
    public class UsersController : BillTrackerController<UserService, User>
    {
        public UsersController(UserService service) : base(service)
        {
        }
    }
}