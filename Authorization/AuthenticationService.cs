using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using ParkerRandalWeb.Models;
using System.Text.Json;
using System.Net.Http.Headers;
using ParkerRandalWeb.Authorization;

namespace ParkerRandalWeb.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly HttpClient _client;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorage;
        private readonly IConfiguration _config;
        private string authTokenStorageKey;


        public AuthenticationService(HttpClient client,
                               AuthenticationStateProvider authStateProvider,
                               ILocalStorageService localStorage,
                               IConfiguration config)
        {
            _client = client;
            _authStateProvider = authStateProvider;
            _localStorage = localStorage;
            _config = config;
            authTokenStorageKey = _config["authTokenStorageKey"];

        }

        public async Task<AuthenticatedUserModel> Login(AuthenticationUserModel userForAuthentication)
        {
            var data = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", userForAuthentication.UserName),
                new KeyValuePair<string, string>("password", userForAuthentication.Password)
            });

            string api = _config["apiLocation"] + _config["tokenEndpoint"];
            var authResult = await _client.PostAsync(api, data);
            var authContent = await authResult.Content.ReadAsStringAsync();

            if (authResult.IsSuccessStatusCode == false)
            {
                return null;
            }

            var result = JsonSerializer.Deserialize<AuthenticatedUserModel>(
                authContent,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            await _localStorage.SetItemAsync(authTokenStorageKey, result.AccessToken);

            ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(result.AccessToken);

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.AccessToken);

            return result;
        }

        public async Task logout()
        {
            await _localStorage.RemoveItemAsync(authTokenStorageKey);
            ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
        }
    }

    public interface IAuthenticationService
    {
    }
}