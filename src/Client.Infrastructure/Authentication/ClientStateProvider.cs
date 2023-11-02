using Blazored.LocalStorage;
using GlobalShared.Constants.Permission;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace Client.Infrastructure.Authentication
{
    public class ClientStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        public ClaimsPrincipal? AuthenticationStateUser { get; set; }

        public ClientStateProvider(
            HttpClient httpClient,
            ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            //TODO: change all the authorization chain.
            //SECRET from boiler plate: S0M3RAN0MS3CR3T!1!MAG1C!1!
            //var savedToken = await _localStorage.GetItemAsync<string>(StorageConstants.Local.AuthToken);
            //var savedToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImIwNGM1ZDAxLWE0ZDgtNDNkYy1iZmNmLTJlZTNkNWQ5ZDVhZCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6Im11a2VzaEBibGF6b3JoZXJvLmNvbSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJNdWtlc2giLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9zdXJuYW1lIjoiTXVydWdhbiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL21vYmlsZXBob25lIjoiIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiQWRtaW5pc3RyYXRvciIsIlBlcm1pc3Npb24iOlsiUGVybWlzc2lvbnMuUHJvZHVjdHMuVmlldyIsIlBlcm1pc3Npb25zLlByb2R1Y3RzLkNyZWF0ZSIsIlBlcm1pc3Npb25zLlByb2R1Y3RzLkVkaXQiLCJQZXJtaXNzaW9ucy5Qcm9kdWN0cy5EZWxldGUiLCJQZXJtaXNzaW9ucy5Qcm9kdWN0cy5FeHBvcnQiLCJQZXJtaXNzaW9ucy5Qcm9kdWN0cy5TZWFyY2giLCJQZXJtaXNzaW9ucy5CcmFuZHMuVmlldyIsIlBlcm1pc3Npb25zLkJyYW5kcy5DcmVhdGUiLCJQZXJtaXNzaW9ucy5CcmFuZHMuRWRpdCIsIlBlcm1pc3Npb25zLkJyYW5kcy5EZWxldGUiLCJQZXJtaXNzaW9ucy5CcmFuZHMuRXhwb3J0IiwiUGVybWlzc2lvbnMuQnJhbmRzLlNlYXJjaCIsIlBlcm1pc3Npb25zLkJyYW5kcy5JbXBvcnQiLCJQZXJtaXNzaW9ucy5Eb2N1bWVudHMuVmlldyIsIlBlcm1pc3Npb25zLkRvY3VtZW50cy5DcmVhdGUiLCJQZXJtaXNzaW9ucy5Eb2N1bWVudHMuRWRpdCIsIlBlcm1pc3Npb25zLkRvY3VtZW50cy5EZWxldGUiLCJQZXJtaXNzaW9ucy5Eb2N1bWVudHMuU2VhcmNoIiwiUGVybWlzc2lvbnMuRG9jdW1lbnRUeXBlcy5WaWV3IiwiUGVybWlzc2lvbnMuRG9jdW1lbnRUeXBlcy5DcmVhdGUiLCJQZXJtaXNzaW9ucy5Eb2N1bWVudFR5cGVzLkVkaXQiLCJQZXJtaXNzaW9ucy5Eb2N1bWVudFR5cGVzLkRlbGV0ZSIsIlBlcm1pc3Npb25zLkRvY3VtZW50VHlwZXMuRXhwb3J0IiwiUGVybWlzc2lvbnMuRG9jdW1lbnRUeXBlcy5TZWFyY2giLCJQZXJtaXNzaW9ucy5Eb2N1bWVudEV4dGVuZGVkQXR0cmlidXRlcy5WaWV3IiwiUGVybWlzc2lvbnMuRG9jdW1lbnRFeHRlbmRlZEF0dHJpYnV0ZXMuQ3JlYXRlIiwiUGVybWlzc2lvbnMuRG9jdW1lbnRFeHRlbmRlZEF0dHJpYnV0ZXMuRWRpdCIsIlBlcm1pc3Npb25zLkRvY3VtZW50RXh0ZW5kZWRBdHRyaWJ1dGVzLkRlbGV0ZSIsIlBlcm1pc3Npb25zLkRvY3VtZW50RXh0ZW5kZWRBdHRyaWJ1dGVzLkV4cG9ydCIsIlBlcm1pc3Npb25zLkRvY3VtZW50RXh0ZW5kZWRBdHRyaWJ1dGVzLlNlYXJjaCIsIlBlcm1pc3Npb25zLlVzZXJzLlZpZXciLCJQZXJtaXNzaW9ucy5Vc2Vycy5DcmVhdGUiLCJQZXJtaXNzaW9ucy5Vc2Vycy5FZGl0IiwiUGVybWlzc2lvbnMuVXNlcnMuRGVsZXRlIiwiUGVybWlzc2lvbnMuVXNlcnMuRXhwb3J0IiwiUGVybWlzc2lvbnMuVXNlcnMuU2VhcmNoIiwiUGVybWlzc2lvbnMuUm9sZXMuVmlldyIsIlBlcm1pc3Npb25zLlJvbGVzLkNyZWF0ZSIsIlBlcm1pc3Npb25zLlJvbGVzLkVkaXQiLCJQZXJtaXNzaW9ucy5Sb2xlcy5EZWxldGUiLCJQZXJtaXNzaW9ucy5Sb2xlcy5TZWFyY2giLCJQZXJtaXNzaW9ucy5Sb2xlQ2xhaW1zLlZpZXciLCJQZXJtaXNzaW9ucy5Sb2xlQ2xhaW1zLkNyZWF0ZSIsIlBlcm1pc3Npb25zLlJvbGVDbGFpbXMuRWRpdCIsIlBlcm1pc3Npb25zLlJvbGVDbGFpbXMuRGVsZXRlIiwiUGVybWlzc2lvbnMuUm9sZUNsYWltcy5TZWFyY2giLCJQZXJtaXNzaW9ucy5Db21tdW5pY2F0aW9uLkNoYXQiLCJQZXJtaXNzaW9ucy5QcmVmZXJlbmNlcy5DaGFuZ2VMYW5ndWFnZSIsIlBlcm1pc3Npb25zLkRhc2hib2FyZHMuVmlldyIsIlBlcm1pc3Npb25zLkhhbmdmaXJlLlZpZXciLCJQZXJtaXNzaW9ucy5BdWRpdFRyYWlscy5WaWV3IiwiUGVybWlzc2lvbnMuQXVkaXRUcmFpbHMuRXhwb3J0IiwiUGVybWlzc2lvbnMuQXVkaXRUcmFpbHMuU2VhcmNoIl0sImV4cCI6MTY4MjY4MTE2Mn0.TCbcDeNkjmgj-HgVZB-TpFyYsfXmgp8zBfs4qpJJSjk";
            var savedToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImIwNGM1ZDAxLWE0ZDgtNDNkYy1iZmNmLTJlZTNkNWQ5ZDVhZCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6ImZlbGlwZUBmaXNjaGVyLmRldi5iciIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJGZWxpcGUiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9zdXJuYW1lIjoiRmlzY2hlciIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluaXN0cmF0b3IiLCJQZXJtaXNzaW9uIjpbIlBlcm1pc3Npb25zLlByb2R1Y3RzLlZpZXciLCJQZXJtaXNzaW9ucy5Qcm9kdWN0cy5DcmVhdGUiLCJQZXJtaXNzaW9ucy5Qcm9kdWN0cy5FZGl0IiwiUGVybWlzc2lvbnMuUHJvZHVjdHMuRGVsZXRlIl0sImV4cCI6MTY4MjY4MTE2Mn0.djTwgRPtq40EeNOrXF9OXet43sCITuGGIbsh0mPC2CM";
            if (string.IsNullOrWhiteSpace(savedToken))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
            var state = await Task.Run(() =>
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", savedToken);
                var id = new ClaimsIdentity(GetClaimsFromJwt(savedToken), "jwt");
                var claimsPrincipal = new ClaimsPrincipal(id);
                var state = new AuthenticationState(claimsPrincipal);
                AuthenticationStateUser = state.User;
                return state;
            });
            return state;
        }

        private IEnumerable<Claim> GetClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            if (keyValuePairs != null)
            {
                keyValuePairs.TryGetValue(ClaimTypes.Role, out var roles);

                if (roles != null)
                {
                    if (roles.ToString()!.Trim().StartsWith("["))
                    {
                        var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString()!);

                        claims.AddRange(parsedRoles!.Select(role => new Claim(ClaimTypes.Role, role)));
                    }
                    else
                    {
                        claims.Add(new Claim(ClaimTypes.Role, roles.ToString()!));
                    }

                    keyValuePairs.Remove(ClaimTypes.Role);
                }

                keyValuePairs.TryGetValue(ApplicationClaimTypes.Permission, out var permissions);
                if (permissions != null)
                {
                    if (permissions.ToString()!.Trim().StartsWith("["))
                    {
                        var parsedPermissions = JsonSerializer.Deserialize<string[]>(permissions.ToString()!);
                        claims.AddRange(parsedPermissions!.Select(permission => new Claim(ApplicationClaimTypes.Permission, permission)));
                    }
                    else
                    {
                        claims.Add(new Claim(ApplicationClaimTypes.Permission, permissions.ToString()!));
                    }
                    keyValuePairs.Remove(ApplicationClaimTypes.Permission);
                }

                claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!)));
            }
            return claims;
        }

        private byte[] ParseBase64WithoutPadding(string payload)
        {
            payload = payload.Trim().Replace('-', '+').Replace('_', '/');
            var base64 = payload.PadRight(payload.Length + (4 - payload.Length % 4) % 4, '=');
            return Convert.FromBase64String(base64);
        }
    }
}