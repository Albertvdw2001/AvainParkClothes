using AutoMapper;
using AvainParkKlere.Api.EntityFrameworkCore.Entities;
using AvainParkKlere.Api.Repositories;
using AvainParkKlere.Api.RepositoryInterfaces;
using AvianParkKlere.Contracts.Dtos.Clothing;
using AvianParkKlere.Contracts.Dtos.Student;
using Microsoft.AspNetCore.Mvc;

namespace AvainParkKlere.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClothingController : ControllerBase
    {
        private readonly ClothingRepository clothingRepository;
        private readonly IMapper mapper;

        public ClothingController(ClothingRepository clothingRepository, IMapper mapper)
        {
            this.clothingRepository = clothingRepository;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<List<ClothingGetDto>?>> Get()
        {
            var response = await clothingRepository.GetAllAsync();

            var results = mapper.Map<List<ClothingGetDto>>(response);

            return Ok(results);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ClothingGetDto?>> GetById(int id)
        {
            var response = await clothingRepository.GetAsync(id);

            if (response == null)
            {
                return NotFound();
            }

            var result = mapper.Map<ClothingGetDto>(response);   

            return Ok(result);
        }


        [HttpPost]
        public async Task<ActionResult> Create(ClothingPostDto clothingPostDto)
        {
            var clothing = mapper.Map<Clothing>(clothingPostDto);
            var repResponse = await clothingRepository.AddAsync(clothing);
            
            if (repResponse == null)
            {
                return BadRequest();
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var clothing = await clothingRepository.GetAsync(id);

            if (clothing == null)
            {
                return NotFound();
            }

            await clothingRepository.DeleteAsync(id);
            return NoContent();
        }


        //TODO: Put



    }
}
