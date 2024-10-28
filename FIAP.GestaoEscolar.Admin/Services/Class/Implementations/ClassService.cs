﻿using FIAP.GestaoEscolar.Admin.Models.Class;
using FIAP.GestaoEscolar.Admin.Models.Options;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System;
using System.Text.Json;

namespace FIAP.GestaoEscolar.Admin.Services.Class.Implementations
{
    public class ClassService : IClassService
    {
        private readonly string _urlApi;
        private readonly IHttpClientFactory _httpClientFactory;
        public ClassService(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> apiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _urlApi = apiSettings.Value.UrlAPI;
        }

        public async Task<ClassModelResponse?> GetAllAsync()
        {
            try
            {
                string endpoint = $"turma";

                using var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri(_urlApi);

                var responseStream = await client.GetStreamAsync(endpoint);

                var response = await JsonSerializer.DeserializeAsync<ClassModelResponse>(responseStream,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return response;
            }
            catch (HttpRequestException ex)
            {
                return new ClassModelResponse() { Success = false, Message = ex.Message, Data = null };
            }

        }
    }
}