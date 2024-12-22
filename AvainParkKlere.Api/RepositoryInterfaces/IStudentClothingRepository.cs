using AvainParkKlere.Api.EntityFrameworkCore.Entities;

namespace AvainParkKlere.Api.RepositoryInterfaces
{
    public interface IStudentClothingRepository : IGenericRepository<StudentClothing>
    {
        Task<List<StudentClothing>> GetStudentClothingByStudent(int studentId);
        Task<List<StudentClothing>> GetStudentClothingByClothing(int clothingId);
        Task<bool> StudentClothingExists(int studentId, int clothingId);
        Task DeleteStudentClothing(int studentId, int clothingId);
    }
}
