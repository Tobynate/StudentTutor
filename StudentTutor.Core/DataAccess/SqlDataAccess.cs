using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace StudentTutor.Core.DataAccess
{
    public class SqlDataAccess
    {
        public string GetConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
