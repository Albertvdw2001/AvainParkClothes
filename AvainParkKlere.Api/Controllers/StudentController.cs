using AvainParkKlere.Api.EntityFrameworkCore.Entities;
using AvainParkKlere.Api.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace AvainParkKlere.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Student>?>> Get()
        {
            var response = await studentRepository.GetAllAsync();
            return response;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student?>> GetById(int id)
        {
            var response = await studentRepository.GetAsync(id);

            if (response == null)
            {
                return NotFound();  
            }

            return response;
        }

        [HttpPost]
        public async Task<ActionResult<Student?>> Create(Student student)
        {
            var response = await studentRepository.AddAsync(student);
            return response;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await studentRepository.DeleteAsync(id);
            return NoContent();    
        }


        //TODO: Put

    }
}
