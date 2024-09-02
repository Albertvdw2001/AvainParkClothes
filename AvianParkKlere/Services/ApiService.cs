using AvianParkKlere.Contracts.Dtos.Clothing;
using AvianParkKlere.Contracts.Dtos.Student;
using AvianParkKlere.Contracts.Dtos.StudentClothing;
using Newtonsoft.Json;

namespace AvianParkKlere.Services
{
    public class ApiService
    {
        private readonly HttpClient httpClient;

        public ApiService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        /* Students */
        
        public async Task<List<StudentGetDto>?> GetStudentsAsync()
        {
            var response = await httpClient.GetAsync("Student");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<StudentGetDto>>(content);

            return result;
        }


        /* Clothing */
        
        public async Task<List<ClothingGetDto>?> GetClothingAsync()
        {
            var response = await httpClient.GetAsync("Clothing");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ClothingGetDto>>(content);

            return result;
        }


        /* StudentClothing */

        public async Task<List<StudentClothingGetDto>?> GetStudentClothingAsync()
        {
            var response = await httpClient.GetAsync("StudentClothing");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<StudentClothingGetDto>>(content);

            return result;  
        }

    }
}
