using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleWebAPI.Data.DAL;
using SampleWebAPI.Domain;
using SampleWebAPI.DTO;
using SampleWebAPI.Helpers;
using SampleWebAPI.Models;
using SampleWebAPI.Services;

namespace SampleWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUser _userDAL;
        private readonly IUserService _userService;

        public UsersController(IUserService userService, IMapper mapper, IUser UserDAL)
        {
            _mapper = mapper;
            _userDAL = UserDAL;
            _userService = userService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<UserReadDTO>> Get()
        {
            var results = await _userDAL.GetAll();

            var userReadDto = _mapper.Map<IEnumerable<UserReadDTO>>(results);

            return userReadDto;
        }

        [HttpPost("Login")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Login(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Post(UserCreateDTO userCreateDTO)
        {
            try
            {
                var NewUser = _mapper.Map<User>(userCreateDTO);
                var result = await _userDAL.Insert(NewUser);
                var ReadData = _mapper.Map<UserReadDTO>(result);
                return CreatedAtAction("Get", new { Id = result.Id }, ReadData);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
    }
}