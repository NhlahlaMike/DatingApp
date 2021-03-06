﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.Data;
using DatingApp.Dtos;
using DatingApp.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.Controllers
{
	[ServiceFilter(typeof(LogUserActivity))]
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
			var usersToReturn = _mapper.Map<IEnumerable<UserForDetailedDto>>(users); // _mapper.Map<Destination>(Source);
			return Ok(usersToReturn);
		}

		// GET: api/Users/5
		[HttpGet("{id}", Name ="GetUser")]
		public async Task<IActionResult> GetUser(int id)
		{
			var user = await _repo.GetUser(id);
			var userToReturn = _mapper.Map<UserForDetailedDto>(user); // _mapper.Map<Destination>(Source);
			return Ok(userToReturn);
		}

		// GET: api/Users/1
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateUser(int id, UserForUpdateDto userForUpdateDto)
		{
			if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
				return Unauthorized();

			var userFromRepo = await _repo.GetUser(id);
			_mapper.Map(userForUpdateDto, userFromRepo); // _mapper.Map<Destination>(Source);

			if (await _repo.SaveAll())
				return NoContent();

			throw new Exception($"Updating user {id} failed on save");
		}
		// POST: api/Users
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		// PUT: api/Users/5
		/*[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}*/

		// DELETE: api/ApiWithActions/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
