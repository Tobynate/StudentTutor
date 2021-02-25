using StudentTutor.Core.Helpers.Interfaces;
using StudentTutor.Core.Models;
using StudentTutor.Core.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace StudentTutor.Core.Helpers
{
    public class ApiHelper : IApiHelper
    {
        private readonly ILoggedInUserModel _loggedInUser;

        private HttpClient apiClient { get; set; }

        public ApiHelper(ILoggedInUserModel loggedInUser)
        {
            InitializeClient();
            _loggedInUser = loggedInUser;
        }
        private void InitializeClient()
        {
            apiClient = new HttpClient();
            apiClient.BaseAddress = new Uri("https://localhost:44332/");
            apiClient.DefaultRequestHeaders.Accept.Clear();
            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        private HttpClient InitializeClientWithAuth(HttpClient client, string mediaType, Dictionary<string,string> authorization)
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
            client.DefaultRequestHeaders.Add(authorization["auth"], authorization["token"]);
            return client;
        }

        private String SecureStringToString(SecureString value)
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                var output = Marshal.PtrToStringUni(valuePtr);
                return output;
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }
        public async Task<AuthenticatedUser> Authenticate(string username, SecureString password)
        {
            
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", username),
                new KeyValuePair<string, string>("password", SecureStringToString(password))
            });

            using (HttpResponseMessage response = await apiClient.PostAsync("/token", data))
            {
                if (response.IsSuccessStatusCode)
                {//TODO continue from here
                    var result = await response.Content.ReadAsAsync<AuthenticatedUser>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task GetLoggedInUserData(string token)
        {
            InitializeClientWithAuth(apiClient, "application/json", new Dictionary<string, string>() { { "auth", "Authorization" }, { "token", $"Bearer {token}" } });
            using (HttpResponseMessage response = await apiClient.GetAsync("api/User"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<LoggedInUserModel>>();
                    _loggedInUser.Address = result[0].Address;
                    _loggedInUser.EmailAddress = result[0].EmailAddress;
                    _loggedInUser.CreatedDate = result[0].CreatedDate;
                    _loggedInUser.FirstName = result[0].FirstName;
                    _loggedInUser.Id = result[0].Id;
                    _loggedInUser.LastName = result[0].LastName;
                    _loggedInUser.Passport = result[0].Passport;
                    _loggedInUser.SubjectOfInterest = result[0].SubjectOfInterest;
                    _loggedInUser.Token = token;

                }
                else
                {
                    throw new Exception(response.ReasonPhrase);

                }
            }

        }
    }
}
