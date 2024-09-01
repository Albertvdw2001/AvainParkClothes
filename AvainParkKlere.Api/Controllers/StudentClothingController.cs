using AutoMapper;
using AvainParkKlere.Api.EntityFrameworkCore.Entities;
using AvainParkKlere.Api.RepositoryInterfaces;
using AvianParkKlere.Contracts.Dtos.StudentClothing;
using Microsoft.AspNetCore.Mvc;

namespace AvainParkKlere.Api.Controllers
{
    public class StudentClothingController : ControllerBase
    {
        private readonly IStudentClothingRepository studentClothingRepository;
        private readonly IMapper mapper;

        public StudentClothingController(IStudentClothingRepository studentClothingRepository, IMapper mapper)
        {
            this.studentClothingRepository = studentClothingRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<StudentClothingGetDto>?>> Get()
        {
            var response = await studentClothingRepository.GetAllAsync();
            var result = mapper.Map<List<StudentClothingGetDto>>(response);   

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentClothingGetDto?>> GetById(int id)
        {
            var response = await studentClothingRepository.GetAsync(id);

            if (response == null)
            {
                return NotFound();  
            }

            var result = mapper.Map<StudentClothingGetDto>(response);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Create(StudentClothingPostDto studentClothingPostDto)
        {
            var studentClothing = mapper.Map<StudentClothing>(studentClothingPostDto);  
            var response = await studentClothingRepository.AddAsync(studentClothing);

            if (response == null)
            {
                return BadRequest();
            }   

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var getResponse = await studentClothingRepository.GetAsync(id); 

            if (getResponse == null)
            {
                return NotFound();
            }

            await studentClothingRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
