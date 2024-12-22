using AutoMapper;
using AvainParkKlere.Api.EntityFrameworkCore.Entities;
using AvainParkKlere.Api.Repositories;
using AvainParkKlere.Api.RepositoryInterfaces;
using AvianParkKlere.Contracts.Dtos.Student;
using AvianParkKlere.Contracts.Dtos.StudentClothing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace AvainParkKlere.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
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


        [HttpGet("clothing-for-student/{studentId}")]    
        public async Task<ActionResult<List<StudentClothingGetDto>?>> GetByStudent(int studentId)
        {
            var response = await studentClothingRepository.GetStudentClothingByStudent(studentId);
            var result = mapper.Map<List<StudentClothingGetDto>>(response);

            return Ok(result);
        }


        [HttpGet("students-for-clothing/{clothingId}")]
        public async Task<ActionResult<List<StudentClothingGetDto>?>> GetByClothing(int clothingId)
        {
            var response = await studentClothingRepository.GetStudentClothingByClothing(clothingId);
            var result = mapper.Map<List<StudentClothingGetDto>>(response);

            return Ok(result);
        }


        [HttpPost]
        public async Task<ActionResult> Create([FromBody] StudentClothingPostDto studentClothingPostDto)
        {
            var sc = mapper.Map<StudentClothing>(studentClothingPostDto);
            var response = await studentClothingRepository.AddAsync(sc);

            if (response == null)
            {
                return BadRequest();
            }

            return NoContent();

        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int studentId, int clothingId)
        {
            var exists = await studentClothingRepository.StudentClothingExists(studentId, clothingId);

            if (!exists)
            {
                return NotFound();
            }

            await studentClothingRepository.DeleteStudentClothing(studentId, clothingId);
            return NoContent();
        }


        [HttpGet("exists")]
        public async Task<ActionResult<bool>> Exists(int studentId, int clothingId)
        {
            var response = await studentClothingRepository.StudentClothingExists(studentId, clothingId);
            return Ok(response);
        }


    }
}
