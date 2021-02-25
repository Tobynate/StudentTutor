using StudentTutorApi.Library.DataAccess.Internal;
using StudentTutorApi.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTutorApi.Library.DataAccess
{
    public class UserData
    {
        public List<UserModel> GetUserById(string Id)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new { Id };
            var output = sql.LoadData<UserModel, dynamic>("dbo.UserData_GetUser", p, "StudentTutorDb");

            return output;
        }

        public void UpdatePassport(string Id, byte[] Passport)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var p = new { Id, Passport };
            sql.SaveData<dynamic>("dbo.UserData_Passport_Update", p, "StudentTutorDb");
            
        }
    }
}
