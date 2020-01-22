using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.Data;
using DatingApp.Dtos;
using DatingApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthRepository _repo;
		private readonly IConfiguration _config; // in order to use and access appsettings.json
		private readonly IMapper _mapper;

		public AuthController(IAuthRepository repo, IConfiguration config, IMapper mapper)
		{
			_repo = repo;
			_config = config;
			_mapper = mapper;
		}
		// GET: api/Auth
		[HttpPost("register")]
		public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
		{
			// validate request

			userForRegisterDto.Username = userForRegisterDto.Username.ToLower();
			if (await _repo.UserExists(userForRegisterDto.Username))
				return BadRequest("username already exists");

			/*var userToCreate = new User
			{
				Username = userForRegisterDto.Username,
			};*/

			
			var userToCreate = _mapper.Map<User>(userForRegisterDto); // _mapper.Map<Destination>(Source);

			var createdUser = await _repo.Register(userToCreate, userForRegisterDto.Password);

			var userToReturn = _mapper.Map<UserForDetailedDto>(createdUser);

			return CreatedAtRoute("GetUser", new { controller = "Users", id = createdUser.Id }, userToReturn);
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login(UserForloginDto userForloginDto)
		{

			var userFromRepo = await _repo.Login(userForloginDto.Username.ToLower(), userForloginDto.Password);
			
			if (userFromRepo == null)
				return Unauthorized();

			// add identity claims
			var claims = new[]
			{
				new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
				new Claim(ClaimTypes.Name, userFromRepo.Username)

			};

			// generate key / IssuerSigningKey
			var key = new SymmetricSecurityKey(Encoding.UTF8
				.GetBytes(_config.GetSection("AppSettings:Token").Value));

			// apply signin credentials
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.Now.AddDays(1),
				SigningCredentials = creds
			};

			var tokenHandler = new JwtSecurityTokenHandler();

			var token = tokenHandler.CreateToken(tokenDescriptor);
			var user = _mapper.Map<UserForListDto>(userFromRepo); // _mapper.Map<Destination>(Source);

			return Ok(new { token = tokenHandler.WriteToken(token), user });
		}

		// GET: api/Auth/5
		[HttpGet("{id}", Name = "Get")]
		public string Get(int id)
		{
			return "value";
		}

		// POST: api/Auth
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		// PUT: api/Auth/5
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
