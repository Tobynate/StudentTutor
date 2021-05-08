using Microsoft.Extensions.Configuration;
using StudentTutor.Core.Helpers.Interfaces;
using StudentTutor.Core.Models;
using StudentTutor.Core.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
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
        private readonly ISubjectOfInterestModelList _subjectsOfInterest;

        private HttpClient apiClient { get; set; }

        public ApiHelper(ILoggedInUserModel loggedInUser, ISubjectOfInterestModelList subjectsOfInterest)
        {
            InitializeClient();
            _loggedInUser = loggedInUser;
            this._subjectsOfInterest = subjectsOfInterest;
        }
        private IConfiguration _config = AddConfiguration();

        public IConfiguration Configuration
        {
            get { return _config; }
            set 
            {
                _config = value; 
            }
        }

        private static IConfiguration AddConfiguration()
        {
            DirectoryInfo path = Directory.GetParent(Directory.GetCurrentDirectory());
            string p = path.FullName + "\\netcoreapp3.1\\AppSettings";
            IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetParent(Directory.GetCurrentDirectory()).FullName + "\\netcoreapp3.1\\AppSettings").AddJsonFile("appsettings.json");
#if DEBUG
            builder.AddJsonFile("appsettings.development.json", optional: true, reloadOnChange: true);
#else
            builder.AddJsonFile("appsettings.production.json", optional: true, reloadOnChange: true);
#endif
            return builder.Build();
        }
        private void InitializeClient()
        {
            apiClient = new HttpClient();
            //apiClient.BaseAddress = new Uri("https://localhost:44332/");
            apiClient.BaseAddress = new Uri(Configuration.GetValue<string>("api"));
            apiClient.DefaultRequestHeaders.Accept.Clear();
            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        private HttpClient InitializeClientWithAuth(HttpClient client, string mediaType, Dictionary<string,string> authorization)
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
            if (authorization != null)
            {
                client.DefaultRequestHeaders.Add(authorization["auth"], authorization["token"]);
            }
            return client;
        }

        private String SecureStringToString(SecureString value)
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
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
                {
                    var result = await response.Content.ReadAsAsync<AuthenticatedUser>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        
        public async Task<byte> AddDefaultRole(string token)
        {
            byte isSuccess = 0;
            InitializeClientWithAuth(apiClient, "application/json", new Dictionary<string, string>() { { "auth", "Authorization" }, { "token", $"Bearer {token}" } });
            using (HttpResponseMessage response = await apiClient.PostAsync("api/User/DefaultRole_Add", null))
            {
                if (response.IsSuccessStatusCode)
                {
                    isSuccess = 1;
                    return isSuccess;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        public async Task GetSubjectOfInterest()
        {
            InitializeClientWithAuth(apiClient, "application/json", null);
            using (HttpResponseMessage response = await apiClient.GetAsync("api/User/GetSubjectOfInterest"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<SubjectOfInterestModel>>();

                    if (result?.Count > 0)
                    {
                        foreach (SubjectOfInterestModel subjectOfInterest in result)
                        {
                            _subjectsOfInterest.SubjectOfInterestModels.Add(subjectOfInterest);
                        } 
                    }
                    else
                    {
                        throw new Exception("This query returned a null or empty object");
                    }
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
                   // _loggedInUser.SubjectOfInterest = result[0].SubjectOfInterest;
                    _loggedInUser.Token = token;

                }
                else
                {
                    throw new Exception(response.ReasonPhrase);

                }
            }

        }

        public async Task<HttpStatusCode> RegisterUser(IUserRegistrationTrModel registrationTrModel, SecureString securePassword, SecureString confirmPassword)
        {
            InitializeClientWithAuth(apiClient, "application/json", null);
            if (SecureStringToString(securePassword) == SecureStringToString(confirmPassword))
            {
                var data = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("username", registrationTrModel.userModel.EmailAddress),
                        new KeyValuePair<string, string>("password", SecureStringToString(securePassword))

                    });
                using (HttpResponseMessage response = await apiClient.PostAsJsonAsync("api/User/RegisterUser", data ))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return response.StatusCode;
                    }
                    else
                    {
                        throw new Exception(response.ReasonPhrase);
                    }
                } 
            }
            else
            {
                throw new Exception("Password and Confirm Password do not match");
            }
        }
    }
}
