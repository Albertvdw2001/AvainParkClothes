using AvainParkKlere.Api.EntityFrameworkCore;
using AvainParkKlere.Api.EntityFrameworkCore.Entities;
using AvainParkKlere.Api.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace AvainParkKlere.Api.Repositories
{
    public class StudentClothingRepository : GenericRepository<StudentClothing>, IStudentClothingRepository
    {
        private readonly AvianParkDbContext _apDbContext;

        public StudentClothingRepository(AvianParkDbContext ApDbContext) : base(ApDbContext)
        {
            _apDbContext = ApDbContext;
        }

        public async Task<List<StudentClothing>> GetStudentClothingByStudent(int studentId)
        {
            var response = await _apDbContext.StudentClothes.Where(sc => sc.StudentId == studentId).ToListAsync();
            return response;
        }

        public async Task<List<StudentClothing>> GetStudentClothingByClothing(int clothingId)
        {
            var response = await _apDbContext.StudentClothes.Where(sc => sc.ClothingId == clothingId).ToListAsync();
            return response;
        }   

    }
}
