using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PortalRandkowy.API.Data;
using PortalRandkowy.API.DTOs;
using PortalRandkowy.API.Models;

namespace PortalRandkowy.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRespository respository;

        public AuthController(IAuthRespository respository)
        {
            this.respository = respository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();

            if (await respository.UserExists(userForRegisterDto.Username))
                return BadRequest("Użytkownik o takej nazwie już istnieje !");

            User userToCreate = new User { Username = userForRegisterDto.Username };
            User createdUser = await respository.Register(userToCreate, userForRegisterDto.Password);

            return StatusCode(201);
        }
    }
}