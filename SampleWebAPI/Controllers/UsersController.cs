using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleWebAPI.Domain;
using SampleWebAPI.DTO;
using SampleWebAPI.Models;
using SampleWebAPI.Services;

namespace SampleWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private IUserService _userService;
        private readonly UserService _userServiceDAL;

        public UsersController(IUserService userService,IMapper mapper, UserService userServiceDAL)
        {
            _userService = userService;
            _userServiceDAL = userServiceDAL;
            _mapper = mapper;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Post(UserCreateDTO userCreateDto)
        {
            try
            {
                var newUser = _mapper.Map<User>(userCreateDto);
                var result = await _userServiceDAL.Insert(newUser);

                var userReadDto = _mapper.Map<UserReadDTO>(result);
                return CreatedAtAction("Get", new { id = result.Id }, userReadDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
