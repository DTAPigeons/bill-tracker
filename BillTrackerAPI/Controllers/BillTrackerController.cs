using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BillTrackerAPI.Data.Models;
using BillTrackerAPI.Data.MongoDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BillTrackerAPI.Controllers
{
    [ApiController]
    public abstract class BillTrackerController<TService, TModel> : ControllerBase
        where TService: MongoService<TModel>
        where TModel: BaseEntity
    {
        protected readonly TService service;

        public BillTrackerController(TService service)
        {
            this.service = service;

        }

        
        [HttpGet]
        public virtual async Task<ActionResult<List<TModel>>> GetAll()
        {
            return await GetAll(item => true);
        }

        protected virtual async Task<ActionResult<List<TModel>>> GetAll(Expression<Func<TModel, bool>> filterExpression)
        {
            return await service.GetAll(filterExpression);
        }

        
        [HttpGet("{id:length(24)}")]
        public virtual async Task<ActionResult<TModel>> GetById(string id)
        {
            var item = await service.GetById(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        
        
        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Put(string id, TModel itemIn)
        {
            var item = await service.GetById(id);

            if (!itemIn.IsValid())
            {
                return BadRequest();
            }

            if (id != item.Id)
            {
                return BadRequest();
            }

            if (item == null)
            {
                return NotFound();
            }

            await service.Update(id, itemIn);

            return NoContent();
        }

        
        [HttpPost]
        public virtual async Task<ActionResult<TModel>> Create(TModel item)
        {
            if (!item.IsValid())
            {
                return BadRequest();
            }

            await service.Create(item);

            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public virtual async Task<ActionResult<TModel>> Delete(string id)
        {
            var item = await service.GetById(id);
            if (item == null)
            {
                return NotFound();
            }

            await service.Delete(id);

            return item;
        }
    }
}
