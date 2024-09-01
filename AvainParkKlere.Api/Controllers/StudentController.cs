using AutoMapper;
using AvainParkKlere.Api.EntityFrameworkCore.Entities;
using AvainParkKlere.Api.RepositoryInterfaces;
using AvianParkKlere.Contracts.Dtos.Student;
using Microsoft.AspNetCore.Mvc;

namespace AvainParkKlere.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository studentRepository;
        private readonly IMapper mapper;

        public StudentController(IStudentRepository studentRepository, IMapper mapper)
        {
            this.studentRepository = studentRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<StudentGetDto>?>> Get()
        {
            var response = await studentRepository.GetAllAsync();
            var result = mapper.Map<List<StudentGetDto>>(response);   

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentGetDto?>> GetById(int id)
        {
            var response = await studentRepository.GetAsync(id);

            if (response == null)
            {
                return NotFound();  
            }

            var result = mapper.Map<StudentGetDto>(response);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Create(StudentPostDto studentPostDto)
        {
            var student = mapper.Map<Student>(studentPostDto);  
            var response = await studentRepository.AddAsync(student);

            if (response == null)
            {
                return BadRequest();
            }   

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var getResponse = await studentRepository.GetAsync(id); 

            if (getResponse == null)
            {
                return NotFound();
            }   
            
            await studentRepository.DeleteAsync(id);
            return NoContent();    
        }


        //TODO: Put

    }
}
