using Application.Interfaces;
using Application.Requests;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Application.Services
{
    public class KeycloakAuthService : IKeycloakAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private string _accessToken;

        public KeycloakAuthService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task AuthenticateAdminAsync()
        {
            var tokenEndpoint = $"{_configuration["Keycloak:BaseUrl"]}/realms/master/protocol/openid-connect/token";

            var requestBody = new Dictionary<string, string>
            {
                { "grant_type", "password" },
                { "client_id", _configuration["Keycloak:AdminClientId"]! },
                { "username", _configuration["Keycloak:AdminUser"]! },
                { "password", _configuration["Keycloak:AdminPassword"]! }
            };

            var content = new FormUrlEncodedContent(requestBody);

            var response = await _httpClient.PostAsync(tokenEndpoint, content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                using var jsonDoc = JsonDocument.Parse(responseContent);
                var root = jsonDoc.RootElement;

                _accessToken = root.GetProperty("access_token").GetString()!;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            }
            else
                throw new Exception("failed");
        }

        public async Task<string> CreateUserAsync(User user, bool forcePasswordUpdated)
        {
            var userPayload = new
            {
                username = user.Username,
                email = user.Email,
                firstName = user.FirstName,
                lastName = user.LastName,
                enabled = true,
                credentials = new[]
                {
                    new
                    {
                        type = "password",
                        value = user.Password,
                        temporary = forcePasswordUpdated
                    }
                },
                requiredActions = forcePasswordUpdated ? new[] { "UPDATE_PASSWORD" } : Array.Empty<string>(),
            };

            var url = $"{_configuration["Keycloak:BaseUrl"]}/admin/realms/{_configuration["Keycloak:Realm"]}/users";

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(userPayload, options);
            Console.WriteLine(json);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);

            response.EnsureSuccessStatusCode();

            if (response.StatusCode == HttpStatusCode.Conflict)
            {
                throw new Exception("User already exists (409 Conflict).");
            }

            if (!response.IsSuccessStatusCode)
            {
                var responseText = await response.Content.ReadAsStringAsync();
                throw new Exception($"Failed to create user. Status: {response.StatusCode}. Response: {responseText}");
            }

            var location = response.Headers.Location!.ToString();
            var id = location.Split('/').Last(); // KeycloakId
            return id;
        }

        public async Task DeleteUserAsync(string keycloakUserId)
        {
            var realm = _configuration["Keycloak:Realm"];
            var baseUrl = _configuration["Keycloak:BaseUrl"];

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

            await client.DeleteAsync($"{baseUrl}/admin/realms/{realm}/users/{keycloakUserId}");
        }

        public async Task AssignRoleAsync(string keycloakUserId, string roleName)
        {
            var realm = _configuration["Keycloak:Realm"];
            var baseUrl = _configuration["Keycloak:BaseUrl"];

            var roleUrl = $"{baseUrl}/admin/realms/{realm}/roles/{roleName}";
            var roleResp = await _httpClient.GetAsync(roleUrl);
            roleResp.EnsureSuccessStatusCode();

            var roleJson = await roleResp.Content.ReadAsStringAsync();
            var roleObj = JsonSerializer.Deserialize<JsonElement>(roleJson);

            var roleRep = new[]
            {
                new
                {
                    id = roleObj.GetProperty("id").GetString(),
                    name = roleObj.GetProperty("name").GetString()
                }
            };

            var assignUrl = $"{baseUrl}/admin/realms/{realm}/users/{keycloakUserId}/role-mappings/realm";

            var json = JsonSerializer.Serialize(roleRep);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var assignResp = await _httpClient.PostAsync(assignUrl, content);
            assignResp.EnsureSuccessStatusCode();
        }
    }
}
