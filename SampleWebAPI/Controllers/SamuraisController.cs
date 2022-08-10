﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleWebAPI.Data.DAL;
using SampleWebAPI.Domain;
using SampleWebAPI.DTO;
using SampleWebAPI.Helpers;
using SampleWebAPI.Models;

namespace SampleWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SamuraisController : ControllerBase
    {
        private readonly ISamurai _samuraiDAL;
        private readonly IMapper _mapper;

        public SamuraisController(ISamurai samuraiDAL,IMapper mapper)
        {
            _samuraiDAL = samuraiDAL;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<SamuraiReadDTO>> Get()
        {
            //List<SamuraiReadDTO> samuraiDTO = new List<SamuraiReadDTO>();

            var results = await _samuraiDAL.GetAll();
            /*foreach (var result in results)
            {
                samuraiDTO.Add(new SamuraiReadDTO
                {
                    Id = result.Id,
                    Name = result.Name
                });
            }*/

            // Auto Mapper
            var samuraiDTO = _mapper.Map<IEnumerable<SamuraiReadDTO>>(results);

            return samuraiDTO;
        }
        
        [HttpGet("WithQuotes")]
        public async Task<IEnumerable<SamuraiWithQuotesDTO>> GetSamuraiWithQuote()
        {
            //List<SamuraiWithQuotesDTO> samuraiWithQuoteDtos = new List<SamuraiWithQuotesDTO>();
            var results = await _samuraiDAL.GetSamuraiWithQuote();
            var samuraiWithQuoteDtos = _mapper.Map<IEnumerable<SamuraiWithQuotesDTO>>(results);
            /*foreach (var result in results)
            {
                List<QuoteDTO> quoteDtos = new List<QuoteDTO>();
                foreach (var quote in result.Quotes)
                {
                    quoteDtos.Add(new QuoteDTO
                    {
                        Id = quote.Id,
                        Text = quote.Text
                    });
                }
                samuraiWithQuoteDtos.Add(new SamuraiWithQuotesDTO
                {
                    Id = result.Id,
                    Name = result.Name,
                    Quotes = quoteDtos
                });
            }*/
            return samuraiWithQuoteDtos;
            
        }

        [HttpGet("WithSwordElement")]
        public async Task<IEnumerable<SamuraiWithFullAttributeDTO>> GetSamuraiWithSwordElement()
        {
            //List<SamuraiWithQuotesDTO> samuraiWithQuoteDtos = new List<SamuraiWithQuotesDTO>();
            var results = await _samuraiDAL.GetSamuraiWithSwordElement();
            var samuraiWithSwordElementDtos = _mapper.Map<IEnumerable<SamuraiWithFullAttributeDTO>>(results);
            /*foreach (var result in results)
            {
                List<QuoteDTO> quoteDtos = new List<QuoteDTO>();
                foreach (var quote in result.Quotes)
                {
                    quoteDtos.Add(new QuoteDTO
                    {
                        Id = quote.Id,
                        Text = quote.Text
                    });
                }
                samuraiWithQuoteDtos.Add(new SamuraiWithQuotesDTO
                {
                    Id = result.Id,
                    Name = result.Name,
                    Quotes = quoteDtos
                });
            }*/
            return samuraiWithSwordElementDtos;

        }



        [HttpGet("{id}")]
        public async Task<SamuraiReadDTO> Get(int id)
        {
            //SamuraiReadDTO samuraiDTO = new SamuraiReadDTO();
                
            var result = await _samuraiDAL.GetById(id);
            if (result == null) throw new Exception($"data {id} tidak ditemukan");
            
            /*samuraiDTO.Id = result.Id;
            samuraiDTO.Name = result.Name;*/

            var samuraiDTO = _mapper.Map<SamuraiReadDTO>(result);

            return samuraiDTO;

        }

        [HttpGet("ByName")]

        public async Task<IEnumerable<SamuraiReadDTO>> GetByName(string name)
        {
            //List<SamuraiReadDTO> samuraiDtos = new List<SamuraiReadDTO>();
            var results = await _samuraiDAL.GetByName(name);
            /*foreach (var result in results)
            { 
                samuraiDtos.Add(new SamuraiReadDTO
                {
                    Id = result.Id,
                    Name = result.Name
                });
            }*/

            var samuraiDTOs = _mapper.Map<IEnumerable<SamuraiReadDTO>>(results);

            return samuraiDTOs;
        }

        [HttpPost]
        public async Task<ActionResult> Post(SamuraiCreateDTO samuraiCreateDto)
        {
            try
            {
                /*var newSamurai = new Samurai
                {
                    Name = samuraiCreateDto.Name
                };*/
                var newSamurai = _mapper.Map<Samurai>(samuraiCreateDto);
                var result = await _samuraiDAL.Insert(newSamurai);

                /*var samuraiReadDto = new SamuraiReadDTO
                {
                    Id = result.Id,
                    Name = result.Name
                };*/
                var samuraiReadDto = _mapper.Map<SamuraiReadDTO>(result);
                return CreatedAtAction("Get", new { id = result.Id }, samuraiReadDto); 
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("SamuraiWithSword")]
        public async Task<ActionResult> AddSamuraiWithSword(AddSamuraiWithSwordDTO addSamuraiWithSwordDto)
        {
            try
            {
                var newSamurai = _mapper.Map<Samurai>(addSamuraiWithSwordDto);
                var result = await _samuraiDAL.AddSamuraiWithSword(newSamurai);

                var samuraiReadDto = _mapper.Map<SamuraiReadDTO>(result);
                return CreatedAtAction("Get", new { id = result.Id }, samuraiReadDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        public async Task<ActionResult> Put(SamuraiReadDTO samuraiDto)
        {
            try
            {
                var updateSamurai = new Samurai
                {
                    Id = samuraiDto.Id,
                    Name = samuraiDto.Name
                };
                var result = await _samuraiDAL.Update(updateSamurai);
                return Ok(samuraiDto);
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
                await _samuraiDAL.Delete(id);
                return Ok($"Data Samurai dengan id {id} berhasil didelete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        

        
    } 
    
        /*
    {
        private readonly ISamurai _samuraiDAL;
        public SamuraisController(ISamurai samuraiDAL)
        {
            _samuraiDAL = samuraiDAL;
        }

        [HttpGet]
        public async Task<IEnumerable<SamuraiReadDTO>> Get()
        {
            List<SamuraiReadDTO> samuraiDTO = new List<SamuraiReadDTO>();

            var results = await _samuraiDAL.GetAll();
            foreach (var result in results)
            {
                samuraiDTO.Add(new SamuraiReadDTO
                {
                    Id = result.Id,
                    Name = result.Name
                });
            }
            return samuraiDTO;
        }

        [HttpGet("{id}")]
        public async Task<SamuraiReadDTO> Get(int id)
        {
            SamuraiReadDTO samuraiDTO = new SamuraiReadDTO();
            var result = await _samuraiDAL.GetById(id);
            if (result == null) throw new Exception($"data {id} tidak ditemukan");

            samuraiDTO.Id = result.Id;
            samuraiDTO.Name = result.Name;
            return samuraiDTO;
        }

        [HttpGet("ByName")]
        public async Task<IEnumerable<SamuraiReadDTO>> Hello(string name)
        {
            List<SamuraiReadDTO> samuraiDtos = new List<SamuraiReadDTO>();
            var results = await _samuraiDAL.GetByName(name);
            foreach(var result in results)
            {
                samuraiDtos.Add(new SamuraiReadDTO
                {
                    Id = result.Id,
                    Name = result.Name
                });
            }
            return samuraiDtos;
        }

        [HttpPost]
        public async Task<ActionResult> Post(SamuraiCreateDTO samuraiCreateDto)
        {
            try
            {
                var newSamurai = new Samurai
                {
                    Name = samuraiCreateDto.Name
                };

                var result = await _samuraiDAL.Insert(newSamurai);
                var samuraiReadDto = new SamuraiReadDTO
                {
                    Id = result.Id,
                    Name = result.Name
                };
                return CreatedAtAction("Get", new { id = result.Id }, samuraiReadDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put(SamuraiReadDTO samuraiDto)
        {
            try
            {
                var updateSamurai = new Samurai
                {
                    Id = samuraiDto.Id,
                    Name = samuraiDto.Name
                };
                var result = await _samuraiDAL.Update(updateSamurai);
                return Ok(samuraiDto);
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
                await _samuraiDAL.Delete(id);
                return Ok($"Data samurai dengan id {id} berhasil didelete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }   
        */
}
