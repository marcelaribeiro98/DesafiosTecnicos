using FIAP.GestaoEscolar.Admin.Models;
using FIAP.GestaoEscolar.Admin.Models.Options;
using FIAP.GestaoEscolar.Admin.Models.Student;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace FIAP.GestaoEscolar.Admin.Services.Student.Implementations
{
    public class StudentService : IStudentService
    {
        private string _endpoint = "aluno";
        private readonly string _urlApi;
        private readonly IHttpClientFactory _httpClientFactory;
        public StudentService(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _urlApi = apiSettings.Value.UrlAPI;
        }

        public async Task<ModelResponse?> CreateAsync(StudentModel studentModel)
        {
            try
            {
                using var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri(_urlApi);

                var json = JsonSerializer.Serialize(studentModel);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PostAsync(_endpoint, content);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return new ModelResponse() { Success = false, Message = $"Falha ao requisitar o endpoint: [POST] {_urlApi}/{_endpoint}" };
                }

                var responseData = await response.Content.ReadFromJsonAsync<ModelResponse>();

                return responseData;
            }
            catch (HttpRequestException ex)
            {
                return new ModelResponse() { Success = false, Message = ex.Message };
            }
        }

        public async Task<ModelResponse?> UpdateAsync(StudentModel studentModel)
        {
            try
            {
                string request = $"{_endpoint}/{studentModel.Id}";

                using var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri(_urlApi);

                var response = await client.PutAsJsonAsync(request, studentModel);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return new ModelResponse() { Success = false, Message = $"Falha ao requisitar o endpoint: [PUT] {_urlApi}/{request}" };
                }

                var responseData = await response.Content.ReadFromJsonAsync<ModelResponse>();

                return responseData;
            }
            catch (HttpRequestException ex)
            {
                return new ModelResponse() { Success = false, Message = ex.Message };
            }
        }

        public async Task<ModelResponse?> UpdateActiveAsync(int studentId)
        {
            try
            {
                string request = $"{_endpoint}/{studentId}/ativo";

                using var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri(_urlApi);

                var response = await client.PutAsync(request, null);
                if (!response.IsSuccessStatusCode)
                {
                    return new ModelResponse() { Success = false, Message = $"Falha ao requisitar o endpoint: [PUT] {_urlApi}/{request}" };
                }

                var responseData = await response.Content.ReadFromJsonAsync<ModelResponse>();

                return responseData;
            }
            catch (HttpRequestException ex)
            {
                return new ModelResponse() { Success = false, Message = ex.Message };
            }
        }

        public async Task<StudentModelResponse?> GetByIdAsync(int studentId)
        {
            try
            {
                string request = $"{_endpoint}/{studentId}";

                using var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri(_urlApi);

                var responseStream = await client.GetStreamAsync(request);

                var response = await JsonSerializer.DeserializeAsync<StudentModelResponse>(responseStream,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return response;
            }
            catch (HttpRequestException ex)
            {
                return new StudentModelResponse() { Success = false, Message = ex.Message, Data = null };
            }
        }

        public async Task<ListStudentModelResponse?> GetAllAsync()
        {
            try
            {
                using var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri(_urlApi);

                var responseStream = await client.GetStreamAsync(_endpoint);

                var response = await JsonSerializer.DeserializeAsync<ListStudentModelResponse>(responseStream,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return response;
            }
            catch (HttpRequestException ex)
            {
                return new ListStudentModelResponse() { Success = false, Message = ex.Message, Data = null };
            }
        }
    }
}
