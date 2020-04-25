using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PortalRandkowy.API.Data;
using PortalRandkowy.API.Models;

namespace PortalRandkowy.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext context;

        public ValuesController(DataContext context)
        {
            this.context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Value>>> GetValues()
        {
            List<Value> values =await context.Values.ToListAsync();

            return values;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Value>> GetValue(int id)
        {
            Value value = await context.Values.FirstOrDefaultAsync(v => v.Id == id);

            return value;
        }

        [HttpPost]
        public async Task<ActionResult> AddValue([FromBody] Value value)
        {
            context.Values.Add(value);
            await context.SaveChangesAsync();

            return Ok(value);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditValue(int id, [FromBody] Value value)
        {
            Value data = await context.Values.FindAsync(id);
            data.Name = value.Name;
            context.Values.Update(data);
            await context.SaveChangesAsync();

            return Ok(data);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteValue(int id)
        {
            Value data = await context.Values.FindAsync(id);

            if (data == null)
                return NoContent();

            context.Values.Remove(data);
            await context.SaveChangesAsync();

            return Ok(data);
        }
    }
}
