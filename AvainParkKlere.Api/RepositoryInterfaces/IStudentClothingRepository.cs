using AvainParkKlere.Api.EntityFrameworkCore.Entities;

namespace AvainParkKlere.Api.RepositoryInterfaces
{
    public interface IStudentClothingRepository : IGenericRepository<StudentClothing>
    {
        Task<List<StudentClothing>> GetStudentClothingByStudent(int studentId);
    }
}
