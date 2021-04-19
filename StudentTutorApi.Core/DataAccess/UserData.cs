using Microsoft.Extensions.Configuration;
using StudentTutorApi.Core.DataAccess.Internal;
using StudentTutorApi.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudentTutorApi.Core.DataAccess
{
    public class UserData
    {
        private readonly IConfiguration _config;

        public UserData(IConfiguration config)
        {
            this._config = config;
        }
        public List<UserModel> GetUserById(string Id)
        {
            SqlDataAccess sqlDataAccess = new SqlDataAccess(_config);

            var p = new { Id };
            var output = sqlDataAccess.LoadData<UserModel, dynamic>("dbo.UserData_GetUser", p, "UsersConnection");

            return output;
        }

        public async Task SaveUser(UserAccountBindingModel userAccount)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);

            var p = new { userAccount.Address, userAccount.CreatedDate, userAccount.EmailAddress, userAccount.FirstName, userAccount.Id, userAccount.LastName, userAccount.Passport, userAccount.SubjectOfInterest };
            await Task.Run(() => sql.SaveData<dynamic>("dbo.UserData_Insert", p, "UsersConnection"));
        }

        public void UpdatePassport(string Id, byte[] Passport)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);

            var p = new { Id, Passport };
            sql.SaveData<dynamic>("dbo.UserData_Passport_Update", p, "UsersConnection");

        }
    }
}
