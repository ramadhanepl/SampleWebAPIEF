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
    public class SwordTypesController : ControllerBase
    {
        private readonly ISwordType _swordTypeDAL;
        private readonly IMapper _mapper;
        private readonly ISword _swordDAL;

        public SwordTypesController(ISwordType swordTypeDAL,IMapper mapper,ISword swordDAL)
        {
            _swordTypeDAL = swordTypeDAL;
            _mapper = mapper;
            _swordDAL = swordDAL;
        }

        [HttpGet]
        public async Task<IEnumerable<SwordTypeReadDTO>> Get()
        {
            var results = await _swordTypeDAL.GetAll();
           
            var swordTypeiDtos = _mapper.Map<IEnumerable<SwordTypeReadDTO>>(results);

            return swordTypeiDtos;
        }

    }
}
