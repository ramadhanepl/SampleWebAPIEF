using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleWebAPI.Data.DAL;
using SampleWebAPI.Domain;
using SampleWebAPI.DTO;
using SampleWebAPI.Helpers;

namespace SampleWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SwordsController : ControllerBase
    {
        private readonly ISword _swordDAL;
        private readonly IMapper _mapper;
        private readonly ISwordType _swordTypeDAL;

        public SwordsController(ISword swordDAL, IMapper mapper,ISwordType swordTypeDAL)
        {
            _swordDAL = swordDAL;
            _mapper = mapper;
            _swordTypeDAL = swordTypeDAL;
        }

        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<SwordReadDTO>> Get()
        {
            var results = await _swordDAL.GetAll();
            var swordReadDTO = _mapper.Map<IEnumerable<SwordReadDTO>>(results);

            return swordReadDTO;
        }

        [Authorize]
        [HttpGet("SwordWithElements")]
        public async Task<IEnumerable<SwordWithElementDTO>> GetSwordWithElement()
        {
            var results = await _swordDAL.GetSwordWithElement();
            var swordWithElementDtos = _mapper.Map<IEnumerable<SwordWithElementDTO>>(results);
           
            return swordWithElementDtos;

        }

        [Authorize]
        [HttpGet("SwordWithType")]
        public async Task<IEnumerable<SwordReadDTO>> GetSwordWithType(int page)
        {
            var results = await _swordDAL.GetSwordWithType(page);
            var swordWithTypeDtos = _mapper.Map<IEnumerable<SwordReadDTO>>(results);

            return swordWithTypeDtos;

        }

        [Authorize]
        [HttpPost("SwordWithType")]
        public async Task<ActionResult> AddSwordWithType(AddSwordWithTypeDTO addSwordWithTypeDto)
        {
            try
            {
                var newSamurai = _mapper.Map<Sword>(addSwordWithTypeDto);
                var result = await _swordDAL.AddSwordWithType(newSamurai);

                var swordReadDto = _mapper.Map<SwordReadDTO>(result);
                return CreatedAtAction("Get", new { id = result.Id }, swordReadDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Post(SwordCreateDTO swordCreateDto)
        {
            try
            {
                var newSword = _mapper.Map<Sword>(swordCreateDto);
                var result = await _swordDAL.Insert(newSword);

                var swordReadDTO = _mapper.Map<SwordReadDTO>(result);
                return CreatedAtAction("Get", new { id = result.Id }, swordReadDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult> Put(UpdateSwordDTO swordUpdateDto)
        {
            try
            {
                var updateSword = new Sword
                {
                    Id = swordUpdateDto.Id,
                    SwordName = swordUpdateDto.SwordName,
                    Weight = swordUpdateDto.Weight
                };
                var result = await _swordDAL.Update(updateSword);
                return Ok(updateSword);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [Authorize]
        [HttpPut("AddElementToExistingSword")]
        public async Task<ActionResult> Put(AddElementToExistingSwordDTO swordUpdateDto)
        {
            try
            {
                var updateSword = _mapper.Map<Sword>(swordUpdateDto);
                var result = await _swordDAL.Insert(updateSword);

                return Ok(updateSword);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [Authorize]
        [HttpDelete("DeleteElementInSword")]
        public async Task<SwordWithElementDTO> DeleteElementInSword(int id)
        {
            var result = await _swordDAL.DeleteElementInSword(id);
            if (result == null) throw new Exception($"data {id} tidak ditemukan");

            var swordReadDTO = _mapper.Map<SwordWithElementDTO>(result);

            return swordReadDTO;

        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<SwordReadDTO> Get(int id)
        {

            var result = await _swordDAL.GetById(id);
            if (result == null) throw new Exception($"data {id} tidak ditemukan");

            var swordReadDTO = _mapper.Map<SwordReadDTO>(result);

            return swordReadDTO;

        }

        [Authorize]
        [HttpGet("ByName")]

        public async Task<IEnumerable<SwordReadDTO>> GetByName(string name)
        {
            
            var results = await _swordDAL.GetBySwordName(name);

            var swordReadDTO = _mapper.Map<IEnumerable<SwordReadDTO>>(results);

            return swordReadDTO;
        }

    }
}
