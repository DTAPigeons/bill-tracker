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

        
        [HttpGet("{accountName:length(50)}")]
        public async Task<ActionResult<User>> GetByAccountName(string accountName)
        {
            var item = await service.GetUserByAccountName(accountName);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        public async override Task<ActionResult<User>> Create(User item)
        {
            var user = await service.GetUserByAccountName(item.AccountName);
            if (user != null)
            {
                user.AccountName = null;
            }
            return await base.Create(item);
        }
    }
}
