﻿using StudentTutor.Core.Models;
using System.Net.Http;
using System.Security;
using System.Threading.Tasks;

namespace StudentTutor.Core.Helpers
{
    public interface IApiHelper
    {
        Task<AuthenticatedUser> Authenticate(string username, SecureString password);
    }
}