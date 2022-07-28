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
    public class ElementsController : ControllerBase
    {
        private readonly IElement _elementDAL;
        private readonly IMapper _mapper;

        public ElementsController(IElement elementDAL,IMapper mapper)
        {
            _elementDAL = elementDAL;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ElementReadDTO>> Get()
        {
            //List<SamuraiReadDTO> samuraiDTO = new List<SamuraiReadDTO>();

            var results = await _elementDAL.GetAll();
            /*foreach (var result in results)
            {
                samuraiDTO.Add(new SamuraiReadDTO
                {
                    Id = result.Id,
                    Name = result.Name
                });
            }*/

            // Auto Mapper
            var elementReadDTO = _mapper.Map<IEnumerable<ElementReadDTO>>(results);

            return elementReadDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post(ElementCreateDTO elementCreateDto)
        {
            try
            {
                /*var newSamurai = new Samurai
                {
                    Name = samuraiCreateDto.Name
                };*/
                var newElement = _mapper.Map<Element>(elementCreateDto);
                var result = await _elementDAL.Insert(newElement);

                /*var samuraiReadDto = new SamuraiReadDTO
                {
                    Id = result.Id,
                    Name = result.Name
                };*/
                var elementReadDto = _mapper.Map<ElementReadDTO>(result);
                return CreatedAtAction("Get", new { id = result.Id }, elementReadDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put(ElementReadDTO elementUpdateDto)
        {
            try
            {
                var updateElement = new Element
                {
                    Id = elementUpdateDto.Id,
                    ElementType = elementUpdateDto.ElementType
                };
                var result = await _elementDAL.Update(updateElement);
                return Ok(elementUpdateDto);
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
                await _elementDAL.Delete(id);
                return Ok($"Data Element dengan id {id} berhasil didelete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<ElementReadDTO> Get(int id)
        {
            //SamuraiReadDTO samuraiDTO = new SamuraiReadDTO();

            var result = await _elementDAL.GetById(id);
            if (result == null) throw new Exception($"data element id {id} tidak ditemukan");

            /*samuraiDTO.Id = result.Id;
            samuraiDTO.Name = result.Name;*/

            var elementReadDtos = _mapper.Map<ElementReadDTO>(result);

            return elementReadDtos;

        }

        [HttpGet("ByName")]
        public async Task<IEnumerable<ElementReadDTO>> GetByName(string name)
        {
            //List<SamuraiReadDTO> samuraiDtos = new List<SamuraiReadDTO>();
            var results = await _elementDAL.GetByName(name);
            /*foreach (var result in results)
            { 
                samuraiDtos.Add(new SamuraiReadDTO
                {
                    Id = result.Id,
                    Name = result.Name
                });
            }*/

            var elementReadDtos = _mapper.Map<IEnumerable<ElementReadDTO>>(results);

            return elementReadDtos;
        }


    }
}
