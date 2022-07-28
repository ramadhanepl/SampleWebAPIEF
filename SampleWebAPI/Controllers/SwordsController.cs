using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleWebAPI.Data.DAL;
using SampleWebAPI.Domain;
using SampleWebAPI.DTO;

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

        [HttpGet]
        public async Task<IEnumerable<SwordReadDTO>> Get()
        {
            //List<SamuraiReadDTO> samuraiDTO = new List<SamuraiReadDTO>();

            var results = await _swordDAL.GetAll();
            /*foreach (var result in results)
            {
                samuraiDTO.Add(new SamuraiReadDTO
                {
                    Id = result.Id,
                    Name = result.Name
                });
            }*/

            // Auto Mapper
            var swordReadDTO = _mapper.Map<IEnumerable<SwordReadDTO>>(results);

            return swordReadDTO;
        }

        [HttpGet("WithElements")]
        public async Task<IEnumerable<SwordWithElementDTO>> GetSwordWithElement()
        {
            var results = await _swordDAL.GetSwordWithElement();
            var swordWithElementDtos = _mapper.Map<IEnumerable<SwordWithElementDTO>>(results);
           
            return swordWithElementDtos;

        }

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

        [HttpPut]
        public async Task<ActionResult> Put(SwordReadDTO swordUpdateDto)
        {
            try
            {
                var updateSword = new Sword
                {
                    Id = swordUpdateDto.Id,
                    SwordName = swordUpdateDto.SwordName
                };
                var result = await _swordDAL.Update(updateSword);
                return Ok(updateSword);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _swordDAL.Delete(id);
                return Ok($"Data Sword dengan id {id} berhasil didelete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }



        [HttpGet("{id}")]
        public async Task<SwordReadDTO> Get(int id)
        {

            var result = await _swordDAL.GetById(id);
            if (result == null) throw new Exception($"data {id} tidak ditemukan");

            var swordReadDTO = _mapper.Map<SwordReadDTO>(result);

            return swordReadDTO;

        }

        [HttpGet("ByName")]

        public async Task<IEnumerable<SwordReadDTO>> GetByName(string name)
        {
            
            var results = await _swordDAL.GetBySwordName(name);

            var swordReadDTO = _mapper.Map<IEnumerable<SwordReadDTO>>(results);

            return swordReadDTO;
        }

        [HttpPost("SwordToExistingElement")]
        public async Task<ActionResult> AddSwordToExistingElement(AddSwordToExistingElementDTO addSwordToExistingElement)
        {
            try
            {
                var newSword = _mapper.Map<Sword>(addSwordToExistingElement);
                var result = await _swordDAL.AddSwordToExistingElement(newSword);

                var swordReadDto = _mapper.Map<SwordReadDTO>(result);
                return CreatedAtAction("Get", new { id = result.Id }, swordReadDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
