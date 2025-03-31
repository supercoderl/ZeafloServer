using DeepSeek.ApiClient.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ZeafloServer.Application.Interfaces;
using ZeafloServer.Application.ViewModels.Deepseeks;
using ZeafloServer.Domain.Entities;
using ZeafloServer.Domain.Settings;

namespace ZeafloServer.Application.Services
{
    public class DeepseekService : IDeepseekService
    {
        private readonly DeepseekSettings _deepseekSettings;
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public DeepseekService(
             IOptions<DeepseekSettings> deepseekSettings,
             IHttpClientFactory httpClientFactory
        )
        {
            _deepseekSettings = deepseekSettings.Value;
            _httpClient = httpClientFactory.CreateClient(); // ✅ Use factory to get HttpClient
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {deepseekSettings.Value.ApiKey}");
            _httpClient.DefaultRequestHeaders.Add("HTTP-Referer", "https://your-site.com"); // Thay bằng URL thật
            _httpClient.DefaultRequestHeaders.Add("X-Title", "Your Site Name");
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<object?> GenerateResponseAsync(string prompt)
        {
            var requestBody = new
            {
                model = "deepseek/deepseek-r1-zero:free",
                messages = new[]
                {
                    new { role = "user", content = prompt }
                }
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody, _jsonOptions), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://openrouter.ai/api/v1/chat/completions", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"API Error: {responseContent}");
            }

            return JsonSerializer.Deserialize<object>(responseContent.Replace("\n", "").Trim());
        }
    }
}
