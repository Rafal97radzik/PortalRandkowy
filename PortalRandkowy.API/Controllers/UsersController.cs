using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalRandkowy.API.Data;
using PortalRandkowy.API.DTOs;
using PortalRandkowy.API.Helpers;

namespace PortalRandkowy.API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    [Route("api/[Controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserRepository repository;
        private readonly IMapper mapper;

        public UsersController(IUserRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery]UserParams userParams)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var userFromRepo = await repository.GetUser(currentUserId);
            
            userParams.UserId = currentUserId;

            if (string.IsNullOrEmpty(userParams.Gender))
                userParams.Gender = userFromRepo.Gender == "mężczyzna" ? "kobieta" : "mężczyzna";

            var users = await repository.GetUsers(userParams);

            var usersToReturn = mapper.Map<IEnumerable<UserForListDto>>(users);

            Response.AddPagination(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);

            return Ok(usersToReturn);
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await repository.GetUser(id);

            var userForReturn = mapper.Map<UserForDetailedDto>(user);

            return Ok(userForReturn);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserForUpdateDto userForUpdateDto)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var userFromRepo = await repository.GetUser(id);
            mapper.Map(userForUpdateDto, userFromRepo);

            if(await repository.SaveAll())
            {
                return NoContent();
            }
            else
            {
                throw new Exception($"Aktualizacja użytkownika o id: {id} nie powiodła się przy zapisywaniu do bazy");
            }
        }
    }
}