using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.Data;
using DatingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class ValuesController : ControllerBase
	{
		private readonly DataContext _context;

		public ValuesController(DataContext context)
		{
			_context = context;
		}
		// GET api/values
		[HttpGet("[action]")]
		public async Task<ActionResult> GetValues()
		{
			var values = await _context.Values.ToListAsync();
			return Ok(values);
		}

		// GET api/values/5
		[AllowAnonymous]
		[HttpGet("[action]/{id}")]
		public async Task<ActionResult> GetOneVal([FromRoute] int id)
		{
			var values = await _context.Values.FirstOrDefaultAsync(x => x.Id == id);

			if (values != null)
			{
				return Ok(values);
			}

			return NotFound(new JsonResult("Not found"));
		}

		// POST api/values
		[HttpPost("[action]")]
		public async Task<ActionResult<Value>> PostValues([FromBody] Value model)
		{
			var productAlreadyExist = _context.Values.Any(v => v.Name == model.Name);

			if (productAlreadyExist)
			{
				return BadRequest("Product already Exist");
			}

			var values = new Value()
			{
				Name = model.Name
			};

			try
			{
				await _context.Values.AddAsync(values);
				await _context.SaveChangesAsync();
				return Ok(new JsonResult("Successfully Added a value"));
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		// PUT api/values/5
		[HttpPut("[action]/{id}")]
		public async Task<ActionResult<Value>> UpdateValue(int id, [FromBody] Value model)
		{
			var getValue = await _context.Values.FirstOrDefaultAsync(v => v.Id == id);

			if (getValue != null)
			{
				getValue.Name = model.Name;


				try
				{
					_context.Entry(getValue).State = EntityState.Modified;
					await _context.SaveChangesAsync();
					return Ok(new JsonResult("Value updated"));
				}
				catch (Exception ex)
				{

					throw ex;
				}
			}
			return NotFound("Not Available");
		}

		// DELETE api/values/5
		[HttpDelete("[action]/{id}")]
		public async Task<ActionResult> DeleteValue([FromRoute] int id)
		{
			var getvalue = await _context.Values.FirstOrDefaultAsync(x => x.Id == id);

			if (getvalue != null)
			{
				_context.Remove(getvalue);
				await _context.SaveChangesAsync();
				return Ok(new JsonResult("Deleted successfully"));
			}

			return NotFound(new JsonResult("value not found"));
		}
	}
}
