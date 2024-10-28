using FIAP.GestaoEscolar.Admin.Models.Class;
using FIAP.GestaoEscolar.Admin.Models.Options;
using Microsoft.Extensions.Options;
using System.Text.Json;
using FIAP.GestaoEscolar.Admin.Models;

namespace FIAP.GestaoEscolar.Admin.Services.Class.Implementations
{
    public class ClassService : IClassService
    {
        private string _endpoint = "turma";
        private readonly string _urlApi;
        private readonly IHttpClientFactory _httpClientFactory;
        public ClassService(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _urlApi = apiSettings.Value.UrlAPI;
        }

        public async Task<ModelResponse?> CreateAsync(ClassModel classModel)
        {
            try
            {
                using var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri(_urlApi);

                var json = JsonSerializer.Serialize(classModel);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PostAsync(_endpoint, content);
                if (!response.IsSuccessStatusCode)
                {
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

        public async Task<ModelResponse?> UpdateAsync(ClassModel classModel)
        {
            try
            {
                string request = $"{_endpoint}/{classModel.Id}";

                using var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri(_urlApi);

                var response = await client.PutAsJsonAsync(request, classModel);
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
        public async Task<ModelResponse?> UpdateActiveAsync(int classId)
        {
            try
            {
                string request = $"{_endpoint}/{classId}/ativo";

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

        public async Task<ClassModelResponse?> GetByIdAsync(int classId)
        {
            try
            {
                ClassModelResponse? response = null;

                string request = $"{_endpoint}/{classId}";

                using var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri(_urlApi);

                var responseMessage = await client.GetAsync(request);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var content = await responseMessage.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(content))
                    {
                        response = JsonSerializer.Deserialize<ClassModelResponse>(content,
                            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    }
                }

                return response;

            }
            catch (HttpRequestException ex)
            {
                return new ClassModelResponse() { Success = false, Message = ex.Message, Data = null };
            }
        }

        public async Task<ListClassModelResponse?> GetAllAsync()
        {
            try
            {
                ListClassModelResponse? response = null;

                using var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri(_urlApi);

                var responseMessage = await client.GetAsync(_endpoint);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var content = await responseMessage.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(content))
                    {
                        response = JsonSerializer.Deserialize<ListClassModelResponse>(content,
                            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    }
                }

                return response;
            }
            catch (HttpRequestException ex)
            {
                return new ListClassModelResponse() { Success = false, Message = ex.Message, Data = null };
            }

        }
    }
}
