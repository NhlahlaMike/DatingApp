using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.Data;
using DatingApp.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IDatingRepository _repo;
		private readonly IMapper _mapper;

		public UsersController(IDatingRepository repo, IMapper mapper)
		{
			_repo = repo;
			_mapper = mapper;
		}
		// GET: api/Users
		// [AllowAnonymous] allow access when there is global restriction
		[HttpGet()]
		public async Task<IActionResult> GetUsers()
		{
			var users = await _repo.GetUsers();
			var usersToReturn = _mapper.Map<IEnumerable<UserForDetailedDto>>(users);
			return Ok(usersToReturn);
		}

		// GET: api/Users/5
		[HttpGet("[Action]/{id}")]
		public async Task<IActionResult> GetUser(int id)
		{
			var user = await _repo.GetUser(id);
			var userToReturn = _mapper.Map<UserForDetailedDto>(user); 
			return Ok(userToReturn);
		}

		// POST: api/Users
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		// PUT: api/Users/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE: api/ApiWithActions/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
