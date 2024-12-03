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

        public async Task<StudentGetDto?> GetStudent(int id)
        {
            var response = await httpClient.GetAsync($"Student/{id}");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<StudentGetDto>(content);

            return result;
        }   

        public async Task<bool> CreateStudent(StudentPostDto student)
        {
            var response = await httpClient.PostAsJsonAsync("Student", student);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteStudent(int id)
        {
            var response = await httpClient.DeleteAsync($"Student/{id}");
            return response.IsSuccessStatusCode;
        }


        /* Clothing */
        public async Task<List<ClothingGetDto>?> GetClothing()
        {
            var response = await httpClient.GetFromJsonAsync<List<ClothingGetDto>>("Clothing");
            return response;
        }

        public async Task<bool> CreateClothing(ClothingPostDto clothing)
        {
            var response = await httpClient.PostAsJsonAsync("Clothing", clothing);
            return response.IsSuccessStatusCode;
        }

        public async Task<ClothingGetDto?> GetClothing(int id)
        {
            var response = await httpClient.GetAsync($"Clothing/{id}");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ClothingGetDto>(content);

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

        public async Task<List<StudentClothingGetDto>> GetClothingForStudent(int studentId)
        {
            var response = await httpClient.GetAsync($"StudentClothing/Student/{studentId}");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<StudentClothingGetDto>>(content);

            return result;
        }

        public async Task<List<StudentClothingGetDto>> GetStudentForClothing(int clothingId)
        {
            var response = await httpClient.GetAsync($"StudentClothing/Clothing/{clothingId}");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<StudentClothingGetDto>>(content);

            return result;
        }   

    }
}
