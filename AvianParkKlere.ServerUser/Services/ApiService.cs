using AvianParkKlere.Contracts.Dtos.Clothing;
using AvianParkKlere.Contracts.Dtos.Student;
using AvianParkKlere.Contracts.Dtos.StudentClothing;
using AvianParkKlere.ServerUser.Models;
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
            var response = await httpClient.GetAsync($"StudentClothing/clothing-for-student/{studentId}");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<StudentClothingGetDto>>(content);

            return result;
        }

        public async Task<List<StudentClothingGetDto>> GetStudentForClothing(int clothingId)
        {
            var response = await httpClient.GetAsync($"StudentClothing/students-for-clothing/{clothingId}");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<StudentClothingGetDto>>(content);

            return result;
        }

        public async Task<bool> AddOrDeleteAssignedStudents(int clothingId, StudentSelection studentSelection)
        {
            var existsResponse = await httpClient.GetAsync($"StudentClothing/exists?studentId={studentSelection.Student.Id}&clothingId={clothingId}");
            if (existsResponse.IsSuccessStatusCode == false)
            {
                return false;
            }
            var content = await existsResponse.Content.ReadAsStringAsync();
            var exists = JsonConvert.DeserializeObject<bool>(content);

            if (exists)
            {
                if (studentSelection.Selected == false)
                {
                    // delete
                    var deleteResponse = await httpClient.DeleteAsync($"StudentClothing?studentId={studentSelection.Student.Id}&clothingId={clothingId}");
                    if (deleteResponse.IsSuccessStatusCode == false)
                    {
                        return false;
                    }
                    return true;
                }
                return true;
            }
            else
            {
                if (studentSelection.Selected)
                {
                    // add
                    StudentClothingPostDto studentClothing = new StudentClothingPostDto
                    {
                        StudentId = studentSelection.Student.Id,
                        ClothingId = clothingId,
                        Size = studentSelection.Size == null || studentSelection.Size == "" ? "Not Specified" : studentSelection.Size
                    };
                    var postResponse = await httpClient.PostAsJsonAsync("StudentClothing", studentClothing);
                    return postResponse.IsSuccessStatusCode;
                }
                return true;
            }
        }


        public async Task<bool> AddOrDeleteAssignedClothes(int studentId, ClothesSelection clothesSelection)
        {
            var existsResponse = await httpClient.GetAsync($"StudentClothing/exists?studentId={studentId}&clothingId={clothesSelection.Clothing.Id}");
            if (existsResponse.IsSuccessStatusCode == false)
            {
                return false;
            }
            var content = await existsResponse.Content.ReadAsStringAsync();
            var exists = JsonConvert.DeserializeObject<bool>(content);

            if (exists)
            {
                if (clothesSelection.Selected == false)
                {
                    // delete
                    var deleteResponse = await httpClient.DeleteAsync($"StudentClothing?studentId={studentId}&clothingId={clothesSelection.Clothing.Id}");
                    if (deleteResponse.IsSuccessStatusCode == false)
                    {
                        return false;
                    }
                    return true;
                }
                return true;
            }
            else
            {
                if (clothesSelection.Selected)
                {
                    // add
                    StudentClothingPostDto studentClothing = new StudentClothingPostDto
                    {
                        StudentId = studentId,
                        ClothingId = clothesSelection.Clothing.Id,
                        Size = clothesSelection.Size == null || clothesSelection.Size == "" ? "Not Specified" : clothesSelection.Size
                    };
                    var postResponse = await httpClient.PostAsJsonAsync("StudentClothing", studentClothing);
                    return postResponse.IsSuccessStatusCode;
                }
                return true;
            }
        }

    }
}
