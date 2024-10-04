using AvianParkKlere.Contracts.Dtos.Clothing;
using AvianParkKlere.Contracts.Dtos.Student;
using AvianParkKlere.Contracts.Dtos.StudentClothing;
using Newtonsoft.Json;
using System.Net.Http;

namespace AvianParkKlere.ServerUser.Services
{
    public class ApiService
    {
        private readonly HttpClient httpClient;

        public ApiService(IHttpClientFactory httpClientFactory)
        {
            httpClient = httpClientFactory.CreateClient("Default");
        }

        /* Students */

        public async Task<List<StudentGetDto>?> GetStudents()
        {
            var response = await httpClient.GetFromJsonAsync<List<StudentGetDto>>("Student");

            return response;
        }

        public async Task<bool> CreateStudent(StudentPostDto student)
        {
            var response = await httpClient.PostAsJsonAsync("Student", student);

            return response.IsSuccessStatusCode;
        }


        /* Clothing */

        public async Task<List<ClothingGetDto>?> GetClothing()
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
