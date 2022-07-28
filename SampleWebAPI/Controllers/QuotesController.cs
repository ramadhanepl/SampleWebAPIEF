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
    public class QuotesController : ControllerBase
    {
        private readonly IQuote _quoteDAL;
        private readonly IMapper _mapper;

        public QuotesController(IQuote quoteDAL, IMapper mapper)
        {
            _quoteDAL = quoteDAL;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<QuoteDTO>> Get()
        {
            //List<QuoteDTO> quotesDtos = new List<QuoteDTO>();
            var results = await _quoteDAL.GetAll();
            /*foreach (var result in results)
            {
                quotesDtos.Add(new QuoteDTO
                {
                    Id = result.Id,
                    Text = result.Text
                });
            }*/
            var quotesDtos = _mapper.Map<IEnumerable<QuoteDTO>>(results);
            return quotesDtos;
        }

        /*
        public async Task<IEnumerable<Quote>> Get()
        {
            var results = await _quoteDAL.GetAll();
            return results;
        }*/
    }

}
