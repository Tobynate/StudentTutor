using StudentTutor.Core.Models;
using StudentTutor.Core.Models.Interfaces;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Threading.Tasks;

namespace StudentTutor.Core.Helpers.Interfaces
{
    public interface IApiHelper
    {
        Task<AuthenticatedUser> Authenticate(string username, SecureString password);

        Task GetLoggedInUserData(string token);

        Task<byte> AddDefaultRole(string token);

        Task GetSubjectOfInterest();
        Task<HttpStatusCode> RegisterUser(IUserRegistrationTrModel registrationTrModel, SecureString securePassword, SecureString confirmPassword);
    }
}