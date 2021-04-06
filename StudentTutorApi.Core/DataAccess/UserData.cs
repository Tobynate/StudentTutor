using Microsoft.Extensions.Configuration;
using StudentTutorApi.Core.DataAccess.Internal;
using StudentTutorApi.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

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

        public void UpdatePassport(string Id, byte[] Passport)
        {
            SqlDataAccess sql = new SqlDataAccess(_config);

            var p = new { Id, Passport };
            sql.SaveData<dynamic>("dbo.UserData_Passport_Update", p, "UsersConnection");

        }
    }
}
