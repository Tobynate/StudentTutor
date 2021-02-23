using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace StudentTutorApi.Library.DataAccess.Internal
{
    internal class SqlDataAccess
    {
        private string GetConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        public List<T> LoadData<T, U>(string storedProcedure, U parameter, string connectionString)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString(connectionString)))
            {
                List<T> rows = connection.Query<T>(storedProcedure, parameter, commandType: CommandType.StoredProcedure).AsList();

                return rows;
            }
        }
        public void SaveData<T>(string storedProcedure, T parameters, string connectionString)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString(connectionString)))
            {
                connection.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
